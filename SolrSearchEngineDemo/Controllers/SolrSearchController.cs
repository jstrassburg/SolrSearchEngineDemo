using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrSearchEngineDemo.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
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
			var query = new SolrQuery(Sanitize(q));

			var filterQueries = new List<ISolrQuery>();
			if (!string.IsNullOrEmpty(categoryFilter))
			{
				filterQueries.Add(new SolrQueryByField("CategoryName", categoryFilter));
			}

			var solrResult = _solrOperations.Query(
				query,
				new QueryOptions
				{
					Start = 0,
					Rows = 5,
					FilterQueries = filterQueries,
					Facet = new FacetParameters
					{
						Queries = new List<ISolrFacetQuery>
						{
							new SolrFacetFieldQuery("CategoryName") { MinCount = 1 }
						}
					},
					ExtraParams = new Dictionary<string, string>
					{
						{ "defType", "dismax" },
						{ "pf", ConfigurationManager.AppSettings["SolrPhraseFields"]}
					}
				});

			return new SearchResult
			{
				Items = solrResult,
				NumFound = solrResult.NumFound,
				Categories = solrResult.FacetFields["CategoryName"].Select(
					x => new KeyValuePair<string, int>(x.Key, x.Value))
			};
		}

		private static readonly Regex SolrSanitizationPattern = new Regex(
			@"\+|\-|!|\(|\)|\{|\}|\[|\]|\^|~|\*|\?|:|;|&", RegexOptions.Compiled);

		private static string Sanitize(string input)
		{
			return string.IsNullOrWhiteSpace(input) ?
				input :
				SolrSanitizationPattern.Replace(input, string.Empty);
		}
	}
}
