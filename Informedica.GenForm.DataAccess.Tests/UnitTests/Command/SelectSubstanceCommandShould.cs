using System;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Assembler;
using Informedica.GenForm.DataAccess.Transactions;
using Informedica.GenForm.Database;
using Informedica.GenForm.Library.DomainModel.Products;
using Informedica.GenForm.Library.DomainModel.Products.Data;
using Informedica.GenForm.Library.Factories;
using Informedica.GenForm.Library.Transactions;
using Informedica.GenForm.Library.Transactions.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.DataAccess.Tests.UnitTests.Command
{
    /// <summary>
    /// Summary description for SelectSubstanceCommandShould
    /// </summary>
    [TestClass]
    public class SelectSubstanceCommandShould
    {
        private TestContext testContextInstance;

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
        public static void MyClassInitialize(TestContext testContext) { GenFormApplication.Initialize(); }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ReturnAListOfThreeItemsWhenNoCriteriaSupplied()
        {
            GenFormApplication.Initialize();

            var list = CreateSubstanceList();
            var commands = new CommandQueue();

            using (var context = CreateContext())
            {
                context.Connection.Open();
                context.Transaction = context.Connection.BeginTransaction();

                foreach (var product in list)
                {
                    commands.Enqueue(CommandFactory.CreateInsertCommand(product));
                }
                commands.Enqueue(CommandFactory.CreateSelectCommand<ISubstance>());
                try
                {
                    foreach (var command in commands.Commands)
                    {
                        ((IExecutable)command).Execute(context);
                    }

                    Assert.IsTrue(((ISelectCommand<ISubstance>)commands.Commands.Last()).Result.Count() == 3);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.ToString());
                }
                finally
                {
                    context.Transaction.Rollback();
                    context.Connection.Close();
                }
            }
        }

        private static IEnumerable<ISubstance> CreateSubstanceList()
        {
            return SubstanceTestFixtures.GetSubstanceDtoListWithThreeItems().Select(
                dto => DomainFactory.Create<ISubstance, SubstanceDto>(dto));
        }

        private GenFormDataContext CreateContext()
        {
            return new GenFormDataContext();
        }
    }

    public static class SubstanceTestFixtures
    {
        public static IEnumerable<SubstanceDto> GetSubstanceDtoListWithThreeItems()
        {
            var list = new List<SubstanceDto>();
            list.Add(new SubstanceDto{Name = "paracetamol"});
            list.Add(new SubstanceDto{Name = "dopamine"});
            list.Add(new SubstanceDto{Name = "lactulose"});

            return list;
        }
    }
}
