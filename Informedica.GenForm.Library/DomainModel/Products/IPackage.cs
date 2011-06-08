using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IPackage
    {
        Int32 PackageId { get; set; }
        String PackageName { get; set; }
    }
}
