namespace FormatConverter.Format
{
    public interface IFileFormat
    {
        protected const string TitleTagString = "title";
        protected const string TextTagString = "text";

        public Document? LoadDocument(string data);

        public string? PrepareToSave(Document? document);
    }
}
