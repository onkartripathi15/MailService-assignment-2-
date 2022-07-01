using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailService.WebApi.DataContext;
using MailService.WebApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {


            using (var results = new ApplicationDbContext())
            {
             
                var st = results.user.ToList();

                string sourceFile = "";
                string destinationFolderName = "";
                foreach (UserClass stobj in st)
                {

                    sourceFile = stobj.source.ToString();
                    destinationFolderName = stobj.destination.ToString();

                }
                Console.WriteLine(sourceFile);

                if (sourceFile == "")
                {
                    Console.WriteLine("No Source Found");
                }
                else if (destinationFolderName == "")
                {
                    Console.WriteLine("No Detination Found ");
                }
                else
                {

                    //string sourceFile = @"C:\Users\thinksysuser\Desktop\Data\OldDate\ofolder";
                    //string destinationFolderName = @"C:\Users\thinksysuser\Desktop\Data\NewData\";

                    int fileCount = Directory.GetFiles(sourceFile, "*.*", SearchOption.AllDirectories).Length;
                    string[] getAllFiles = Directory.GetFiles(sourceFile, "*.txt");
                   
                    while (fileCount > 0)
                    {
                    

                        if (fileCount < 25)
                        {
                            if (fileCount < 0)
                            {
                                Console.WriteLine("No file left in the source folder please add some");
                                break;
                            }
                            else
                            {
                                string[] txtFiles;
                                txtFiles = Directory.GetFiles(sourceFile);
                                using (StreamWriter writer = new StreamWriter(@"C:\Users\thinksysuser\Desktop\Data\NewData\Combine\" + @"\allfiles.txt"))
                                {
                                    for (int i = 0; i < fileCount; i++)
                                    {
                                        using (StreamReader reader = File.OpenText(txtFiles[i]))
                                        {
                                            writer.Write(reader.ReadToEnd());
                                        }
                                    }
                                }

                                for (int i = 0; i < fileCount; i++)
                                //foreach (var file in getAllFiles)
                                {
                                    var file = getAllFiles[i];
                                    var fileName = Path.GetFileNameWithoutExtension(file);
                                    var extension = Path.GetExtension(file);
                                    var destFileName = destinationFolderName + fileName + extension;
                                    File.Move(file, destFileName);
                                    fileCount--;
                                }

                            }
                            break;
                        }

                        else
                        {
                            {

                                string[] txtFiles;
                               
                                txtFiles = Directory.GetFiles(sourceFile);
                                using (StreamWriter writer = new StreamWriter(@"C:\Users\thinksysuser\Desktop\Data\NewData\Combine\" + @"\allfiles.txt"))
                                {
                                    for (int i = 0; i < fileCount; i++)
                                    {
                                        using (StreamReader reader = File.OpenText(txtFiles[i]))
                                        {
                                            writer.Write(reader.ReadToEnd());
                                        }
                                    }
                                }
                                int k = 0;
                                for (k = 0; k < 25; k++)
                                //foreach (var file in getAllFiles)
                                {
                                    var file = getAllFiles[k];
                                    var fileName = Path.GetFileNameWithoutExtension(file);
                                    var extension = Path.GetExtension(file);
                                    var destFileName = destinationFolderName + fileName + extension;
                                    if (File.Exists(destFileName))
                                    {
                                        File.Move(file, destFileName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No file found");
                                    }
                                 
                                }
                                k = 0;
                            }
                            fileCount = fileCount - 25;
                        }
                   

                        //string path = destinationFolderName;
                        //string[] files = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
                        //using (var output = File.Create(path + "output.txt"))
                        //{
                        //    foreach (var file in files)
                        //    {
                        //        using (var data = File.OpenRead(file))
                        //        {
                        //            data.CopyTo(output);
                        //        }
                        //    }

                        //}
                        //string[] getAllFiles = Directory.GetFiles(sourceFile, "*.txt");
                        //{
                        //     foreach (var file in getAllFiles)
                        //    {
                        //        var fileName = Path.GetFileNameWithoutExtension(file);
                        //        var extension = Path.GetExtension(file);
                        //        var destFileName = destinationFolderName + fileName + extension;
                        //        File.Move(file, destFileName);
                        //    }
                        //}

                    }
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
