namespace Informedica.GenForm.Library.DomainModel.Validation
{
    public delegate bool ValidationRule<in TEnt>(TEnt entity) where TEnt : Entity<TEnt>;
}