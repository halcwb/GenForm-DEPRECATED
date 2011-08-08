CREATE SCHEMA dbo
GO

GO


CREATE TABLE dbo.Pharmacy
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	CONSTRAINT Pharmacy_PK PRIMARY KEY(Id),
	CONSTRAINT Pharmacy_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.Pharmacist
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	UserId INTEGER CHECK (UserId >= 0) NOT NULL,
	PharmacyId INTEGER CHECK (PharmacyId >= 0) NOT NULL,
	CONSTRAINT Pharmacist_PK PRIMARY KEY(Id),
	CONSTRAINT Pharmacist_UC UNIQUE(UserId)
)
GO


CREATE TABLE dbo.Formularium
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	PharmacistId INTEGER CHECK (PharmacistId >= 0) NOT NULL,
	CONSTRAINT Formularium_PK PRIMARY KEY(Id),
	CONSTRAINT Formularium_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.FormulariumPharmacist
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	PharmacistId INTEGER CHECK (PharmacistId >= 0) NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0) NOT NULL,
	CONSTRAINT FormulariumPharmacist_UC UNIQUE(PharmacistId, FormulariumId),
	CONSTRAINT FormulariumPharmacist_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo."User"
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Email NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER NOT NULL,
	PassWord NATIONAL CHARACTER VARYING(255) NOT NULL,
	FirstName NATIONAL CHARACTER VARYING(125),
	LastName NATIONAL CHARACTER VARYING(125),
	PagerNumber NATIONAL CHARACTER VARYING(30),
	CONSTRAINT User_PK PRIMARY KEY(Id),
	CONSTRAINT User_UC1 UNIQUE(Name),
	CONSTRAINT User_UC4 UNIQUE(Email)
)
GO


CREATE VIEW dbo.User_UC2 (FirstName, LastName)
WITH SCHEMABINDING
AS
	SELECT FirstName, LastName
	FROM 
		dbo."User"
	WHERE FirstName IS NOT NULL AND LastName IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX User_UC2Index ON dbo.User_UC2(FirstName, LastName)
GO


CREATE VIEW dbo.User_UC3 (PagerNumber)
WITH SCHEMABINDING
AS
	SELECT PagerNumber
	FROM 
		dbo."User"
	WHERE PagerNumber IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX User_UC3Index ON dbo.User_UC3(PagerNumber)
GO


CREATE TABLE dbo.Role
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	Description NATIONAL CHARACTER VARYING(1000),
	CONSTRAINT Role_PK PRIMARY KEY(Id),
	CONSTRAINT Role_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.UserRole
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	UserId INTEGER CHECK (UserId >= 0) NOT NULL,
	RoleId INTEGER CHECK (RoleId >= 0) NOT NULL,
	CONSTRAINT UserRole_UC UNIQUE(UserId, RoleId),
	CONSTRAINT UserRole_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.Product
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	DisplayName NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	ShapeId INTEGER CHECK (ShapeId >= 0) NOT NULL,
	PackageId INTEGER CHECK (PackageId >= 0) NOT NULL,
	GenericId INTEGER CHECK (GenericId >= 0) NOT NULL,
	UnitId INTEGER CHECK (UnitId >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255),
	ProductKey NATIONAL CHARACTER VARYING(255),
	ProductQuantity DECIMAL(38,12),
	TradeProductCode NATIONAL CHARACTER VARYING(255),
	ProductCode NATIONAL CHARACTER VARYING(255),
	Divisor INTEGER CHECK (Divisor >= 0),
	BrandId INTEGER CHECK (BrandId >= 0),
	CONSTRAINT Product_PK PRIMARY KEY(Id),
	CONSTRAINT Product_UC2 UNIQUE(DisplayName)
)
GO


