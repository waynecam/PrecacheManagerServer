using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PrecacheManagerServer.BLL.Services;
using PrecacheManagerServer.Controllers;
using PrecacheManagerServer.API.Models;
using PrecacheManagerServer.Shared.Enums;
using PrecacheManagerServer.Shared.Models;
using PrecacheManagerServer.BLL.Models;
using PrecacheManagerServer.DAL.Models;
using System.Threading.Tasks;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PrecacheServerManager.Test.PrecacheServerManager.Api.Tests
{
    public class TestBase
    {
        internal Mock<IServiceProvider> mockServiceProvider;
        internal Mock<IPlatformSettings> mockPlatformSettings;

        public TestBase()
        {
           mockServiceProvider = new Mock<IServiceProvider>();
           mockPlatformSettings = new Mock<IPlatformSettings>();
        }

        #region Helper Methods
        public PrecacheManagerServer.API.Models.User GetTestUser(ApplicationMode appMode)
        {
            var u = new PrecacheManagerServer.API.Models.User();

            u.ApplicationModes = new List<ApplicationMode>() { appMode };
            u.PlatformSettings = new PlatformSettings() { ConnectionStrings = new Dictionary<ApplicationMode, string>() };
            u.PlatformSettings.ConnectionStrings.Add(appMode, "");
            u.UserName = "TestUser@test.com";

            return u;
        }


        protected void SimulateModelValidation(object model, ControllerBase controller)
        {
            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }


        protected Dictionary<ApplicationMode, string> GetTestPlatformConfigServiceConnStrings()
        {
            var connectionStrings = new Dictionary<ApplicationMode, string>();
            //ConnectionStrings.Add(ApplicationMode.Australia, $"Connection Timeout=300;Data Source=SYDWINSQLP001;Initial Catalog=PortfolioManagementAUS;persist security info=True;user id=LinkedServerUser;password=7XvAAkG82b6vDECypojf;");
            connectionStrings.Add(ApplicationMode.International, $"Connection Timeout=300;Data Source=10.236.234.20\\PortfolioINT;Initial Catalog=PortfolioManagementINT;persist security info=True;Integrated Security=True;");
            connectionStrings.Add(ApplicationMode.GermanyMedia, $"Connection Timeout=300;Data Source=10.236.234.20\\PORTFOLIOGER;initial catalog=PortfolioManagementGER;persist security info=True;Integrated Security=True;");

            return connectionStrings;
        }

        #endregion
    }
}
