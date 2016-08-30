using System.Linq;
using SeleniumDemo.Data;
using SeleniumDemo.IntegrationTests.KnownData.Model;
using SeleniumDemo.Models;

namespace SeleniumDemo.IntegrationTests.KnownData
{
    /// <summary>
    /// Generates and removes known data relating to Countries.
    /// </summary>
    public class CountriesDataGenerator
    {
        #region Constants

        private const string KnownCountryName = "England";

        private const string KnownCountryDescription = "Home of the Queen";

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates sample data relating to countries.
        /// </summary>
        /// <returns>Details of the created data.</returns>
        public CountriesKnownData EnsureSampleCountriesData()
        {
            using (var dataContext = new SeleniumDemoDataContext())
            {
                var country = new Country { Name = KnownCountryName, Description = KnownCountryDescription };
                dataContext.Countries.Add(country);
                dataContext.SaveChanges();

                return new CountriesKnownData
                       {
                           SampleCountry = country
                       };
            }
        }

        /// <summary>
        /// Ensures that any created sample data is cleaned up.
        /// </summary>
        public void EmptySampleCountriesData()
        {
            using (var dataContext = new SeleniumDemoDataContext())
            {
                // delete sample country
                var countries = dataContext.Countries;
                dataContext.Countries.RemoveRange(countries);
                dataContext.SaveChanges();
            }
        }

        #endregion
    }
}