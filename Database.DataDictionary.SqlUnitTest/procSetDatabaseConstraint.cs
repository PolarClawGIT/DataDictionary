﻿using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
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
    public class procSetDatabaseConstraint : SqlDatabaseTestClass
    {

        public procSetDatabaseConstraint()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(procSetDatabaseConstraint));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition notEmptyResultSetCondition1;
            this.App_DataDictionary_procSetDatabaseConstraint_InsertData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            notEmptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            // 
            // App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction
            // 
            App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction.Conditions.Add(notEmptyResultSetCondition1);
            resources.ApplyResources(App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction, "App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction");
            // 
            // notEmptyResultSetCondition1
            // 
            notEmptyResultSetCondition1.Enabled = true;
            notEmptyResultSetCondition1.Name = "notEmptyResultSetCondition1";
            notEmptyResultSetCondition1.ResultSet = 1;
            // 
            // App_DataDictionary_procSetDatabaseConstraint_InsertData
            // 
            this.App_DataDictionary_procSetDatabaseConstraint_InsertData.PosttestAction = null;
            this.App_DataDictionary_procSetDatabaseConstraint_InsertData.PretestAction = null;
            this.App_DataDictionary_procSetDatabaseConstraint_InsertData.TestAction = App_DataDictionary_procSetDatabaseConstraint_Insert_TestAction;
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
        public void App_DataDictionary_procSetDatabaseConstraint_Insert()
        {
            SqlDatabaseTestActions testActions = this.App_DataDictionary_procSetDatabaseConstraint_InsertData;
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
        private SqlDatabaseTestActions App_DataDictionary_procSetDatabaseConstraint_InsertData;
    }
}
