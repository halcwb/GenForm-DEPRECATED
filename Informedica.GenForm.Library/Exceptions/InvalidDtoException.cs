using System;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Exceptions
{
    public class InvalidDtoException<TDto, TId> : Exception
    {
        public InvalidDtoException(DataTransferObject<TDto, TId> dto) : base(dto.ToString())
        {

        }
    }
}