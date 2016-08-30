using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium;
using SeleniumDemo.IntegrationTests.IntegrationTests;

namespace SeleniumDemo.IntegrationTests.Countries.PageObjects
{
    /// <summary>
    /// Page access and control manipulate for the add Country page.
    /// </summary>
    public class AddCountryPageObject : PageObjectBase<AddCountryPageObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public AddCountryPageObject(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
        }

        public AddCountryPageObject EnterCountryName(string name)
        {
            this.EnterText("Name", name);
            return this;
        }

        public AddCountryPageObject EnterCountryDescription(string description)
        {
            this.EnterText("Description", description);
            return this;
        }

        public AddCountryPageObject ClickSave()
        {
            this.ClickSubmit();
            return this;
        }
    }
}