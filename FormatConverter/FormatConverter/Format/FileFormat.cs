namespace FormatConverter.Format
{
    public abstract class FileFormat
    {
        public FileFormat(string data)
        {
            this.Data = data;
        }

        protected string Data { get; set; }


        public abstract Document ToDocument(string data);

        public abstract string ToSave();
    }
}
