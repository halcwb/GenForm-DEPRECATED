using Informedica.GenForm.Library.DomainModel;
using FluentNHibernate.Mapping;

namespace Informedica.GenForm.DataAccess.Mappings
{
    public abstract class EntityMap<TEnt, TId, TDto> : ClassMap<TEnt>
        where TEnt: Entity<TId, TDto> 
        where TDto : DataTransferObject<TDto, TId>
    {
        protected EntityMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name).Not.Nullable().Unique().Length(255);
            Version(x => x.Version);
        } 
    }
}
