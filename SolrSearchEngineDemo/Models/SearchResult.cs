using SolrNet.Attributes;

namespace SolrSearchEngineDemo.Models
{
	public class SearchResult
	{
		[SolrField("ProductID")]
		public string ProductId { get; set; }

		[SolrField("ProductNumber")]
		public string ProductNumber { get; set; }

		[SolrField("Name")]
		public string Name { get; set; }

		[SolrField("ProductDescription")]
		public string ProductDescription { get; set; }

		[SolrField("Color")]
		public string Color { get; set; }

		[SolrField("CategoryName")]
		public string CategoryName { get; set; }

		[SolrField("SubcategoryName")]
		public string SubcategoryName { get; set; }

		[SolrField("ListPrice")]
		public double ListPrice { get; set; }

		[SolrField("ThumbnailPhotoFileName")]
		public string ThumbnailPhotoFileName { get; set; }
	}
}