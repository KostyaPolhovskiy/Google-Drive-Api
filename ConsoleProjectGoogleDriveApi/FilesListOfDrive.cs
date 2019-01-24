using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System.Collections.Generic;
using System.Linq;

/// Carefully! type 'File' in namespace Google.Apis.Drive.v2.Data

namespace ConsoleProjectGoogleDriveApi
{
    class FilesListOfDrive
    {
        private readonly DriveService Service;
        private readonly List<File> ListOfFiles;

        //Инициализация ListOfFiles
        public FilesListOfDrive(DriveService service)
        {
            this.Service = service;

            this.ListOfFiles = new List<File>();
            FilesResource.ListRequest request = service.Files.List();

            do
            {
                try
                {
                    FileList files = request.Execute();

                    ListOfFiles.AddRange(files.Items);
                    request.PageToken = files.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));
        }
       
        
        public List<File> RetrieveAllFiles()
        {
            return ListOfFiles;
        }

        //Получение всех свойств файла, необходимо их перечислить, File не имеет enum
        //Использование List<KeyValuePair<string, string>> вместо Dictionary<string, string> потому что выскакивает исключение с повторением свойста
        //можно чекнуть и разобраться
        public List<KeyValuePair<string, string>> RetrieveFileInfo(string fileName)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            foreach (File file in ListOfFiles)
            {
                if (fileName == file.Title)
                {
                    
                    foreach(var property in PropertiesOfType<string>(file))
                    {
                        result.Add(new KeyValuePair<string, string>(property.Key, property.Value));
                    }
                    
                }
            }

            return result;
        }


        //Перечислитель для свойств, for example
        //                  foreach(var property in PropertiesOfType<string>(file))
        public static IEnumerable<KeyValuePair<string, T>> PropertiesOfType<T>(object obj)
        {
            return from p in obj.GetType().GetProperties()
                   where p.PropertyType == typeof(T)
                   select new KeyValuePair<string, T>(p.Name, (T)p.GetValue(obj));
        }

    }
}
