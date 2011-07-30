using System;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Repositories;
using Informedica.GenForm.Library.Services.Products.dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Informedica.GenForm.DataAccess.Tests.TestBase
{
    public abstract class DataMapperTestBase<TMapper, TBo, TDao> where TMapper : IDataMapper<TBo, TDao>
    {
        protected TDao Dao;
        protected TBo Bo;
        protected TMapper Mapper;

        protected abstract Boolean IsMapped(TBo bo, TDao dao);

        protected DataMapperTestBase()
        {
            GenFormApplication.Initialize();
            SetUpMapper();
        }

        protected void SetUpMapper()
        {
            Dao = GetDao();
            Bo = GetBo();
            Mapper = GetMapper();
        }

        private static TMapper GetMapper()
        {
            return ObjectFactory.GetInstance<TMapper>();
        }

        private static TBo GetBo()
        {
            return ObjectFactory.GetInstance<TBo>();
        }

        protected static TBo GetBoWithDto(ProductDto dto)
        {
            return ObjectFactory.With(dto).GetInstance<TBo>();
        }

        private static TDao GetDao()
        {
            return ObjectFactory.GetInstance<TDao>();
        }

        protected void AssertIsMapped()
        {
            Assert.IsTrue(IsMapped(Bo, Dao));
        }
    }
}
