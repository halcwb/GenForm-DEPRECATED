using System;

namespace Informedica.GenForm.Acceptance.Utilities
{
    public class TryCatchTestMethod
    {
        public virtual bool TryCatch(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
