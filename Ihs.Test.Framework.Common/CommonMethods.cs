using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ihs.Test.Framework.Common
{
    public class CommonMethods
    {
        public static bool IsDirectoryExist(string dir)
        {
            if (Directory.Exists(dir))
                return true;

            return false;
        }

        public static bool IsFileExist(string filePath)
        {
            if (File.Exists(filePath))
                return true;

            return false;
        }

        public static void CreateDirectoryIfDoesntExist(string dir)
        {
            if (!IsDirectoryExist(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
