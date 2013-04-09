Solr Search Engine Demo
=======================
A demonstration of using [Apache Solr](http://lucene.apache.org/solr/) as a search engine behind an ASP.NET MVC4 
web application and glued together with [SolrNet](https://code.google.com/p/solrnet/) as a .NET Solr client library.

------------------------------------------------------

Running the Demo
----------------
1. Get the [AdventureWorks](http://msftdbprodsamples.codeplex.com/releases/view/93587) 
sample database and deploy it locally.
2. Clone [this](https://github.com/jstrassburg/SolrSearchEngineDemo) repo from github.
3. Deploy the vSolrImport view to your AdventureWorks database.
4. Install the [Java runtime](http://java.com/en/download/index.jsp) if you don't have it.
5. Start Solr by executing the Solr/start_solr.cmd script in the solution.
6. On http://localhost:8983/solr (the administration site for Solr) trigger a data-import by browsing to 
adventureWorksProductsCollection -> Dataimport. Click Clean/Commit/Optimize and then Execute.
Note: The Solr data import is configured via 
Solr\solr-4.1.0\adventureWorksInstance\solr\adventureWorksProductsCollection\conf\data-config.xml 
You will likely need to tweak the connection string and credentials for your database server.
6. Run the web application.

Key Solr Configuration Files
----------------------------
The following files exist in the 
Solr\solr-4.1.0\adventureWorksInstance\solr\adventureWorksProductsCollection\conf

* data-config.xml - Configuration for [Solr data import handler](http://wiki.apache.org/solr/DataImportHandler)
* elevate.xml - Configuration for [Solr query elevation component](http://wiki.apache.org/solr/QueryElevationComponent)
* schema.xml - The Solr document schema
* solrconfig.xml - Solr's main configuration, contains configuration of all request handlers
* synonyms.txt - Solr's word replacement configuration