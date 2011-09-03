using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IPackage
    {
        Guid Id { get; }
        String Name { get; }
        String Abbreviation { get; }
        IEnumerable<IProduct> Products { get; }
    }
}
