namespace FormatConverterTests
{
    using FormatConverter.Format;
    using Newtonsoft.Json;

    [TestClass]
    public class JsonFormatTests
    {
        [TestMethod]
        public void LoadDocument_empty_data()
        {
            var data = string.Empty;
            var jsonFormat = new JsonFormat();
            Assert.ThrowsException<JsonReaderException>(() => jsonFormat.LoadDocument(data));
        }

        [TestMethod]
        public void LoadDocument_wrong_format()
        {
            var data = "some text";
            var jsonFormat = new JsonFormat();
            Assert.ThrowsException<JsonReaderException>(() => jsonFormat.LoadDocument(data));
        }

        [TestMethod]
        public void LoadDocument_correct_format_missing_tags()
        {
            var data = "{ \"Some\": \"text\"}";
            var jsonFormat = new JsonFormat();
            Assert.IsNull(jsonFormat.LoadDocument(data));
        }

        [TestMethod]
        public void LoadDocument_correct_format_missing_text_tag()
        {
            var data = "{ \"title\": \"Some title\"}";
            var jsonFormat = new JsonFormat();
            Assert.IsNull(jsonFormat.LoadDocument(data));
        }

        [TestMethod]
        public void LoadDocument_correct_format()
        {
            var data = "{ \"title\": \"Some title\", \"text\": \"some nice text\"}";
            var jsonFormat = new JsonFormat();
            Assert.IsNotNull(jsonFormat.LoadDocument(data));
        }
    }
}