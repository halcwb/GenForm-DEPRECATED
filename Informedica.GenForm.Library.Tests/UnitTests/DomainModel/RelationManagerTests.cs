using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Informedica.GenForm.Library.DomainModel.Relations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Library.Tests.UnitTests.DomainModel
{
    /// <summary>
    /// Summary description for RelationManagerTests
    /// </summary>
    [TestClass]
    public class RelationManagerTests
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
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
        public void ThatTheRelationBetweenGroupAndItemCanBeRetrieved()
        {
            RelationManager.Add(typeof(IRelation<Group, Item>), new OneToManyRelation<Group, Item>());
            var relation = RelationManager.OneToMany<Group, Item>();
            Assert.IsInstanceOfType(relation, typeof(OneToManyRelation<Group, Item>));
        }

        [TestMethod]
        public void ThatAOneToManyRelationCanBeAdded()
        {
            RelationManager.Add(typeof(IRelation<Group, Item>), new OneToManyRelation<Group, Item>());
            var relation = RelationManager.OneToMany<Group, Item>();
            var group = new Group();
            var item = new Item();
            relation.Add(group, item);
            Assert.IsTrue(group.Items.Contains(item));
            Assert.IsTrue(item.Group == group);
        }

        [TestMethod]
        public void ThatAOneToManyRelationCanBeAddedWithManySet()
        {
            RelationManager.Add(typeof(IRelation<Group, Item>), new OneToManyRelation<Group, Item>());
            var relation = RelationManager.OneToMany<Group, Item>();
            var group = new Group();
            var item = new Item();
            var items = new HashSet<Item> { item };
            relation.Add(group, items);
            Assert.IsTrue(group.Items.Contains(item));
            Assert.IsTrue(item.Group == group);
        }

        [TestMethod]
        public void ThatAOneToManyRelationCanRemoved()
        {
            RelationManager.Add(typeof(IRelation<Group, Item>), new OneToManyRelation<Group, Item>());
            var relation = RelationManager.OneToMany<Group, Item>();
            var group = new Group();
            var item = new Item();
            relation.Add(group, item);
            relation.Remove(group, item);
            Assert.IsFalse(group.Items.Contains(item));
            Assert.IsFalse(item.Group == group);
        }

        [TestMethod]
        public void ThatAManyToManyRelationCanBeAdded()
        {
            RelationManager.Add(typeof(IRelation<Item, Item2>), new ManyToManyRelation<Item, Item2>());
            var relation = RelationManager.ManyToMany<Item, Item2>();
            var item = new Item();
            var item2 = new Item2();
            relation.Add(item, item2);
            Assert.IsTrue(item.Items.Contains(item2));
            Assert.IsTrue(item2.Items.Contains(item));
        }

        [TestMethod]
        public void ThatAManyToManyRelationCanBeRemoved()
        {
            RelationManager.Add(typeof(IRelation<Item, Item2>), new ManyToManyRelation<Item, Item2>());
            var relation = RelationManager.ManyToMany<Item, Item2>();
            var item = new Item();
            var item2 = new Item2();
            relation.Add(item, item2);
            relation.Remove(item, item2);
            Assert.IsFalse(item.Items.Contains(item2));
            Assert.IsFalse(item2.Items.Contains(item));
        }


    }

    public class Item2 : IRelationPart
    {
        public IEnumerable<Item> Items
        {
            get { return RelationManager.ManyToMany<Item, Item2>().GetManyPartLeft(this); }
        }
    }

    public class Group : IRelationPart
    {
        public IEnumerable<Item> Items
        {
            get { return RelationManager.OneToMany<Group, Item>().GetManyPart(this); }
        }
    }

    public class Item : IRelationPart
    {
        public Group Group { get { return RelationManager.OneToMany<Group, Item>().GetOnePart(this); } }

        public IEnumerable<Item2> Items
        {
            get { return RelationManager.ManyToMany<Item, Item2>().GetManyPartRight(this); }
        }
    }
}
