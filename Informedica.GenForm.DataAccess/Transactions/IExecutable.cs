using Informedica.GenForm.Database;

namespace Informedica.GenForm.DataAccess.Transactions
{
    public interface IExecutable
    {
        void Execute(GenFormDataContext context);
    }
}