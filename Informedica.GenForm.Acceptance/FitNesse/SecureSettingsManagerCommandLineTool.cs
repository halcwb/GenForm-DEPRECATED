using Informedica.GenForm.Acceptance.SecureSettingsManager;
using Informedica.GenForm.Acceptance.Utilities;

namespace Informedica.GenForm.Acceptance.FitNesse
{
    public class SecureSettingsManagerCommandLineTool: TryCatchTestMethod
    {
        public static CommandLineToolForScsmShould Should = new CommandLineToolForScsmShould();

        public bool CheckCommandLine()
        {
            return TryCatch((Should.BeAbleToRun));
        }

        public bool CanEnterScsm()
        {
            return TryCatch(Should.BeAbleToRun);
        }

        public bool ReturnsOptionList()
        {
            return TryCatch(Should.ReturnOptionList);
        }

        public bool WhenUserEntersSecretKey(string key)
        {
            return TryCatch(Should.BeAbleToSetASecureKey);
        }

        public bool ThenHasSecretKeyReturnsTrue(string key)
        {
            return TryCatch(Should.ReturnTrueWhenSecretIsSecret);
        }

    }
}
