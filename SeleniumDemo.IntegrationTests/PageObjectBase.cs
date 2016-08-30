
namespace SeleniumDemo.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;


    namespace IntegrationTests
    {
        /// <summary>
        /// Base class which provides common functionality for finding and editing page elements and controls to all inheriting classes. 
        /// </summary>
        public abstract class PageObjectBase<T> where T : class
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Object"/> class.
            /// </summary>
            protected PageObjectBase(IWebDriver webDriver, UrlBuilder urlBuilder)
            {
                this.WebDriver = webDriver;
                this.UrlBuilder = urlBuilder;
            }

            #endregion

            #region Fields

            protected readonly IWebDriver WebDriver;

            protected readonly UrlBuilder UrlBuilder;

            private bool acceptNextAlert = true;

            #endregion

            #region Element finding methods

            protected bool IsElementPresent(By by)
            {
                try
                {
                    this.WebDriver.FindElement(by);
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }

            protected bool IsAlertPresent()
            {
                try
                {
                    this.WebDriver.SwitchTo().Alert();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            }

            protected SelectElement FindSelectElement(By selectIdentifier)
            {
                var element = this.WebDriver.FindElement(selectIdentifier);
                return new SelectElement(element);
            }

            protected IWebElement FindElementById(string id)
            {
                return this.WebDriver.FindElement(By.Id(id));
            }

            protected IEnumerable<IWebElement> FindElementsByCssSelector(string selector)
            {
                return this.WebDriver.FindElements(By.CssSelector(selector));
            }

            protected IWebElement FindElementByCssSelector(string selector)
            {
                return this.WebDriver.FindElement(By.CssSelector(selector));
            }

            protected IWebElement FindElementByClass(string className)
            {
                return this.WebDriver.FindElement(By.ClassName(className));
            }

            protected IEnumerable<IWebElement> FindElementsByClass(string className)
            {
                return this.WebDriver.FindElements(By.ClassName(className));
            }

            #endregion

            #region Page manipulation methods

            protected string CloseAlertAndGetItsText()
            {
                try
                {
                    IAlert alert = this.WebDriver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    if (this.acceptNextAlert)
                    {
                        alert.Accept();
                    }
                    else
                    {
                        alert.Dismiss();
                    }
                    return alertText;
                }
                finally
                {
                    this.acceptNextAlert = true;
                }
            }

            protected T SelectDropDownListItem(string elementId, string selectedText)
            {
                return this.SelectDropDownListItem(By.Id(elementId), selectedText);
            }

            protected T SelectDropDownListItem(By elementIdentifier, string selectedText)
            {
                var selectElement = this.FindSelectElement(elementIdentifier);

                if (string.IsNullOrEmpty(selectedText))
                {
                    this.DeselectDropDownList(elementIdentifier);
                }
                else
                {
                    selectElement.SelectByText(selectedText);
                }

                return this as T;
            }

            protected T SelectMulitpleListItems(string elementId, params string[] selectedItemsText)
            {
                return this.SelectMulitpleListItems(By.Id(elementId), selectedItemsText);
            }

            protected T SelectMulitpleListItems(By elementIdentifier, params string[] selectedItemsText)
            {
                var selectElement = this.FindSelectElement(elementIdentifier);
                selectElement.DeselectAll();

                if (selectedItemsText.Any())
                {
                    foreach (var selectItemText in selectedItemsText)
                    {
                        selectElement.SelectByText(selectItemText);
                    }
                }

                return this as T;
            }

            protected T DeselectDropDownList(string elementId)
            {
                return this.DeselectDropDownList(By.Id(elementId));
            }

            protected T DeselectDropDownList(By elementIdentifier)
            {
                var selectElement = this.FindSelectElement(elementIdentifier);
                selectElement.SelectByIndex(0);

                return this as T;
            }

            protected T DeselectMultipleItemsList(string elementId)
            {
                return this.SelectMulitpleListItems(elementId);
            }

            protected T EnterText(string elementId, string text)
            {
                return this.EnterText(By.Id(elementId), text);
            }

            protected T EnterText(By elementIdentifier, string text)
            {
                var element = this.WebDriver.FindElement(elementIdentifier);
                element.Clear();
                element.SendKeys(text);
                return this as T;
            }

            protected T SelectCheckBox(string elementId)
            {
                return this.SetCheckBoxState(elementId, true);
            }

            protected T UnselectCheckBox(string elementId)
            {
                return this.SetCheckBoxState(elementId, false);
            }

            protected T SetCheckBoxState(string elementId, bool check)
            {
                return this.SetCheckBoxState(By.Id(elementId), check);
            }

            protected T SetCheckBoxState(By elementIdentifier, bool check)
            {
                var element = this.WebDriver.FindElement(elementIdentifier);
                if (element.Selected && !check || !element.Selected && check)
                {
                    element.Click();
                }

                return this as T;
            }

            protected T ClickSubmit()
            {
                this.FindElementByCssSelector("input[type=submit]").Click();
                return this as T;
            }

            public T DismissDatePicker()
            {
                // Click on a random h1 to stop the DatePicker being shown an obsuring following elements
                this.WebDriver.FindElements(By.CssSelector("h1")).First().Click();

                // Give the browser a chance for the closing animation to render
                Thread.Sleep(1000);

                return this as T;
            }

            #endregion

            #region Page reading methods

            protected string GetTextboxText(string elementId)
            {
                return this.GetTextboxText(By.Id(elementId));
            }

            protected string GetTextboxText(By elementIdentifier)
            {
                var element = this.WebDriver.FindElement(elementIdentifier);
                return element.GetAttribute("value");
            }

            protected bool GetCheckboxSelected(string elementId)
            {
                return this.GetCheckboxSelected(By.Id(elementId));
            }

            protected bool GetCheckboxSelected(By elementIdentifier)
            {
                var element = this.WebDriver.FindElement(elementIdentifier);
                return element.Selected;
            }

            protected IEnumerable<string> GetAllSelectOptionTexts(string elementId)
            {
                return this.GetAllSelectOptionTexts(By.Id(elementId));
            }

            protected IEnumerable<string> GetAllSelectOptionTexts(By elementIdentifier)
            {
                return this.FindSelectElement(elementIdentifier).Options.Select(option => option.Text);
            }

            protected string GetSelectedDropDownListText(By elementIdentifier)
            {
                return this.FindSelectElement(elementIdentifier).SelectedOption.Text;
            }

            public virtual string GetValidationSummaryText()
            {
                var validationSummary = this.FindElementByClass("validation-summary-errors");
                if (validationSummary == null)
                {
                    return string.Empty;
                }

                return validationSummary.Text;
            }

            public virtual bool IsValidationErrorDisplayed()
            {
                return !String.IsNullOrEmpty(this.GetValidationSummaryText());
            }

            #endregion
        }
    }

}