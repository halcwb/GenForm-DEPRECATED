using Informedica.GenForm.Presentation.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Presentation.Tests
{
    
    
    /// <summary>
    ///This is a test class for IFormTest and is intended
    ///to contain all IFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFormTest
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void Caption_of_presentation_can_be_set_to_test()
        {
            IForm target = CreateIForm(); // Get Fake Instance
            string caption = "Test";
            string expected = caption; 
            string actual;
            target.Caption = expected;
            actual = target.Caption;
            Assert.AreEqual(expected, actual);
        }

        internal virtual IForm CreateIForm()
        {
            IForm target = Isolate.Fake.Instance<IForm>();
            
            return target;
        }

        private IButton CreateIButton()
        {
            IButton button = Isolate.Fake.Instance<IButton>();
            return button;
        }

        [Isolated]
        [TestMethod]
        public void A_button_can_be_added_to_a_presentation()
        {
            IForm target = CreateIForm();
            Isolate.WhenCalled(() => target.Buttons.Count).WillReturn(1);

            target.AddButton(CreateIButton());

            Assert.IsTrue(target.Buttons.Count == 1);
        }

    }
}
