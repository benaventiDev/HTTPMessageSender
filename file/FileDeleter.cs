using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPMessageSender.file
{
    public class FileDeleter
    {
        /*
        static void DeleteFilesByCriteria(string folderPath, string extension, string keyword)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(folderPath);
                FileInfo[] files = directory.GetFiles($"*{keyword}*.{extension}");

                foreach (FileInfo file in files)
                {
                    Console.WriteLine($"Deleting file: {file.Name}");
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }*/
        public static void DeleteDownloads(string DownloadFolder)
        {
            if (DownloadFolder.Equals(""))
            {
                return;
            }
            string folderPath = DownloadFolder;
            //string[] files = Directory.GetFiles(folderPath, "report*.xlsx");
            // Include both .xlsx and .csv files in the search pattern
            string[] xlsxFiles = Directory.GetFiles(folderPath, "report*.xlsx");
            string[] csvFiles = Directory.GetFiles(folderPath, "report*.csv");
            string[] files = xlsxFiles.Concat(csvFiles).ToArray();
            try
            {
                foreach (string file in files)
                {
                    File.Delete(file);                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not delete one or more files.");
            }

        }
    }
}
