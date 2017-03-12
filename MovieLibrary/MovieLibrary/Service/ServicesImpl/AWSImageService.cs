using System;
using System.Collections.Generic;
using System.Xml;
using MovieLibrary.Service.IServices;
using MovieLibrary.Models;
using MovieLibrary.Helpers;
using System.Data.Objects.DataClasses;

namespace MovieLibrary.Service.ServicesImpl
{
    public class AWSImageService : IImageService
    {
        private static readonly string SERVICE = "AWSECommerceService";
        private static readonly string VERSION = "2010-11-01";
        private static readonly string OPERATION = "ItemSearch";
        private static readonly string RESPONSEGROUP = "Images";

        public static readonly string SWATCHIMAGE = "SwatchImage";
        public static readonly string SMALLIMAGE = "SmallImage";
        public static readonly string THUMBNAILIMAGE = "ThumbnailImage";
        public static readonly string TINYIMAGE = "TinyImage";
        public static readonly string MEDIUMIMAGE = "MediumImage";
        public static readonly string LARGEIMAGE = "LargeImage";

        public static readonly Image IMAGE_NOIMAGEFOUND = new Image("../../Content/images/noImage.jpg", 160, 120);
        public static readonly Image SWATCH_NOIMAGEFOUND = new Image("../../Content/images/noImage.jpg", 30, 20);

        private AWSRequestHelper requestHelper;

        public AWSImageService()
        {
            this.requestHelper = new AWSRequestHelper();
        }

        public AWSImageService(AWSRequestHelper request)
        {
            this.requestHelper = request;
        }

        public IDictionary<string,Image> GetImagesForVideo(Video video)
        {
            string searchIndex = "SearchIndex=Video";
            string keywords = "Keywords=" + video.Title;
            string title = "Title=" + video.Title;
            string actor;

            string actors = "";

            foreach (Actor a in video.Actor)
                actors += " " + a.LastName;

            actor = "Actor=" + actors.Trim();

            List<string> param = GetParamList();
            param.Add(searchIndex);
            param.Add(keywords);
            param.Add(title);
            param.Add(actor);

            XmlNode imageSetNode = GetImageSetNode(param);

            if (imageSetNode == null)
            {
                param.Remove(actor);
                imageSetNode = GetImageSetNode(param);
            }
            
            return GetImagesFor(imageSetNode);
        }

        public IDictionary<string, Image> GetImagesForBook(Book book)
        {
            string searchIndex = "SearchIndex=Books";
            string keywords = "Keywords=" + book.Title;
            string title = "Title=" + book.Title;
            string author;

            string authors = "";

            foreach (Author a in book.Author)
                authors += " " + a.LastName;

            author = "Author=" + authors.Trim();

            List<string> param = GetParamList();
            param.Add(searchIndex);
            param.Add(keywords);
            param.Add(title);
            param.Add(author);

            XmlNode imageSetNode = GetImageSetNode(param);

            if (imageSetNode == null)
            {
                param.Remove(author);
                imageSetNode = GetImageSetNode(param);
            }

            return GetImagesFor(imageSetNode);
        }

        private XmlNode GetImageSetNode(List<string> param)
        {
            XmlDocument xmlDoc = requestHelper.GetResponse(param);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("aws", "http://webservices.amazon.com/AWSECommerceService/2010-11-01");

            string xpath = "//aws:Item[1]/aws:ImageSets/aws:ImageSet[@Category='primary']";

            return xmlDoc.SelectSingleNode(xpath, nsmgr);
        }

        private IDictionary<string, Image> GetImagesFor(XmlNode imageSetNode)
        {
            Dictionary<string, Image> images = new Dictionary<string, Image>();
            images.Add(SWATCHIMAGE, imageSetNode != null ? GetImageFromXmlNode(imageSetNode[SWATCHIMAGE]) : null);
            images.Add(SMALLIMAGE, imageSetNode != null ? GetImageFromXmlNode(imageSetNode[SMALLIMAGE]) : null);
            images.Add(THUMBNAILIMAGE, imageSetNode != null ? GetImageFromXmlNode(imageSetNode[THUMBNAILIMAGE]) : null);
            images.Add(TINYIMAGE, imageSetNode != null ? GetImageFromXmlNode(imageSetNode[TINYIMAGE]) : null);
            images.Add(MEDIUMIMAGE, imageSetNode != null ? GetImageFromXmlNode(imageSetNode[MEDIUMIMAGE]) : null);
            images.Add(LARGEIMAGE, imageSetNode != null ? GetImageFromXmlNode(imageSetNode[LARGEIMAGE]) : null);

            return images;
        }

        private Image GetImageFromXmlNode(XmlNode node)
        {
            if (node != null)
            {
                string url = node["URL"].InnerText;
                int height = Convert.ToInt32(node["Height"].InnerText);
                int width = Convert.ToInt32(node["Width"].InnerText);
                return new Image(url, height, width);
            }
            else
                return null;
        }

        private List<string> GetParamList()
        {
            List<string> param = new List<string>();
            param.Add("Service=" + SERVICE);
            param.Add("Version=" + VERSION);
            param.Add("Operation=" + OPERATION);
            param.Add("ResponseGroup=" + RESPONSEGROUP);

            return param;
        }
    }
}