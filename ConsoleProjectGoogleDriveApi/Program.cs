using System;
using System.Collections.Generic;

namespace ConsoleProjectGoogleDriveApi
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveServiceAccount driveServiceAccount = new DriveServiceAccount();
            List<string> filesTittleList = driveServiceAccount.GetFilesTittleList();
            foreach (String tittle in filesTittleList)
            {
                Console.WriteLine(tittle);
            }
            Console.ReadLine();
        }
    }
}
