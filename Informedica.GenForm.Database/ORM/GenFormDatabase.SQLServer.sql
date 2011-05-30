CREATE SCHEMA dbo
GO

GO


CREATE TABLE dbo.Pharmacy
(
	PharmacyId INTEGER IDENTITY (1, 1) NOT NULL,
	PharmacyName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Pharmacy_PK PRIMARY KEY(PharmacyId),
	CONSTRAINT Pharmacy_UC UNIQUE(PharmacyName)
)
GO


CREATE TABLE dbo.Pharmacist
(
	PharmacistId INTEGER IDENTITY (1, 1) NOT NULL,
	UserId INTEGER NOT NULL,
	PharmacyId INTEGER NOT NULL,
	CONSTRAINT Pharmacist_PK PRIMARY KEY(PharmacistId),
	CONSTRAINT Pharmacist_UC UNIQUE(UserId)
)
GO


CREATE TABLE dbo.Formularium
(
	FormulariumId INTEGER IDENTITY (1, 1) NOT NULL,
	FormulariumName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	PharmacistId INTEGER NOT NULL,
	CONSTRAINT Formularium_PK PRIMARY KEY(FormulariumId),
	CONSTRAINT Formularium_UC UNIQUE(FormulariumName)
)
GO


CREATE TABLE dbo.FormulariumPharmacist
(
	FormulariumPharmacistId INTEGER IDENTITY (1, 1) NOT NULL,
	PharmacistId INTEGER NOT NULL,
	FormulariumId INTEGER NOT NULL,
	CONSTRAINT FormulariumPharmacist_UC UNIQUE(PharmacistId, FormulariumId),
	CONSTRAINT FormulariumPharmacist_PK PRIMARY KEY(FormulariumPharmacistId)
)
GO


CREATE TABLE dbo.GenFormUser
(
	UserId INTEGER IDENTITY (1, 1) NOT NULL,
	UserName NATIONAL CHARACTER VARYING(255) NOT NULL,
	Email NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	PassWord NATIONAL CHARACTER VARYING(255) NOT NULL,
	FirstName NATIONAL CHARACTER VARYING(125),
	LastName NATIONAL CHARACTER VARYING(125),
	PagerNumber NATIONAL CHARACTER VARYING(30),
	CONSTRAINT GenFormUser_PK PRIMARY KEY(UserId),
	CONSTRAINT GenFormUser_UC1 UNIQUE(UserName),
	CONSTRAINT GenFormUser_UC4 UNIQUE(Email)
)
GO


CREATE VIEW dbo.GenFormUser_UC2 (FirstName, LastName)
WITH SCHEMABINDING
AS
	SELECT FirstName, LastName
	FROM 
		dbo.GenFormUser
	WHERE FirstName IS NOT NULL AND LastName IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX GenFormUser_UC2Index ON dbo.GenFormUser_UC2(FirstName, LastName)
GO


CREATE VIEW dbo.GenFormUser_UC3 (PagerNumber)
WITH SCHEMABINDING
AS
	SELECT PagerNumber
	FROM 
		dbo.GenFormUser
	WHERE PagerNumber IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX GenFormUser_UC3Index ON dbo.GenFormUser_UC3(PagerNumber)
GO


CREATE TABLE dbo.Role
(
	RoleId INTEGER IDENTITY (1, 1) NOT NULL,
	RoleName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	RoleDescription NATIONAL CHARACTER VARYING(1000),
	CONSTRAINT Role_PK PRIMARY KEY(RoleId),
	CONSTRAINT Role_UC UNIQUE(RoleName)
)
GO


CREATE TABLE dbo.UserRole
(
	UserRoleId INTEGER IDENTITY (1, 1) NOT NULL,
	UserId INTEGER NOT NULL,
	RoleId INTEGER NOT NULL,
	CONSTRAINT UserRole_UC UNIQUE(UserId, RoleId),
	CONSTRAINT UserRole_PK PRIMARY KEY(UserRoleId)
)
GO


