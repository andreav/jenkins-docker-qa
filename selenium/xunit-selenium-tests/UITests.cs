using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using System.Linq;

namespace xunit_selenium_tests;

public enum BrowserType
{
    NotSet,
    Chrome,
    Firefox,
    Edge,
}


public class UITests
{
    private readonly ITestOutputHelper output;
    private readonly string selenium_hub_address;

    public UITests(ITestOutputHelper output)
    {
        this.output = output;
        this.selenium_hub_address = Environment.GetEnvironmentVariable("SELENIUM_HUB_ADDRESS") ?? "http://localhost:4444";
    }

    public static IWebDriver Create_Browser(BrowserType browserType)
    {
        switch (browserType)
        {
            case BrowserType.Chrome:
                return new ChromeDriver();
            case BrowserType.Firefox:
                return new FirefoxDriver();
            case BrowserType.Edge:
                return new EdgeDriver();
            default:
                throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
        }
    }

    [Fact]
    [Trait("test", "TESTPROJ-10")]
    [Trait("requirement","TESTPROJ-9")]
    public void Test_Chrome_SpecificVersion()
    {
        var chromeOptions = new ChromeOptions();
        // chromeOptions.BrowserVersion = "67";
        // chromeOptions.PlatformName = "Windows XP";
        IWebDriver driver = new RemoteWebDriver(new Uri(selenium_hub_address), chromeOptions);
        driver.Navigate().GoToUrl("http://www.google.com");
        driver.Quit();

    }

    [Theory]
    [InlineData(typeof(ChromeOptions))]
    [InlineData(typeof(FirefoxOptions))]
    [Trait("Category","inlinedata")]
    public void Test_Theory_InlineBrowser(Type DriverOptionsType)
    {
        DriverOptions chromeOptions = Activator.CreateInstance(DriverOptionsType) as DriverOptions ?? throw new ArgumentNullException("Invalid type", nameof(DriverOptionsType));
        IWebDriver driver = new RemoteWebDriver(new Uri(selenium_hub_address), chromeOptions);
        driver.Navigate().GoToUrl("http://www.google.com");
        driver.Quit();
    }

    private static List<object[]> MultiplyByBrowser(List<object[]> test_params)
    {
        var retParams = new List<object[]>();
        foreach (var curr_params in test_params)
        {
            retParams.Add(curr_params.Prepend(typeof(ChromeOptions)).ToArray());
            retParams.Add(curr_params.Prepend(typeof(FirefoxOptions)).ToArray());
            retParams.Add(curr_params.Prepend(typeof(EdgeOptions)).ToArray());
        }
        return retParams;
    }

    protected static List<object[]> Test_Parametrized_MultipleBrowsers_Params()
    {
        return MultiplyByBrowser(new List<object[]>() {
            new object[] { "https://www.google.com"},
            new object[] { "https://www.bing.com"},
        });
    }

    [Theory]
    [MemberData(nameof(Test_Parametrized_MultipleBrowsers_Params))]
    public void Test_Parametrized_MultipleBrowsers(Type DriverOptionsType, string url)
    {
        DriverOptions driverOptions = Activator.CreateInstance(DriverOptionsType) as DriverOptions ?? throw new ArgumentNullException("Invalid type", nameof(DriverOptionsType));
        using (IWebDriver driver = new RemoteWebDriver(new Uri(selenium_hub_address), driverOptions))
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}