CREATE VIEW dbo.Product_UC1 (Name)
WITH SCHEMABINDING
AS
	SELECT Name
	FROM 
		dbo.Product
	WHERE Name IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Product_UC1Index ON dbo.Product_UC1(Name)
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
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	ProductId INTEGER CHECK (ProductId >= 0) NOT NULL,
	Component INTEGER CHECK (Component >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255),
	CONSTRAINT ProductCombination_UC UNIQUE(Component, ProductId),
	CONSTRAINT ProductCombination_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.Package
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER NOT NULL,
	CONSTRAINT Package_PK PRIMARY KEY(Id),
	CONSTRAINT Package_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.Shape
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	CONSTRAINT Shape_PK PRIMARY KEY(Id),
	CONSTRAINT Shape_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.Unit
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	Multiplier DECIMAL(38,19) NOT NULL,
	UnitGroupId INTEGER CHECK (UnitGroupId >= 0) NOT NULL,
	Abbreviation NATIONAL CHARACTER VARYING(255),
	Divisor INTEGER CHECK (Divisor >= 0),
	IsReference BIT,
	CONSTRAINT Unit_PK PRIMARY KEY(Id),
	CONSTRAINT Unit_UC1 UNIQUE(Name)
)
GO


CREATE VIEW dbo.Unit_UC2 (Abbreviation)
WITH SCHEMABINDING
AS
	SELECT Abbreviation
	FROM 
		dbo.Unit
	WHERE Abbreviation IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Unit_UC2Index ON dbo.Unit_UC2(Abbreviation)
GO


CREATE TABLE dbo.Brand
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	CONSTRAINT Brand_PK PRIMARY KEY(Id),
	CONSTRAINT Brand_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.Substance
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	SubstanceName NATIONAL CHARACTER VARYING(255) NOT NULL,
	VersionTimeStamp INTEGER CHECK (VersionTimeStamp >= 0) NOT NULL,
	IsGeneric BIT,
	SubstanceGroupId INTEGER CHECK (SubstanceGroupId >= 0),
	CONSTRAINT Substance_PK PRIMARY KEY(Id),
	CONSTRAINT Substance_UC UNIQUE(SubstanceName)
)
GO


CREATE TABLE dbo.ProductSubstance
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	ProductId INTEGER CHECK (ProductId >= 0) NOT NULL,
	SubstanceId INTEGER CHECK (SubstanceId >= 0) NOT NULL,
	SortOrder INTEGER CHECK (SortOrder >= 0) NOT NULL,
	Concentration DECIMAL(38,19),
	Quantity DECIMAL(38,16),
	UnitId INTEGER CHECK (UnitId >= 0),
	CONSTRAINT ProductSubstance_UC UNIQUE(ProductId, SubstanceId),
	CONSTRAINT ProductSubstance_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.SubstanceGroup
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	MainSubstanceGroupId INTEGER CHECK (MainSubstanceGroupId >= 0),
	CONSTRAINT SubstanceGroup_PK PRIMARY KEY(Id),
	CONSTRAINT SubstanceGroup_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.UnitGroup
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	AllowsConversion BIT,
	CONSTRAINT UnitGroup_PK PRIMARY KEY(Id),
	CONSTRAINT UnitGroup_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.ShapePackage
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	PackageId INTEGER CHECK (PackageId >= 0) NOT NULL,
	ShapeId INTEGER CHECK (ShapeId >= 0) NOT NULL,
	CONSTRAINT ShapePackage_UC UNIQUE(ShapeId, PackageId),
	CONSTRAINT ShapePackage_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.ShapeUnit
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	ShapeId INTEGER CHECK (ShapeId >= 0) NOT NULL,
	UnitId INTEGER CHECK (UnitId >= 0) NOT NULL,
	CONSTRAINT ShapeUnit_UC UNIQUE(ShapeId, UnitId),
	CONSTRAINT ShapeUnit_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.Chapter
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	CONSTRAINT Chapter_PK PRIMARY KEY(Id),
	CONSTRAINT Chapter_UC UNIQUE(Name)
)
GO


