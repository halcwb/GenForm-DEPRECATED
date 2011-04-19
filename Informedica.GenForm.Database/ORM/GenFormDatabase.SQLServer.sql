CREATE SCHEMA GenFormDatabase
GO

GO


CREATE TABLE GenFormDatabase.Pharmacy
(
	pharmacyId INTEGER IDENTITY (1, 1) NOT NULL,
	pharmacyName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Pharmacy_PK PRIMARY KEY(pharmacyId),
	CONSTRAINT Pharmacy_UC UNIQUE(pharmacyName)
)
GO


CREATE TABLE GenFormDatabase.Pharmacist
(
	pharmacistId INTEGER IDENTITY (1, 1) NOT NULL,
	userId INTEGER NOT NULL,
	pharmacyId INTEGER NOT NULL,
	CONSTRAINT Pharmacist_PK PRIMARY KEY(pharmacistId),
	CONSTRAINT Pharmacist_UC UNIQUE(userId)
)
GO


CREATE TABLE GenFormDatabase.Formularium
(
	formulariumId INTEGER IDENTITY (1, 1) NOT NULL,
	formulariumName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	pharmacistId INTEGER NOT NULL,
	CONSTRAINT Formularium_PK PRIMARY KEY(formulariumId),
	CONSTRAINT Formularium_UC UNIQUE(formulariumName)
)
GO


CREATE TABLE GenFormDatabase.FormulariumPharmacist
(
	formulariumPharmacistId INTEGER IDENTITY (1, 1) NOT NULL,
	pharmacistId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	CONSTRAINT FormulariumPharmacist_UC UNIQUE(pharmacistId, formulariumId),
	CONSTRAINT FormulariumPharmacist_PK PRIMARY KEY(formulariumPharmacistId)
)
GO


CREATE TABLE GenFormDatabase.GenFormUser
(
	userId INTEGER IDENTITY (1, 1) NOT NULL,
	userName NATIONAL CHARACTER VARYING(255) NOT NULL,
	email NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	passWord NATIONAL CHARACTER VARYING(255) NOT NULL,
	firstName NATIONAL CHARACTER VARYING(125),
	lastName NATIONAL CHARACTER VARYING(125),
	pagerNumber NATIONAL CHARACTER VARYING(30),
	CONSTRAINT GenFormUser_PK PRIMARY KEY(userId),
	CONSTRAINT GenFormUser_UC1 UNIQUE(userName),
	CONSTRAINT GenFormUser_UC4 UNIQUE(email)
)
GO


CREATE VIEW GenFormDatabase.GenFormUser_UC2 (firstName, lastName)
WITH SCHEMABINDING
AS
	SELECT firstName, lastName
	FROM 
		GenFormDatabase.GenFormUser
	WHERE firstName IS NOT NULL AND lastName IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX GenFormUser_UC2Index ON GenFormDatabase.GenFormUser_UC2(firstName, lastName)
GO


CREATE VIEW GenFormDatabase.GenFormUser_UC3 (pagerNumber)
WITH SCHEMABINDING
AS
	SELECT pagerNumber
	FROM 
		GenFormDatabase.GenFormUser
	WHERE pagerNumber IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX GenFormUser_UC3Index ON GenFormDatabase.GenFormUser_UC3(pagerNumber)
GO


CREATE TABLE GenFormDatabase.Role
(
	roleId INTEGER IDENTITY (1, 1) NOT NULL,
	roleName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	roleDescription NATIONAL CHARACTER VARYING(1000),
	CONSTRAINT Role_PK PRIMARY KEY(roleId),
	CONSTRAINT Role_UC UNIQUE(roleName)
)
GO


CREATE TABLE GenFormDatabase.UserRole
(
	userRoleId INTEGER IDENTITY (1, 1) NOT NULL,
	userId INTEGER NOT NULL,
	roleId INTEGER NOT NULL,
	CONSTRAINT UserRole_UC UNIQUE(userId, roleId),
	CONSTRAINT UserRole_PK PRIMARY KEY(userRoleId)
)
GO


