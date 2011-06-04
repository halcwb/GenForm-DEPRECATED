using System;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IBrand
    {
        Int32 BrandId { get; set; }
        String BrandName { get; set; }
    }
}