CREATE TABLE dbo.Product
(
	ProductId INTEGER IDENTITY (1, 1) NOT NULL,
	DisplayName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	ShapeId INTEGER NOT NULL,
	PackageId INTEGER NOT NULL,
	GenericId INTEGER NOT NULL,
	UnitId INTEGER NOT NULL,
	ProductName NATIONAL CHARACTER VARYING(255),
	ProductKey NATIONAL CHARACTER VARYING(255),
	ProductQuantity DECIMAL(38,12),
	TradeProductCode NATIONAL CHARACTER VARYING(255),
	ProductCode NATIONAL CHARACTER VARYING(255),
	Divisor INTEGER CHECK (Divisor >= 0),
	BrandId INTEGER,
	CONSTRAINT Product_PK PRIMARY KEY(ProductId),
	CONSTRAINT Product_UC2 UNIQUE(DisplayName)
)
GO


CREATE VIEW dbo.Product_UC1 (ProductName)
WITH SCHEMABINDING
AS
	SELECT ProductName
	FROM 
		dbo.Product
	WHERE ProductName IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Product_UC1Index ON dbo.Product_UC1(ProductName)
GO


CREATE VIEW dbo.Product_UC3 (ProductKey)
WITH SCHEMABINDING
AS
	SELECT ProductKey
	FROM 
		dbo.Product
	WHERE ProductKey IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Product_UC3Index ON dbo.Product_UC3(ProductKey)
GO


CREATE TABLE dbo.ProductCombination
(
	ProductCombinationId INTEGER IDENTITY (1, 1) NOT NULL,
	ProductId INTEGER NOT NULL,
	Component INTEGER NOT NULL,
	ProductCombinationName NATIONAL CHARACTER VARYING(255),
	CONSTRAINT ProductCombination_UC UNIQUE(Component, ProductId),
	CONSTRAINT ProductCombination_PK PRIMARY KEY(ProductCombinationId)
)
GO


CREATE TABLE dbo.Package
(
	PackageId INTEGER IDENTITY (1, 1) NOT NULL,
	PackageName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Package_PK PRIMARY KEY(PackageId),
	CONSTRAINT Package_UC UNIQUE(PackageName)
)
GO


CREATE TABLE dbo.Shape
(
	ShapeId INTEGER IDENTITY (1, 1) NOT NULL,
	ShapeName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Shape_PK PRIMARY KEY(ShapeId),
	CONSTRAINT Shape_UC UNIQUE(ShapeName)
)
GO


CREATE TABLE dbo.Unit
(
	UnitId INTEGER IDENTITY (1, 1) NOT NULL,
	UnitName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	Multiplier DECIMAL(38,19) NOT NULL,
	UnitAbbreviation NATIONAL CHARACTER VARYING(255),
	Divisor INTEGER CHECK (Divisor >= 0),
	IsReference BIT,
	UnitGroupId INTEGER,
	CONSTRAINT Unit_PK PRIMARY KEY(UnitId),
	CONSTRAINT Unit_UC1 UNIQUE(UnitName)
)
GO


CREATE VIEW dbo.Unit_UC2 (UnitAbbreviation)
WITH SCHEMABINDING
AS
	SELECT UnitAbbreviation
	FROM 
		dbo.Unit
	WHERE UnitAbbreviation IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Unit_UC2Index ON dbo.Unit_UC2(UnitAbbreviation)
GO


CREATE TABLE dbo.Brand
(
	BrandId INTEGER IDENTITY (1, 1) NOT NULL,
	BrandName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Brand_PK PRIMARY KEY(BrandId),
	CONSTRAINT Brand_UC UNIQUE(BrandName)
)
GO


CREATE TABLE dbo.Substance
(
	SubstanceId INTEGER IDENTITY (1, 1) NOT NULL,
	SubstanceName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	IsGeneric BIT,
	SubstanceGroupId INTEGER,
	CONSTRAINT Substance_PK PRIMARY KEY(SubstanceId),
	CONSTRAINT Substance_UC UNIQUE(SubstanceName)
)
GO


