using Informedica.GenForm.Library.DomainModel.Identification;

namespace Informedica.GenForm.Library.DomainModel
{
    public interface IDomainModel<T,TC>: IIdentifiable<TC>, IEquals<T>
    {
    }
}
