﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.NET
{
    /// <summary>
    /// Represents an OpenLibrary List request.
    /// </summary>
    public sealed record OLListData : OLContainer
    {
        [JsonProperty("name")]
        public string Name { get; init; } = "";
        [JsonProperty("url")]
        public string URL { get; init; } = "";
        [JsonProperty("last_update")]
        public string LastUpdate { get; init; } = "";

        public bool Equals(OLListData? data)
        {
            return data != null &&
                CompareExtensionData(data.extensionData) &&
                this.Name == data.Name &&
                this.URL == data.URL &&
                this.LastUpdate == data.LastUpdate;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
