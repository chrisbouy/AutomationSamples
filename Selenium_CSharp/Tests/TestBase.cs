using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SeleniumAutomation;

namespace SeleniumAutomation.Tests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    public class TestBase<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        public IWebDriver Driver { get; set; }
        private string _baseUrl => ConfigurationSettingsUtility.BaseUrl;

        [OneTimeSetUp]
        public void BeforeClass()
        {
        }

        [SetUp]
        public void Setup()
        {
            try
            {
                Driver = InitializeDriver(typeof(TWebDriver));
                Driver.Navigate().GoToUrl(_baseUrl);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error during setup: {ex.Message}");
                throw;
            }
        }

        private IWebDriver InitializeDriver(Type driverType)
        {
            if (driverType == typeof(ChromeDriver))
            {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");
                return new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            }
            else if (driverType == typeof(FirefoxDriver))
            {
                var options = new FirefoxOptions();
                var driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                driver.Manage().Window.Maximize();
                return driver;
            }
            // Add other browser drivers initialization as needed
            throw new NotSupportedException($"Driver type '{driverType.Name}' is not supported.");
        }
        [TearDown]
        public void AfterTest()
        {
            Driver?.Quit();
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
        }

    }
}