CREATE TABLE dbo.FormulariumChapter
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0) NOT NULL,
	ChapterId INTEGER CHECK (ChapterId >= 0) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	MainChapterId INTEGER CHECK (MainChapterId >= 0),
	CONSTRAINT FormulariumChapter_UC UNIQUE(FormulariumId, ChapterId),
	CONSTRAINT FormulariumChapter_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.SubstanceDosingAdvice
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	TextItemId INTEGER CHECK (TextItemId >= 0) NOT NULL,
	SubstanceId INTEGER CHECK (SubstanceId >= 0) NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0),
	IndicationId INTEGER CHECK (IndicationId >= 0),
	CONSTRAINT SubstanceDosingAdvice_PK PRIMARY KEY(Id),
	CONSTRAINT SubstanceDosingAdvice_UC UNIQUE(TextItemId)
)
GO


CREATE TABLE dbo.Route
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Name NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	Abbreviation NATIONAL CHARACTER VARYING(50),
	CONSTRAINT Route_PK PRIMARY KEY(Id),
	CONSTRAINT Route_UC1 UNIQUE(Name)
)
GO


CREATE VIEW dbo.Route_UC2 (Abbreviation)
WITH SCHEMABINDING
AS
	SELECT Abbreviation
	FROM 
		dbo.Route
	WHERE Abbreviation IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Route_UC2Index ON dbo.Route_UC2(Abbreviation)
GO


CREATE TABLE dbo.DosingAdviceChapter
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	SubstanceDosingAdviceId INTEGER CHECK (SubstanceDosingAdviceId >= 0) NOT NULL,
	FormulariumChapterId INTEGER CHECK (FormulariumChapterId >= 0) NOT NULL,
	CONSTRAINT DosingAdviceChapter_UC UNIQUE(SubstanceDosingAdviceId, FormulariumChapterId),
	CONSTRAINT DosingAdviceChapter_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.DosingAdviceRoute
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	SubstanceDosingAdviceId INTEGER CHECK (SubstanceDosingAdviceId >= 0) NOT NULL,
	RouteId INTEGER CHECK (RouteId >= 0) NOT NULL,
	CONSTRAINT DosingAdviceRoute_UC UNIQUE(SubstanceDosingAdviceId, RouteId),
	CONSTRAINT DosingAdviceRoute_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.DosingAdviceProduct
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	SubstanceDosingAdviceId INTEGER CHECK (SubstanceDosingAdviceId >= 0) NOT NULL,
	ProductId INTEGER CHECK (ProductId >= 0) NOT NULL,
	CONSTRAINT DosingAdviceProduct_UC UNIQUE(SubstanceDosingAdviceId, ProductId),
	CONSTRAINT DosingAdviceProduct_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.ProductRoute
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	ProductId INTEGER CHECK (ProductId >= 0) NOT NULL,
	RouteId INTEGER CHECK (RouteId >= 0) NOT NULL,
	CONSTRAINT ProductRoute_UC UNIQUE(ProductId, RouteId),
	CONSTRAINT ProductRoute_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.ShapeRoute
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	ShapeId INTEGER CHECK (ShapeId >= 0) NOT NULL,
	RouteId INTEGER CHECK (RouteId >= 0) NOT NULL,
	CONSTRAINT ShapeRoute_UC UNIQUE(ShapeId, RouteId),
	CONSTRAINT ShapeRoute_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.TextItem
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Type NATIONAL CHARACTER VARYING(50) NOT NULL,
	Status NATIONAL CHARACTER VARYING(255) NOT NULL,
	Version INTEGER CHECK (Version >= 0) NOT NULL,
	Text NATIONAL CHARACTER VARYING(MAX),
	Caption NATIONAL CHARACTER VARYING(255),
	CONSTRAINT TextItem_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.FormulariumText
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0) NOT NULL,
	TextItemId INTEGER CHECK (TextItemId >= 0) NOT NULL,
	CONSTRAINT FormulariumText_UC UNIQUE(FormulariumId, TextItemId),
	CONSTRAINT FormulariumText_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.ProductFormulariumText
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	ProductId INTEGER CHECK (ProductId >= 0) NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0) NOT NULL,
	TextItemId INTEGER CHECK (TextItemId >= 0) NOT NULL,
	CONSTRAINT ProductFormulariumText_UC UNIQUE(ProductId, FormulariumId),
	CONSTRAINT ProductFormulariumText_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.FormulariumSubstanceText
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	SubstanceId INTEGER CHECK (SubstanceId >= 0) NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0) NOT NULL,
	TextItemId INTEGER CHECK (TextItemId >= 0) NOT NULL,
	CONSTRAINT FormulariumSubstanceText_UC UNIQUE(FormulariumId, SubstanceId),
	CONSTRAINT FormulariumSubstanceText_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.FormulariumChapterText
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	FormulariumChapterId INTEGER CHECK (FormulariumChapterId >= 0) NOT NULL,
	TextItemId INTEGER CHECK (TextItemId >= 0) NOT NULL,
	CONSTRAINT FormulariumChapterText_UC UNIQUE(TextItemId, FormulariumChapterId),
	CONSTRAINT FormulariumChapterText_PK PRIMARY KEY(Id)
)
GO