CREATE TABLE GenFormDatabase.Product
(
	productId INTEGER IDENTITY (1, 1) NOT NULL,
	displayName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	shapeId INTEGER NOT NULL,
	packageId INTEGER NOT NULL,
	substanceId INTEGER NOT NULL,
	unitId INTEGER NOT NULL,
	productName NATIONAL CHARACTER VARYING(255),
	productKey NATIONAL CHARACTER VARYING(255),
	productQuantity DOUBLE PRECISION,
	tradeProductCode NATIONAL CHARACTER VARYING(255),
	productCode NATIONAL CHARACTER VARYING(255),
	divisor INTEGER CHECK (divisor >= 0),
	brandId INTEGER,
	CONSTRAINT Product_PK PRIMARY KEY(productId),
	CONSTRAINT Product_UC2 UNIQUE(displayName)
)
GO


CREATE VIEW GenFormDatabase.Product_UC1 (productName)
WITH SCHEMABINDING
AS
	SELECT productName
	FROM 
		GenFormDatabase.Product
	WHERE productName IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Product_UC1Index ON GenFormDatabase.Product_UC1(productName)
GO


CREATE VIEW GenFormDatabase.Product_UC3 (productKey)
WITH SCHEMABINDING
AS
	SELECT productKey
	FROM 
		GenFormDatabase.Product
	WHERE productKey IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Product_UC3Index ON GenFormDatabase.Product_UC3(productKey)
GO


CREATE TABLE GenFormDatabase.ProductCombination
(
	productCombinationId INTEGER IDENTITY (1, 1) NOT NULL,
	productId INTEGER NOT NULL,
	component INTEGER NOT NULL,
	productCombinationName NATIONAL CHARACTER VARYING(255),
	CONSTRAINT ProductCombination_UC UNIQUE(component, productId),
	CONSTRAINT ProductCombination_PK PRIMARY KEY(productCombinationId)
)
GO


CREATE TABLE GenFormDatabase.Package
(
	packageId INTEGER IDENTITY (1, 1) NOT NULL,
	packageName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Package_PK PRIMARY KEY(packageId),
	CONSTRAINT Package_UC UNIQUE(packageName)
)
GO


CREATE TABLE GenFormDatabase.Shape
(
	shapeId INTEGER IDENTITY (1, 1) NOT NULL,
	shapeName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Shape_PK PRIMARY KEY(shapeId),
	CONSTRAINT Shape_UC UNIQUE(shapeName)
)
GO


CREATE TABLE GenFormDatabase.Unit
(
	unitId INTEGER IDENTITY (1, 1) NOT NULL,
	unitName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	multiplier DOUBLE PRECISION NOT NULL,
	unitAbbreviation NATIONAL CHARACTER VARYING(255),
	divisor INTEGER CHECK (divisor >= 0),
	isReference BIT,
	unitGroupId INTEGER,
	CONSTRAINT Unit_PK PRIMARY KEY(unitId),
	CONSTRAINT Unit_UC1 UNIQUE(unitName)
)
GO


CREATE VIEW GenFormDatabase.Unit_UC2 (unitAbbreviation)
WITH SCHEMABINDING
AS
	SELECT unitAbbreviation
	FROM 
		GenFormDatabase.Unit
	WHERE unitAbbreviation IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Unit_UC2Index ON GenFormDatabase.Unit_UC2(unitAbbreviation)
GO


CREATE TABLE GenFormDatabase.Brand
(
	brandId INTEGER IDENTITY (1, 1) NOT NULL,
	brandName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Brand_PK PRIMARY KEY(brandId),
	CONSTRAINT Brand_UC UNIQUE(brandName)
)
GO


CREATE TABLE GenFormDatabase.Substance
(
	substanceId INTEGER IDENTITY (1, 1) NOT NULL,
	substanceName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	isGeneric BIT,
	substanceGroupId INTEGER,
	CONSTRAINT Substance_PK PRIMARY KEY(substanceId),
	CONSTRAINT Substance_UC UNIQUE(substanceName)
)
GO


CREATE TABLE GenFormDatabase.ProductSubstance
(
	productSubstanceId INTEGER IDENTITY (1, 1) NOT NULL,
	productId INTEGER NOT NULL,
	substanceId INTEGER NOT NULL,
	substanceOrdering INTEGER CHECK (substanceOrdering >= 0) NOT NULL,
	concentration DOUBLE PRECISION,
	substanceQuantity DOUBLE PRECISION,
	unitId INTEGER,
	CONSTRAINT ProductSubstance_UC UNIQUE(productId, substanceId),
	CONSTRAINT ProductSubstance_PK PRIMARY KEY(productSubstanceId)
)
GO


