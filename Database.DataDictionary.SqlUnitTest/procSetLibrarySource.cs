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
    public class procSetLibrarySource : SqlDatabaseTestClass
    {

        public procSetLibrarySource()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction App_DataDictionary_procSetLibrarySource_Insert_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(procSetLibrarySource));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyResultSetCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction App_DataDictionary_procSetLibrarySource_Delete_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyResultSetCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition emptyResultSetCondition1;
            this.App_DataDictionary_procSetLibrarySource_InsertData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.App_DataDictionary_procSetLibrarySource_DeleteData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            App_DataDictionary_procSetLibrarySource_Insert_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyResultSetCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            App_DataDictionary_procSetLibrarySource_Delete_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            emptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            // 
            // App_DataDictionary_procSetLibrarySource_Insert_TestAction
            // 
            App_DataDictionary_procSetLibrarySource_Insert_TestAction.Conditions.Add(notEmptyResultSetCondition2);
            resources.ApplyResources(App_DataDictionary_procSetLibrarySource_Insert_TestAction, "App_DataDictionary_procSetLibrarySource_Insert_TestAction");
            // 
            // notEmptyResultSetCondition2
            // 
            notEmptyResultSetCondition2.Enabled = true;
            notEmptyResultSetCondition2.Name = "notEmptyResultSetCondition2";
            notEmptyResultSetCondition2.ResultSet = 1;
            // 
            // App_DataDictionary_procSetLibrarySource_Delete_TestAction
            // 
            App_DataDictionary_procSetLibrarySource_Delete_TestAction.Conditions.Add(notEmptyResultSetCondition1);
            App_DataDictionary_procSetLibrarySource_Delete_TestAction.Conditions.Add(emptyResultSetCondition1);
            resources.ApplyResources(App_DataDictionary_procSetLibrarySource_Delete_TestAction, "App_DataDictionary_procSetLibrarySource_Delete_TestAction");
            // 
            // notEmptyResultSetCondition1
            // 
            notEmptyResultSetCondition1.Enabled = true;
            notEmptyResultSetCondition1.Name = "notEmptyResultSetCondition1";
            notEmptyResultSetCondition1.ResultSet = 1;
            // 
            // emptyResultSetCondition1
            // 
            emptyResultSetCondition1.Enabled = true;
            emptyResultSetCondition1.Name = "emptyResultSetCondition1";
            emptyResultSetCondition1.ResultSet = 2;
            // 
            // App_DataDictionary_procSetLibrarySource_InsertData
            // 
            this.App_DataDictionary_procSetLibrarySource_InsertData.PosttestAction = null;
            this.App_DataDictionary_procSetLibrarySource_InsertData.PretestAction = null;
            this.App_DataDictionary_procSetLibrarySource_InsertData.TestAction = App_DataDictionary_procSetLibrarySource_Insert_TestAction;
            // 
            // App_DataDictionary_procSetLibrarySource_DeleteData
            // 
            this.App_DataDictionary_procSetLibrarySource_DeleteData.PosttestAction = null;
            this.App_DataDictionary_procSetLibrarySource_DeleteData.PretestAction = null;
            this.App_DataDictionary_procSetLibrarySource_DeleteData.TestAction = App_DataDictionary_procSetLibrarySource_Delete_TestAction;
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
        public void App_DataDictionary_procSetLibrarySource_Insert()
        {
            SqlDatabaseTestActions testActions = this.App_DataDictionary_procSetLibrarySource_InsertData;
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
        public void App_DataDictionary_procSetLibrarySource_Delete()
        {
            SqlDatabaseTestActions testActions = this.App_DataDictionary_procSetLibrarySource_DeleteData;
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

        private SqlDatabaseTestActions App_DataDictionary_procSetLibrarySource_InsertData;
        private SqlDatabaseTestActions App_DataDictionary_procSetLibrarySource_DeleteData;
    }
}
