using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : ISubstance
    {
        private String _substanceName;
        private Int32 _Id;

        public int SubstanceId
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public String SubstanceName
        {
            get { return _substanceName; }
            set { _substanceName = value; }
        }
    }
}