using SolrNet.Attributes;
using System.Collections.Generic;
using System;

namespace SolrSearchEngineDemo.Models
{
	public class SearchResult
	{
		public int NumFound { get; set; }

		public IEnumerable<SearchResultItem> Items { get; set; }
	}
}