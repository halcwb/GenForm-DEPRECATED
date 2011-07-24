using System;
using Informedica.GenForm.Library.Services;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Substance : ISubstance
    {
        private String _substanceName;

        public String SubstanceName
        {
            get { return _substanceName; }
            set { _substanceName = value; }
        }
    }
}