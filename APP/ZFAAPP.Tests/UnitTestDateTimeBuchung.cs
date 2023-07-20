using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using ZFAAPP.Models;
using ZFAAPP.Services;

namespace ZFAAPP.Tests
{
    /*
     * @Class UnitTestDateTimeBuchung
     * <Summary>
     * This Class is testing the format of data and storing of data
     * </Summary>
     */
    [TestClass]
    public class UnitTestDateTimeBuchung
    {
        /*
         * @Param TimeTouchBuchungGoing()
         * If Result.message == "Ihre Zeit wurde erfasst!"
         */
        [TestMethod]
        public async Task TimeTouchBuchungGoing()
        {
            //Arrange
            Fetch fetch = new Fetch();
            DateTimeFormat dt = new DateTimeFormat();
            TimetouchEntry timetouchEntry = await dt.prepareTime("2", "d4735e3a265e16eee03f59718b9b5d03019c07d8b6c51f90da3a666eec13ab35", "3");

            //Act
            var Result = await fetch.SetMyStamps("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/createtimetouch.php", timetouchEntry);

            //Assert
            Assert.AreEqual("Ihre Zeit wurde erfasst!", Result["message"].ToString());
        }
        /*
         * @Param TimeTouchBuchungComing()
         * If Result.message == "Ihre Zeit wurde erfasst!"
         */
        [TestMethod]
        public async Task TimeTouchBuchungComing()
        {
            //Arrange
            Fetch fetch = new Fetch();
            DateTimeFormat dt = new DateTimeFormat();
            TimetouchEntry timetouchEntry = await dt.prepareTime("2", "d4735e3a265e16eee03f59718b9b5d03019c07d8b6c51f90da3a666eec13ab35", "2");

            //Act
            var Result = await fetch.SetMyStamps("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/createtimetouch.php", timetouchEntry);

            //Assert
            Assert.AreEqual("Ihre Zeit wurde erfasst!", Result["message"].ToString());
        }
        /*
         * @Param InvalidTimeTouchBuchung()
         * If Result.message == "Fehler bei der Erfassung aufgetreten!"
         */
        [TestMethod]
        public async Task InvalidTimeTouchBuchung()
        {
            //Arrange
            Fetch fetch = new Fetch();
            DateTimeFormat dt = new DateTimeFormat();
            TimetouchEntry timetouchEntry = await dt.prepareTime("2", "2", "3");

            //Act
            var Result = await fetch.SetMyStamps("https://itsnando.com/ZFA/ZFA_REST_API/rest_api/createtimetouch.php", timetouchEntry);

            //Assert
            Assert.AreEqual("Fehler bei der Erfassung aufgetreten!", Result["message"].ToString());
        }
    }
}