using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumDemo.IntegrationTests.Countries;
using SeleniumDemo.IntegrationTests.Countries.PageObjects;

namespace SeleniumDemo.IntegrationTests.Login
{
    /// <summary>
    /// Provides access to all country related pages that require authorization.
    /// </summary>
    public class CountriesLoginPage : LoginPageBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LoginPage class.
        /// </summary>
        public CountriesLoginPage(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
        }

        #endregion

        #region Get Page Methods

        /// <summary>
        /// Gets object for manipulating the Countries index page.
        /// </summary>
        public CountriesIndexPageObject GetIndexPage()
        {
            return this.GetPageObject(this.UrlBuilder.CountryIndexUrl(), () => new CountriesIndexPageObject(this.WebDriver, this.UrlBuilder));
        }

        /// <summary>
        /// Gets object for manipulating the Add Country page.
        /// </summary>
        public AddCountryPageObject GetAddCountryPage()
        {
            return this.GetPageObject(this.UrlBuilder.AddCountryUrl(), () => new AddCountryPageObject(this.WebDriver, this.UrlBuilder));
        }

        /// <summary>
        /// Gets object for manipulating the Delete Country page.
        /// </summary>
        public DeleteCountryPageObject GetDeleteCountryPage(int countryId)
        {
            return this.GetPageObject(this.UrlBuilder.DeleteCountryUrl(countryId), () => new DeleteCountryPageObject(this.WebDriver, this.UrlBuilder));
        }

        /// <summary>
        /// Gets object for manipulating the View Country page.
        /// </summary>
        public ViewCountryPageObject GetViewCountryPage(int countryId)
        {
            return this.GetPageObject(this.UrlBuilder.ViewCountryUrl(countryId), () => new ViewCountryPageObject(this.WebDriver, this.UrlBuilder));
        }

        /// <summary>
        /// Gets object for manipulating the Edit Country page.
        /// </summary>
        public EditCountryPageObject GetEditCountryPage(int countryId)
        {
            return this.GetPageObject(this.UrlBuilder.EditCountryUrl(countryId), () => new EditCountryPageObject(this.WebDriver, this.UrlBuilder));
        }

        #endregion
    }
}
