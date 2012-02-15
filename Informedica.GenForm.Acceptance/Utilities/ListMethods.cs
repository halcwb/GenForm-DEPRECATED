using System;

namespace Informedica.GenForm.Acceptance.Utilities
{
    public class ListMethods
    {
        public bool IsListWithDelimiterEmpty(string list, string delimiter)
        {
            return string.IsNullOrWhiteSpace(list) || list.Split(Convert.ToChar(delimiter)).GetLength(0) == 0;
        }
    }
}
