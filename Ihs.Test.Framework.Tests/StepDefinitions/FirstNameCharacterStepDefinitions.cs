using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Ihs.Test.Framework.Pages;
using Ihs.Test.Framework.Webdriver;
using NUnit.Framework;
using Ihs.Test.Framework.Common;

namespace Ihs.Test.Framework.Tests
{
    [Binding]
    public sealed class FirstNameCharacterStepDefinitions : UserActions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
        private EditorPage EditorPage;
        private readonly ScenarioContext _scenarioContext;
        private string _firstNameFirstChar;

        public FirstNameCharacterStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            EditorPage = new EditorPage((IDriver)_scenarioContext["driver"]);
        }

        [Given(@"I Click the Run button, with text in code editor as Console.WriteLine\(""(.*)""\)")]
        public void GivenIClickTheRunButtonWithTextInCodeEditorAsConsole_WriteLine(string customTesxt)
        {
            EditorPage.AmendCodeEditor(customTesxt);
        }

        [Then(@"The text in the output window should match the input text, and should therefore be (.*)")]
        public void ThenTheTextInTheOutputWindowShouldMatchTheInputText(string outputTest)
        {

            Assert.IsTrue(EditorPage.GetOutputWindowContent().Equals(outputTest),
                "Expected output window to contain {" + outputTest + "} But instead it had {" +
                EditorPage.GetOutputWindowContent() + "}");
        }

        [Given(@"My first name, (.*) starts with a particular letter")]
        public void GivenMyFirstNameStartsWithAParticularLetter(string firstName)
        {
            _firstNameFirstChar = firstName.ToUpper()[0].ToString();
            _scenarioContext.Add(CommonConstants.Step, 
                string.IsNullOrEmpty(firstName) ? "" : "=> " + firstName + " starts with letter (" + _firstNameFirstChar + ")");
        }

        [Then(@"Perform relevant actions")]
        public void ThenPerformRelevantActions()
        {
            if (CommonConstants.A_B_C_D_E.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + SelectNuGetPackages_Nunit_3_12_0);
                EditorPage.SelectNugetPackage("NUnit", "3.12.0");
            }
            else if (CommonConstants.F_G_H_I_J_K.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + ClickShareButton);
                EditorPage.ShowShareDialogModal();
            }
            else if (CommonConstants.L_M_N_O_P.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + ClickHideOptionsPanel);
                EditorPage.HideOptionsPanel();
            }
            else if(CommonConstants.Q_R_S_T_U.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + ClickSaveButton);
                EditorPage.TrySavingChangesForUnLoggedUser();
            }
            else if (CommonConstants.V_W_X_Y_Z.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + ClickGettingStartedButton);
                EditorPage.ClickGettingStartedNavButton();
            }
        }

        [Then(@"Verify outcomes are as expected")]
        public void ThenVerifyOutcomesAreAsExpected()
        {
            if (CommonConstants.A_B_C_D_E.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + ConfirmPageIsAdded);
                Assert.IsTrue(EditorPage.IsPackageAdded("NUnit"));
            }
            else if (CommonConstants.F_G_H_I_J_K.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + CheckShareLinkContains);
                Assert.IsTrue(EditorPage.GetShareLink().Contains(CommonConstants.DotnetFiddleWebsiteUrl));
            }
            else if (CommonConstants.L_M_N_O_P.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + CheckOptionsPanelIsHidden);
                Assert.IsFalse(EditorPage.IsOptionsSideBarWindowVisible());
            }
            else if (CommonConstants.Q_R_S_T_U.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + CheckLoginWindowAppeared);
                Assert.IsTrue(EditorPage.IsLoginModalVisible());
            }
            else if (CommonConstants.V_W_X_Y_Z.Contains(_firstNameFirstChar))
            {
                _scenarioContext.Add(CommonConstants.Step, "=> " + ConfirmBackToEditorButtonAppeared);
                Assert.IsTrue(EditorPage.IsUserNavigatedToGettingStartedPage());
            }
        }
    }
}
