using System;

namespace Informedica.GenForm.Acceptance
{
    public class HelloFitNesse
    {
        public String TestFitNesse()
        {
            try
            {
                TestSqlLiteFactoryCreation();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "Hello FitNesse";
        }

        private static string TestSqlLiteFactoryCreation()
        {
            Assembler.GenFormApplication.Initialize();
            var testSessionFactory = Assembler.GenFormApplication.TestSessionFactory;

            var sess = testSessionFactory.OpenSession();
            var name = sess.Connection.Database;
            sess.Close();

            return name;
        }

        public String TestGetSqLiteDatabaseName()
        {
            string value;
            try
            {
                value = TestSqlLiteFactoryCreation();
            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return value;
        }
    }
}
