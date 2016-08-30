using System;
using System.Linq;
using OpenQA.Selenium;
using SeleniumDemo.IntegrationTests.IntegrationTests;

namespace SeleniumDemo.IntegrationTests.Countries.PageObjects
{
    /// <summary>
    /// Page access and control manipulate for the Country index page.
    /// </summary>
    public class CountriesIndexPageObject : PageObjectBase<CountriesIndexPageObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public CountriesIndexPageObject(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
        }

        public int GetNumberOfCountryDataRows()
        {
            return this.FindElementsByCssSelector("table > tbody > tr").Count() - 1;  // note: th row is contained within tbody element
        }

        private IWebElement GetFirstCountryRow()
        {
            return this.FindElementByCssSelector("table > tbody > tr:nth-child(2)"); // note: first row is header
        }

        public string GetFirstCountryDescription()
        {
            return this.GetFirstCountryRow().FindElement(By.CssSelector("td:nth-child(2)")).Text;
        }

        public string GetFirstCountryName()
        {
            return this.GetFirstCountryRow().FindElement(By.CssSelector("td:nth-child(1)")).Text;
        }
    }
}