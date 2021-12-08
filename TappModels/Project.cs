namespace TappModels
{
    /// <summary>
    /// Represents project containing one text file and its translation
    ///</summary>
    public class Project : IFilterable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Original_language { get; set; }
        public string Translate_language { get; set; }

        public string Original_text { get; set; }

        public string Translated_text { get; set; }

        /// <summary>
        /// Checks if translated text is empty
        /// </summary>
        public bool HasTranslation { get => (Translated_text != null && Translated_text.Length > 0); }

        /// <summary>
        /// Dependent on the is_complete status of project's translation in database
        ///</summary>
        public bool IsCompleted { get; set; }
    }
}
