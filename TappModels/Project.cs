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

        private string _translated_text;

        public Project(int id, string name,
                       string original_language, string translate_language,
                       string original_text, string translated_text)
        {
            Id = id;
            Name = name;
            Original_language = original_language;
            Translate_language = translate_language;
            Original_text = original_text;
            _translated_text = translated_text;
        }

        public string Translated_text
        {
            get => _translated_text;
            set
            {
                IsChanged = true;
                _translated_text = value;
            }
        }

        /// <summary>
        /// Checks if translated text is empty
        /// </summary>
        public bool HasTranslation { get => (Translated_text != null && Translated_text.Length > 0); }

        /// <summary>
        /// Dependent on the is_complete status of project's translation in database
        ///</summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Changes to true when translation of project changes
        ///</summary>
        public bool IsChanged { get; private set; } = false;


    }
}
