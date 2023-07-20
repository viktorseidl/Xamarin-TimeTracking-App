using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFAAPP.Models;
using ZFAAPP.Services;

namespace ZFAAPP.Tests
{
    /*
     * @Class UnitTestLogWithFail
     * <Summary>
     * This Class is testing the login Functions with valid and invalid Credentials
     * </Summary>
     */
    [TestClass]
    public class UnitTestLogWithFail
    {
        /*
         * @Param LogInWithValidCredentials()
         * If u == NULL => True
         */
        [TestMethod]
        public async Task LogInWithValidCredentials()
        {
            //Arrange
            Fetch fetch = new Fetch();
            var PassWord = fetch.sha256("abcdefg").Replace("-", string.Empty).ToLower();
            var PinWord = fetch.sha256("5").Replace("-", string.Empty).ToLower();
            var GKey = PinWord + PassWord;
            var b = new Anmelden();

            //Act
            var u = b.LogMeIn(GKey);

            //Assert
            Assert.IsNotNull(await u);
        }
        /*
         * @Param LogInWithInvalidCredentialsPassword()
         * If u != NULL => True
         */
        [TestMethod]
        public async Task LogInWithInvalidCredentialsPassword()
        {
            //Arrange
            Fetch fetch = new Fetch();
            var PassWord = fetch.sha256("passwort").Replace("-", string.Empty).ToLower();
            var PinWord = fetch.sha256("2").Replace("-", string.Empty).ToLower();
            var GKey = PinWord + PassWord;
            var b = new Anmelden();

            //Act
            var u = b.LogMeIn(GKey);

            //Assert
            Assert.IsNull(await u);

        }
        /*
         * @Param LogInWithInvalidsCredentialsPin()
         * If u != NULL => True
         */
        [TestMethod]
        public async Task LogInWithInvalidsCredentialsPin()
        {
            //Arrange
            Fetch fetch = new Fetch();
            var PassWord = fetch.sha256("passwort").Replace("-", string.Empty).ToLower();
            var PinWord = fetch.sha256("2").Replace("-", string.Empty).ToLower();
            var GKey = PinWord + PassWord;
            var b = new Anmelden();

            //Act
            var u = b.LogMeIn(GKey);

            //Assert
            Assert.IsNull(await u);
        }
    } 
}
