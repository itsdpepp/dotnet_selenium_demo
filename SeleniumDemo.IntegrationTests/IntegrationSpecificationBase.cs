using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumDemo.IntegrationTests.Login;

namespace SeleniumDemo.IntegrationTests
{
    /// <summary>
    /// Base class for UI tests.
    /// </summary>
    public abstract class IntegrationSpecificationBase
    {
        #region Fields

        protected LoginPage LoginPage;

        private IWebDriver webDriver;

        #endregion

        #region Overrideable methods

        /// <summary>
        /// Base functionality to create web driver and url builder objects. Passes these to the entry point login page.
        /// </summary>
        public virtual void TestInitialize()
        {
            var urlBuilder = new UrlBuilder();
            var driverSettings = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };

            this.webDriver = new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(), driverSettings);

            this.LoginPage = new LoginPage
            {
                CountriesLogin = new CountriesLoginPage(webDriver, urlBuilder)
            };
        }

        /// <summary>
        /// Base functionality to close the webdriver when the test completes. Ensures that browser instances are destroyed after tests.
        /// </summary>
        public virtual void TestCleanup()
        {
            if (this.webDriver != null)
            {
                this.webDriver.Dispose();
            }
        }

        #endregion
    }
}