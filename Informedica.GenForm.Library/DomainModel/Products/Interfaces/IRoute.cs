using System;

namespace Informedica.GenForm.Library.DomainModel.Products.Interfaces
{
    public interface IRoute
    {
        void AddShape(Shape shape);
        String Abbreviation { get; set; }
        Guid Id { get; }
        string Name { get; }
        void RemoveShape(Shape shape);
        void AddProduct(Product product);
        void RemoveProduct(Product product);
    }
}