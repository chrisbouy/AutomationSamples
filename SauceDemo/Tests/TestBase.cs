using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SauceDemo.PageObjects;
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

namespace SauceDemo.Tests
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
            ////try to get customer id.  May not exist yet.  May need to run register test first.
            ////Doing this here for debugging individual tests 
            //var r_options = new RestClientOptions("https://www.saucedemo.com/");
            //var client = new RestClient(r_options);
            //var request = new RestRequest("");
            //var response = client.Execute(request);
            ////Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    dynamic r = JsonConvert.DeserializeObject(response.Content);
            //    string customerId = r.id;
            //    request = new RestRequest("/customers/" + customerId + "/accounts");
            //    response = client.Execute(request);
            //    dynamic r2 = JsonConvert.DeserializeObject(response.Content);
            //    var accounts = (JObject)r2[0];
            //    customeraccount = accounts.Property("id").Value.ToString();
            //}
        }

        [SetUp]
        public void Setup()
        {
            if (typeof(TWebDriver).Name == "ChromeDriver")
            {
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");
                //options.PlatformName = "Windows";
                //options.PlatformName = "macOS Sonoma";
                Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                //Driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));
            }
            else if (typeof(TWebDriver).Name == "FirefoxDriver")
            {
                FirefoxOptions options = new FirefoxOptions();
                //options.BrowserExecutableLocation = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
                //options.PlatformName = "Windows";
                Driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                //Driver = new RemoteWebDriver(new Uri("http://localhost:5577/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));
                Driver.Manage().Window.Maximize();
            }
            Driver.Navigate().GoToUrl(_baseUrl);
            //read cookies from file if it exists
            Driver.Manage().Cookies.DeleteAllCookies();
            try
            {
                string filePath = "Cookies.data";
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        string strLine;
                        while ((strLine = sr.ReadLine()) != null)
                        {
                            string[] cookieInfo = strLine.Split(';');
                            string name = cookieInfo[0];
                            string value = cookieInfo[1];
                            string domain = cookieInfo[2];
                            string path = cookieInfo[3];
                            DateTime? expiry = null;
                            OpenQA.Selenium.Cookie ck = new OpenQA.Selenium.Cookie(name, value);
                            Console.WriteLine(ck);
                            Driver.Manage().Cookies.AddCookie(ck);
                        }
                    }
                    Console.WriteLine("Cookies loaded from file successfully.");
                }
                else
                {
                    Console.WriteLine("Cookies file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
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