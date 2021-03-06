﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace TaskrowSharp.Utils
{
    internal class JsonWebClient
    {
        public string UserAgent = null;
        public CookieContainer Cookies = new CookieContainer();
        public bool AllowAutoRedirect = false;
        public string Host = null;
        public string Origin = null;
        public string Referer = null;
        public int TimeOutMilliseconds = 60000; //1 minute
        public string ContentType = "application/x-www-form-urlencoded";
        public string Accept = null;
        public Dictionary<string, string> Headers = new Dictionary<string, string>();
        public bool ValidateHttpErrors = true;

        public JsonWebClient(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                throw new System.ArgumentNullException(nameof(userAgent));

            this.UserAgent = userAgent;
        }

        #region GET

        private WebResponse Get(Uri url, string requestString = null, string contentType = null)
        {
            try
            {
                System.Net.HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = (!string.IsNullOrEmpty(contentType) ? contentType : this.ContentType);
                request.UserAgent = this.UserAgent;
                request.AllowAutoRedirect = this.AllowAutoRedirect;
                request.CookieContainer = this.Cookies;
                request.Timeout = this.TimeOutMilliseconds;
                request.Accept = this.Accept;

                foreach (var header in this.Headers)
                    request.Headers.Add(header.Key, header.Value);

                if (!string.IsNullOrEmpty(requestString))
                {
                    using (var stream = request.GetRequestStream())
                    {
                        var writer = new StreamWriter(stream);
                        writer.Write(requestString);
                        writer.Flush();
                    }
                }

                var response = request.GetResponse() as HttpWebResponse;

                if (this.ValidateHttpErrors)
                    ValidateHttpStatusCode(response, url);

                return response;
            }
            catch (TaskrowWebException tex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing GET {url} -- Error: {tex.Message}");
                throw;
            }
            catch (WebException wex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing GET {url} -- Error: {wex.Message}");
                throw new TaskrowWebException($"Error executing GET {url} -- Error: {wex.Message}", wex);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing GET {url} -- Error: {ex.Message}");
                throw new TaskrowWebException($"Error executing GET {url} -- Error: {ex.Message}", ex);
            }
        }

        public string GetValuesReturnString(Uri url, NameValueCollection values)
        {
            string requestString = BuildRequestString(values);
            var response = this.Get(url, requestString);
            string ret = null;

            using (var stream = response.GetResponseStream())
            {
                var streamReader = new StreamReader(stream);
                ret = streamReader.ReadToEnd();
            }
            response.Close();
            return ret;
        }

        public string GetReturnString(Uri url)
        {
            var response = this.Get(url);
            string ret = null;

            using (var stream = response.GetResponseStream())
            {
                var streamReader = new StreamReader(stream);
                ret = streamReader.ReadToEnd();
            }
            response.Close();

            return ret;
        }

        public T GetReturnObject<T>(Uri url)
        {
            string json = GetReturnString(url);

            try
            {
                var obj = Utils.JsonHelper.Deserialize<T>(json);
                return obj;
            }
            catch (System.Exception ex)
            {
                string content = (json != null && json.Length > 500 ? string.Concat(json.Substring(0, 500), "...") : json);
                throw new TaskrowException($"Error converting Http Response to Json -- {ex.Message} -- {content}", ex);
            }
        }
        
        #endregion

        #region POST

        private WebResponse Post(Uri url, string requestString = null, string contentType = null)
        {
            try
            {
                System.Net.HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = this.TimeOutMilliseconds;
                request.Method = "POST";
                request.ContentType = (!string.IsNullOrEmpty(contentType) ? contentType : this.ContentType);
                request.UserAgent = this.UserAgent;
                request.AllowAutoRedirect = this.AllowAutoRedirect;
                request.CookieContainer = this.Cookies;

                if (!string.IsNullOrEmpty(this.Host))
                    request.Host = this.Host;

                if (!string.IsNullOrEmpty(this.Origin))
                    request.Headers.Add("Origin", this.Origin);

                if (!string.IsNullOrEmpty(this.Referer))
                    request.Referer = this.Referer;

                foreach (var header in this.Headers)
                    request.Headers.Add(header.Key, header.Value);

                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(requestString);
                    writer.Flush();
                }

                var response = request.GetResponse() as HttpWebResponse;

                if (this.ValidateHttpErrors)
                    ValidateHttpStatusCode(response, url);

                return response;
            }
            catch (TaskrowWebException tex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing POST {url} -- Error: {tex.Message}");
                throw;
            }
            catch (WebException wex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing POST {url} -- Error: {wex.Message}");
                throw new TaskrowWebException($"Error executing POST {url} -- Error: {wex.Message}", wex);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error executing POST {url} -- Error: {ex.Message}");
                throw new TaskrowWebException($"Error executing POST {url} -- Error: {ex.Message}", ex);
            }
        }

        public string PostValuesReturnString(Uri url, NameValueCollection values)
        {
            var requestString = BuildRequestString(values);
            var response = Post(url, requestString);
            string json;

            using (var stream = response.GetResponseStream())
            {
                var streamReader = new StreamReader(stream);
                json = streamReader.ReadToEnd();
            }

            response.Close();
            return json;
        }
        
        public string PostObjReturnString(Uri url, object obj)
        {
            string requestString = Utils.JsonHelper.Serialize(obj);
            var response = Post(url, requestString, "application/json; charset=utf-8");
            string json;

            using (var stream = response.GetResponseStream())
            {
                var streamReader = new StreamReader(stream);
                json = streamReader.ReadToEnd();
            }

            response.Close();
            return json;
        }

        public T PostObjReturnObject<T>(Uri url, object obj)
        {
            string json = PostObjReturnString(url, obj);

            try
            {
                var objRet = Utils.JsonHelper.Deserialize<T>(json);
                return objRet;
            }
            catch (System.Exception ex)
            {
                string content = (json != null && json.Length > 500 ? string.Concat(json.Substring(0, 500), "...") : json);
                throw new TaskrowException($"Error converting Http Response to Json -- {ex.Message} -- {content}", ex);
            }
        }

        #endregion

        public void AddHeader(string name, string value)
        {
            this.Headers.Add(name, value);
        }

        #region Validate

        public static void ValidateHttpStatusCode(HttpWebResponse response, Uri uri, bool acceptRedirect = false)
        {
            int code = (int)response.StatusCode;
            if (code >= 200 && code <= 299) //Success codes (200 OK, 201 Created, 202 Accepted...)
                return;

            if (acceptRedirect && (code == 301 || code == 302)) //301 Moved Permanently / 302 Found
                return;

            string content = null;
            try
            {
                using (var stream = response.GetResponseStream())
                {
                    var streamReader = new StreamReader(stream);
                    content = streamReader.ReadToEnd();
                }
                response.Close();
            }
            catch (System.Exception)
            {
                //Ignora erros ao ler content
            }

            if (content != null && content.Length > 500)
                content = string.Concat(content.Substring(0, 500), "...");

            throw new TaskrowWebException(response.StatusCode, $"HttpStatusError: {(int)response.StatusCode} ({response.StatusCode}) -- Url: {uri} -- {content}");
        }

        #endregion

        #region Auxiliar Methods

        private string BuildRequestString(NameValueCollection values)
        {
            if (values == null)
                return null;

            StringBuilder sb = new StringBuilder();
            foreach (var key in values.AllKeys)
            {
                if (sb.Length != 0)
                    sb.Append("&");

                sb.AppendFormat("{0}={1}", key, WebUtility.UrlEncode(values[key]));
            }

            return sb.ToString();
        }

        #endregion
    }
}
