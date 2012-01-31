namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class SubstanceGroupDto: DataTransferObject<SubstanceGroupDto>
    {

        public override SubstanceGroupDto CloneDto()
        {
            return CreateClone();
        }

    }
}