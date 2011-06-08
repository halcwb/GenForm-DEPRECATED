using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Unit: IUnit
    {
        #region Implementation of IUnit

        private int _unitId;

        private string _unitName;

        public int UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }

        public string UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }

        #endregion
    }
}
