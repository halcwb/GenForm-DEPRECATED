namespace Informedica.GenForm.Library.DomainModel.Validation
{
    public delegate bool ValidationRule<in T>(T dto) where T : DataTransferObject<T>;
}