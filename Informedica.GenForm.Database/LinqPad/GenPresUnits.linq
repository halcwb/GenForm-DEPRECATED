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
	Shape = "",
	ProductUnit = ""
	
};

var Data1 = from e in Eenheids
			from eg in e.EenheidGroep.Eenheids
			where e.naam.StartsWith(_Filter.Generic)
		   	select new
		   	{
			   	DoseUnit = eg.naam,
			   	Multiplier = eg.factor,
				Groep = e.EenheidGroep.naam,
				AllowsConversion = e.EenheidGroep.conversieToestaan
		   	};

var Data2 =	from e in Eenheids
			from eg in e.EenheidGroep.Eenheids
			where e.naam == _Filter.ProductUnit
		   	select new
		   	{
			   	AdministerUnit = eg.naam,
			   	Multiplier = eg.factor,
				Groep = e.EenheidGroep.naam,
				AllowsConversion = e.EenheidGroep.conversieToestaan
		   	};			
			
Data1.Dump();
Data2.Dump();