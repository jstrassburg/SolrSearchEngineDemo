using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SolrNet;
using SolrNet.Impl;
using SolrSearchEngineDemo.Models;

namespace SolrSearchEngineDemo
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			var solrServerUrl = ConfigurationManager.AppSettings["SolrServerUrl"];
			var solrConnection = new SolrConnection(solrServerUrl);
			Startup.Init<SearchResult>(solrConnection);
		}
	}
}