CREATE TABLE dbo.ProductSubstance
(
	ProductSubstanceId INTEGER IDENTITY (1, 1) NOT NULL,
	ProductId INTEGER NOT NULL,
	SubstanceId INTEGER NOT NULL,
	SubstanceOrdering INTEGER CHECK (SubstanceOrdering >= 0) NOT NULL,
	Concentration DECIMAL(38,19),
	SubstanceQuantity DECIMAL(38,16),
	UnitId INTEGER,
	CONSTRAINT ProductSubstance_UC UNIQUE(ProductId, SubstanceId),
	CONSTRAINT ProductSubstance_PK PRIMARY KEY(ProductSubstanceId)
)
GO


CREATE TABLE dbo.SubstanceGroup
(
	SubstanceGroupId INTEGER IDENTITY (1, 1) NOT NULL,
	SubstanceGroupName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	MainSubstanceGroupId INTEGER,
	CONSTRAINT SubstanceGroup_PK PRIMARY KEY(SubstanceGroupId),
	CONSTRAINT SubstanceGroup_UC UNIQUE(SubstanceGroupName)
)
GO


CREATE TABLE dbo.UnitGroup
(
	UnitGroupId INTEGER IDENTITY (1, 1) NOT NULL,
	UnitGroupName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	AllowsConversion BIT,
	CONSTRAINT UnitGroup_PK PRIMARY KEY(UnitGroupId),
	CONSTRAINT UnitGroup_UC UNIQUE(UnitGroupName)
)
GO


CREATE TABLE dbo.ShapePackage
(
	ShapePackageId INTEGER IDENTITY (1, 1) NOT NULL,
	PackageId INTEGER NOT NULL,
	ShapeId INTEGER NOT NULL,
	CONSTRAINT ShapePackage_UC UNIQUE(ShapeId, PackageId),
	CONSTRAINT ShapePackage_PK PRIMARY KEY(ShapePackageId)
)
GO


CREATE TABLE dbo.ShapeUnit
(
	ShapeUnitId INTEGER IDENTITY (1, 1) NOT NULL,
	ShapeId INTEGER NOT NULL,
	UnitId INTEGER NOT NULL,
	CONSTRAINT ShapeUnit_UC UNIQUE(ShapeId, UnitId),
	CONSTRAINT ShapeUnit_PK PRIMARY KEY(ShapeUnitId)
)
GO


CREATE TABLE dbo.Chapter
(
	ChapterId INTEGER IDENTITY (1, 1) NOT NULL,
	ChapterName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Chapter_PK PRIMARY KEY(ChapterId),
	CONSTRAINT Chapter_UC UNIQUE(ChapterName)
)
GO


CREATE TABLE dbo.FormulariumChapter
(
	FormulariumChapterId INTEGER IDENTITY (1, 1) NOT NULL,
	FormulariumId INTEGER NOT NULL,
	ChapterId INTEGER NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	MainChapterId INTEGER,
	CONSTRAINT FormulariumChapter_UC UNIQUE(FormulariumId, ChapterId),
	CONSTRAINT FormulariumChapter_PK PRIMARY KEY(FormulariumChapterId)
)
GO


CREATE TABLE dbo.SubstanceDosingAdvice
(
	SubstanceDosingAdviceId INTEGER IDENTITY (1, 1) NOT NULL,
	TextItemId INTEGER NOT NULL,
	SubstanceId INTEGER NOT NULL,
	FormulariumId INTEGER,
	IndicationId INTEGER,
	CONSTRAINT SubstanceDosingAdvice_PK PRIMARY KEY(SubstanceDosingAdviceId),
	CONSTRAINT SubstanceDosingAdvice_UC UNIQUE(TextItemId)
)
GO


CREATE TABLE dbo.Route
(
	RouteId INTEGER IDENTITY (1, 1) NOT NULL,
	RouteName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	RouteAbbreviation NATIONAL CHARACTER VARYING(50),
	CONSTRAINT Route_PK PRIMARY KEY(RouteId),
	CONSTRAINT Route_UC1 UNIQUE(RouteName)
)
GO


