using Newtonsoft.Json.Linq;
using System.Net.Http;
using ShopifySharp.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifySharp.Infrastructure;

namespace ShopifySharp
{
    /// <summary>
    /// A service for manipulating Shopify metafields.
    /// </summary>
    public class MetaFieldService : ShopifyService
    {
        /// <summary>
        /// Creates a new instance of <see cref="MetaFieldService" />.
        /// </summary>
        /// <param name="myShopifyUrl">The shop's *.myshopify.com URL.</param>
        /// <param name="shopAccessToken">An API access token for the shop.</param>
        public MetaFieldService(string myShopifyUrl, string shopAccessToken) : base(myShopifyUrl, shopAccessToken) { }
        
        private async Task<int> _CountAsync(string path, MetaFieldFilter filter = null)
        {
            var req = PrepareRequest(path);

            if (filter != null)
            {
                req.Url.QueryParams.AddRange(filter.ToParameters());
            }

            return await ExecuteRequestAsync<int>(req, HttpMethod.Get, rootElement: "count");
        }

        /// <summary>
        /// Gets a count of the metafields on the shop itself.
        /// </summary>
        /// <param name="filter">Options to filter the results.</param>
        public virtual async Task<int> CountAsync(MetaFieldFilter filter = null)
        {
            return await _CountAsync("metafields/count.json", filter);
        }

        /// <summary>
        /// Gets a count of the metafields for the given entity type and filter options.
        /// </summary>
        /// <param name="resourceType">The type of shopify resource to obtain metafields for. This could be variants, products, orders, customers, custom_collections.</param>
        /// <param name="resourceId">The Id for the resource type.</param>
        /// <param name="filter">Options to filter the results.</param>
        public virtual async Task<int> CountAsync(long resourceId, string resourceType, MetaFieldFilter filter = null)
        {
            return await _CountAsync($"{resourceType}/{resourceId}/metafields/count.json", filter);
        }

        private async Task<IEnumerable<MetaField>> _ListAsync(string path, MetaFieldFilter filter = null)
        {
            var req = PrepareRequest(path);

            if (filter != null)
            {
                req.Url.QueryParams.AddRange(filter.ToParameters());
            }

            return await ExecuteRequestAsync<List<MetaField>>(req, HttpMethod.Get, rootElement: "metafields");
        }

        /// <summary>
        /// Gets a list of the metafields for the shop itself.
        /// </summary>
        /// <param name="filter">Options to filter the results.</param>
        public virtual async Task<IEnumerable<MetaField>> ListAsync(MetaFieldFilter filter = null)
        {
            return await _ListAsync("metafields.json", filter);
        }

        /// <summary>
        /// Gets a list of the metafields for the given entity type and filter options.
        /// </summary>
        /// <param name="resourceType">The type of shopify resource to obtain metafields for. This could be variants, products, orders, customers, custom_collections.</param>
        /// <param name="resourceId">The Id for the resource type.</param>
        /// <param name="filter">Options to filter the results.</param>
        public virtual async Task<IEnumerable<MetaField>> ListAsync(long resourceId, string resourceType, MetaFieldFilter filter = null)
        {
            return await _ListAsync( $"{resourceType}/{resourceId}/metafields.json", filter);
        }

        /// <summary>
        /// Retrieves the metafield with the given id.
        /// </summary>
        /// <param name="metafieldId">The id of the metafield to retrieve.</param>
        /// <param name="fields">A comma-separated list of fields to return.</param>
        public virtual async Task<MetaField> GetAsync(long metafieldId, string fields = null)
        {
            var req = PrepareRequest($"metafields/{metafieldId}.json");

            if (! string.IsNullOrEmpty(fields))
            {
                req.Url.QueryParams.Add("fields", fields);
            }

            return await ExecuteRequestAsync<MetaField>(req, HttpMethod.Get, rootElement: "metafield");
        }

        /// <summary>
        /// Creates a new metafield on the shop itself.
        /// </summary>
        /// <param name="metafield">A new metafield. Id should be set to null.</param>
        public virtual async Task<MetaField> CreateAsync(MetaField metafield)
        {
            var req = PrepareRequest("metafields.json");
            var content = new JsonContent(new 
            {
                metafield = metafield
            });

            return await ExecuteRequestAsync<MetaField>(req, HttpMethod.Post, content, "metafield");
        }

        /// <summary>
        /// Creates a new metafield on the given entity.
        /// </summary>
        /// <param name="metafield">A new metafield. Id should be set to null.</param>
        /// <param name="resourceId">The Id of the resource the metafield will be associated with. This can be variants, products, orders, customers, custom_collections, etc.</param>
        /// <param name="resourceType">The resource type the metaifeld will be associated with. This can be variants, products, orders, customers, custom_collections, etc.</param>
        public virtual async Task<MetaField> CreateAsync(MetaField metafield, long resourceId, string resourceType)
        {
            var req = PrepareRequest( $"{resourceType}/{resourceId}/metafields.json");
            var content = new JsonContent(new
            {
                metafield = metafield
            });
            
            return await ExecuteRequestAsync<MetaField>(req, HttpMethod.Post, content, "metafield");
        }

        /// <summary>
        /// Updates the given metafield. Id must not be null.
        /// </summary>
        /// <param name="metafield">The metafield to update.</param>
        public virtual async Task<MetaField> UpdateAsync(MetaField metafield)
        {
            var req = PrepareRequest($"metafields/{metafield.Id.Value}.json");
            var content = new JsonContent(new
            {
                metafield = metafield
            });

            return await ExecuteRequestAsync<MetaField>(req, HttpMethod.Put, content, "metafield");
        }

        /// <summary>
        /// Deletes a metafield with the given Id.
        /// </summary>
        /// <param name="metafieldId">The metafield object's Id.</param>
        public virtual async Task DeleteAsync(long metafieldId)
        {
            var req = PrepareRequest($"metafields/{metafieldId}.json");

            await ExecuteRequestAsync(req, HttpMethod.Delete);
        }
    }
}