using System.Collections.Generic;

namespace Informedica.GenForm.Library.DomainModel.Relations
{
    public class OneToOneRelation<TOnePart1, TOnePart2> : IRelation<IRelationPart, IRelationPart>
        where TOnePart1 : IRelationPart
        where TOnePart2 : IRelationPart
    {
        private IDictionary<TOnePart1, TOnePart2>       _oneToOne1;
        private IDictionary<TOnePart2, TOnePart1>       _oneToOne2;

    }
}