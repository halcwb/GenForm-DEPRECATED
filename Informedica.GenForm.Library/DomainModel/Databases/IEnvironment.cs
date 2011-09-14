using System;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public interface IEnvironment
    {
        String Name { get; set; }
        String ConnectionString { get; set; }
    }
}