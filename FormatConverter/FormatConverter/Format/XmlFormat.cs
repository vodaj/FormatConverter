namespace FormatConverter.Format
{
    using System.Xml.Linq;

    public class XmlFormat : IFileFormat
    {
        public Document? LoadDocument(string data)
        {
            var xdoc = XDocument.Parse(data);
            if (xdoc == null || xdoc.Root == null)
            {
                return default;
            }

            var title = xdoc.Root.Element(IFileFormat.TitleTagString)?.Value;
            if (title == null)
            {
                return default;
            }

            var text = xdoc.Root.Element(IFileFormat.TextTagString)?.Value;
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

            var xdoc = new XDocument(new XElement("data"));
            xdoc.Root.Add(new XElement(IFileFormat.TitleTagString, document.Title));
            xdoc.Root.Add(new XElement(IFileFormat.TextTagString, document.Text));
            return xdoc.ToString();
        }
    }
}
