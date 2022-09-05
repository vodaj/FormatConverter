namespace FormatConverter.Storage
{
    public class FileSystem : IStorage
    {
        public string? Load(string path, Dictionary<string, string>? additionalParameters = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                return default;
            }

            if (!File.Exists(path))
            {
                throw new Exception("Source file not exists");
            }

            var res = string.Empty;
            using (FileStream sourceStream = File.Open(path, FileMode.Open))
            {
                using (var reader = new StreamReader(sourceStream))
                {
                    res = reader.ReadToEnd();
                }
            }

            return res;
        }

        public void Save(string input, string path, Dictionary<string, string>? additionalParameters = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Path can not be empty!!!");
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            using (var targetStream = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(targetStream))
                {
                    sw.Write(input);
                }
            }
        }
    }
}
