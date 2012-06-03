﻿using Informedica.GenForm.Mvc3.Controllers;
using NHibernate;
using TypeMock;
using TypeMock.ArrangeActAssert;
using System.Web.Mvc;
using Informedica.GenForm.Mvc3.Environments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class TransactionAttributeShould
    {
        private ActionExecutingContext _context;
        
        private ISession _session;
        private Mock<TransactionAttribute> _mock;
        private TransactionAttribute _attr;
        private ResultExecutedContext _result;
        private ITransaction _transaction;
        private LoginController _loginController;

        [TestInitialize]
        public void Init()
        {
            _context = Isolate.Fake.Instance<ActionExecutingContext>();
            _result = Isolate.Fake.Instance<ResultExecutedContext>();

            _session = Isolate.Fake.Instance<ISession>();
            _transaction = Isolate.Fake.Instance<ITransaction>();
            Isolate.WhenCalled(() => _session.Transaction).WillReturn(_transaction);
            Isolate.WhenCalled(() => _transaction.IsActive).WillReturn(true);


            _mock = MockManager.Mock<TransactionAttribute>();
            _mock.CallBase.ExpectCall("OnActionExecuting");
            _attr = new TransactionAttribute();
            Isolate.NonPublic.Property.WhenGetCalled(_attr, "Session").WillReturn(_session);

            _loginController = new LoginController();
        }

        [Isolated]
        [TestMethod]
        public void BeginATransactionOnASessionOnActionExecuting()
        {
            _attr.OnActionExecuting(_context);
            Isolate.Verify.WasCalledWithAnyArguments(() => _session.BeginTransaction());
        }

        [Isolated]
        [TestMethod]
        public void NotBeginATransactionWhenLoginController()
        {
            Isolate.WhenCalled(() => _context.Controller).WillReturn(_loginController);

            _attr.OnActionExecuting(_context);
            Isolate.Verify.WasNotCalled(() => _session.BeginTransaction());
        }

        [Isolated]
        [TestMethod]
        public void CommitATransactionWhenResultExecuted()
        {
            _attr.OnResultExecuted(_result);
            
            Isolate.Verify.WasCalledWithAnyArguments(() => _session.Transaction.Commit());
        }

        [Isolated]
        [TestMethod]
        public void NotCommitATransactionWhenLoginController()
        {
            Isolate.WhenCalled(()=> _result.Controller).WillReturn(_loginController);

            _attr.OnResultExecuted(_result);
            Isolate.Verify.WasNotCalled(() => _session.Transaction.Commit());
        }

    }
}