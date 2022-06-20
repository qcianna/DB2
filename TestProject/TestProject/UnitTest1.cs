using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data;

namespace TestProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        static private SqlConnection conn;

        public UnitTest1()
        {
            string connString = "DATA SOURCE=MSSQLSERVER58;" + "INITIAL CATALOG=AdventureWorks2008; INTEGRATED SECURITY=SSPI;";
            conn = new SqlConnection(connString);
            conn.Open();
        }

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

        //CountRecentlyUpdated

        [TestMethod]
        public void TestCountRecentlyUpdated()
        {
            int expected = 19;

            SqlCommand cmd = new SqlCommand("SELECT dbo.CountRecentlyUpdated(ModifiedDate) FROM (SELECT TOP 100 * FROM [Person].[Address]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = (int)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        //FindMaxOccuranceString

        [TestMethod]
        public void TestFindMaxOccuranceString()
        {
            string expected = "Paris";

            SqlCommand cmd = new SqlCommand("SELECT dbo.FindMaxOccuranceString(City) FROM (SELECT TOP 100 * FROM [Person].[Address]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            string result = (string)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        //FindAvgValue

        [TestMethod]
        public void TestFindAvgValue()
        {
            double expected = 41.45;

            SqlCommand cmd = new SqlCommand("SELECT dbo.FindAvgValue(SickLeaveHours) FROM (SELECT TOP 100 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            double result = (double)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        //FindSum

        [TestMethod]
        public void TestFindSum()
        {
            int expected = 4145;

            SqlCommand cmd = new SqlCommand("SELECT dbo.FindSum(SickLeaveHours) FROM (SELECT TOP 100 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int resultult = (int)dataReader[0];

            Assert.AreEqual(expected, resultult);
        }

        [TestMethod]
        public void TestFindSumIfEmpty()
        {
            int expected = 0;
          
            SqlCommand cmd = new SqlCommand("SELECT dbo.FindSum(SickLeaveHours) FROM (SELECT TOP 0 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int resultult = (int)dataReader[0];

            Assert.AreEqual(expected, resultult);
        }

        //CountAllOccurances

        [TestMethod]
        public void TestCountAllOccurances()
        {
            int expected = 3;

            SqlCommand cmd = new SqlCommand("SELECT dbo.CountAllOccurances(JobTitle) FROM (SELECT TOP 100 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = (int)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCountAllOccurancesIfNoneFound()
        {
            int expected = 0;

            SqlCommand cmd = new SqlCommand("SELECT dbo.CountAllOccurances(JobTitle) FROM (SELECT TOP 100 * FROM [Humanresources].[Employee]) AS result WHERE JobTitle != 'Design Engineer'", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = (int)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        //FindShortestString

        [TestMethod]
        public void TestFindShortestString()
        {
            string expected = "adventure-works\\ed0";

            SqlCommand cmd = new SqlCommand("SELECT dbo.FindShortestString(LoginID) FROM (SELECT TOP 100 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            string result = (string)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        //CountUniqueStrings

        [TestMethod]
        public void TestCountUniqueStrings()
        {
            int result;
            SqlCommand cmd = new SqlCommand("SELECT dbo.CountUniqueStrings(City) FROM (SELECT TOP 100 * FROM [Person].[Address]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            result = (int)dataReader[0];

            Assert.AreEqual(51, result);
        }

        //FindOddEvenDiff

        [TestMethod]
        public void TestFindOddEvenDiff()
        {
            int expected = 4;

            SqlCommand cmd = new SqlCommand("SELECT dbo.FindOddEvenDiff(NationalIDNumber) FROM (SELECT TOP 100 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = (int)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFindOddEvenDiffIfSameNumber()
        {
            int expected = 0;

            SqlCommand cmd = new SqlCommand("SELECT dbo.FindOddEvenDiff(NationalIDNumber) FROM (SELECT TOP 10 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            int result = (int)dataReader[0];

            Assert.AreEqual(expected, result);
        }

        //ConnectStrings

        [TestMethod]
        public void TestConnectStrings()
        {
            string expected = "M;F;M;M;F;";

            SqlCommand cmd = new SqlCommand("SELECT dbo.ConnectStrings(Gender) FROM (SELECT TOP 5 * FROM [Humanresources].[Employee]) AS result", conn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            string result = (string)dataReader[0];

            Assert.AreEqual(expected, result);

        }
    }
}
