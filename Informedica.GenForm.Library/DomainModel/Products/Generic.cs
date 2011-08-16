namespace Informedica.GenForm.Library.DomainModel.Products
{
    public class Generic : IGeneric
    {
        #region Implementation of IGeneric

        private int _genericId;

        private string _genericName;

        public int GenericId
        {
            get { return _genericId; }
            set { _genericId = value; }
        }

        public string GenericName
        {
            get { return _genericName; }
            set { _genericName = value; }
        }

        #endregion
    }
}
