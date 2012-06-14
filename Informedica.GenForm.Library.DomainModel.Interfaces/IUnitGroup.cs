using System;
using System.Collections.Generic;

namespace Informedica.GenForm.DomainModel.Interfaces
{
    public interface IUnitGroup
    {
        Guid Id { get; }
        String Name { get; }
        Boolean AllowsConversion { get; }
        IEnumerable<IUnit> Units { get; }
        IEnumerable<IShape> Shapes { get; }
        void AddUnit(IUnit unit);
        void AddShape(IShape shape);
        void RemoveShape(IShape shape);
    }
}