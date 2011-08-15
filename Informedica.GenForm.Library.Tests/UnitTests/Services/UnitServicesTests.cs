using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.Assembler.Contexts;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Repositories;
using Informedica.GenForm.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.Services
{
    /// <summary>
    /// Summary description for UnitServicesTests
    /// </summary>
    [TestClass]
    public class UnitServicesTests
    {
        private TestContext testContextInstance;
        private SessionContext _context;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            GenFormApplication.Initialize();
            DatabaseCleaner.CleanDataBase();
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _context = new SessionContext();
            _context.CurrentSession().Transaction.Begin();            
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _context.CurrentSession().Transaction.Rollback();
            _context.Dispose();
            _context = null;
        }
        
        #endregion

        [TestMethod]
        public void ThatServicesCanCreateNewUnitWithNewUnitGroup()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).GetUnit();
            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void ThatServicesCanCreateNewUnitAndAddUnitToGroup()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).AddToGroup(GetGroupDto()).GetUnit();
            Assert.IsNotNull(unit);
        }

        [TestMethod]
        public void ThatServicesGetsTheUnitFromTheRepositoryOnceItsAdded()
        {
            var unit = UnitServices.WithDto(GetUnitDto()).AddToGroup(GetGroupDto()).GetUnit();
            Assert.AreEqual(unit, UnitServices.GetUnit(unit.Id));
        }

        [TestMethod]
        public void ThatServicesReturnsSameUnitWhenAskedTwiceForSameUnit()
        {
            var unit1 = UnitServices.WithDto(GetUnitDto()).GetUnit();
            var unit2 = UnitServices.WithDto(GetUnitDto()).GetUnit();

            Assert.AreEqual(unit1, unit2);
        }

        private UnitGroupDto GetGroupDto()
        {
            return new UnitGroupDto
                       {
                           AllowConversion = true,
                           Name = "massa"
                       };
        }

        private UnitDto GetUnitDto()
        {
            return new UnitDto
                       {
                           Abbreviation = "mg",
                           AllowConversion = true,
                           Divisor = 1,
                           IsReference = false,
                           Name = "milligram",
                           UnitGroupName = "massa"
                       };
        }
    }

    public static class UnitServices
    {
        public static UnitCreator WithDto(UnitDto dto)
        {
            return new UnitCreator(dto);
        }

        public static Unit GetUnit(Guid id)
        {
            return Repository.SingleOrDefault(x => x.Id == id);
        }

        private static IEnumerable<Unit> Repository
        {
            get { return RepositoryFactory.Create<Unit, Guid, UnitDto>(); }
        }


    }

    public class UnitCreator
    {
        private readonly UnitDto _dto;
        private UnitGroupDto _groupDto;
        private IRepository<Unit, Guid, UnitDto> _repository;

        public UnitCreator(UnitDto dto)
        {
            _dto = dto;
        }

        public Unit GetUnit()
        {
            return FindUnit() ?? CreateUnit();
        }

        private Unit FindUnit()
        {
            return Repository.SingleOrDefault(x => x.Name == _dto.Name);
        }

        private IRepository<Unit, Guid, UnitDto> Repository
        {
            get { return _repository ?? (_repository = RepositoryFactory.Create<Unit, Guid, UnitDto>()); }
        }

        private Unit CreateUnit()
        {
            AddNewUnitToRepository();
            return FindUnit();
        }

        private void AddNewUnitToRepository()
        {
            Repository.Add(_groupDto != null
                               ? Unit.CreateUnit(_dto, _groupDto)
                               : Unit.CreateUnit(_dto));
        }

        public UnitCreator AddToGroup(UnitGroupDto groupDto)
        {
            _groupDto = groupDto;
            return this;
        }
    }
}