CREATE VIEW dbo.Route_UC2 (RouteAbbreviation)
WITH SCHEMABINDING
AS
	SELECT RouteAbbreviation
	FROM 
		dbo.Route
	WHERE RouteAbbreviation IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Route_UC2Index ON dbo.Route_UC2(RouteAbbreviation)
GO


CREATE TABLE dbo.DosingAdviceChapter
(
	DosingAdviceChapterId INTEGER IDENTITY (1, 1) NOT NULL,
	SubstanceDosingAdviceId INTEGER NOT NULL,
	FormulariumChapterId INTEGER NOT NULL,
	CONSTRAINT DosingAdviceChapter_UC UNIQUE(SubstanceDosingAdviceId, FormulariumChapterId),
	CONSTRAINT DosingAdviceChapter_PK PRIMARY KEY(DosingAdviceChapterId)
)
GO


CREATE TABLE dbo.DosingAdviceRoute
(
	DosingAdviceRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	SubstanceDosingAdviceId INTEGER NOT NULL,
	RouteId INTEGER NOT NULL,
	CONSTRAINT DosingAdviceRoute_UC UNIQUE(SubstanceDosingAdviceId, RouteId),
	CONSTRAINT DosingAdviceRoute_PK PRIMARY KEY(DosingAdviceRouteId)
)
GO


CREATE TABLE dbo.DosingAdviceProduct
(
	DosingAdviceProductId INTEGER IDENTITY (1, 1) NOT NULL,
	SubstanceDosingAdviceId INTEGER NOT NULL,
	ProductId INTEGER NOT NULL,
	CONSTRAINT DosingAdviceProduct_UC UNIQUE(SubstanceDosingAdviceId, ProductId),
	CONSTRAINT DosingAdviceProduct_PK PRIMARY KEY(DosingAdviceProductId)
)
GO


CREATE TABLE dbo.ProductRoute
(
	ProductRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	ProductId INTEGER NOT NULL,
	RouteId INTEGER NOT NULL,
	CONSTRAINT ProductRoute_UC UNIQUE(ProductId, RouteId),
	CONSTRAINT ProductRoute_PK PRIMARY KEY(ProductRouteId)
)
GO


CREATE TABLE dbo.ShapeRoute
(
	ShapeRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	ShapeId INTEGER NOT NULL,
	RouteId INTEGER NOT NULL,
	CONSTRAINT ShapeRoute_UC UNIQUE(ShapeId, RouteId),
	CONSTRAINT ShapeRoute_PK PRIMARY KEY(ShapeRouteId)
)
GO


CREATE TABLE dbo.TextItem
(
	TextItemId INTEGER IDENTITY (1, 1) NOT NULL,
	TextType NATIONAL CHARACTER VARYING(50) NOT NULL,
	TextStatus NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp DATETIME NOT NULL,
	Text NATIONAL CHARACTER VARYING(MAX),
	TextHeading NATIONAL CHARACTER VARYING(255),
	CONSTRAINT TextItem_PK PRIMARY KEY(TextItemId)
)
GO


CREATE TABLE dbo.FormulariumText
(
	FormulariumTextId INTEGER IDENTITY (1, 1) NOT NULL,
	FormulariumId INTEGER NOT NULL,
	TextItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumText_UC UNIQUE(FormulariumId, TextItemId),
	CONSTRAINT FormulariumText_PK PRIMARY KEY(FormulariumTextId)
)
GO


CREATE TABLE dbo.ProductFormulariumText
(
	ProductFormulariumTextId INTEGER IDENTITY (1, 1) NOT NULL,
	ProductId INTEGER NOT NULL,
	FormulariumId INTEGER NOT NULL,
	TextItemId INTEGER NOT NULL,
	CONSTRAINT ProductFormulariumText_UC UNIQUE(ProductId, FormulariumId),
	CONSTRAINT ProductFormulariumText_PK PRIMARY KEY(ProductFormulariumTextId)
)
GO


