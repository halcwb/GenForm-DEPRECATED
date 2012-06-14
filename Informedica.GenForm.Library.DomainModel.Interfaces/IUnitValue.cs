namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface IUnitValue
    {
        IUnit Unit { get; }
        decimal Value { get; }
    }
}
