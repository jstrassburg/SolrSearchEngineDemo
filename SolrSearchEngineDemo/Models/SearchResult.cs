using System.Collections.Generic;

namespace SolrSearchEngineDemo.Models
{
	public class SearchResult
	{
		public int NumFound { get; set; }

		public IEnumerable<KeyValuePair<string, int>> Categories { get; set; }

		public IEnumerable<SearchResultItem> Items { get; set; }
	}
}