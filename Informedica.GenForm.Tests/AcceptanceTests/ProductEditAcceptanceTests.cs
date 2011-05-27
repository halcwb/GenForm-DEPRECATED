using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Tests.AcceptanceTests
{
    [TestClass]
    public class ProductEditAcceptanceTests
    {
        [TestMethod]
        public void User_can_start_with_a_new_empty_product()
        {
            Assert.Fail("User could not start with empty product");
        }

        [TestMethod]
        public void User_can_change_fields_of_empty_product()
        {
            Assert.Fail("User could not change the fields of an empty product");
        }

        [TestMethod]
        public void User_can_see_if_entered_values_are_valid()
        {
            Assert.Fail("User could not see whether entered values were valid");
        }

        [TestMethod]
        public void User_can_save_product_with_values_entered()
        {
            Assert.Fail("Product with entered values could not be saved");
        }

        [TestMethod]
        public void When_product_is_save_the_saved_version_of_product_is_returned()
        {
            Assert.Fail("after save, no saved version of the product was returned");
        }
    }
}
