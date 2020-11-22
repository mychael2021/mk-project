using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ihs.Test.Framework.Common;
using Ihs.Test.Framework.Webdriver;
using OpenQA.Selenium;

namespace Ihs.Test.Framework.Pages
{
    public class EditorPage : EditorPageElements
    {
        IDriver _driver;
        public EditorPage(IDriver driver)
        {
            _driver = driver;
        }

        public void AmendCodeEditor(string updateString)
        {
            _driver.WaitForPageLoad();
            var d = _driver.GetVisibleElements(CodeEditorContent);
            foreach (var s in d)
            {
                if(s.Text.Contains("Console.WriteLine"))
                {
                    _driver.MoveToElement(s);
                    _driver.FindElementVisible(s).Click();
                    var c = s.Text.Split('(').LastOrDefault().ToCharArray().Count();
                    for (int i = 0; i< c - 1; i++)
                    {
                        _driver.SendKeyBoardKey(Keys.Backspace);
                    }

                    _driver.SendKeyBoardKeys(updateString.ToCharArray().Select(a => a.ToString()).ToArray());
                    _driver.SendKeyBoardKeys(new string[] { "\"", ")", ";" } );
                    break;
                }
            }

            var outputWindow = _driver.FindElementVisible(OutputWindow);
            _driver.Click(RunButton);
            _driver.WaitFor(0.6);
            _driver.WaitForTextNotVisible(Loader);
            _driver.WaitUntilElementTextChanges(OutputWindow, updateString, 10);
        }
        public string GetOutputWindowContent()
        {
            if (_driver.GetText(OutputWindow) == null)
                return string.Empty;

            return _driver.GetText(OutputWindow);
        }

        public void SelectNugetPackage(string packageName, string version)
        {
            _driver.Type(NugetPackageInput, packageName);
            _driver.WaitForElementVisible(NugetPackagesListItems);
            _driver.ClickAllVisible(NugetSpecificPackage(packageName));
            _driver.WaitForTextNotVisible(VersionsLoading);
            if (_driver.BrowserName() != CommonConstants.Chrome)
            {
                _driver.FindElementVisible(NugetSpecificPackage(packageName)).SendKeys(Keys.Space);
                _driver.SendKeyBoardKey(Keys.ArrowDown);
                _driver.WaitForTextNotVisible(VersionsLoading);
            }
            
            _driver.WaitFor(0.5);
            if (!string.IsNullOrEmpty(version))
            {
                if(!_driver.IsElementVisible(NugetPackageVersion(version)))
                {
                    _driver.ClickAllVisible(NugetSpecificPackage(packageName));
                    _driver.WaitForTextNotVisible(VersionsLoading);
                    _driver.WaitFor(0.5);
                }

                _driver.MoveToElement(NugetPackageVersion(version));
                _driver.Click(NugetPackageVersion(version));
            }
            _driver.WaitForElementVisible(SelectedNugetPackage, 5);
        }

        public void ShowShareDialogModal()
        {
            _driver.WaitForElementClickable(ShareButton);
            _driver.Click(ShareButton);
            _driver.WaitForElementNotClickable(ShareLoadingLayerGif);
            _driver.WaitForElementClickable(ShareDialog);
            _driver.WaitForElementVisible(ShareLink);
        }

        public void HideOptionsPanel()
        {
            _driver.ClickAllVisible(OptionsLeftChevron);
            _driver.WaitForElementNotClickable(OptionsLeftChevron, 5);
        }

        public void TrySavingChangesForUnLoggedUser()
        {
            _driver.ClickAllVisible(SaveButton);
            _driver.WaitForElementVisible(LogingWindow, 15);
        }

        public void ClickGettingStartedNavButton()
        {
            _driver.Click(GettingStartedButton);
            _driver.WaitForElementNotClickable(ShareButton, 15);
        }

        public bool IsElementOnThePage(string locator)
        {
            _driver.WaitForPageLoad();
            return _driver.IsElementVisible(locator);
        }

        public string GetShareLink()
        {
            return _driver.GetValue(ShareLink);
        }

        public bool IsPackageAdded(string packageName)
        {
            if (_driver.IsElementVisible(SelectedNugetPackage) &&
                _driver.FindElementVisible(SelectedNugetPackage).Text.Contains(packageName))
                return true;
            return false;
        }

        public bool IsOptionsSideBarWindowVisible()
        {
            return _driver.IsElementVisible(OptionsLeftChevron);
        }

        public bool IsLoginModalVisible()
        {
            return _driver.IsElementVisible(LogingWindow);
        }

        public bool IsUserNavigatedToGettingStartedPage()
        {
            if (_driver.IsElementVisible(BackToEditorButton) && _driver.IsElementVisible("Getting Started"))
                return true;
            return false;
        }
    }
}
