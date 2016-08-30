using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumDemo.IntegrationTests.IntegrationTests;
using SeleniumDemo.IntegrationTests.KnownData;

namespace SeleniumDemo.IntegrationTests.Login
{
    /// <summary>
    /// Provides common functionality to ensure user is logged in before attempting to visit a specified page.
    /// </summary>
    public abstract class LoginPageBase : PageObjectBase<LoginPageBase>
    {
        private AccountDataGenerator accountDataGenerator;

        /// <summary>
        /// Initializes a new instance of the LoginPage class.
        /// </summary>
        protected LoginPageBase(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
            this.accountDataGenerator = new AccountDataGenerator();
        }

        protected T GetPageObject<T>(string url, Func<T> constructorFunction) where T : PageObjectBase<T>
        {
            this.PerformLogin();
            this.WebDriver.Navigate().GoToUrl(url);
            return constructorFunction();
        }

        protected void PerformLogin()
        {
            // check the .ASPXAUTH cookie existence, if not there then not logged in
            var authorizationCookie = this.WebDriver.Manage().Cookies.GetCookieNamed(".AspNet.ApplicationCookie");
            if (authorizationCookie != null)
            {
                return;
            }

            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];

            // ensure that the account exists
            this.accountDataGenerator.EnsureKnownAccount(username, password);

            // enter and submit the login details
            this.WebDriver.Navigate().GoToUrl(this.UrlBuilder.LoginUrl());
            
            this.EnterText("Email", username)
                .EnterText("Password", password)
                .ClickSubmit();
        }
    }
}
