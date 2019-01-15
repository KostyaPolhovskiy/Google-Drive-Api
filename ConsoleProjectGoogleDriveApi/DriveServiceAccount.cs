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
        }

        public List<String> GetFilesTittleList()
        {
            // Retrieve all files from GoogleDrive  
            List <Google.Apis.Drive.v2.Data.File> filesList = FilesList.RetrieveAllFiles(Service);

            //Choose only Tittles of Files
            List<String> filesTittleList = new List<String>(); 
            foreach(Google.Apis.Drive.v2.Data.File file in filesList)
            {
                filesTittleList.Add(file.Title);
            }
            return filesTittleList;

        }
    }
}
