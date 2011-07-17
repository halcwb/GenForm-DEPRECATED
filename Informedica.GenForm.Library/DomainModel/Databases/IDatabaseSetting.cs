using System;

namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public interface IDatabaseSetting
    {
        String Name { get; set; }
        String ConnectionString { get; set; }
        String Machine { get; set; }
    }
}