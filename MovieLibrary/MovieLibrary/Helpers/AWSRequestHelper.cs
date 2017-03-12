using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace MovieLibrary.Helpers
{
    public class AWSRequestHelper
    {
        private static readonly string ENDPOINT = "ecs.amazonaws.de";
        private static readonly string REQUESTURI = "/onca/xml";

        private static readonly string AWSACCESSKEYID = "AKIAJLMJY7B5UY6NRDUQ";
        private static readonly string SECRETKEY = "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";

        private int CompareByUTF8ByteValue(string x, string y)
        {
            UTF8Encoding utf8 = new UTF8Encoding();

            Byte[] xBytes = utf8.GetBytes(x);
            Byte[] yBytes = utf8.GetBytes(y);

            int xSize = xBytes.Count();
            int ySize = yBytes.Count();
            int smallest;
            int returnValForSize;

            if (xSize < ySize)
            {
                smallest = xSize;
                returnValForSize = -1;
            }
            else if (xSize > ySize)
            {
                smallest = ySize;
                returnValForSize = 1;
            }
            else
            {
                smallest = xSize;
                returnValForSize = 0;
            }

            for (int i = 0; i < smallest; i++)
            {
                if(xBytes[i] < yBytes[i])
                    return -1;
                else if(yBytes[i] < xBytes[i])
                    return 1;
            }

            return returnValForSize;
        }

        public virtual XmlDocument GetResponse(List<string> param)
        {
            try
            {
                string canonicalizedQueryString = "";
                List<string> encParam = new List<string>();

                foreach (string s in param)
                {
                    string[] keyValuePair = s.Split(new Char[] { '=' }, 2);
                    keyValuePair[1] = UrlEncodeUpperCase(keyValuePair[1]);

                    encParam.Add(keyValuePair[0] + "=" + keyValuePair[1]);
                }

                encParam.Add("AWSAccessKeyId=" + UrlEncodeUpperCase(AWSACCESSKEYID));
                encParam.Add("Timestamp=" + UrlEncodeUpperCase(GetTimestamp()));

                encParam.Sort(CompareByUTF8ByteValue);

                foreach (string s in encParam)
                {
                    canonicalizedQueryString += "&" + s;
                }

                canonicalizedQueryString = canonicalizedQueryString.Substring(1);

                string signedUrl = GetSignedUrl(canonicalizedQueryString);

                Debug.WriteLine(signedUrl);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(signedUrl);
                request.Method = "GET";
                request.Proxy = null;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream stream = response.GetResponseStream();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);

                response.Close();
                stream.Close();

                return xmlDoc;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string UrlEncodeUpperCase(string value)
        {
            value = HttpUtility.UrlEncode(value);
            value = Regex.Replace(value, "(\\+)", "%20");
            return Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());
        }

        private string GetTimestamp()
        {
            DateTime dateTime = DateTime.UtcNow;

            return dateTime.ToString("yyyy-MM-dd") + "T" + dateTime.ToString("HH:mm:ss") + "Z";
        }

        private string GetSignedUrl(string canonicalizedQueryString)
        {
            string stringToSign = "GET\n" +
                                   ENDPOINT + "\n" +
                                   REQUESTURI + "\n" +
                                   canonicalizedQueryString;

            UTF8Encoding utf8 = new UTF8Encoding();

            HMACSHA256 hmac = new HMACSHA256(utf8.GetBytes(SECRETKEY));

            Byte[] signatureBytes = hmac.ComputeHash(utf8.GetBytes(stringToSign));

            string signature = UrlEncodeUpperCase(Convert.ToBase64String(signatureBytes));

            return "http://" + ENDPOINT + REQUESTURI + "?" + canonicalizedQueryString + "&Signature=" + signature;
        }
    }
}