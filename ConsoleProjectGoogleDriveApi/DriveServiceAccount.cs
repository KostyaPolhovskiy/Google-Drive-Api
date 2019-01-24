using System;
using System.IO;
using Google.Apis.Drive.v2;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.Collections.Generic;


namespace ConsoleProjectGoogleDriveApi
{
    class DriveServiceAccount
    {

        private readonly DriveService Service;
        
        //необходим update данных
        private readonly FilesListOfDrive FilesList;

        private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };

        public DriveServiceAccount()
        {
            GoogleWebAuthorizationBroker.Folder = "Drive.Sample";
            UserCredential credential;
            using (var stream = new FileStream("..//..//client_secrets.json",
                FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None).Result;
            }

            // Create the service.
            Service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Drive API",
            });

            //Set init in constructor, try to solve! This is only DriveServiceAccount
            FilesList = new FilesListOfDrive(Service);
        }

        public List<String> GetFilesTitleList()
        {
            // Retrieve all files 
            List <Google.Apis.Drive.v2.Data.File> filesList = FilesList.RetrieveAllFiles();

            //Choose only Tittles of Files
            List<String> filesTitleList = new List<String>(); 
            foreach(Google.Apis.Drive.v2.Data.File file in filesList)
            {
                filesTitleList.Add(file.Title);               
            }
            return filesTitleList;
        }

        public void GetFileInfoToConsole(string fileName)
        {
            var fileFields = FilesList.RetrieveFileInfo(fileName);
            foreach(KeyValuePair<string, string> fieldValue in fileFields)
            {
                Console.WriteLine("Key = {0}, Value = {1}", fieldValue.Key, fieldValue.Value);
            }
        }

        //If i want to add delete or download -> init ConfFiles in constructor
        public void UploadFile()
        {
            ConfigureFiles ConfFiles = new ConfigureFiles(Service);
            if (ConfFiles.UploadFileFromFileDialog() == null)
            {
                Console.WriteLine("File was not upload");
            }

        }
    }
}
