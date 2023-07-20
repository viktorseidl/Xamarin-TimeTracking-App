using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ZFAAPP.Models;
using ZFAAPP.Services;

namespace ZFAAPP.Tests
{
    /*
     * @Class UnitTestHash
     * <Summary>
     * This Class is testing the Hash Encryption SHA256
     * </Summary>
     */
    [TestClass]
    public class UnitTestHash
    {
        /*
         * @Param HashInt()
         * If hashedNum == "7902699be42c8a8e46fbbb4501726517e86b22c56a189f7625a6da49081b2451" => True
         */
        [TestMethod]
        public void HashInt()
        {
            //Arrange
            Fetch fetch = new Fetch();
            int num = 7;
            var TrueHash = "7902699be42c8a8e46fbbb4501726517e86b22c56a189f7625a6da49081b2451";

            //Act
            var hashedNum = fetch.sha256(num.ToString()).Replace("-", string.Empty).ToLower();

            //Assert
            Assert.AreEqual(TrueHash, hashedNum);
        }
        /*
         * @Param HashString()
         * If hashedPhrase == "bd79b59e4e59935067e8519bbbc64d5392b6e05eed973db17531cb6cb5124319" => True
         */
        [TestMethod]
        public void HashString()
        {
            //Arrange
            Fetch fetch = new Fetch();
            string phrase = "Just testing out this function";
            var TrueHash = "bd79b59e4e59935067e8519bbbc64d5392b6e05eed973db17531cb6cb5124319";

            //Act
            var hashedPhrase = fetch.sha256(phrase).Replace("-", string.Empty).ToLower();

            //Assert
            Assert.AreEqual(TrueHash, hashedPhrase);
        }
        /*
         * @Param WrongHashOfInt()
         * If hashedNum != "0x0000000000000000000000000000000000" => True
         */
        [TestMethod]
        public void WrongHashOfInt()
        {
            //Arrange
            Fetch fetch = new Fetch();
            int num = 7;
            var wrongHash = "0x0000000000000000000000000000000000";

            //Act
            var hashedNum = fetch.sha256(num.ToString()).Replace("-", string.Empty).ToLower();

            //Assert
            Assert.AreEqual(wrongHash, hashedNum);
        }
        /*
         * @Param WrongHashOfString()
         * If hashedPhrase != "0x0000000000000000000000000000000000" => False
         */
        [TestMethod]
        public void WrongHashOfString()
        {
            //Arrange
            Fetch fetch = new Fetch();
            string phrase = "Just testing out this function";
            var wrongHash = "0x0000000000000000000000000000000000";

            //Act
            var hashedPhrase = fetch.sha256(phrase).Replace("-", string.Empty).ToLower();

            //Assert
            Assert.AreEqual(wrongHash, hashedPhrase);
        }
    }
}