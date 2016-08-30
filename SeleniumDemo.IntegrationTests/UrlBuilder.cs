using System.Configuration;

namespace SeleniumDemo.IntegrationTests
{
    /// <summary>
    /// Provides URLs to be used in UI tests
    /// </summary>
    public class UrlBuilder
    {
        #region Core

        public string BaseUrl()
        {
            var portNumber = ConfigurationManager.AppSettings["HostedWebsitePort"];
            return string.Format("http://localhost:{0}", portNumber);
        }

        #endregion

        #region Login

        public string LoginUrl()
        {
            return this.BaseUrl() + "/";
        }

        #endregion

        #region Country pages

        private string CountryBaseUrl()
        {
            return this.BaseUrl() + "/Countries/";
        }

        public string CountryIndexUrl()
        {
            return this.CountryBaseUrl();
        }

        public string AddCountryUrl()
        {
            return this.CountryBaseUrl() + "Create";
        }

        public string DeleteCountryUrl(int countryId)
        {
            return this.CountryBaseUrl() + "Delete/" + countryId;
        }

        public string ViewCountryUrl(int countryId)
        {
            return this.CountryBaseUrl() + "Details/" + countryId;
        }

        public string EditCountryUrl(int countryId)
        {
            return this.CountryBaseUrl() + "Edit/" + countryId;
        }

        #endregion
    }
}