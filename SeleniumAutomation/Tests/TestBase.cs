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
        public string customeraccount { get; set; }
        private string _baseUrl => ConfigurationSettingsUtility.BaseUrl;

        [OneTimeSetUp]
        public void BeforeClass()
        {
        }

        [SetUp]
        public void Setup()
        {
            if (typeof(TWebDriver).Name == "ChromeDriver")
            {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");
                Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                //Driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));
            }
            else if (typeof(TWebDriver).Name == "FirefoxDriver")
            {
                FirefoxOptions options = new FirefoxOptions();
                Driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                //Driver = new RemoteWebDriver(new Uri("http://localhost:5577/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));
                Driver.Manage().Window.Maximize();
            }
            Driver.Navigate().GoToUrl(_baseUrl);
        }

        [TearDown]
        public void AfterTest()
        {
            Driver.Quit();
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
        }

    }
}