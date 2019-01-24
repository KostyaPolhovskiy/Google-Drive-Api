using System;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;


/// Carefully! type 'File' in namespace Google.Apis.Drive.v2.Data

namespace ConsoleProjectGoogleDriveApi
{
    class ConfigureFiles
    {
        private readonly DriveService service;

        public ConfigureFiles(DriveService service)
        {
            this.service = service;
        }

        //Загрузка потока файла, наподобие LogBuffer только на облако
        public void UploadFile()
        {

        }

        
        //Return file ID
        public string UploadFileFromFileDialog()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            FilesResource.InsertMediaUpload request;
            File file = new File();

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "..";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                                
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    var fileExt = openFileDialog.DefaultExt;
                    

                    file = new File()
                    {
                        Title = filePath.Remove(0, filePath.LastIndexOf('\\') + 1),
                        FileExtension = fileExt
                        //try to add more information about uploading file (from openFileDialog)
                        
                    };

                    request = service.Files.Insert(file, fileStream, fileExt);
                    request.Fields = "id";
                    request.Upload();

                    file = request.ResponseBody;
                   
                }
            }

            return file.Id;
        }
    }
}
