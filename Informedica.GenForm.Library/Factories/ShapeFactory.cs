using System.Collections.Generic;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class ShapeFactory : EntityFactory<Shape, ShapeDto>
    {
        public ShapeFactory(ShapeDto dto) : base(dto) {}

        protected override Shape Create()
        {
            var shape = Shape.Create(GetShapeDto());
            shape = AddPackages(shape);
            shape = AddRoutes(shape);
            shape = AddUnits(shape);

            Add(shape);
            return shape;
        }

        private Shape AddUnits(Shape shape)
        {
            foreach (var unitGroup in GetUnitGroups())
            {
                shape.AddUnitGroup(unitGroup);
            }
            return shape;
        }

        private IEnumerable<UnitGroup> GetUnitGroups()
        {
            var list = new HashSet<UnitGroup>();
            foreach (var dto in Dto.UnitGroups)
            {
                list.Add(GetUnitGroup(dto));
            }
            return list;
        }

        private UnitGroup GetUnitGroup(UnitGroupDto dto)
        {
            return new UnitGroupFactory(dto).Get();
        }

        private Shape AddRoutes(Shape shape)
        {
            foreach (var route in GetRoutes())
            {
                shape.AddRoute(route);
            }
            return shape;
        }

        private IEnumerable<Route> GetRoutes()
        {
            var list = new HashSet<Route>();
            foreach (var route in Dto.Routes)
            {
                list.Add(GetRoute(route));
            }
            return list;
        }

        private static Route GetRoute(RouteDto route)
        {
            return new RouteFactory(route).Get();
        }

        private Shape AddPackages(Shape shape)
        {
            foreach (var package in GetPackages())
            {
                shape.AddPackage(package);
            }
            return shape;
        }

        private ShapeDto GetShapeDto()
        {
            return new ShapeDto {Name = Dto.Name};
        }

        private IEnumerable<Package> GetPackages()
        {
            var list = new HashSet<Package>();
            foreach (var packageDto in Dto.Packages)
            {
                list.Add(GetPackage(packageDto));
            }
            return list;
        }

        private Package GetPackage(PackageDto packageDto)
        {
            return new PackageFactory(packageDto).Get();
        }
    }
}