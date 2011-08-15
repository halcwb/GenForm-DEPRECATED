using System;
using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public static class UnitValueMap
    {
        public static Action<ComponentPart<UnitValue>> GetMap()
        {
            return unitValue =>
                       {
                           unitValue.Map(x => x.Value).Not.Nullable();
                           unitValue.References(x => x.Unit)
                               .Cascade.SaveUpdate()
                               .Not.Nullable();
                       };
        }
    }
}