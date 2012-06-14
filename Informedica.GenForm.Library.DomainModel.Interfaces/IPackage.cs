using System;
using System.Collections.Generic;

namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface IPackage
    {
        Guid Id { get; }
        String Name { get; }
        String Abbreviation { get; }
        IEnumerable<IProduct> Products { get; }
    }
}
