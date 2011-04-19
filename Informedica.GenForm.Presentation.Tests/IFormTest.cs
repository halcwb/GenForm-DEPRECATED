using Informedica.GenForm.Mocking.Presentation;
using Informedica.GenForm.Presentation.Forms;
using Informedica.GenForm.Presentation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TypeMock;
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


        /// <summary>
        ///A test for Fields
        ///</summary>
        [TestMethod()]
        public void FieldsTest()
        {
            IForm target = CreateIForm(); // TODO: Initialize to an appropriate value
            IList<IFormField> expected = null; // TODO: Initialize to an appropriate value
            IList<IFormField> actual;
            actual = target.Fields;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Caption
        ///</summary>
        [TestMethod()]
        public void CaptionTest()
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
            // TODO: Instantiate an appropriate concrete class.
            IForm target = PresentationMockProvider.Instance.Resolve<IForm>();
            
            return target;
        }

        private IButton CreateIButton()
        {
            IButton button = PresentationMockProvider.Instance.Resolve<IButton>();
            IForm target = PresentationMockProvider.Instance.Resolve<IForm>();
            Isolate.WhenCalled(() => target.Buttons.Count).WillReturn(1);
            return button;
        }

        [TestMethod]
        public void AddButtonTest()
        {
            IForm target = CreateIForm();
            target.AddButton(CreateIButton());

            Assert.IsTrue(target.Buttons.Count == 1);
        }

        /// <summary>
        ///A test for Buttons
        ///</summary>
        [TestMethod()]
        public void ButtonsTest()
        {
            IForm target = CreateIForm(); // TODO: Initialize to an appropriate value
            IList<IButton> expected = null; // TODO: Initialize to an appropriate value
            IList<IButton> actual;
            actual = target.Buttons;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
