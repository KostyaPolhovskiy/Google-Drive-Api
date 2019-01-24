using System;
using System.Collections.Generic;

namespace ConsoleProjectGoogleDriveApi
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DriveServiceAccount driveServiceAccount = new DriveServiceAccount();
            //driveServiceAccount.UploadFile();

            //temply call
            driveServiceAccount.GetFileInfoToConsole("test.js");
            //temply call 2
            driveServiceAccount.GetFileInfoToConsole("test 2.js");

            //List<string> filesTitleList = driveServiceAccount.GetFilesTitleList();
            //foreach (String title in filesTitleList)
            //{
            //    Console.WriteLine(title);
            //}
            Console.ReadLine();
        }
    }
}
