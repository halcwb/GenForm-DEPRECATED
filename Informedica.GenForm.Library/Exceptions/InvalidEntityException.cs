using System;
using System.Collections.Generic;
using System.Text;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Exceptions
{
    public class InvalidEntityException <TEnt> : Exception
        where TEnt : Entity<TEnt>
    {
        public InvalidEntityException(Entity<TEnt> entity, IEnumerable<String>broken) : base(CreateMessage(entity, broken))
        {
        }

        private static string CreateMessage(Entity<TEnt> entity, IEnumerable<string> broken)
        {
            var message = new StringBuilder();
            message.AppendFormat("The following rules are broken for {0}:", entity.GetType().Name).AppendLine();
            foreach (var rule in broken)
            {
                message.AppendLine(rule);
            }
            return message.ToString();
        }
    }
}