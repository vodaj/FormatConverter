namespace FormatConverter.Storage
{
    public interface IStorage
    {
        string? Load(string path, Dictionary<string, string>? additionalParameters = null);
        
        void Save(string input, string path, Dictionary<string, string>? additionalParameters = null);
    }
}
