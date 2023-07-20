using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ZFAAPP.Models;
using ZFAAPP.Services;

namespace ZFAAPP.Tests
{
    /*
     * @Class UnitTestLogin
     * <Summary>
     * This Class is testing the login Functions with valid and invalid Credentials
     * </Summary>
     */
    [TestClass]
    public class UnitTestLogin
    {
        /*
         * @Param LogInWithValidCredentials()
         * If u != NULL => True
         */
        [TestMethod]
        public async Task LogInWithValidCredentials()
        {
            //Arrange
            Fetch fetch = new Fetch();
            var PassWord = fetch.sha256("passwort").Replace("-", string.Empty).ToLower();
            var PinWord = fetch.sha256("2").Replace("-", string.Empty).ToLower();
            var GKey = PinWord + PassWord;
            var b = new Anmelden();

            //Act
            var u = await b.LogMeIn(GKey);

            //Assert
            Assert.IsNotNull(u);
        }
        /*
         * @Param LogInWithInvalidCredentialsPassword()
         * If u == NULL => True
         */
        [TestMethod]
        public async Task  LogInWithInvalidCredentialsPassword()
        {
            //Arrange
            Fetch fetch = new Fetch();
            var PassWord = fetch.sha256("abcdefgh").Replace("-", string.Empty).ToLower();
            var PinWord = fetch.sha256("2").Replace("-", string.Empty).ToLower();
            var GKey = PinWord + PassWord;
            var b = new Anmelden();

            //Act
            var u = await b.LogMeIn(GKey);

            //Assert
            Assert.IsNull(u);

        }
        /*
         * @Param LogInWithInvalidCredentialsPin()
         * If u == NULL => True
         */
        [TestMethod]
        public async Task LogInWithInvalidCredentialsPin()
        {
            //Arrange
            Fetch fetch = new Fetch();
            var PassWord = fetch.sha256("passwort").Replace("-", string.Empty).ToLower();
            var PinWord = fetch.sha256("8").Replace("-", string.Empty).ToLower();
            var GKey = PinWord + PassWord;
            var b = new Anmelden();

            //Act
            var u = await b.LogMeIn(GKey);

            //Assert
            Assert.IsNull(u);
        }
    }
}