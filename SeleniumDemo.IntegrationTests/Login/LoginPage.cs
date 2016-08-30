namespace SeleniumDemo.IntegrationTests.Login
{
    /// <summary>
    /// Single entry point to access all pages requiring authentication on the site.
    /// </summary>
    public class LoginPage
    {
        /// <summary>
        /// Gets or sets an object that provides access to all pages from the Countries controller.
        /// </summary>
        public CountriesLoginPage CountriesLogin { get; set; }
    }
}