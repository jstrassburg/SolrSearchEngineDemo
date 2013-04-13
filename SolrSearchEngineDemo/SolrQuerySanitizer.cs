using System.Text.RegularExpressions;

namespace SolrSearchEngineDemo
{
	public static class SolrQuerySanitizer
	{
		private static readonly Regex SolrSanitizationPattern = new Regex(
			@"\+|\-|!|\(|\)|\{|\}|\[|\]|\^|~|\*|\?|:|;|&", RegexOptions.Compiled);

		public static string Sanitize(string input)
		{
			return string.IsNullOrWhiteSpace(input) ?
				input :
				SolrSanitizationPattern.Replace(input, string.Empty);
		}
	}
}