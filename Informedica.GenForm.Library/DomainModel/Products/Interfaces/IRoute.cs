using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IRoute
    {
        Guid Id { get; }
        string Name { get; }
        String Abbreviation { get; set; }
        void AddShape(IShape shape);
        void RemoveShape(IShape shape);
        void AddProduct(IProduct product);
        void RemoveProduct(IProduct product);
    }
}