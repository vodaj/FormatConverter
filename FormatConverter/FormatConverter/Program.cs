namespace FormatConverter
{
    using System;
    using System.Collections.Generic;
    using FormatConverter.Format;
    using FormatConverter.Storage;


    // FOR COMMENTED ISSUES SEE BELOW
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert source file format:");
            string? sourceFormatString;
            do
            {
                sourceFormatString = Console.ReadLine();

            } while (string.IsNullOrEmpty(sourceFormatString));
            
            var sourceFormat = (Format.Format)Enum.Parse(typeof(Format.Format), sourceFormatString, true);

            Console.WriteLine("Insert source file path:");
            string? sourcePath;
            do
            {
                sourcePath = Console.ReadLine();

            } while (string.IsNullOrEmpty(sourcePath));

            Console.WriteLine("Insert source storage type:");
            string? sourceStorageTypeString;
            do
            {
                sourceStorageTypeString = Console.ReadLine();

            } while (string.IsNullOrEmpty(sourceStorageTypeString));

            var sourceStorageType = (StorageType)Enum.Parse(typeof(StorageType), sourceStorageTypeString, true);

            Console.WriteLine("Insert target file format:");
            string? targetFormatString;
            do
            {
                targetFormatString = Console.ReadLine();

            } while (string.IsNullOrEmpty(targetFormatString));

            var targetFormat = (Format.Format)Enum.Parse(typeof(Format.Format), targetFormatString, true);

            Console.WriteLine("Insert target file path:");
            string? targetPath;
            do
            {
                targetPath = Console.ReadLine();

            } while (string.IsNullOrEmpty(targetPath));

            Console.WriteLine("Insert target storage type:");
            string? targetStorageTypeString;
            do
            {
                targetStorageTypeString = Console.ReadLine();

            } while (string.IsNullOrEmpty(targetStorageTypeString));

            var targetStorageType = (StorageType)Enum.Parse(typeof(StorageType), targetStorageTypeString, true);

            try
            {
                var result = Convert(sourceFormat, sourceStorageType, sourcePath, targetFormat, targetStorageType, targetPath);
                if (!string.IsNullOrEmpty(result))
                {
                    Console.WriteLine($"Error while converting file: {result}");
                }
                else
                {
                    Console.WriteLine("File successfully converted...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while converting file: {ex.Message}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="sourceFormat">Source file format</param>
        /// <param name="sourceStorageType">Type of storage from which to load source file</param>
        /// <param name="sourcePath">Path to source file</param>
        /// <param name="targetFormat">Target file format</param>
        /// <param name="targetStorageType">Type of storage to which save target file</param>
        /// <param name="targetPath">Path to target file</param>
        /// <param name="sourceParameters">Parameters for loading source file (login etc.)</param>
        /// <param name="targetParameters">Parameters for saving target file (login etc.)</param>
        /// <exception cref="Exception"></exception>
        private static string Convert(Format.Format sourceFormat, StorageType sourceStorageType, string sourcePath, Format.Format targetFormat, StorageType targetStorageType, string targetPath, Dictionary<string, string>? sourceParameters = null, Dictionary<string, string>? targetParameters = null)
        {
            try
            {

                IStorage sourceStorage = GetStorage(sourceStorageType);
                var input = sourceStorage.Load(sourcePath, sourceParameters);

                IFileFormat sourceFileFormat = GetFormat(sourceFormat);
                var doc = sourceFileFormat.LoadDocument(input);
                if (doc == null)
                {
                    return "Source file is in wrong format and can't be loaded.";
                }

                IFileFormat targetFileFormat = GetFormat(targetFormat);
                var serializedDoc = targetFileFormat.PrepareToSave(doc);
                if (serializedDoc == null)
                {
                    return "Target file couldn't be created.";
                }

                IStorage targetStorage = GetStorage(targetStorageType);
                targetStorage.Save(serializedDoc, targetPath, targetParameters);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static IStorage GetStorage(StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.FileSystem:
                    return new FileSystem();

                default:
                    throw new NotImplementedException();
            }
        }

        private static IFileFormat GetFormat(Format.Format format)
        {
            switch (format)
            {
                case Format.Format.Json:
                    return new JsonFormat();
                    
                case Format.Format.Xml:
                    return new XmlFormat();

                default:
                    throw new NotImplementedException();
            }
        }
    }


    /* COMMENTED ISSUES
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection.Metadata;
    using System.Xml.Linq;
    using Newtonsoft.Json;
    using static System.Net.WebRequestMethods;

    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\SourceFiles\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\TargetFiles\\Document1.json");
            try
            {
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open); // needs to be specified from which namespace "File" class is because it exists in two, stream needs to be closed to release resources
                var reader = new StreamReader(sourceStream); // reader needs to be closed or "using" block needs to be used to release resources (loaded file)
                string input = reader.ReadToEnd(); // must be declared outside of try block or try block should be over all method
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); // no reason for this, makes it complicated to track exception
            }
            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };
            var serializedDoc = JsonConvert.SerializeObject(doc);
            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write); // needs to be specified from which namespace "File" class is because it exists in two, stream needs to be closed to release resources
            var sw = new StreamWriter(targetStream); // writer needs to be closed or "using" block needs to be used to release resources (loaded file)
            sw.Write(serializedDoc);
        }
    }*/

}