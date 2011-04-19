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

var filter = new {Group = "ANTIDOTA",
				  Generic = "acetylcysteine",
				  Route = "inhalatie",
				  Product = ""};

var DoseTexts = from s in PreparaatStofs
				from t in s.Preparaat.PreparaatDoseringsadvies
				from g in s.Preparaat.PreparaatToepassingsgroeps
				from r in s.Preparaat.PreparaatToedienings
				//where g.toepassingsgroepName.StartsWith(filter.Group)
				//where s.stofName.StartsWith(filter.Generic)
				//where r.toedieningswijze.StartsWith(filter.Route)
				where t.Doseringsadvies.adviestekst != ""
				group s by new {s.stofName, t.Doseringsadvies.adviestekst} into st
				select new {Generic = st.Key.stofName,
							DoseText = st.Key.adviestekst.Replace("<br", "").
														  Replace("/>", "").
														  Replace("</ul", "").
														  Replace("</li", "").
														  Replace(">", "").
														  Replace("<li", "").
														  Replace("<ul", "").
														  Replace("&gt;", ">").
														  Replace("&lt;", "<").
														  Trim(),
							Products = from p in st
							            select p.preparaatName,
										
							Groups = (from g in DoseringsadviesToepassingsgroeps
									 where g.Doseringsadvies.adviestekst == st.Key.adviestekst
									 select g.toepassingsgroepName).Distinct(),
							
							Routes = (from r in DoseringsadviesToedieningswijzes
									  where r.Doseringsadvies.adviestekst == st.Key.adviestekst
									  select r.toedieningswijze).Distinct()
							};

DoseTexts.Where(o => o.Generic == filter.Generic).Count().Dump();
DoseTexts.Where(o => o.Generic == filter.Generic).Where(o => o.Groups.Contains(filter.Group)).Count().Dump();
DoseTexts.Where(o => o.Generic == filter.Generic).Where(o => o.Groups.Contains(filter.Group)).Where(o => o.Routes.Contains(filter.Route)).Count().Dump();
DoseTexts.Where(o => o.Generic == filter.Generic).Where(o => o.Groups.Contains(filter.Group)).Where(o => o.Routes.Contains(filter.Route)).Where(o => o.Products.Contains(filter.Product)).Count().Dump();

//DoseTexts.Where(o => o.Generic == filter.Generic).Dump();
//DoseTexts.Where(o => o.Generic == filter.Generic).Where(o => o.Groups.Contains(filter.Group)).Dump();
//DoseTexts.Where(o => o.Generic == filter.Generic).Where(o => o.Groups.Contains(filter.Group)).Where(o => o.Routes.Contains(filter.Route)).Dump();
//DoseTexts.Where(o => o.Generic == filter.Generic).Where(o => o.Groups.Contains(filter.Group)).Where(o => o.Routes.Contains(filter.Route)).Where(o => o.Products.Contains(filter.Product)).Dump();


//DoseTexts.Dump();				
			
