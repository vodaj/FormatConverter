namespace FormatConverter.Format
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonFormat : IFileFormat
    {
        public Document? LoadDocument(string data)
        {
            var json = JObject.Parse(data);
            if (json == null)
            {
                return default;
            }

            var title = json.Value<string>(IFileFormat.TitleTagString);
            if (title == null)
            {
                return default;
            }

            var text = json.Value<string>(IFileFormat.TextTagString);
            if (text == null)
            {
                return default;
            }

            var doc = new Document
            {
                Title = title,
                Text = text
            };

            return doc;
        }

        public string? PrepareToSave(Document? document)
        {
            if (document == null)
            {
                return default;
            }

            return JsonConvert.SerializeObject(document);
        }
    }
}
