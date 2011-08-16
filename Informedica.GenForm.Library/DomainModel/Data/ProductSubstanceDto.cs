namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class ProductSubstanceDto
    {
        public int Id;
        public int SortOrder;
        public string Substance;
        public decimal Quantity;
        public string UnitName;
        public string UnitAbbreviation;
        public bool UnitGroupAllowConversion;
        public string UnitGroupName;
        public decimal UnitMultiplier;
        public decimal UnitDivisor;
        public bool UnitIsReference;
    }
}