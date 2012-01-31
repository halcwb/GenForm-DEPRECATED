namespace Informedica.GenForm.Library.DomainModel.Data
{
    public class UserDto : DataTransferObject<UserDto>
    {
        public string Password;
        public string LastName;
        public string FirstName;
        public string Email;
        public string Pager;

        public override UserDto CloneDto()
        {
            return CreateClone();
        }
    }
}