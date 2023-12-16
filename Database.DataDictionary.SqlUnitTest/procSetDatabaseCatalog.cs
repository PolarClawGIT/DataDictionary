using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Database.DataDictionary.SqlUnitTest
{
    [TestClass()]
    public class procSetDatabaseCatalog : SqlDatabaseTestClass
    {

        public procSetDatabaseCatalog()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(procSetDatabaseCatalog));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyResultSetCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition emptyResultSetCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testInitializeAction;
            this.App_DataDictionary_procSetDatabaseCatalog_InsertData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.App_DataDictionary_procSetDatabaseCatalog_DeleteData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            emptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            testInitializeAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction
            // 
            App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction.Conditions.Add(notEmptyResultSetCondition1);
            resources.ApplyResources(App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction, "App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction");
            // 
            // notEmptyResultSetCondition1
            // 
            notEmptyResultSetCondition1.Enabled = true;
            notEmptyResultSetCondition1.Name = "notEmptyResultSetCondition1";
            notEmptyResultSetCondition1.ResultSet = 1;
            // 
            // App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction
            // 
            App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction.Conditions.Add(emptyResultSetCondition1);
            resources.ApplyResources(App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction, "App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction");
            // 
            // emptyResultSetCondition1
            // 
            emptyResultSetCondition1.Enabled = true;
            emptyResultSetCondition1.Name = "emptyResultSetCondition1";
            emptyResultSetCondition1.ResultSet = 1;
            // 
            // testInitializeAction
            // 
            resources.ApplyResources(testInitializeAction, "testInitializeAction");
            // 
            // App_DataDictionary_procSetDatabaseCatalog_InsertData
            // 
            this.App_DataDictionary_procSetDatabaseCatalog_InsertData.PosttestAction = null;
            this.App_DataDictionary_procSetDatabaseCatalog_InsertData.PretestAction = null;
            this.App_DataDictionary_procSetDatabaseCatalog_InsertData.TestAction = App_DataDictionary_procSetDatabaseCatalog_Insert_TestAction;
            // 
            // App_DataDictionary_procSetDatabaseCatalog_DeleteData
            // 
            this.App_DataDictionary_procSetDatabaseCatalog_DeleteData.PosttestAction = null;
            this.App_DataDictionary_procSetDatabaseCatalog_DeleteData.PretestAction = null;
            this.App_DataDictionary_procSetDatabaseCatalog_DeleteData.TestAction = App_DataDictionary_procSetDatabaseCatalog_Delete_TestAction;
            // 
            // procSetDatabaseCatalog
            // 
            this.TestInitializeAction = testInitializeAction;
        }

        #endregion


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
        #endregion

        [TestMethod()]
        public void App_DataDictionary_procSetDatabaseCatalog_Insert()
        {
            SqlDatabaseTestActions testActions = this.App_DataDictionary_procSetDatabaseCatalog_InsertData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void App_DataDictionary_procSetDatabaseCatalog_Delete()
        {
            SqlDatabaseTestActions testActions = this.App_DataDictionary_procSetDatabaseCatalog_DeleteData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }


        private SqlDatabaseTestActions App_DataDictionary_procSetDatabaseCatalog_InsertData;
        private SqlDatabaseTestActions App_DataDictionary_procSetDatabaseCatalog_DeleteData;
    }
}
