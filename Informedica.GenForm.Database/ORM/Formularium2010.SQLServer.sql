CREATE SCHEMA dbo
GO

GO


CREATE TABLE dbo.Gebruiker
(
	gebruikerId INTEGER IDENTITY (1, 1) NOT NULL,
	gebruikerNaam NATIONAL CHARACTER(30) NOT NULL,
	email NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	paswoord NATIONAL CHARACTER(125) NOT NULL,
	voorNaam NATIONAL CHARACTER VARYING(125),
	achterNaam NATIONAL CHARACTER(125),
	sein NATIONAL CHARACTER(125),
	CONSTRAINT Gebruiker_PK PRIMARY KEY(gebruikerId),
	CONSTRAINT Gebruiker_UC1 UNIQUE(gebruikerNaam),
	CONSTRAINT Gebruiker_UC4 UNIQUE(email),
	CONSTRAINT Gebruiker_UC5 UNIQUE(versieTimeStamp)
)
GO


CREATE VIEW dbo.Gebruiker_UC2 (voorNaam, achterNaam)
WITH SCHEMABINDING
AS
	SELECT voorNaam, achterNaam
	FROM 
		dbo.Gebruiker
	WHERE voorNaam IS NOT NULL AND achterNaam IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Gebruiker_UC2Index ON dbo.Gebruiker_UC2(voorNaam, achterNaam)
GO


CREATE VIEW dbo.Gebruiker_UC3 (sein)
WITH SCHEMABINDING
AS
	SELECT sein
	FROM 
		dbo.Gebruiker
	WHERE sein IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Gebruiker_UC3Index ON dbo.Gebruiker_UC3(sein)
GO