CREATE TABLE dbo.TextLog
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	LogData DATETIME NOT NULL,
	FormulariumId INTEGER CHECK (FormulariumId >= 0) NOT NULL,
	TextItemId INTEGER CHECK (TextItemId >= 0) NOT NULL,
	UserId INTEGER CHECK (UserId >= 0) NOT NULL,
	Action NATIONAL CHARACTER VARYING(MAX) NOT NULL,
	CONSTRAINT TextLog_PK PRIMARY KEY(Id),
	CONSTRAINT TextLog_UC UNIQUE(TextItemId, UserId, LogData, FormulariumId)
)
GO


CREATE TABLE dbo.Indication
(
	Id INTEGER CHECK (Id >= 0) NOT NULL,
	Text NATIONAL CHARACTER VARYING(255) NOT NULL,
	CONSTRAINT Indication_PK PRIMARY KEY(Id),
	CONSTRAINT Indication_UC UNIQUE(Text)
)
GO


ALTER TABLE dbo.Pharmacist ADD CONSTRAINT Pharmacist_FK1 FOREIGN KEY (PharmacyId) REFERENCES dbo.Pharmacy (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Pharmacist ADD CONSTRAINT Pharmacist_FK2 FOREIGN KEY (UserId) REFERENCES dbo."User" (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Formularium ADD CONSTRAINT Formularium_FK FOREIGN KEY (PharmacistId) REFERENCES dbo.Pharmacist (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumPharmacist ADD CONSTRAINT FormulariumPharmacist_FK1 FOREIGN KEY (PharmacistId) REFERENCES dbo.Pharmacist (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumPharmacist ADD CONSTRAINT FormulariumPharmacist_FK2 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.UserRole ADD CONSTRAINT UserRole_FK1 FOREIGN KEY (UserId) REFERENCES dbo."User" (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.UserRole ADD CONSTRAINT UserRole_FK2 FOREIGN KEY (RoleId) REFERENCES dbo.Role (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK1 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK2 FOREIGN KEY (PackageId) REFERENCES dbo.Package (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK3 FOREIGN KEY (BrandId) REFERENCES dbo.Brand (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK4 FOREIGN KEY (GenericId) REFERENCES dbo.Substance (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Product ADD CONSTRAINT Product_FK5 FOREIGN KEY (UnitId) REFERENCES dbo.Unit (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductCombination ADD CONSTRAINT ProductCombination_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductCombination ADD CONSTRAINT ProductCombination_FK2 FOREIGN KEY (Component) REFERENCES dbo.Product (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Unit ADD CONSTRAINT Unit_FK FOREIGN KEY (UnitGroupId) REFERENCES dbo.UnitGroup (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Substance ADD CONSTRAINT Substance_FK FOREIGN KEY (SubstanceGroupId) REFERENCES dbo.SubstanceGroup (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductSubstance ADD CONSTRAINT ProductSubstance_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductSubstance ADD CONSTRAINT ProductSubstance_FK2 FOREIGN KEY (SubstanceId) REFERENCES dbo.Substance (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductSubstance ADD CONSTRAINT ProductSubstance_FK3 FOREIGN KEY (UnitId) REFERENCES dbo.Unit (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceGroup ADD CONSTRAINT SubstanceGroup_FK FOREIGN KEY (MainSubstanceGroupId) REFERENCES dbo.SubstanceGroup (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapePackage ADD CONSTRAINT ShapePackage_FK1 FOREIGN KEY (PackageId) REFERENCES dbo.Package (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapePackage ADD CONSTRAINT ShapePackage_FK2 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeUnit ADD CONSTRAINT ShapeUnit_FK1 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeUnit ADD CONSTRAINT ShapeUnit_FK2 FOREIGN KEY (UnitId) REFERENCES dbo.Unit (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK2 FOREIGN KEY (ChapterId) REFERENCES dbo.Chapter (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK3 FOREIGN KEY (MainChapterId) REFERENCES dbo.FormulariumChapter (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK2 FOREIGN KEY (SubstanceId) REFERENCES dbo.Substance (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK3 FOREIGN KEY (IndicationId) REFERENCES dbo.Indication (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK4 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceChapter ADD CONSTRAINT DosingAdviceChapter_FK1 FOREIGN KEY (SubstanceDosingAdviceId) REFERENCES dbo.SubstanceDosingAdvice (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceChapter ADD CONSTRAINT DosingAdviceChapter_FK2 FOREIGN KEY (FormulariumChapterId) REFERENCES dbo.FormulariumChapter (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceRoute ADD CONSTRAINT DosingAdviceRoute_FK1 FOREIGN KEY (SubstanceDosingAdviceId) REFERENCES dbo.SubstanceDosingAdvice (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceRoute ADD CONSTRAINT DosingAdviceRoute_FK2 FOREIGN KEY (RouteId) REFERENCES dbo.Route (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceProduct ADD CONSTRAINT DosingAdviceProduct_FK1 FOREIGN KEY (SubstanceDosingAdviceId) REFERENCES dbo.SubstanceDosingAdvice (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DosingAdviceProduct ADD CONSTRAINT DosingAdviceProduct_FK2 FOREIGN KEY (ProductId) REFERENCES dbo.Product (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductRoute ADD CONSTRAINT ProductRoute_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductRoute ADD CONSTRAINT ProductRoute_FK2 FOREIGN KEY (RouteId) REFERENCES dbo.Route (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeRoute ADD CONSTRAINT ShapeRoute_FK1 FOREIGN KEY (ShapeId) REFERENCES dbo.Shape (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ShapeRoute ADD CONSTRAINT ShapeRoute_FK2 FOREIGN KEY (RouteId) REFERENCES dbo.Route (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumText ADD CONSTRAINT FormulariumText_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumText ADD CONSTRAINT FormulariumText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK1 FOREIGN KEY (ProductId) REFERENCES dbo.Product (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK3 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK1 FOREIGN KEY (SubstanceId) REFERENCES dbo.Substance (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK3 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapterText ADD CONSTRAINT FormulariumChapterText_FK1 FOREIGN KEY (FormulariumChapterId) REFERENCES dbo.FormulariumChapter (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumChapterText ADD CONSTRAINT FormulariumChapterText_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TextLog ADD CONSTRAINT TextLog_FK1 FOREIGN KEY (FormulariumId) REFERENCES dbo.Formularium (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TextLog ADD CONSTRAINT TextLog_FK2 FOREIGN KEY (TextItemId) REFERENCES dbo.TextItem (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TextLog ADD CONSTRAINT TextLog_FK3 FOREIGN KEY (UserId) REFERENCES dbo."User" (Id) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO