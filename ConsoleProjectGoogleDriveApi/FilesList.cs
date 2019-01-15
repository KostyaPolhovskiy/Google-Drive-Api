using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;


namespace ConsoleProjectGoogleDriveApi
{
    class FilesList
    {

        /// <summary>
        /// Retrieve a list of File resources.
        /// </summary>
        /// <param name="service">Drive API service instance.</param>
        /// <returns>List of File resources.</returns>
        /// Carefully! type 'File' in namespace Google.Apis.Drive.v2.Data
        public static List<File> RetrieveAllFiles(DriveService service)
        {
            List<File> result = new List<File>();
            FilesResource.ListRequest request = service.Files.List();

            do
            {
                try
                {
                    FileList files = request.Execute();

                    result.AddRange(files.Items);
                    request.PageToken = files.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));
            return result;
        }
        
    }
}
