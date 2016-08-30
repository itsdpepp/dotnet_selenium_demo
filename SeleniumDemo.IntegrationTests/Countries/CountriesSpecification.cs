using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumDemo.IntegrationTests.KnownData;
using SeleniumDemo.IntegrationTests.KnownData.Model;

namespace SeleniumDemo.IntegrationTests.Countries
{
    /// <summary>
    /// The UI tests relating to pages which handle countries.
    /// </summary>
    [TestClass]
    public class CountriesSpecification : IntegrationSpecificationBase
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the CountriesSpecification class.
        /// </summary>
        public CountriesSpecification()
        {
            this.countriesDataGenerator = new CountriesDataGenerator();
        }

        #endregion

        #region Fields
        /// <summary>
        /// An object that generates and removes sample data.
        /// </summary>
        private readonly CountriesDataGenerator countriesDataGenerator;

        /// <summary>
        /// Details of the sample data with which the database is populated.
        /// </summary>
        private CountriesKnownData knownData;
        #endregion

        [TestMethod]
        public void Index_page_lists_known_countries()
        {
            // act
            var indexPage = this.LoginPage.CountriesLogin.GetIndexPage();

            // assert - the known data generator creates a single country record with known name and description 
            indexPage.GetNumberOfCountryDataRows().Should().Be(1);
            indexPage.GetFirstCountryName().Should().Be(this.knownData.SampleCountry.Name);
            indexPage.GetFirstCountryDescription().Should().Be(this.knownData.SampleCountry.Description);
        }

        [TestMethod]
        public void Index_page_does_not_show_country_after_delete_page_submitted()
        {
            // act
            var deletePage = this.LoginPage.CountriesLogin.GetDeleteCountryPage(this.knownData.SampleCountry.Id);
            deletePage.ClickDelete();
            var indexPage = this.LoginPage.CountriesLogin.GetIndexPage();

            // assert
            indexPage.GetNumberOfCountryDataRows().Should().Be(0);
        }

        [TestMethod]
        public void View_page_shows_known_country_details()
        {
            // act
            var viewPage = this.LoginPage.CountriesLogin.GetViewCountryPage(this.knownData.SampleCountry.Id);

            // assert
            viewPage.GetCountryName().Should().Be(this.knownData.SampleCountry.Name);
            viewPage.GetCountryDescription().Should().Be(this.knownData.SampleCountry.Description);
        }
        
        [TestMethod]
        public void View_page_shows_updated_details_after_update_page_submitted()
        {
            // arrange
            const string NewCountryName = "Japan";
            const string NewCountryDescription = "Where Mount Fuji lives";

            // act
            var updatePage = this.LoginPage.CountriesLogin.GetEditCountryPage(this.knownData.SampleCountry.Id);
            updatePage
                .SetCountryName(NewCountryName)
                .SetCountryDescription(NewCountryDescription)
                .ClickSave();

            var viewPage = this.LoginPage.CountriesLogin.GetViewCountryPage(this.knownData.SampleCountry.Id);

            // assert
            viewPage.GetCountryName().Should().Be(NewCountryName);
            viewPage.GetCountryDescription().Should().Be(NewCountryDescription);
        }

        [TestMethod]
        public void Index_page_shows_country_details_after_add_page_submitted()
        {
            // arrange
            const string SampleName = "France";
            const string SampleDescription = "Where the Arc De Triomphe lives";
            this.countriesDataGenerator.EmptySampleCountriesData(); // simplifies finding record on index page

            // act
            var addCountryPage = this.LoginPage.CountriesLogin.GetAddCountryPage();
            addCountryPage
                .EnterCountryName(SampleName)
                .EnterCountryDescription(SampleDescription)
                .ClickSave();

            var indexPage = this.LoginPage.CountriesLogin.GetIndexPage();
            
            // assert - 
            indexPage.GetNumberOfCountryDataRows().Should().Be(1);
            indexPage.GetFirstCountryName().Should().Be(SampleName);
            indexPage.GetFirstCountryDescription().Should().Be(SampleDescription);
        }

        #region Setup
        /// <summary>
        /// Before each test call the base class  initialize method then clean and re-generate sample data.
        /// </summary>
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            
            this.countriesDataGenerator.EmptySampleCountriesData();
            this.knownData = this.countriesDataGenerator.EnsureSampleCountriesData();
        }

        /// <summary>
        /// After each test call the base class tidy up method.
        /// </summary>
        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        #endregion
    }
}