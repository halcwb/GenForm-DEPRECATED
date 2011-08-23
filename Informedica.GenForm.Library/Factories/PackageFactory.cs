using System;
using Informedica.GenForm.Library.DomainModel.Data;
using Informedica.GenForm.Library.DomainModel.Products;

namespace Informedica.GenForm.Library.Factories
{
    public class PackageFactory : EntityFactory<Package, Guid, PackageDto>
    {
        public PackageFactory(PackageDto dto) : base(dto) {}

        protected override Package Create()
        {
            var package = Package.Create(GetPackageDto());

            AddShapes(package);

            Add(package);
            return package;
        }

        private void AddShapes(Package package)
        {
            foreach (var dto in Dto.Shapes)
            {
                package.AddShape(GetShape(dto));
            }
        }

        private Shape GetShape(ShapeDto dto)
        {
            return new ShapeFactory(dto).Get();
        }

        private PackageDto GetPackageDto()
        {
            return new PackageDto {Abbreviation = Dto.Abbreviation, Name = Dto.Name};
        }
    }
}