using System.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Informedica.GenForm.Mvc3.App_Start.StructuremapMvc), "Start")]

namespace Informedica.GenForm.Mvc3.App_Start {

    public static class StructuremapMvc {
        public static void Start() {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}