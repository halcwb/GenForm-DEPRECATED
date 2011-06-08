using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public interface IUnit
    {
        Int32 UnitId { get; set; }
        String UnitName { get; set; }
    }
}
