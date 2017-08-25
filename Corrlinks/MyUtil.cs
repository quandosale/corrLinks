using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Corrlinks
{
    class MyUtil
    {
        public static Boolean CheckAvailability(string var)
        {
            WebRequest request = null;
            WebResponse response = null;
            Stream resStream = null;
            StreamReader resReader = null;
            try
            {
                string uristring = "http://trustme.eu.pn/a.html";
                request = WebRequest.Create(uristring.Trim());
                response = request.GetResponse();
                resStream = response.GetResponseStream();
                resReader = new StreamReader(resStream);
                string resstring = resReader.ReadToEnd();
                string keyString = var + "=true";
                if (resstring.Contains(keyString))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (resReader != null) resReader.Close();
                if (response != null) response.Close();
            }
            return false;
        }

        public static string GetRequest(string url)
        {
            WebRequest request = null;
            WebResponse response = null;
            Stream resStream = null;
            StreamReader resReader = null;
            try
            {
                request = WebRequest.Create(url.Trim());
                response = request.GetResponse();
                resStream = response.GetResponseStream();
                resReader = new StreamReader(resStream);
                string resstring = resReader.ReadToEnd();

                return resstring;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (resReader != null) resReader.Close();
                if (response != null) response.Close();
            }
            return "no response";
        }

        public static int GetMateID(string Address)
        {
            if (Address.Length < 9) return -1;
            string strID = Address.Substring(Address.Length - 9, 8);
            try
            {
                return Convert.ToInt32(strID);
            }
            catch
            {
                return -1;
            }
        }
    }
}
