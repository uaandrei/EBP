using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidUnitTesting
{
    static class TestHelper
    {
        /// <summary>
        /// Searches for <paramref name="expectedMessage"/> in <paramref name="validationResults"/> messages.
        /// </summary>
        /// <param name="expectedMessage">Expected error message.</param>
        /// <param name="validationResults">Actual list of validation results.</param>
        public static void AssertExpectedMessage(string expectedMessage, ValidationResults validationResults)
        {
            var success = false;
            foreach (var item in validationResults)
            {
                if (item.Message == expectedMessage)
                {
                    success = true;
                    break;
                }
            }
            Assert.IsTrue(success);
        }
    }
}