CREATE TABLE GenFormDatabase.SubstanceGroup
(
	substanceGroupId INTEGER IDENTITY (1, 1) NOT NULL,
	substanceGroupName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	mainSubstanceGroupId INTEGER,
	CONSTRAINT SubstanceGroup_PK PRIMARY KEY(substanceGroupId),
	CONSTRAINT SubstanceGroup_UC UNIQUE(substanceGroupName)
)
GO


CREATE TABLE GenFormDatabase.UnitGroup
(
	unitGroupId INTEGER IDENTITY (1, 1) NOT NULL,
	unitGroupName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	allowsConversion BIT,
	CONSTRAINT UnitGroup_PK PRIMARY KEY(unitGroupId),
	CONSTRAINT UnitGroup_UC UNIQUE(unitGroupName)
)
GO


CREATE TABLE GenFormDatabase.ShapePackage
(
	shapePackageId INTEGER IDENTITY (1, 1) NOT NULL,
	packageId INTEGER NOT NULL,
	shapeId INTEGER NOT NULL,
	CONSTRAINT ShapePackage_UC UNIQUE(shapeId, packageId),
	CONSTRAINT ShapePackage_PK PRIMARY KEY(shapePackageId)
)
GO


CREATE TABLE GenFormDatabase.ShapeUnit
(
	shapeUnitId INTEGER IDENTITY (1, 1) NOT NULL,
	shapeId INTEGER NOT NULL,
	unitId INTEGER NOT NULL,
	CONSTRAINT ShapeUnit_UC UNIQUE(shapeId, unitId),
	CONSTRAINT ShapeUnit_PK PRIMARY KEY(shapeUnitId)
)
GO


CREATE TABLE GenFormDatabase.Chapter
(
	chapterId INTEGER IDENTITY (1, 1) NOT NULL,
	chapterName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	CONSTRAINT Chapter_PK PRIMARY KEY(chapterId),
	CONSTRAINT Chapter_UC UNIQUE(chapterName)
)
GO


CREATE TABLE GenFormDatabase.FormulariumChapter
(
	formulariumChapterId INTEGER IDENTITY (1, 1) NOT NULL,
	formulariumId INTEGER NOT NULL,
	chapterId INTEGER NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	mainChapterId INTEGER,
	CONSTRAINT FormulariumChapter_UC UNIQUE(formulariumId, chapterId),
	CONSTRAINT FormulariumChapter_PK PRIMARY KEY(formulariumChapterId)
)
GO


CREATE TABLE GenFormDatabase.SubstanceDosingAdvice
(
	substanceDosingAdviceId INTEGER IDENTITY (1, 1) NOT NULL,
	textItemId INTEGER NOT NULL,
	substanceId INTEGER NOT NULL,
	formulariumId INTEGER,
	indicationId INTEGER,
	CONSTRAINT SubstanceDosingAdvice_PK PRIMARY KEY(substanceDosingAdviceId),
	CONSTRAINT SubstanceDosingAdvice_UC UNIQUE(textItemId)
)
GO


CREATE TABLE GenFormDatabase.Route
(
	routeId INTEGER IDENTITY (1, 1) NOT NULL,
	routeName NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	routeAbbreviation NATIONAL CHARACTER VARYING(50),
	CONSTRAINT Route_PK PRIMARY KEY(routeId),
	CONSTRAINT Route_UC1 UNIQUE(routeName)
)
GO


CREATE VIEW GenFormDatabase.Route_UC2 (routeAbbreviation)
WITH SCHEMABINDING
AS
	SELECT routeAbbreviation
	FROM 
		GenFormDatabase.Route
	WHERE routeAbbreviation IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Route_UC2Index ON GenFormDatabase.Route_UC2(routeAbbreviation)
GO


CREATE TABLE GenFormDatabase.DosingAdviceChapter
(
	dosingAdviceChapterId INTEGER IDENTITY (1, 1) NOT NULL,
	substanceDosingAdviceId INTEGER NOT NULL,
	formulariumChapterId INTEGER NOT NULL,
	CONSTRAINT DosingAdviceChapter_UC UNIQUE(substanceDosingAdviceId, formulariumChapterId),
	CONSTRAINT DosingAdviceChapter_PK PRIMARY KEY(dosingAdviceChapterId)
)
GO


