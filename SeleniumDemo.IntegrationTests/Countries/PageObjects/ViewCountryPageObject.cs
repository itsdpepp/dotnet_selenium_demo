using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumDemo.IntegrationTests.IntegrationTests;

namespace SeleniumDemo.IntegrationTests.Countries.PageObjects
{
    /// <summary>
    /// Page access and control manipulation for the view Country page.
    /// </summary>
    public class ViewCountryPageObject : PageObjectBase<ViewCountryPageObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ViewCountryPageObject(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
        }

        public string GetCountryName()
        {
            return this.FindElementById("country-name").Text;
        }

        public string GetCountryDescription()
        {
            return this.FindElementById("country-description").Text;
        }
    }
}