CREATE TABLE dbo.FormulariumSubstanceText
(
	FormulariumSubstanceTextId INTEGER IDENTITY (1, 1) NOT NULL,
	SubstanceId INTEGER NOT NULL,
	FormulariumId INTEGER NOT NULL,
	TextItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumSubstanceText_UC UNIQUE(FormulariumId, SubstanceId),
	CONSTRAINT FormulariumSubstanceText_PK PRIMARY KEY(FormulariumSubstanceTextId)
)
GO


CREATE TABLE dbo.FormulariumChapterText
(
	FormulariumChapterTextId INTEGER IDENTITY (1, 1) NOT NULL,
	FormulariumChapterId INTEGER NOT NULL,
	TextItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumChapterText_UC UNIQUE(TextItemId, FormulariumChapterId),
	CONSTRAINT FormulariumChapterText_PK PRIMARY KEY(FormulariumChapterTextId)
)
GO


CREATE TABLE dbo.TextLog
(
	TextLogId INTEGER IDENTITY (1, 1) NOT NULL,
	LogData DATETIME NOT NULL,
	FormulariumId INTEGER NOT NULL,
	TextItemId INTEGER NOT NULL,
	UserId INTEGER NOT NULL,
	Action NATIONAL CHARACTER VARYING(MAX) NOT NULL,
	CONSTRAINT TextLog_PK PRIMARY KEY(TextLogId),
	CONSTRAINT TextLog_UC UNIQUE(TextItemId, UserId, LogData, FormulariumId)
)
GO


CREATE TABLE dbo.Indication
(
	IndicationId INTEGER IDENTITY (1, 1) NOT NULL,
	IndicationText NATIONAL CHARACTER VARYING(255) NOT NULL,
	CONSTRAINT Indication_PK PRIMARY KEY(IndicationId),
	CONSTRAINT Indication_UC UNIQUE(IndicationText)
)
GO


