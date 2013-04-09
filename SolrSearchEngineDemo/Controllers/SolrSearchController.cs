using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Http;
using SolrSearchEngineDemo.Models;

namespace SolrSearchEngineDemo.Controllers
{
	public class SolrSearchController : ApiController
	{
		private readonly ISolrOperations<SearchResult> _solrOperations =
			ServiceLocator.Current.GetInstance<ISolrOperations<SearchResult>>();

		// GET api/solrsearch/turkey
		public IEnumerable<SearchResult> Get(string term)
		{
			var query = new SolrQuery(Sanitize(term));

			return _solrOperations.Query(
				query,
				new QueryOptions
				{
					Start = 0,
					Rows = 25,
					ExtraParams = new Dictionary<string, string>
					{
						{ "defType", "dismax" },
						{ "pf", ConfigurationManager.AppSettings["SolrPhraseFields"]}
					}
				});
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
