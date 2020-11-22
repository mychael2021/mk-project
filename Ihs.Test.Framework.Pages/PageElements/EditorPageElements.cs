using System;
using System.Collections.Generic;
using System.Text;

namespace Ihs.Test.Framework.Pages
{
    public class EditorPageElements
    {
        internal string RunButton => "run-button";
        internal string OutputWindow => "output";
        internal string UpdatedOutputWindow (string input)
        {
            return "//div[@id='" + OutputWindow + "'][text()='"+ input + "']";
        }
        internal string NugetPackageInput => "//input[contains(@class,'new-package')]";
        internal string NugetPackagesListItems => "//ul[@id='menu']";
        internal string NugetSpecificPackage(string package)
        {
            //return "//a[@package-id='"+ package + "']//span[contains(@class,'ui-menu-icon')]";
            return "//a[@package-id='" + package + "']";
        }

        internal string NugetPackageVersionLists(string package)
        {
            return $"//ul//li//a[@package-id='{package}']";
        }

        internal string NugetPackageVersion(string version)
        {
            return $"//ul//a[text()='{version}']";
        }

        internal string SelectedNugetPackage => "//div[@class='package-name']";
        internal string ShareButton => "Share";
        internal string ShareLoadingLayerGif => "//div[@class='loading-layer']//img";
        internal string ShareDialog => "share-dialog";
        internal string ShareLink => "ShareLink";
        internal string OptionsPanel => "//div[@class='sidebar unselectable']";
        internal string OptionsLeftChevron => "//div[@class='sidebar-block']//span[contains(@class,'chevron-left')]";
        internal string SaveButton => "save-button";
        internal string LogingWindow => "login-modal";
        internal string GettingStartedButton => "//a[text()[contains(.,'Getting Started')]]";
        internal string BackToEditorButton => "//a[text()[contains(.,'Back To Editor')]]";
        internal string Loader => "stats-loader";
        internal string VersionsLoading => "Versions loading...";
        internal string CodeEditorContent => "//pre[@class=' CodeMirror-line ']";
    }
}
