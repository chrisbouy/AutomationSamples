using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SauceDemo;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SauceDemo.PageObjects
{
    public class BasePage
    {
        public IWebDriver _driver;
        private string _baseUrl => ConfigurationSettingsUtility.BaseUrl;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string URL
        {
            get
            {
                string _url = string.Empty;
                try
                {
                    _url = _driver.Url.ToLower();
                }
                catch { }
                return _url;
            }
        }

        public string PageTitle
        {
            get
            {
                string _pageTitle = string.Empty;
                try
                {
                    _pageTitle = _driver.Title.ToLower();
                }
                catch { }
                return _pageTitle;
            }
        }

        public void Goto(string url, bool useBaseUrl = true)
        {
            if (useBaseUrl)
            _driver.Navigate().GoToUrl(_baseUrl + "/" + url);

            else
                _driver.Navigate().GoToUrl(url);
        }

        protected void WaitForJS()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => (bool)((OpenQA.Selenium.IJavaScriptExecutor)_driver).ExecuteScript("return (document.readyState == 'complete' && jQuery.active == 0)"));
        }
    }
}
