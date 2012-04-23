namespace Informedica.GenForm.Acceptance.EnvironmentManagement
{
    public class GenFormEnvironmentValidityCheck: GenFormEnvironmentFixture
    {
        public string IsValid()
        {
            return Environment == null ? "No" : "Yes";
        }
    }
}
