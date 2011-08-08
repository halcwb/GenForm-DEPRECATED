using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IBrand
    {
        Guid Id { get; }
        String Name { get; }
    }
}