ALTER TABLE dbo.Pharmacist ADD CONSTRAINT Pharmacist_FK1 FOREIGN KEY (PharmacyId) REFERENCES dbo.Pharmacy (PharmacyId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Pharmacist ADD CONSTRAINT Pharmacist_FK2 FOREIGN KEY (UserId) REFERENCES dbo.GenFormUser (UserId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Formularium ADD CONSTRAINT Formularium_FK FOREIGN KEY (PharmacistId) REFERENCES dbo.Pharmacist (PharmacistId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumPharmacist ADD CONSTRAINT FormulariumPharmacist_FK1 FOREIGN KEY (PharmacistId) REFERENCES dbo.Pharmacist (PharmacistId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumPharmacist ADD CONSTRAINT FormulariumPharmacist_FK2 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.UserRole ADD CONSTRAINT UserRole_FK1 FOREIGN KEY (UserId) REFERENCES dbo.GenFormUser (UserId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.UserRole ADD CONSTRAINT UserRole_FK2 FOREIGN KEY (RoleId) REFERENCES dbo.Role (RoleId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK1 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (ShapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK2 FOREIGN KEY (PackageId) REFERENCES dbo.Package (PackageId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK3 FOREIGN KEY (BrandId) REFERENCES dbo.Brand (BrandId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK4 FOREIGN KEY (GenericId) REFERENCES dbo.Substance (SubstanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK5 FOREIGN KEY (UnitId) REFERENCES dbo.Unit (UnitId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductCombination ADD CONSTRAINT ProductCombination_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (ProductId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductCombination ADD CONSTRAINT ProductCombination_FK2 FOREIGN KEY (Component) REFERENCES dbo.Product (ProductId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Unit ADD CONSTRAINT Unit_FK FOREIGN KEY (UnitGroupId) REFERENCES dbo.UnitGroup (UnitGroupId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Substance ADD CONSTRAINT Substance_FK FOREIGN KEY (SubstanceGroupId) REFERENCES dbo.SubstanceGroup (SubstanceGroupId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductSubstance ADD CONSTRAINT ProductSubstance_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (ProductId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductSubstance ADD CONSTRAINT ProductSubstance_FK2 FOREIGN KEY (SubstanceId) REFERENCES dbo.Substance (SubstanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductSubstance ADD CONSTRAINT ProductSubstance_FK3 FOREIGN KEY (UnitId) REFERENCES dbo.Unit (UnitId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceGroup ADD CONSTRAINT SubstanceGroup_FK FOREIGN KEY (MainSubstanceGroupId) REFERENCES dbo.SubstanceGroup (SubstanceGroupId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapePackage ADD CONSTRAINT ShapePackage_FK1 FOREIGN KEY (PackageId) REFERENCES dbo.Package (PackageId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapePackage ADD CONSTRAINT ShapePackage_FK2 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (ShapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeUnit ADD CONSTRAINT ShapeUnit_FK1 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (ShapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeUnit ADD CONSTRAINT ShapeUnit_FK2 FOREIGN KEY (UnitId) REFERENCES dbo.Unit (UnitId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK2 FOREIGN KEY (ChapterId) REFERENCES dbo.Chapter (ChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK3 FOREIGN KEY (MainChapterId) REFERENCES dbo.FormulariumChapter (FormulariumChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK2 FOREIGN KEY (SubstanceId) REFERENCES dbo.Substance (SubstanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK3 FOREIGN KEY (IndicationId) REFERENCES dbo.Indication (IndicationId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK4 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (TextItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceChapter ADD CONSTRAINT DosingAdviceChapter_FK1 FOREIGN KEY (SubstanceDosingAdviceId) REFERENCES dbo.SubstanceDosingAdvice (SubstanceDosingAdviceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceChapter ADD CONSTRAINT DosingAdviceChapter_FK2 FOREIGN KEY (FormulariumChapterId) REFERENCES dbo.FormulariumChapter (FormulariumChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceRoute ADD CONSTRAINT DosingAdviceRoute_FK1 FOREIGN KEY (SubstanceDosingAdviceId) REFERENCES dbo.SubstanceDosingAdvice (SubstanceDosingAdviceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceRoute ADD CONSTRAINT DosingAdviceRoute_FK2 FOREIGN KEY (RouteId) REFERENCES dbo.Route (RouteId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceProduct ADD CONSTRAINT DosingAdviceProduct_FK1 FOREIGN KEY (SubstanceDosingAdviceId) REFERENCES dbo.SubstanceDosingAdvice (SubstanceDosingAdviceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceProduct ADD CONSTRAINT DosingAdviceProduct_FK2 FOREIGN KEY (ProductId) REFERENCES dbo.Product (ProductId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductRoute ADD CONSTRAINT ProductRoute_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (ProductId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductRoute ADD CONSTRAINT ProductRoute_FK2 FOREIGN KEY (RouteId) REFERENCES dbo.Route (RouteId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeRoute ADD CONSTRAINT ShapeRoute_FK1 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (ShapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeRoute ADD CONSTRAINT ShapeRoute_FK2 FOREIGN KEY (RouteId) REFERENCES dbo.Route (RouteId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumText ADD CONSTRAINT FormulariumText_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumText ADD CONSTRAINT FormulariumText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (TextItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (ProductId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (TextItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK3 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK1 FOREIGN KEY (SubstanceId) REFERENCES dbo.Substance (SubstanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (TextItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK3 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapterText ADD CONSTRAINT FormulariumChapterText_FK1 FOREIGN KEY (FormulariumChapterId) REFERENCES dbo.FormulariumChapter (FormulariumChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapterText ADD CONSTRAINT FormulariumChapterText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (TextItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TextLog ADD CONSTRAINT TextLog_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (FormulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TextLog ADD CONSTRAINT TextLog_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (TextItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TextLog ADD CONSTRAINT TextLog_FK3 FOREIGN KEY (UserId) REFERENCES dbo.GenFormUser (UserId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO