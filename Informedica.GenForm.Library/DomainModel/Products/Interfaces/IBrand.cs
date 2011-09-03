using System;
using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IBrand
    {
        Guid Id { get; }
        String Name { get; }
        IEnumerable<IProduct> Products { get; }
    }
}