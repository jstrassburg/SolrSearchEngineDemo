using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrSearchEngineDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace SolrSearchEngineDemo.Controllers
{
	public class SolrSearchController : ApiController
	{
		private readonly ISolrOperations<SearchResultItem> _solrOperations =
			ServiceLocator.Current.GetInstance<ISolrOperations<SearchResultItem>>();

		// GET api/solrsearch/?q=turkey
		public SearchResult Get(string q)
		{
			return Get(q, null);
		}

		// GET api/solrsearch/category/?q=turkey
		public SearchResult Get(string q, string categoryFilter)
		{
			if (q == "michael")
				throw new ArgumentException("billie jean is not my lover", "q");

			var query = new SolrQuery(SolrQuerySanitizer.Sanitize(q));
			var queryOptions = CreateQueryOptions(categoryFilter);

			var solrResult = _solrOperations.Query(query, queryOptions);

			return CreateSearchResultFromSolrResult(solrResult);
		}

		private static SearchResult CreateSearchResultFromSolrResult(ISolrQueryResults<SearchResultItem> solrResult)
		{
			return new SearchResult
				{
					Items = solrResult,
					NumFound = solrResult.NumFound,
					Categories = solrResult.FacetFields["CategoryName"].Select(
						x => new KeyValuePair<string, int>(x.Key, x.Value))
				};
		}

		private static QueryOptions CreateQueryOptions(string categoryFilter)
		{
			return new QueryOptions
				{
					Start = 0,
					Rows = 5,
					FilterQueries = CreateFilterQueries(categoryFilter),
					Facet = CreateFacetParameters(),
					ExtraParams = CreateExtraParams()
				};
		}

		private static Dictionary<string, string> CreateExtraParams()
		{
			return new Dictionary<string, string>
			{
				{ "defType", "dismax" },
				{ "pf", ConfigurationManager.AppSettings["SolrPhraseFields"]}
			};
		}

		private static FacetParameters CreateFacetParameters()
		{
			return new FacetParameters
			{
				Queries = new List<ISolrFacetQuery>
				{
					new SolrFacetFieldQuery("CategoryName") { MinCount = 1 }
				}
			};
		}

		private static ICollection<ISolrQuery> CreateFilterQueries(string categoryFilter)
		{
			var filterQueries = new List<ISolrQuery>();
			if (!string.IsNullOrEmpty(categoryFilter))
			{
				filterQueries.Add(new SolrQueryByField("CategoryName", categoryFilter));
			}
			return filterQueries;
		}
	}
}
