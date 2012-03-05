using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Two10.RestClient
{
    public class RestClient
    {
        public List<string> _Path { get; private set; }
        public string BaseUrl { get; private set; }
        Dictionary<string, object> _Query = new Dictionary<string, object>();
        public string _Method { get; private set; }
        

        public RestClient(string baseUrl)
        {
            this.BaseUrl = baseUrl;
            this._Method = "GET";
            this._Path = new List<string>();
            
        }
        
        public RestClient Path(params string[] path)
        {
            this._Path.AddRange(path);
            return this;
        }

        public RestClient Method(string method)
        { 
            this._Method = method;
            return this;
        }

        public RestClient Query(dynamic query)
        {
            var dictionary = ParseObject(query);
            foreach (var key in dictionary)
            {
                this._Query.Add(key, dictionary[key]);
            }
            return this;
        }

        public T Execute<T>()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(BaseUrl);
            if (!BaseUrl.EndsWith(@"/"))
            {
                sb.Append("/");
            }
            sb.Append(string.Join(@"/", this._Path));
            var queryString = string.Join("&", ParseDictionary(this._Query));
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                sb.Append("?");
                sb.Append(queryString);
            }

            var request = WebRequest.Create(sb.ToString());
            request.Method = this._Method;
            using (var response = request.GetResponse())
            {
                StreamReader sr = new StreamReader(response.GetResponseStream());
                var jss = new JavaScriptSerializer();
                return jss.Deserialize<T>(sr.ReadToEnd());
            }
        }

        private IEnumerable<string> ParseDictionary(IDictionary<string, object> dictionary)
        { 
            foreach (var key in dictionary.Keys)
            {
                yield return string.Concat(key, "=", HttpUtility.UrlEncode(dictionary[key].ToString()));                
            }
        }

        private IDictionary<string, object> ParseObject(dynamic value)
        {
            if (value == null) return new Dictionary<string, object>();
            if (value is IDictionary<string, object>) return value;
            var dictionary = new Dictionary<string, object>();
            foreach (var prop in value.GetType().GetProperties())
            {
                dictionary.Add(prop.Name, prop.GetValue(value, null));
            }
            return dictionary;
        }


    }
}
