using FluentNHibernate.Mapping;
using Informedica.GenForm.Library.DomainModel.Databases;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public sealed class EmptyDatabaseMap: ClassMap<EmptyDatabase>
    {
        public EmptyDatabaseMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.IsEmpty).Not.Nullable();
        }
    }
}
