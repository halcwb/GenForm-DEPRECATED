using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{

    public interface IProduct
    {

        Int32 ProductId { get;  }
        String ProductName { get;  }
        String ProductCode { get;  }
        String GenericName { get;  }
        String BrandName { get;  }
        String ShapeName { get;  }
        Decimal Quantity { get;  }
        String UnitName { get;  }
        String PackageName { get;  }
        String DisplayName { get;  }
        IEnumerable<IProductSubstance> Substances { get; }
        IProductSubstance AddSubstance();
    }
}
