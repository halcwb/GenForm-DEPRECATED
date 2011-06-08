using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IGeneric
    {
        Int32 GenericId { get; set; }
        String GenericName { get; set; }
    }
}