CREATE TABLE dbo.Apotheek
(
	apotheekId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER VARYING(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	CONSTRAINT Apotheek_PK PRIMARY KEY(apotheekId),
	CONSTRAINT Apotheek_UC1 UNIQUE(naam),
	CONSTRAINT Apotheek_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.Formularium
(
	formulariumId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER VARYING(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	apotheekId INTEGER NOT NULL,
	hoofdauteur INTEGER NOT NULL,
	CONSTRAINT Formularium_PK PRIMARY KEY(formulariumId),
	CONSTRAINT Formularium_UC1 UNIQUE(naam),
	CONSTRAINT Formularium_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.Merk
(
	merkId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	CONSTRAINT Merk_PK PRIMARY KEY(merkId),
	CONSTRAINT Merk_UC1 UNIQUE(naam),
	CONSTRAINT Merk_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.ToedienVorm
(
	toedienVormId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	CONSTRAINT ToedienVorm_PK PRIMARY KEY(toedienVormId),
	CONSTRAINT ToedienVorm_UC1 UNIQUE(naam),
	CONSTRAINT ToedienVorm_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.Verpakking
(
	verpakkingId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	CONSTRAINT Verpakking_PK PRIMARY KEY(verpakkingId),
	CONSTRAINT Verpakking_UC1 UNIQUE(naam),
	CONSTRAINT Verpakking_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.GeneriekGroep
(
	generiekGroepId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	hoofdGroep INTEGER,
	CONSTRAINT GeneriekGroep_PK PRIMARY KEY(generiekGroepId),
	CONSTRAINT GeneriekGroep_UC1 UNIQUE(naam),
	CONSTRAINT GeneriekGroep_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.Generiek
(
	generiekId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	code NATIONAL CHARACTER(125),
	isStof BIT,
	generiekGroepId INTEGER,
	CONSTRAINT Generiek_PK PRIMARY KEY(generiekId),
	CONSTRAINT Generiek_UC1 UNIQUE(naam),
	CONSTRAINT Generiek_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.Artikel
(
	artikelId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	generiekId INTEGER NOT NULL,
	ArtikelGUID NATIONAL CHARACTER(125),
	HPKode NATIONAL CHARACTER(125),
	defaultNaam NATIONAL CHARACTER VARYING(MAX),
	KAE REAL,
	isCombinatie BIT,
	ATKode NATIONAL CHARACTER(125),
	hoeveelheid DOUBLE PRECISION,
	eenheidId INTEGER,
	toedienVormId INTEGER,
	verpakkingId INTEGER,
	merkId INTEGER,
	CONSTRAINT Artikel_PK PRIMARY KEY(artikelId),
	CONSTRAINT Artikel_UC1 UNIQUE(naam),
	CONSTRAINT Artikel_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.ArtikelGeneriek
(
	artikelGeneriekId INTEGER IDENTITY (1, 1) NOT NULL,
	volgorde INTEGER CHECK (volgorde >= 0) NOT NULL,
	artikelId INTEGER NOT NULL,
	generiekId INTEGER NOT NULL,
	sterkte DOUBLE PRECISION,
	hoeveelheid DOUBLE PRECISION,
	eenheidId INTEGER,
	CONSTRAINT ArtikelGeneriek_UC1 UNIQUE(artikelId, generiekId),
	CONSTRAINT ArtikelGeneriek_UC2 UNIQUE(artikelId, volgorde),
	CONSTRAINT ArtikelGeneriek_PK PRIMARY KEY(artikelGeneriekId)
)
GO


CREATE TABLE dbo.Eenheid
(
	eenheidId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	eenheidGroepId INTEGER NOT NULL,
	afkorting NATIONAL CHARACTER(125),
	factor DOUBLE PRECISION,
	KAE REAL,
	isReferentie BIT,
	CONSTRAINT Eenheid_PK PRIMARY KEY(eenheidId),
	CONSTRAINT Eenheid_UC2 UNIQUE(naam),
	CONSTRAINT Eenheid_UC3 UNIQUE(versieTimeStamp)
)
GO


CREATE VIEW dbo.Eenheid_UC1 (afkorting)
WITH SCHEMABINDING
AS
	SELECT afkorting
	FROM 
		dbo.Eenheid
	WHERE afkorting IS NOT NULL
GO


CREATE UNIQUE CLUSTERED INDEX Eenheid_UC1Index ON dbo.Eenheid_UC1(afkorting)
GO


CREATE TABLE dbo.EenheidGroep
(
	eenheidGroepId INTEGER IDENTITY (1, 1) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	conversieToestaan BIT,
	CONSTRAINT EenheidGroep_PK PRIMARY KEY(eenheidGroepId),
	CONSTRAINT EenheidGroep_UC UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.Route
(
	routeId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	CONSTRAINT Route_PK PRIMARY KEY(routeId),
	CONSTRAINT Route_UC1 UNIQUE(naam),
	CONSTRAINT Route_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.ArtikelRoute
(
	artikelRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	artikelId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	CONSTRAINT ArtikelRoute_UC UNIQUE(routeId, artikelId),
	CONSTRAINT ArtikelRoute_PK PRIMARY KEY(artikelRouteId)
)
GO


CREATE TABLE dbo.ToedienVormVerpakking
(
	toedienVormVerpakkingId INTEGER IDENTITY (1, 1) NOT NULL,
	toedienVormId INTEGER NOT NULL,
	verpakkingId INTEGER NOT NULL,
	CONSTRAINT ToedienVormVerpakking_UC UNIQUE(toedienVormId, verpakkingId),
	CONSTRAINT ToedienVormVerpakking_PK PRIMARY KEY(toedienVormVerpakkingId)
)
GO


CREATE TABLE dbo.ToedienVormEenheid
(
	toedienVormEenheidId INTEGER IDENTITY (1, 1) NOT NULL,
	toedienVormId INTEGER NOT NULL,
	eenheidId INTEGER NOT NULL,
	CONSTRAINT ToedienVormEenheid_UC UNIQUE(toedienVormId, eenheidId),
	CONSTRAINT ToedienVormEenheid_PK PRIMARY KEY(toedienVormEenheidId)
)
GO


CREATE TABLE dbo.ToepassingGroep
(
	toepassingGroepId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(125) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	formulariumId INTEGER,
	hoofdGroep INTEGER,
	CONSTRAINT ToepassingGroep_PK PRIMARY KEY(toepassingGroepId),
	CONSTRAINT ToepassingGroep_UC1 UNIQUE(naam),
	CONSTRAINT ToepassingGroep_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.GeneriekDosering
(
	generiekDoseringId INTEGER IDENTITY (1, 1) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	tekstItemId INTEGER NOT NULL,
	generiekId INTEGER NOT NULL,
	formulariumId INTEGER,
	CONSTRAINT GeneriekDosering_PK PRIMARY KEY(generiekDoseringId),
	CONSTRAINT GeneriekDosering_UC1 UNIQUE(versieTimeStamp),
	CONSTRAINT GeneriekDosering_UC2 UNIQUE(tekstItemId)
)
GO


CREATE TABLE dbo.TekstItem
(
	tekstItemId INTEGER IDENTITY (1, 1) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	tekst NATIONAL CHARACTER VARYING(MAX) NOT NULL,
	tekstType NATIONAL CHARACTER(50) NOT NULL,
	tekstStatus NATIONAL CHARACTER(50) NOT NULL,
	tekstKop NATIONAL CHARACTER(125),
	CONSTRAINT TekstItem_PK PRIMARY KEY(tekstItemId),
	CONSTRAINT TekstItem_UC UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.DoseringToepassing
(
	doseringToepassingId INTEGER IDENTITY (1, 1) NOT NULL,
	generiekDoseringId INTEGER NOT NULL,
	toepassingGroepId INTEGER NOT NULL,
	CONSTRAINT DoseringToepassing_UC UNIQUE(generiekDoseringId, toepassingGroepId),
	CONSTRAINT DoseringToepassing_PK PRIMARY KEY(doseringToepassingId)
)
GO


CREATE TABLE dbo.DoseringArtikel
(
	doseringArtikelId INTEGER IDENTITY (1, 1) NOT NULL,
	generiekDoseringId INTEGER NOT NULL,
	artikelId INTEGER NOT NULL,
	CONSTRAINT DoseringArtikel_UC UNIQUE(generiekDoseringId, artikelId),
	CONSTRAINT DoseringArtikel_PK PRIMARY KEY(doseringArtikelId)
)
GO


CREATE TABLE dbo.DoseringRoute
(
	doseringRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	generiekDoseringId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	CONSTRAINT DoseringRoute_UC UNIQUE(generiekDoseringId, routeId),
	CONSTRAINT DoseringRoute_PK PRIMARY KEY(doseringRouteId)
)
GO


CREATE TABLE dbo.ToedienVormRoute
(
	toedienVormRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	toedienVormId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	CONSTRAINT ToedienVormRoute_UC UNIQUE(toedienVormId, routeId),
	CONSTRAINT ToedienVormRoute_PK PRIMARY KEY(toedienVormRouteId)
)
GO


CREATE TABLE dbo.FormulariumAuteur
(
	formulariumAuteurId INTEGER IDENTITY (1, 1) NOT NULL,
	auteur INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	CONSTRAINT FormulariumAuteur_PK PRIMARY KEY(formulariumAuteurId),
	CONSTRAINT FormulariumAuteur_UC UNIQUE(auteur, formulariumId)
)
GO


CREATE TABLE dbo.GeneriekBijzonderheid
(
	generiekBijzonderheidId INTEGER IDENTITY (1, 1) NOT NULL,
	generiekId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	tekstItemId INTEGER NOT NULL,
	CONSTRAINT GeneriekBijzonderheid_UC UNIQUE(generiekId, formulariumId),
	CONSTRAINT GeneriekBijzonderheid_PK PRIMARY KEY(generiekBijzonderheidId)
)
GO


CREATE TABLE dbo.ToepassingBijzonderheid
(
	toepassingBijzonderheidId INTEGER IDENTITY (1, 1) NOT NULL,
	toepassingGroepId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	tekstItemId INTEGER NOT NULL,
	CONSTRAINT ToepassingBijzonderheid_UC UNIQUE(toepassingGroepId, formulariumId),
	CONSTRAINT ToepassingBijzonderheid_PK PRIMARY KEY(toepassingBijzonderheidId)
)
GO


CREATE TABLE dbo.ArtikelBijzonderheid
(
	artikelBijzonderheidId INTEGER IDENTITY (1, 1) NOT NULL,
	artikelId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	tekstItemId INTEGER NOT NULL,
	CONSTRAINT ArtikelBijzonderheid_UC UNIQUE(artikelId, formulariumId),
	CONSTRAINT ArtikelBijzonderheid_PK PRIMARY KEY(artikelBijzonderheidId)
)
GO


CREATE TABLE dbo.FormulariumTekst
(
	formulariumTekstId INTEGER IDENTITY (1, 1) NOT NULL,
	formulariumId INTEGER NOT NULL,
	tekstItemId INTEGER NOT NULL,
	CONSTRAINT FormulariumTekst_UC UNIQUE(formulariumId, tekstItemId),
	CONSTRAINT FormulariumTekst_PK PRIMARY KEY(formulariumTekstId)
)
GO


CREATE TABLE dbo.TekstLog
(
	tekstLogId INTEGER IDENTITY (1, 1) NOT NULL,
	logDatum DATETIME NOT NULL,
	tekstItemId INTEGER NOT NULL,
	formulariumId INTEGER NOT NULL,
	gebruikerId INTEGER NOT NULL,
	actie NATIONAL CHARACTER(50) NOT NULL,
	CONSTRAINT TekstLog_PK PRIMARY KEY(tekstLogId),
	CONSTRAINT TekstLog_UC UNIQUE(tekstItemId, gebruikerId, logDatum, formulariumId)
)
GO


CREATE TABLE dbo.Publicatie
(
	publicatieId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieDatum DATETIME NOT NULL,
	formulariumId INTEGER NOT NULL,
	gebruikerId INTEGER NOT NULL,
	CONSTRAINT Publicatie_PK PRIMARY KEY(publicatieId),
	CONSTRAINT Publicatie_UC UNIQUE(formulariumId, publicatieDatum)
)
GO


CREATE TABLE dbo.PublicatieHoofdstuk
(
	publicatieHoofdstukId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieId INTEGER NOT NULL,
	hoofdstuk INTEGER NOT NULL,
	inleiding INTEGER,
	CONSTRAINT PublicatieHoofdstuk_UC UNIQUE(hoofdstuk, publicatieId),
	CONSTRAINT PublicatieHoofdstuk_PK PRIMARY KEY(publicatieHoofdstukId)
)
GO


CREATE TABLE dbo.PublicatieTherapieGroep
(
	publicatieTherapieGroepId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieHoofdstukId INTEGER NOT NULL,
	therapieGroep INTEGER NOT NULL,
	inleiding INTEGER,
	CONSTRAINT PublicatieTherapieGroep_UC UNIQUE(publicatieHoofdstukId, therapieGroep),
	CONSTRAINT PublicatieTherapieGroep_PK PRIMARY KEY(publicatieTherapieGroepId)
)
GO


CREATE TABLE dbo.PublicatieGeneriek
(
	publicatieGeneriekId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieTherapieGroepId INTEGER NOT NULL,
	generiekId INTEGER NOT NULL,
	generiekBijzonderheid INTEGER,
	CONSTRAINT PublicatieGeneriek_UC UNIQUE(generiekId, publicatieTherapieGroepId),
	CONSTRAINT PublicatieGeneriek_PK PRIMARY KEY(publicatieGeneriekId)
)
GO


CREATE TABLE dbo.PublicatieRoute
(
	publicatieRouteId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieGeneriekId INTEGER NOT NULL,
	routeId INTEGER NOT NULL,
	routeDosering INTEGER,
	bijzonderheid INTEGER,
	CONSTRAINT PublicatieRoute_UC UNIQUE(publicatieGeneriekId, routeId),
	CONSTRAINT PublicatieRoute_PK PRIMARY KEY(publicatieRouteId)
)
GO


CREATE TABLE dbo.PublicatieArtikel
(
	publicatieArtikelId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieRouteId INTEGER NOT NULL,
	artikelId INTEGER NOT NULL,
	artikelDosering INTEGER,
	bijzonderheid INTEGER,
	CONSTRAINT PublicatieArtikel_UC UNIQUE(publicatieRouteId, artikelId),
	CONSTRAINT PublicatieArtikel_PK PRIMARY KEY(publicatieArtikelId)
)
GO


CREATE TABLE dbo.PublicatieFormulariumTekst
(
	publicatieFormulariumTekstId INTEGER IDENTITY (1, 1) NOT NULL,
	publicatieId INTEGER NOT NULL,
	formulariumTekst INTEGER NOT NULL,
	CONSTRAINT PublicatieFormulariumTekst_UC UNIQUE(publicatieId, formulariumTekst),
	CONSTRAINT PublicatieFormulariumTekst_PK PRIMARY KEY(publicatieFormulariumTekstId)
)
GO


CREATE TABLE dbo.Rol
(
	rolId INTEGER IDENTITY (1, 1) NOT NULL,
	naam NATIONAL CHARACTER(30) NOT NULL,
	versieTimeStamp DATETIME NOT NULL,
	omschrijving NATIONAL CHARACTER VARYING(MAX),
	CONSTRAINT Rol_PK PRIMARY KEY(rolId),
	CONSTRAINT Rol_UC1 UNIQUE(naam),
	CONSTRAINT Rol_UC2 UNIQUE(versieTimeStamp)
)
GO


CREATE TABLE dbo.GebruikerRol
(
	gebruikerRolId INTEGER IDENTITY (1, 1) NOT NULL,
	gebruikerId INTEGER NOT NULL,
	rolId INTEGER NOT NULL,
	CONSTRAINT GebruikerRol_UC UNIQUE(rolId, gebruikerId),
	CONSTRAINT GebruikerRol_PK PRIMARY KEY(gebruikerRolId)
)
GO


CREATE TABLE dbo.Apotheker
(
	apothekerId INTEGER IDENTITY (1, 1) NOT NULL,
	gebruikerId INTEGER NOT NULL,
	apotheekId INTEGER NOT NULL,
	CONSTRAINT Apotheker_PK PRIMARY KEY(apothekerId)
)
GO


ALTER TABLE dbo.Formularium ADD CONSTRAINT Formularium_FK1 FOREIGN KEY (apotheekId) REFERENCES dbo.Apotheek (apotheekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Formularium ADD CONSTRAINT Formularium_FK2 FOREIGN KEY (hoofdauteur) REFERENCES dbo.Apotheker (apothekerId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekGroep ADD CONSTRAINT GeneriekGroep_FK FOREIGN KEY (hoofdGroep) REFERENCES dbo.GeneriekGroep (generiekGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Generiek ADD CONSTRAINT Generiek_FK FOREIGN KEY (generiekGroepId) REFERENCES dbo.GeneriekGroep (generiekGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Artikel ADD CONSTRAINT Artikel_FK1 FOREIGN KEY (generiekId) REFERENCES dbo.Generiek (generiekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Artikel ADD CONSTRAINT Artikel_FK2 FOREIGN KEY (eenheidId) REFERENCES dbo.Eenheid (eenheidId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Artikel ADD CONSTRAINT Artikel_FK3 FOREIGN KEY (toedienVormId) REFERENCES dbo.ToedienVorm (toedienVormId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Artikel ADD CONSTRAINT Artikel_FK4 FOREIGN KEY (verpakkingId) REFERENCES dbo.Verpakking (verpakkingId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Artikel ADD CONSTRAINT Artikel_FK5 FOREIGN KEY (merkId) REFERENCES dbo.Merk (merkId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelGeneriek ADD CONSTRAINT ArtikelGeneriek_FK1 FOREIGN KEY (artikelId) REFERENCES dbo.Artikel (artikelId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelGeneriek ADD CONSTRAINT ArtikelGeneriek_FK2 FOREIGN KEY (generiekId) REFERENCES dbo.Generiek (generiekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelGeneriek ADD CONSTRAINT ArtikelGeneriek_FK3 FOREIGN KEY (eenheidId) REFERENCES dbo.Eenheid (eenheidId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Eenheid ADD CONSTRAINT Eenheid_FK FOREIGN KEY (eenheidGroepId) REFERENCES dbo.EenheidGroep (eenheidGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelRoute ADD CONSTRAINT ArtikelRoute_FK1 FOREIGN KEY (artikelId) REFERENCES dbo.Artikel (artikelId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelRoute ADD CONSTRAINT ArtikelRoute_FK2 FOREIGN KEY (routeId) REFERENCES dbo.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToedienVormVerpakking ADD CONSTRAINT ToedienVormVerpakking_FK1 FOREIGN KEY (toedienVormId) REFERENCES dbo.ToedienVorm (toedienVormId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToedienVormVerpakking ADD CONSTRAINT ToedienVormVerpakking_FK2 FOREIGN KEY (verpakkingId) REFERENCES dbo.Verpakking (verpakkingId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToedienVormEenheid ADD CONSTRAINT ToedienVormEenheid_FK1 FOREIGN KEY (toedienVormId) REFERENCES dbo.ToedienVorm (toedienVormId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToedienVormEenheid ADD CONSTRAINT ToedienVormEenheid_FK2 FOREIGN KEY (eenheidId) REFERENCES dbo.Eenheid (eenheidId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToepassingGroep ADD CONSTRAINT ToepassingGroep_FK1 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToepassingGroep ADD CONSTRAINT ToepassingGroep_FK2 FOREIGN KEY (hoofdGroep) REFERENCES dbo.ToepassingGroep (toepassingGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekDosering ADD CONSTRAINT GeneriekDosering_FK1 FOREIGN KEY (generiekId) REFERENCES dbo.Generiek (generiekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekDosering ADD CONSTRAINT GeneriekDosering_FK2 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekDosering ADD CONSTRAINT GeneriekDosering_FK3 FOREIGN KEY (tekstItemId) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DoseringToepassing ADD CONSTRAINT DoseringToepassing_FK1 FOREIGN KEY (generiekDoseringId) REFERENCES dbo.GeneriekDosering (generiekDoseringId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DoseringToepassing ADD CONSTRAINT DoseringToepassing_FK2 FOREIGN KEY (toepassingGroepId) REFERENCES dbo.ToepassingGroep (toepassingGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DoseringArtikel ADD CONSTRAINT DoseringArtikel_FK1 FOREIGN KEY (generiekDoseringId) REFERENCES dbo.GeneriekDosering (generiekDoseringId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DoseringArtikel ADD CONSTRAINT DoseringArtikel_FK2 FOREIGN KEY (artikelId) REFERENCES dbo.Artikel (artikelId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DoseringRoute ADD CONSTRAINT DoseringRoute_FK1 FOREIGN KEY (generiekDoseringId) REFERENCES dbo.GeneriekDosering (generiekDoseringId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.DoseringRoute ADD CONSTRAINT DoseringRoute_FK2 FOREIGN KEY (routeId) REFERENCES dbo.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToedienVormRoute ADD CONSTRAINT ToedienVormRoute_FK1 FOREIGN KEY (toedienVormId) REFERENCES dbo.ToedienVorm (toedienVormId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToedienVormRoute ADD CONSTRAINT ToedienVormRoute_FK2 FOREIGN KEY (routeId) REFERENCES dbo.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumAuteur ADD CONSTRAINT FormulariumAuteur_FK1 FOREIGN KEY (auteur) REFERENCES dbo.Apotheker (apothekerId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumAuteur ADD CONSTRAINT FormulariumAuteur_FK2 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekBijzonderheid ADD CONSTRAINT GeneriekBijzonderheid_FK1 FOREIGN KEY (generiekId) REFERENCES dbo.Generiek (generiekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekBijzonderheid ADD CONSTRAINT GeneriekBijzonderheid_FK2 FOREIGN KEY (tekstItemId) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GeneriekBijzonderheid ADD CONSTRAINT GeneriekBijzonderheid_FK3 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToepassingBijzonderheid ADD CONSTRAINT ToepassingBijzonderheid_FK1 FOREIGN KEY (toepassingGroepId) REFERENCES dbo.ToepassingGroep (toepassingGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToepassingBijzonderheid ADD CONSTRAINT ToepassingBijzonderheid_FK2 FOREIGN KEY (tekstItemId) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ToepassingBijzonderheid ADD CONSTRAINT ToepassingBijzonderheid_FK3 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelBijzonderheid ADD CONSTRAINT ArtikelBijzonderheid_FK1 FOREIGN KEY (artikelId) REFERENCES dbo.Artikel (artikelId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelBijzonderheid ADD CONSTRAINT ArtikelBijzonderheid_FK2 FOREIGN KEY (tekstItemId) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.ArtikelBijzonderheid ADD CONSTRAINT ArtikelBijzonderheid_FK3 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumTekst ADD CONSTRAINT FormulariumTekst_FK1 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.FormulariumTekst ADD CONSTRAINT FormulariumTekst_FK2 FOREIGN KEY (tekstItemId) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TekstLog ADD CONSTRAINT TekstLog_FK1 FOREIGN KEY (tekstItemId) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TekstLog ADD CONSTRAINT TekstLog_FK2 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.TekstLog ADD CONSTRAINT TekstLog_FK3 FOREIGN KEY (gebruikerId) REFERENCES dbo.Gebruiker (gebruikerId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Publicatie ADD CONSTRAINT Publicatie_FK1 FOREIGN KEY (formulariumId) REFERENCES dbo.Formularium (formulariumId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Publicatie ADD CONSTRAINT Publicatie_FK2 FOREIGN KEY (gebruikerId) REFERENCES dbo.Gebruiker (gebruikerId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieHoofdstuk ADD CONSTRAINT PublicatieHoofdstuk_FK1 FOREIGN KEY (publicatieId) REFERENCES dbo.Publicatie (publicatieId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieHoofdstuk ADD CONSTRAINT PublicatieHoofdstuk_FK2 FOREIGN KEY (hoofdstuk) REFERENCES dbo.ToepassingGroep (toepassingGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieHoofdstuk ADD CONSTRAINT PublicatieHoofdstuk_FK3 FOREIGN KEY (inleiding) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieTherapieGroep ADD CONSTRAINT PublicatieTherapieGroep_FK1 FOREIGN KEY (publicatieHoofdstukId) REFERENCES dbo.PublicatieHoofdstuk (publicatieHoofdstukId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieTherapieGroep ADD CONSTRAINT PublicatieTherapieGroep_FK2 FOREIGN KEY (therapieGroep) REFERENCES dbo.ToepassingGroep (toepassingGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieTherapieGroep ADD CONSTRAINT PublicatieTherapieGroep_FK3 FOREIGN KEY (inleiding) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieGeneriek ADD CONSTRAINT PublicatieGeneriek_FK1 FOREIGN KEY (publicatieTherapieGroepId) REFERENCES dbo.PublicatieTherapieGroep (publicatieTherapieGroepId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieGeneriek ADD CONSTRAINT PublicatieGeneriek_FK2 FOREIGN KEY (generiekId) REFERENCES dbo.Generiek (generiekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieGeneriek ADD CONSTRAINT PublicatieGeneriek_FK3 FOREIGN KEY (generiekBijzonderheid) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieRoute ADD CONSTRAINT PublicatieRoute_FK1 FOREIGN KEY (publicatieGeneriekId) REFERENCES dbo.PublicatieGeneriek (publicatieGeneriekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieRoute ADD CONSTRAINT PublicatieRoute_FK2 FOREIGN KEY (routeId) REFERENCES dbo.Route (routeId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieRoute ADD CONSTRAINT PublicatieRoute_FK3 FOREIGN KEY (routeDosering) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieRoute ADD CONSTRAINT PublicatieRoute_FK4 FOREIGN KEY (bijzonderheid) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieArtikel ADD CONSTRAINT PublicatieArtikel_FK1 FOREIGN KEY (publicatieRouteId) REFERENCES dbo.PublicatieRoute (publicatieRouteId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieArtikel ADD CONSTRAINT PublicatieArtikel_FK2 FOREIGN KEY (artikelId) REFERENCES dbo.Artikel (artikelId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieArtikel ADD CONSTRAINT PublicatieArtikel_FK3 FOREIGN KEY (artikelDosering) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieArtikel ADD CONSTRAINT PublicatieArtikel_FK4 FOREIGN KEY (bijzonderheid) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieFormulariumTekst ADD CONSTRAINT PublicatieFormulariumTekst_FK1 FOREIGN KEY (publicatieId) REFERENCES dbo.Publicatie (publicatieId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.PublicatieFormulariumTekst ADD CONSTRAINT PublicatieFormulariumTekst_FK2 FOREIGN KEY (formulariumTekst) REFERENCES dbo.TekstItem (tekstItemId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GebruikerRol ADD CONSTRAINT GebruikerRol_FK1 FOREIGN KEY (gebruikerId) REFERENCES dbo.Gebruiker (gebruikerId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.GebruikerRol ADD CONSTRAINT GebruikerRol_FK2 FOREIGN KEY (rolId) REFERENCES dbo.Rol (rolId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Apotheker ADD CONSTRAINT Apotheker_FK1 FOREIGN KEY (gebruikerId) REFERENCES dbo.Gebruiker (gebruikerId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


ALTER TABLE dbo.Apotheker ADD CONSTRAINT Apotheker_FK2 FOREIGN KEY (apotheekId) REFERENCES dbo.Apotheek (apotheekId) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


GO