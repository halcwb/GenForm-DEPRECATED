using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IPackage
    {
        Guid Id { get; }
        String Name { get; }
        String Abbreviation { get; }
    }
}
