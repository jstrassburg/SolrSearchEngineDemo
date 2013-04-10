USE [AdventureWorks2012]
GO

/****** Object:  View [Production].[vSolrImport]    Script Date: 04/09/2013 11:40:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Production].[vSolrImport]
AS
SELECT P.[ProductID], P.[ProductNumber], P.[Name], pd.[Description] AS ProductDescription,
	P.[Color], PC.[Name] AS CategoryName, PS.[Name] AS SubcategoryName, P.[ListPrice],
	PP.[ThumbnailPhotoFileName]
FROM [Production].[Product] p 
INNER JOIN [Production].[ProductModel] pm 
    ON p.[ProductModelID] = pm.[ProductModelID] 
INNER JOIN [Production].[ProductModelProductDescriptionCulture] pmx 
    ON pm.[ProductModelID] = pmx.[ProductModelID] 
INNER JOIN [Production].[ProductDescription] pd 
    ON pmx.[ProductDescriptionID] = pd.[ProductDescriptionID]
INNER JOIN Production.ProductSubcategory PS 
	ON PS.ProductSubcategoryID = P.ProductSubcategoryID
INNER JOIN Production.ProductCategory PC
	ON PC.ProductCategoryID = PS.ProductCategoryID
INNER JOIN Production.[ProductProductPhoto] PPP
	ON PPP.ProductID = P.ProductID AND PPP.[Primary] = 1
INNER JOIN Production.ProductPhoto PP
	ON PP.ProductPhotoID = PPP.ProductPhotoID
WHERE pmx.[CultureID] = 'en' and P.[ListPrice] > 0;

GO

