using OpenQA.Selenium;
using SeleniumDemo.IntegrationTests.IntegrationTests;

namespace SeleniumDemo.IntegrationTests.Countries.PageObjects
{
    /// <summary>
    /// Page access and control manipulation for edit Country the page.
    /// </summary>
    public class EditCountryPageObject : PageObjectBase<EditCountryPageObject>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public EditCountryPageObject(IWebDriver webDriver, UrlBuilder urlBuilder) : base(webDriver, urlBuilder)
        {
        }

        public EditCountryPageObject SetCountryName(string name)
        {
            this.EnterText("Name", name);
            return this;
        }

        public EditCountryPageObject SetCountryDescription(string description)
        {
            this.EnterText("Description", description);
            return this;
        }

        public EditCountryPageObject ClickSave()
        {
            this.ClickSubmit();
            return this;
        }
    }
}