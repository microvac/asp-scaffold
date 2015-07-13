using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Validation;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.ComponentModel.DataAnnotations;
using Moq;
using Scaffold.Validation;

namespace Scaffold.Tests
{
    [TestClass]
    public class TestValidator
    {
        public class TestTransactionModel
        {
            [Range(1,100)]
            public decimal Amount { get; set; }

            public int MustEven { get; set; }

            [Validator]
            public IEnumerable<ModelValidationResult> Validate()
            {
                if(MustEven % 2 != 0)
                    yield return new ModelValidationResult { MemberName = "fkActorID", Message = "fkActorID must matched either fkDestinationID or fkSourceID" };
            }

            public TestAccountModel Account { get; set; }
        }

        public class TestAccountModel
        {
            [RegularExpression(@"[\d\.]+", ErrorMessage = "Kode invalid")]
            public string Code { get; set; }

            [Validator]
            public IEnumerable<ModelValidationResult> Validate()
            {
                List<string> codes = new List<string>();
                codes.Add("1231.123");
                codes.Add("2345.234");
                codes.Add("2351.432");

                var accountCodesSet = new HashSet<String>(codes.Select(e => e));

                if (accountCodesSet.Contains(Code))
                    yield return new ModelValidationResult { MemberName = "Code", Message = "Kode sudah terdaftar" };
            }
        }

        [TestMethod]
        public void TestValidateTestTransactionModel()
        {
            ModelMetadataProvider metadataProvider = new DataAnnotationsModelMetadataProvider();
            HttpActionContext actionContext = CreateActionContext();
            actionContext.ControllerContext.Configuration.Services.Add(typeof(ModelValidatorProvider), new MethodModelValidatorProvider());

            var singleTransactionTest = new TestTransactionModel();
            singleTransactionTest.Amount = 1;

            var listTransactionTest = new List<TestTransactionModel>() { singleTransactionTest };

            var resultSingleTransactionTest = new DefaultBodyModelValidator().Validate(singleTransactionTest, typeof(TestTransactionModel), metadataProvider, actionContext, string.Empty);
            Assert.IsTrue(resultSingleTransactionTest);

            var resultListTransactionTest = new DefaultBodyModelValidator().Validate(listTransactionTest, typeof(TestTransactionModel), metadataProvider, actionContext, string.Empty);
            Assert.IsTrue(resultListTransactionTest);

            singleTransactionTest.MustEven = 3;

            resultSingleTransactionTest = new DefaultBodyModelValidator().Validate(singleTransactionTest, typeof(TestTransactionModel), metadataProvider, actionContext, string.Empty);
            Assert.IsFalse(resultSingleTransactionTest);

            resultListTransactionTest = new DefaultBodyModelValidator().Validate(listTransactionTest, typeof(TestTransactionModel), metadataProvider, actionContext, string.Empty);
            Assert.IsFalse(resultListTransactionTest);

        }

        [TestMethod]
        public void TestValidateTestAccountModel()
        {
            ModelMetadataProvider metadataProvider = new DataAnnotationsModelMetadataProvider();
            HttpActionContext actionContext = CreateActionContext();
            actionContext.ControllerContext.Configuration.Services.Add(typeof(ModelValidatorProvider), new MethodModelValidatorProvider());


            var account = new TestAccountModel();
            account.Code = "123.4323";

            var accounts = new List<TestAccountModel>() { account };

            var resultSingleAccountTest = new DefaultBodyModelValidator().Validate(account, typeof(TestAccountModel), metadataProvider, actionContext, string.Empty);
            Assert.IsTrue(resultSingleAccountTest);

            var resutltListAccountTest = new DefaultBodyModelValidator().Validate(accounts, typeof(TestAccountModel), metadataProvider, actionContext, string.Empty);
            Assert.IsTrue(resutltListAccountTest);

            account.Code = "1231.123";

            resultSingleAccountTest = new DefaultBodyModelValidator().Validate(account, typeof(TestAccountModel), metadataProvider, actionContext, string.Empty);
            Assert.IsFalse(resultSingleAccountTest);

            resutltListAccountTest = new DefaultBodyModelValidator().Validate(accounts, typeof(TestAccountModel), metadataProvider, actionContext, string.Empty);
            Assert.IsFalse(resutltListAccountTest);
        }

        [TestMethod]
        public void TestValidateChild()
        {
            ModelMetadataProvider metadataProvider = new DataAnnotationsModelMetadataProvider();
            HttpActionContext actionContext = CreateActionContext();
            actionContext.ControllerContext.Configuration.Services.Add(typeof(ModelValidatorProvider), new MethodModelValidatorProvider());

            var transaction = new TestTransactionModel();
            transaction.Amount = 1;

            var account = new TestAccountModel();
            account.Code = "123.4323";

            transaction.Account = account;

            var isValid = new DefaultBodyModelValidator().Validate(transaction, typeof(TestTransactionModel), metadataProvider, actionContext, string.Empty);
            Assert.IsTrue(isValid);

            account.Code = "1231.123";

            isValid = new DefaultBodyModelValidator().Validate(transaction, typeof(TestTransactionModel), metadataProvider, actionContext, string.Empty);
            Assert.IsFalse(isValid);

        }

        public static HttpActionContext CreateActionContext(HttpControllerContext controllerContext = null, HttpActionDescriptor actionDescriptor = null)
        {
            HttpControllerContext context = controllerContext ?? CreateControllerContext();
            HttpActionDescriptor descriptor = actionDescriptor ?? CreateActionDescriptor();
            descriptor.ControllerDescriptor = context.ControllerDescriptor;
            return new HttpActionContext(context, descriptor);
        }

        public static HttpActionDescriptor CreateActionDescriptor()
        {
            var mock = new Mock<HttpActionDescriptor>() { CallBase = true };
            mock.SetupGet(d => d.ActionName).Returns("Bar");
            return mock.Object;
        }

        public static HttpControllerContext CreateControllerContext(HttpConfiguration configuration = null, IHttpController instance = null, IHttpRouteData routeData = null, HttpRequestMessage request = null)
        {
            HttpConfiguration config = configuration ?? new HttpConfiguration();
            IHttpRouteData route = routeData ?? new HttpRouteData(new HttpRoute());
            HttpRequestMessage req = request ?? new HttpRequestMessage();
            req.SetConfiguration(config);
            req.SetRouteData(route);

            HttpControllerContext context = new HttpControllerContext(config, route, req);
            if (instance != null)
            {
                context.Controller = instance;
            }
            context.ControllerDescriptor = CreateControllerDescriptor(config);

            return context;
        }

        public static HttpActionContext GetHttpActionContext(HttpRequestMessage request)
        {
            HttpActionContext actionContext = CreateActionContext();
            actionContext.ControllerContext.Request = request;
            return actionContext;
        }

        public static HttpControllerDescriptor CreateControllerDescriptor(HttpConfiguration config = null)
        {
            if (config == null)
            {
                config = new HttpConfiguration();
            }
            return new HttpControllerDescriptor() { Configuration = config, ControllerName = "FooController" };
        }
    }
}
