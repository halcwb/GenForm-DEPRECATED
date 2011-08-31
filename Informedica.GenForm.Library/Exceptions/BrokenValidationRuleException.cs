using System;

namespace Informedica.GenForm.Library.Exceptions
{
    public class BrokenValidationRuleException : Exception
    {
        public BrokenValidationRuleException(string brokenRule) : base(brokenRule)
        {
            
        }
    }
}