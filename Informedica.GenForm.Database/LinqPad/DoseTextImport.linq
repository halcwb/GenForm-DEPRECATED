<Query Kind="Statements">
  <Reference>D:\Data\VisualStudio\genform\GenForm\Informedica.GenForm.Database\bin\Debug\Csla.dll</Reference>
  <Reference>D:\Data\VisualStudio\genform\GenForm\Informedica.GenForm.Database\bin\Debug\Informedica.GenForm.Database.ORM.dll</Reference>
  <Reference>D:\Data\VisualStudio\genform\GenForm\Informedica.GenForm.Library\bin\Debug\Informedica.GenForm.Library.dll</Reference>
  <Namespace>Informedica.GenForm.Database.ORM.LINQtoSQL</Namespace>
  <Namespace>Csla.Data</Namespace>
  <Namespace>Informedica.GenForm.Library.DomainModel.Products</Namespace>
  <Namespace>Informedica.GenForm.Library.DomainModel.Formularia</Namespace>
</Query>

var formArthur = ContextManager<FormulariumImportDataContext>.GetManager(
                 "Data Source=HAL-2008;Initial Catalog=FormulariumArthur;Integrated Security=True", false);

var formularia = ContextManager<FormulariaDataContext>.GetManager(
				 "Data Source=HAL-2008;Initial Catalog=Formularium2010;Integrated Security=True", false);
				 
var products = new ProductsDataContext("Data Source=HAL-2008;Initial Catalog=Formularium2010;Integrated Security=True");

int formulariumId = formularia.DataContext.Formulariums.First ().formulariumId;

var DoseTexts = from s in formArthur.DataContext.PreparaatStof
				from t in s.Preparaat.PreparaatDoseringsadvies
				from g in s.Preparaat.PreparaatToepassingsgroep
				from r in s.Preparaat.PreparaatToediening
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
														  Replace("\n\n", "\n").
														  Trim(),
							Products = (from p in st
							            select p.preparaatName).Distinct(),
										
							Groups = (from g in formArthur.DataContext.DoseringsadviesToepassingsgroep
									 where g.Doseringsadvies.adviestekst == st.Key.adviestekst
									 select g.toepassingsgroepName).Distinct(),
							
							Routes = (from r in formArthur.DataContext.DoseringsadviesToedieningswijze
									  where r.Doseringsadvies.adviestekst == st.Key.adviestekst
									  select r.toedieningswijze).Distinct()
							};
		
			
GeneriekDosering generiekdosering = null;
DoseringToepassing toepassing = null;
DoseringRoute route = null;
DoseringArtikel artikel = null;
foreach (var item in DoseTexts.Where (o =>o.Generic.Contains("")))
{
	item.Generic.Dump();
	item.DoseText.Dump();
	// get the generic id 
	int genericid = (from g in products.Generieks
			                    where g.naam == item.Generic
								select g.generiekId).SingleOrDefault();
	// Check if item does not already exist in database
	if ((from i in formularia.DataContext.GeneriekDoserings
	     where i.TekstItem.tekst == item.DoseText &&
		       i.generiekId == genericid
		select i).Count() > 0)
		continue;
	
	if (products.Generieks.Where (g => g.naam == item.Generic).Count() > 0)
	{
		generiekdosering = new GeneriekDosering();
		generiekdosering.TekstItem = new TekstItem();
		generiekdosering.TekstItem.tekst = item.DoseText;
		generiekdosering.TekstItem.tekstType = DoseText.TEXT_TYPE;
		generiekdosering.TekstItem.tekstStatus = Enum.GetName(typeof(TextItem.TextStatus), TextItem.TextStatus.New);
		generiekdosering.formulariumId = formulariumId;
		generiekdosering.generiekId = (from g in products.Generieks
									   where g.naam == item.Generic
									   select g.generiekId).Single();
		
		foreach (var gr in item.Groups)
		{
			gr.Dump();
			toepassing = new DoseringToepassing();
			
			// Get or create toepassingsgroup
			ToepassingGroep groep = (from g in formularia.DataContext.ToepassingGroeps
			                         where g.naam == gr
									 select g).SingleOrDefault();
			if (groep == null) groep = new ToepassingGroep();
			toepassing.ToepassingGroep = groep;
			toepassing.ToepassingGroep.naam = gr;
			toepassing.ToepassingGroep.formulariumId = formulariumId;
			generiekdosering.DoseringToepassings.Add(toepassing);
		}
		
		foreach (var dr in item.Routes)
		{
			dr.Dump();
			route = new DoseringRoute();
			route.routeId = (from r in products.Routes
			                 where r.naam.ToLower().Trim() == dr.ToLower().Trim()
							 select r.routeId).Single();
			generiekdosering.DoseringRoutes.Add(route);
		}
		
		foreach (var pr in item.Products)
		{
			if (products.Artikels.Where(a => a.naam == pr).Count() > 0)
			{
				pr.Dump();
				artikel = new DoseringArtikel();
				artikel.artikelId = (from a in products.Artikels
									where a.naam == pr
									select a.artikelId).Single();
				generiekdosering.DoseringArtikels.Add(artikel);
			}
		}
		
	formularia.DataContext.GeneriekDoserings.InsertOnSubmit(generiekdosering);
	formularia.DataContext.SubmitChanges();
	}
	
	"".Dump();
}