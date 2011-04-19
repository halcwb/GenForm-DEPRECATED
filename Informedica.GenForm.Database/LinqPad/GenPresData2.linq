<Query Kind="Statements">
  <Connection>
    <ID>74a56cb1-17fd-497b-b8f3-fbbd5fcd050d</ID>
    <Persist>true</Persist>
    <Server>HAL-2008</Server>
    <Database>Formularium2010</Database>
    <ShowServer>true</ShowServer>
    <NoCapitalization>true</NoCapitalization>
  </Connection>
</Query>

var _Filter = new 
{
	Generic = "",
	GenericUnit = "",
	GenericUnitGroup = "",
	Shape = "",
	ProductUnit = "",
	ProductUnitGroup = "Volume",
	Route = ""
};


var Data = from p in Artikels
		   from pg in p.ArtikelGenerieks
		   from r in p.ArtikelRoutes
		   where pg.Generiek.naam.StartsWith(_Filter.Generic)
		   where pg.Eenheid.naam.StartsWith(_Filter.GenericUnit)
		   where pg.Eenheid.EenheidGroep.naam.StartsWith(_Filter.GenericUnitGroup)
		   where p.ToedienVorm.naam.StartsWith(_Filter.Shape)
		   where p.Eenheid.naam.StartsWith(_Filter.ProductUnit)
		   where p.Eenheid.EenheidGroep.naam.StartsWith(_Filter.ProductUnitGroup)
		   where r.Route.naam.StartsWith(_Filter.Route)
		   select new
		   {
			   ComponentName = p.naam.Trim(),
			   Substance = pg.Generiek.naam.Trim(),
			   SubstanceQuantity = pg.hoeveelheid,
			   SubstanceUnit = pg.Eenheid.naam.Trim(),
			   SubstanceUnitGroup = pg.Eenheid.EenheidGroep.naam.Trim(),
			   Shape = p.ToedienVorm.naam.Trim(),
			   ComponentKAE = p.KAE,
			   ComponentUnit = p.Eenheid.naam.Trim(),
			   ComponentUnitGroup = p.Eenheid.EenheidGroep.naam.Trim(),
			   SubstanceConcentration = pg.sterkte,
			   Route = r.Route.naam.Trim()
		   };
		   
Data.Dump();