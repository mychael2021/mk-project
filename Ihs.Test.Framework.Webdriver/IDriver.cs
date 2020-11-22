using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Ihs.Test.Framework.Webdriver
{
    public interface IDriver
    {
        IWebDriver Browser();
        void StopBrowser();
        void StartBrowser(string browser = "chrome", string path = "");
        void Click(string locator);
        void ClickAllVisible(string locator);
        void ClickAllPresent(string locator);
        void WaitForPageLoad(TimeSpan? span = null);
        void Navigate(string url);
        IWebElement FindElementVisible(string locator);
        IWebElement FindElementVisible(IWebElement element);
        IWebElement FindElementPresent(string locator);
        bool IsElementVisible(string locator);
        void WaitForElementVisible(string locator, double timeInSeconds = 60);
        void Type(string locator, string textToSend, bool attemptReEntry = true);
        void SendKeyBoardKeys(string element, List<string> keys);
        void SendKeyBoardKey(string element, string key);
        void SendKeyBoardKey(string key);
        void SendKeyBoardKeys(string[] keys);
        IList<IWebElement> GetVisibleElements(string locator);
        void MoveToElement(string locator);
        void MoveToElement(IWebElement el);
        bool IsTextPresent(string text);
        string GetText(string locator);
        string BrowserName();
        void Dispose();
        IWebElement FindElementEnabled(string locator);
        void WaitFor(double timeInSec);
        bool IsTextVisible(string text);
        void WaitForElementNotVisible(string locator, double timeInSeconds = 60);
        void WaitForTextVisible(string text, double timeInSeconds = 60);
        string GetUrl();
        void WaitForElementClickable(string element, double timeSpan = 60);
        bool IsElementClickable(IWebElement element);
        bool IsElementClickable(string locator);
        void WaitForElementNotClickable(string element, double timeInSeconds = 60);
        void WaitForElementNotClickable(IWebElement element, double timeInSeconds = 60);
        void WaitForElementNotVisible(IWebElement element, double timeInSeconds = 60);
        bool IsElementVisible(IWebElement element);
        bool IsElementPresent(string locator);
        bool IsElementPresent(IWebElement element);
        void WaitForTextNotVisible(string text, double timeInSeconds = 60);
        void WaitForElementPresent(string locator, double timeInSeconds = 60);
        string GetValue(string locator);
        void WaitUntilElementTextChanges(string element, string expectedText, double timeInSeconds = 60);
    }
}
