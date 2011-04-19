<Query Kind="Statements">
  <Connection>
    <ID>74a56cb1-17fd-497b-b8f3-fbbd5fcd050d</ID>
    <Persist>true</Persist>
    <Server>HAL-2008</Server>
    <Database>FormulariumArthur</Database>
    <ShowServer>true</ShowServer>
    <NoCapitalization>true</NoCapitalization>
  </Connection>
</Query>

var dosering = from d in Doseringsadvies
               group d by d.adviestekst into g
			   select g;
			   
var prepadv = (from p in PreparaatDoseringsadvies
			  from s in PreparaatStofs
			  from r in PreparaatToedienings
			  from g in PreparaatToepassingsgroeps
		      from d in dosering
			  where p.Doseringsadvies.adviestekst == d.Key
			  where s.preparaatName == p.preparaatName
			  where r.preparaatName == p.preparaatName
			  where g.preparaatName == p.preparaatName
			  select new { DoseText = d.Key.Replace("<br", "").
			  								Replace("/>", "").
											Replace("</ul", "").
											Replace("</li", "").
											Replace(">", "").
											Replace("<li", "").
											Replace("<ul", "").
											Replace("&gt;", ">").
											Replace("&lt;", "<").
											Trim(), 
			               Group = g.toepassingsgroepName, 
						   Generic = s.stofName, 
						   Route = r.toedieningswijze, 
						   Product = p.preparaatName }).Distinct();
			  

(from i in prepadv where i.Product.StartsWith("para") select i).Dump();

