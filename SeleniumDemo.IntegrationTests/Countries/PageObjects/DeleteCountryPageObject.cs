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
    /// Page access and control manipulation for the delete Country page.
    /// </summary>
    public class DeleteCountryPageObject : PageObjectBase<DeleteCountryPageObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DeleteCountryPageObject(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
        }

        public DeleteCountryPageObject ClickDelete()
        {
            this.ClickSubmit();
            return this;
        }
    }
}
