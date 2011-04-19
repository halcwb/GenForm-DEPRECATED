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

var Filter = new 
{
	Generic = "para",
	GenericUnit = "",
	Shape = "",
	ProductUnit = "",
	Route = ""
};

var Data = 	from p in Artikels
			from pg in p.ArtikelGenerieks
			from r in p.ArtikelRoutes
			where pg.Generiek.naam.StartsWith(Filter.Generic)
			where pg.Eenheid.naam.StartsWith(Filter.GenericUnit)
			where p.ToedienVorm.naam.StartsWith(Filter.Shape)
			where p.Eenheid.naam.StartsWith(Filter.ProductUnit)
			where r.Route.naam.StartsWith(Filter.Route)
			select new 
			{
				ComponentName = p.naam,
				Substance = pg.Generiek.naam,
				SubstanceQuantity = pg.hoeveelheid,
				SubstanceQuantityUnit = pg.Eenheid.naam,
				Shape = p.ToedienVorm.naam, 
				ComponentKAE = p.KAE,
				ComponentQuantityUnit = p.Eenheid.naam,
				SubstanceComponentConcentration = pg.sterkte,
				Route = r.Route.naam
			};
			
var SubstanceKAEs = from k in Data
                    select k.SubstanceComponentConcentration * k.ComponentKAE;
					
var ComponentKAEs = from c in Data
				    select c.ComponentKAE;
					
ComponentKAEs.Dump();
					
SubstanceKAEs.Dump();
			
var GenPresData = 	from d in "R"
					select new
					
			{
				Components = 	(from c in Data
								select c.ComponentName).Distinct(),
			
				Substances =	(from s in Data
								select s.Substance).Distinct(),
								
				SubstanceQuantitieKAEs = 	(from sq in Data
											select ((Single)(sq.SubstanceComponentConcentration * 
											sq.ComponentKAE)).ToString()).Distinct(),
										
				SubstanceQuantityUnits = 	(from squ in Data
											select squ.SubstanceQuantityUnit).Distinct(),
											
				Shapes = 	(from s in Data
							select s.Shape).Distinct(),
						
				ComponentKAEs = 	(from cq in Data
									select ((Single)cq.ComponentKAE).ToString()).Distinct(),
										
				ComponentQuantityUnits = 	(from cqu in Data
											select cqu.ComponentQuantityUnit).Distinct(),
											
				Routes = (from r in Data
				          select r.Route).Distinct()
			};
			
GenPresData.Dump();