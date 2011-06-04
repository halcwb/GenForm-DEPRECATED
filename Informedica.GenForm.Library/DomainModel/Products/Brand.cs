using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Brand : IBrand
    {
        public Int32 BrandId { get; set; }
        public String BrandName { get; set; }
    }
}
