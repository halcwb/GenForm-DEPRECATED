namespace Informedica.GenForm.Acceptance
{
    public class ValidUserNames
    {

        public string UserName { get; set; }
        public string ExistingUserName { get; set; }

        public bool IsValid()
        {
            return true;
        }

    }
}