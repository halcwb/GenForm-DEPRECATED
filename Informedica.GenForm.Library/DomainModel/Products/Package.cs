using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Package: IPackage
    {
        #region Implementation of IPackage

        private int _packageId;

        private string _packageName;

        public int PackageId
        {
            get { return _packageId; }
            set { _packageId = value; }
        }

        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }

        #endregion
    }
}