CREATE TABLE GenFormDatabase.DosingAdviceRoute
(
	dosingAdviceRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	substanceDosingAdviceId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	CONSTRAINT DosingAdviceRoute_UC UNIQUE(substanceDosingAdviceId, routeId),
	CONSTRAINT DosingAdviceRoute_PK PRIMARY KEY(dosingAdviceRouteId)
)
GO


CREATE TABLE GenFormDatabase.DosingAdviceProduct
(
	dosingAdviceProductId INTEGER IDENTITY (1, 1) NOT NULL,
	substanceDosingAdviceId INTEGER NOT NULL,
	productId INTEGER NOT NULL,
	CONSTRAINT DosingAdviceProduct_UC UNIQUE(substanceDosingAdviceId, productId),
	CONSTRAINT DosingAdviceProduct_PK PRIMARY KEY(dosingAdviceProductId)
)
GO


CREATE TABLE GenFormDatabase.ProductRoute
(
	productRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	productId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	CONSTRAINT ProductRoute_UC UNIQUE(productId, routeId),
	CONSTRAINT ProductRoute_PK PRIMARY KEY(productRouteId)
)
GO


CREATE TABLE GenFormDatabase.ShapeRoute
(
	shapeRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	shapeId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	CONSTRAINT ShapeRoute_UC UNIQUE(shapeId, routeId),
	CONSTRAINT ShapeRoute_PK PRIMARY KEY(shapeRouteId)
)
GO


CREATE TABLE GenFormDatabase.TextItem
(
	textItemId INTEGER IDENTITY (1, 1) NOT NULL,
	textType NATIONAL CHARACTER VARYING(50) NOT NULL,
	textStatus NATIONAL CHARACTER VARYING(255) NOT NULL,
	versionTimeStamp DATETIME NOT NULL,
	text NATIONAL CHARACTER VARYING(MAX),
	textHeading NATIONAL CHARACTER VARYING(255),
	CONSTRAINT TextItem_PK PRIMARY KEY(textItemId)
)
GO


CREATE TABLE GenFormDatabase.FormulariumText
(
	formulariumTextId INTEGER IDENTITY (1, 1) NOT NULL,
	formulariumId INTEGER NOT NULL,
	textItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumText_UC UNIQUE(formulariumId, textItemId),
	CONSTRAINT FormulariumText_PK PRIMARY KEY(formulariumTextId)
)
GO


CREATE TABLE GenFormDatabase.ProductFormulariumText
(
	productFormulariumTextId INTEGER IDENTITY (1, 1) NOT NULL,
	productId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	textItemId INTEGER NOT NULL,
	CONSTRAINT ProductFormulariumText_UC UNIQUE(productId, formulariumId),
	CONSTRAINT ProductFormulariumText_PK PRIMARY KEY(productFormulariumTextId)
)
GO


CREATE TABLE GenFormDatabase.FormulariumSubstanceText
(
	formulariumSubstanceTextId INTEGER IDENTITY (1, 1) NOT NULL,
	substanceId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	textItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumSubstanceText_UC UNIQUE(formulariumId, substanceId),
	CONSTRAINT FormulariumSubstanceText_PK PRIMARY KEY(formulariumSubstanceTextId)
)
GO


CREATE TABLE GenFormDatabase.FormulariumChapterText
(
	formulariumChapterTextId INTEGER IDENTITY (1, 1) NOT NULL,
	formulariumChapterId INTEGER NOT NULL,
	textItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumChapterText_UC UNIQUE(textItemId, formulariumChapterId),
	CONSTRAINT FormulariumChapterText_PK PRIMARY KEY(formulariumChapterTextId)
)
GO


CREATE TABLE GenFormDatabase.TextLog
(
	textLogId INTEGER IDENTITY (1, 1) NOT NULL,
	logData DATETIME NOT NULL,
	formulariumId INTEGER NOT NULL,
	textItemId INTEGER NOT NULL,
	userId INTEGER NOT NULL,
	action NATIONAL CHARACTER VARYING(MAX) NOT NULL,
	CONSTRAINT TextLog_PK PRIMARY KEY(textLogId),
	CONSTRAINT TextLog_UC UNIQUE(textItemId, userId, logData, formulariumId)
)
GO


