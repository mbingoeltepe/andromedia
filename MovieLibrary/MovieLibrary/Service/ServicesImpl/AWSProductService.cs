using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using MovieLibrary.Helpers;
using MovieLibrary.Models;
using System.Xml.Linq;

namespace MovieLibrary.Service.ServicesImpl
{
    public class AWSProductService
    {
        private static readonly string SERVICE = "AWSECommerceService";
        private static readonly string VERSION = "2010-11-01";
        private static readonly string OPERATION = "ItemSearch";
        private static readonly string[] SEARCHINDEX = {"Books", "DVD"};

        private AWSRequestHelper requestHelper;

        /// <summary>
        /// Hier kann man zwischen Buch oder Film auswählen.
        /// </summary>
        private enum BookOrMovie
        { 
            Book,
            Movie
        }

        public AWSProductService()
        {
            requestHelper = new AWSRequestHelper();
        }

        public AWSProductService(AWSRequestHelper helper)
        {
            requestHelper = helper;
        }

        private XmlNode GetProductSetNode(List<string> param)
        {
            XmlDocument xmlDoc = requestHelper.GetResponse(param);

            XDocument element = XDocument.Load(xmlDoc.CreateNavigator().ReadSubtree());



            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("aws", "http://webservices.amazon.com/AWSECommerceService/2010-11-01");

            string xpath = "//aws:Item[1]/aws:ImageSets/aws:ImageSet[@Category='primary']";

            return xmlDoc.SelectSingleNode(xpath, nsmgr);
        }

        public IDictionary<string, Image> GetProducstForVideo(Video video)
        {
            string searchIndex = "SearchIndex=Video";
            string keywords = "Keywords=" + video.Title;
            string title = "Title=" + video.Title;
            string actor;
            string productGroup = "ProductGroup=DVD";

            string actors = "";

            foreach (Actor a in video.Actor)
                actors += " " + a.LastName;

            actor = "Actor=" + actors.Trim();

            List<string> param = GetParamList(BookOrMovie.Movie);
            param.Add(searchIndex);
            param.Add(keywords);
            param.Add(title);
            param.Add(actor);
            param.Add(productGroup);

            XmlNode productSetNode = GetProductSetNode(param);

            if (productSetNode == null)
            {
                param.Remove(actor);
                productSetNode = GetProductSetNode(param);
            }
            return null;
//            return GetImagesFor(imageSetNode);
        }

        public IDictionary<string, Image> GetProductsForBook(Book book)
        {
            string searchIndex = "SearchIndex=Books";
            string keywords = "Keywords=" + book.Title;
            string title = "Title=" + book.Title;
            string author;

            string authors = "";

            foreach (Author a in book.Author)
                authors += " " + a.LastName;

            author = "Author=" + authors.Trim();

            List<string> param = GetParamList(BookOrMovie.Book);
            param.Add(searchIndex);
            param.Add(keywords);
            param.Add(title);
            param.Add(author);

            XmlNode productSetNode = GetProductSetNode(param);

            if (productSetNode == null)
            {
                param.Remove(author);
                productSetNode = GetProductSetNode(param);
            }
            return null;
            //return GetImagesFor(imageSetNode);
        }

        private List<string> GetParamList(BookOrMovie option)
        {
            List<string> param = new List<string>();
            param.Add("Service=" + SERVICE);
            param.Add("Version=" + VERSION);
            param.Add("Operation=" + OPERATION);

            return param;
        }
    }
}