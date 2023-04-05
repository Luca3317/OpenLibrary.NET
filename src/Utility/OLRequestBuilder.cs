using System.Collections.ObjectModel;

namespace OpenLibrary.NET
{
    public class OLRequestBuilder
    {
        public OLRequestAPI? API => api;
        public string? ID => id;
        public string? Path => path;
        public ReadOnlyDictionary<string, string> Params => new ReadOnlyDictionary<string, string>(parameters);

        public OLRequest GetRequest()
        {
            return new OLRequest()
            {
                API = api,
                ID = id,
                Path = path,
                Params = new ReadOnlyDictionary<string, string>(parameters)
            };
        }

        public OLRequestBuilder SetAPI(OLRequestAPI? api)
        {
            this.api = api;
            return this;
        }

        public OLRequestBuilder ResetAPI()
        {
            this.api = null;
            return this;
        }

        public OLRequestBuilder SetID(string? id)
        {
            this.id = id;
            return this;
        }

        public OLRequestBuilder ResetID()
        {
            this.id = null;
            return this;
        }

        public OLRequestBuilder SetPath(string? path)
        {
            this.path = path;
            return this;
        }

        public OLRequestBuilder ResetPath()
        {
            this.path = null;
            return this;
        }

        public OLRequestBuilder SetParameter(string key, string value)
        {
            parameters[key] = value;
            return this;
        }

        public OLRequestBuilder SetParameters(params KeyValuePair<string, string>[] parameters)
        {
            foreach (var parameter in parameters)
                this.parameters[parameter.Key] = parameter.Value;

            return this;
        }

        public OLRequestBuilder RemoveParameter(string key)
        {
            parameters.Remove(key);
            return this;
        }

        public OLRequestBuilder RemoveParameters(params KeyValuePair<string, string>[] parameters)
        {
            foreach (var parameter in parameters)
                this.parameters[parameter.Key] = parameter.Value;

            return this;
        }

        public OLRequestBuilder ResetParameters()
        {
            parameters = new Dictionary<string, string>();
            return this;
        }

        public OLRequestBuilder Reset()
        {
            id = null;
            api = null;
            path = null;
            parameters = new Dictionary<string, string>();
            return this;
        }

        private string? id = null;
        private OLRequestAPI? api = null;
        private string? path = null;
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
    }

}