CREATE TABLE GenFormDatabase.Indication
(
	indicationId INTEGER IDENTITY (1, 1) NOT NULL,
	indicationText NATIONAL CHARACTER VARYING(255) NOT NULL,
	CONSTRAINT Indication_PK PRIMARY KEY(indicationId),
	CONSTRAINT Indication_UC UNIQUE(indicationText)
)
GO


ALTER TABLE GenFormDatabase.Pharmacist ADD CONSTRAINT Pharmacist_FK1 FOREIGN KEY (pharmacyId) REFERENCES GenFormDatabase.Pharmacy (pharmacyId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Pharmacist ADD CONSTRAINT Pharmacist_FK2 FOREIGN KEY (userId) REFERENCES GenFormDatabase.GenFormUser (userId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Formularium ADD CONSTRAINT Formularium_FK FOREIGN KEY (pharmacistId) REFERENCES GenFormDatabase.Pharmacist (pharmacistId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumPharmacist ADD CONSTRAINT FormulariumPharmacist_FK1 FOREIGN KEY (pharmacistId) REFERENCES GenFormDatabase.Pharmacist (pharmacistId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumPharmacist ADD CONSTRAINT FormulariumPharmacist_FK2 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.UserRole ADD CONSTRAINT UserRole_FK1 FOREIGN KEY (userId) REFERENCES GenFormDatabase.GenFormUser (userId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.UserRole ADD CONSTRAINT UserRole_FK2 FOREIGN KEY (roleId) REFERENCES GenFormDatabase.Role (roleId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Product ADD CONSTRAINT Product_FK1 FOREIGN KEY (shapeId) REFERENCES GenFormDatabase.Shape (shapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Product ADD CONSTRAINT Product_FK2 FOREIGN KEY (packageId) REFERENCES GenFormDatabase.Package (packageId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Product ADD CONSTRAINT Product_FK3 FOREIGN KEY (brandId) REFERENCES GenFormDatabase.Brand (brandId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Product ADD CONSTRAINT Product_FK4 FOREIGN KEY (substanceId) REFERENCES GenFormDatabase.Substance (substanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Product ADD CONSTRAINT Product_FK5 FOREIGN KEY (unitId) REFERENCES GenFormDatabase.Unit (unitId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductCombination ADD CONSTRAINT ProductCombination_FK1 FOREIGN KEY (productId) REFERENCES GenFormDatabase.Product (productId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductCombination ADD CONSTRAINT ProductCombination_FK2 FOREIGN KEY (component) REFERENCES GenFormDatabase.Product (productId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Unit ADD CONSTRAINT Unit_FK FOREIGN KEY (unitGroupId) REFERENCES GenFormDatabase.UnitGroup (unitGroupId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.Substance ADD CONSTRAINT Substance_FK FOREIGN KEY (substanceGroupId) REFERENCES GenFormDatabase.SubstanceGroup (substanceGroupId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductSubstance ADD CONSTRAINT ProductSubstance_FK1 FOREIGN KEY (productId) REFERENCES GenFormDatabase.Product (productId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductSubstance ADD CONSTRAINT ProductSubstance_FK2 FOREIGN KEY (substanceId) REFERENCES GenFormDatabase.Substance (substanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductSubstance ADD CONSTRAINT ProductSubstance_FK3 FOREIGN KEY (unitId) REFERENCES GenFormDatabase.Unit (unitId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.SubstanceGroup ADD CONSTRAINT SubstanceGroup_FK FOREIGN KEY (mainSubstanceGroupId) REFERENCES GenFormDatabase.SubstanceGroup (substanceGroupId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ShapePackage ADD CONSTRAINT ShapePackage_FK1 FOREIGN KEY (packageId) REFERENCES GenFormDatabase.Package (packageId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ShapePackage ADD CONSTRAINT ShapePackage_FK2 FOREIGN KEY (shapeId) REFERENCES GenFormDatabase.Shape (shapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ShapeUnit ADD CONSTRAINT ShapeUnit_FK1 FOREIGN KEY (shapeId) REFERENCES GenFormDatabase.Shape (shapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ShapeUnit ADD CONSTRAINT ShapeUnit_FK2 FOREIGN KEY (unitId) REFERENCES GenFormDatabase.Unit (unitId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK1 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK2 FOREIGN KEY (chapterId) REFERENCES GenFormDatabase.Chapter (chapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumChapter ADD CONSTRAINT FormulariumChapter_FK3 FOREIGN KEY (mainChapterId) REFERENCES GenFormDatabase.FormulariumChapter (formulariumChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK1 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK2 FOREIGN KEY (substanceId) REFERENCES GenFormDatabase.Substance (substanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK3 FOREIGN KEY (indicationId) REFERENCES GenFormDatabase.Indication (indicationId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.SubstanceDosingAdvice ADD CONSTRAINT SubstanceDosingAdvice_FK4 FOREIGN KEY (textItemId) REFERENCES GenFormDatabase.TextItem (textItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.DosingAdviceChapter ADD CONSTRAINT DosingAdviceChapter_FK1 FOREIGN KEY (substanceDosingAdviceId) REFERENCES GenFormDatabase.SubstanceDosingAdvice (substanceDosingAdviceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.DosingAdviceChapter ADD CONSTRAINT DosingAdviceChapter_FK2 FOREIGN KEY (formulariumChapterId) REFERENCES GenFormDatabase.FormulariumChapter (formulariumChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.DosingAdviceRoute ADD CONSTRAINT DosingAdviceRoute_FK1 FOREIGN KEY (substanceDosingAdviceId) REFERENCES GenFormDatabase.SubstanceDosingAdvice (substanceDosingAdviceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.DosingAdviceRoute ADD CONSTRAINT DosingAdviceRoute_FK2 FOREIGN KEY (routeId) REFERENCES GenFormDatabase.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.DosingAdviceProduct ADD CONSTRAINT DosingAdviceProduct_FK1 FOREIGN KEY (substanceDosingAdviceId) REFERENCES GenFormDatabase.SubstanceDosingAdvice (substanceDosingAdviceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.DosingAdviceProduct ADD CONSTRAINT DosingAdviceProduct_FK2 FOREIGN KEY (productId) REFERENCES GenFormDatabase.Product (productId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductRoute ADD CONSTRAINT ProductRoute_FK1 FOREIGN KEY (productId) REFERENCES GenFormDatabase.Product (productId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductRoute ADD CONSTRAINT ProductRoute_FK2 FOREIGN KEY (routeId) REFERENCES GenFormDatabase.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ShapeRoute ADD CONSTRAINT ShapeRoute_FK1 FOREIGN KEY (shapeId) REFERENCES GenFormDatabase.Shape (shapeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ShapeRoute ADD CONSTRAINT ShapeRoute_FK2 FOREIGN KEY (routeId) REFERENCES GenFormDatabase.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumText ADD CONSTRAINT FormulariumText_FK1 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumText ADD CONSTRAINT FormulariumText_FK2 FOREIGN KEY (textItemId) REFERENCES GenFormDatabase.TextItem (textItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK1 FOREIGN KEY (productId) REFERENCES GenFormDatabase.Product (productId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK2 FOREIGN KEY (textItemId) REFERENCES GenFormDatabase.TextItem (textItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.ProductFormulariumText ADD CONSTRAINT ProductFormulariumText_FK3 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK1 FOREIGN KEY (substanceId) REFERENCES GenFormDatabase.Substance (substanceId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK2 FOREIGN KEY (textItemId) REFERENCES GenFormDatabase.TextItem (textItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumSubstanceText ADD CONSTRAINT FormulariumSubstanceText_FK3 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumChapterText ADD CONSTRAINT FormulariumChapterText_FK1 FOREIGN KEY (formulariumChapterId) REFERENCES GenFormDatabase.FormulariumChapter (formulariumChapterId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.FormulariumChapterText ADD CONSTRAINT FormulariumChapterText_FK2 FOREIGN KEY (textItemId) REFERENCES GenFormDatabase.TextItem (textItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.TextLog ADD CONSTRAINT TextLog_FK1 FOREIGN KEY (formulariumId) REFERENCES GenFormDatabase.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.TextLog ADD CONSTRAINT TextLog_FK2 FOREIGN KEY (textItemId) REFERENCES GenFormDatabase.TextItem (textItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE GenFormDatabase.TextLog ADD CONSTRAINT TextLog_FK3 FOREIGN KEY (userId) REFERENCES GenFormDatabase.GenFormUser (userId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO