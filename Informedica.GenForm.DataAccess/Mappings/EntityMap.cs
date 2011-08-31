using Informedica.GenForm.Library.DomainModel;
using FluentNHibernate.Mapping;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public abstract class EntityMap<TEnt> : ClassMap<TEnt>
        where TEnt: Entity<TEnt> 
    {
        private readonly int _nameLength;

        protected EntityMap()
        {
            _nameLength = Entity<TEnt>.NameLength;
            Map();
        } 

        protected EntityMap(int nameLength)
        {
            _nameLength = nameLength;
            Map();
        }

        private void Map()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable().Unique().Length(_nameLength);
            Version(x => x.Version);
            SelectBeforeUpdate();
        }
    }
}