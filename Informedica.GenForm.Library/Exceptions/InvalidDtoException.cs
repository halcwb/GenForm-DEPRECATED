using System;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.Library.Exceptions
{
    public class InvalidDtoException<TDto> : Exception
    {
        public InvalidDtoException(DataTransferObject<TDto> dto) : base(dto.ToString())
        {

        }
    }
}