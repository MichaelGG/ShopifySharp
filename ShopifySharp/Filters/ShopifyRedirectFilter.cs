﻿using Newtonsoft.Json;

namespace ShopifySharp.Filters
{
    /// <summary>
    /// Options for filtering <see cref="ShopifyRedirectService.ListAsync(ShopifyRedirectFilter)"/> results.
    /// </summary>
    public class ShopifyRedirectFilter : ShopifyListFilter
    {
        /// <summary>
        /// An optional parameter that filters the result to redirects with the given path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// An optional parameter that filters the result to redirects with the given target.
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }
    }
}
