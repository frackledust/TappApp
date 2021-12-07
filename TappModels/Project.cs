namespace TappModels
{
    public class Project : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Original_language { get; set; }
        public string Translate_language { get; set; }

        public string Original_text { get; set; }

        public string Translated_text { get; set; }

        public bool HasTranslation { get => (Translated_text != null && Translated_text.Length > 0); }

        public bool IsCompleted { get; set; }
    }
}
