using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using TappModels;

namespace TappService
{
    public static class StatsService
    {
        public static string TimeStamp { get => DateTime.Now.ToString("dd'_'MM'_'HH'_'mm"); }
        private static string CsvPath
        {
            get { return $@"../stats_{TimeStamp}_version.csv"; }
        }

        public static int SentenceCount(string text)
        {
            char[] split_chars = new char[] { '.', '?', '!' };
            return text.Split(split_chars, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static int WordCount(string text)
        {
            char [] split_chars = new char[] { ' ', '\r', '\n' };
            return text.Split(split_chars, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string GenerateStats(Collection<Project> projects, string col_separator, string row_separator)
        {
            if(ReferenceEquals(projects, null)) { throw new ArgumentNullException(nameof(projects)); }

            StringBuilder stats = new StringBuilder();

            //Header
            stats.Append("Project name");
            stats.Append(col_separator);

            stats.Append("Original language");
            stats.Append(col_separator);

            stats.Append("Translation language");
            stats.Append(col_separator);

            stats.Append("Original words");
            stats.Append(col_separator);

            stats.Append("Translated words");
            stats.Append(col_separator);

            stats.Append("Original sentences");
            stats.Append(col_separator);

            stats.Append("Translated sentences");

            stats.Append(row_separator);

            foreach (var project in projects)
            {
                stats.Append(project.Name);
                stats.Append(col_separator);

                stats.Append(project.Original_language);
                stats.Append(col_separator);

                stats.Append(project.Translate_language);
                stats.Append(col_separator);

                stats.Append(WordCount(project.Original_text));
                stats.Append(col_separator);

                stats.Append(WordCount(project.Translated_text));
                stats.Append(col_separator);

                stats.Append(SentenceCount(project.Original_text));
                stats.Append(col_separator);

                stats.Append(SentenceCount(project.Translated_text));
                stats.Append(col_separator);

                stats.Append(row_separator);
            }
            
            return stats.ToString();
        }

        public static string GenerateStatsToCSV(Collection<Project> projects)
        {

            string stats = GenerateStats(projects, ";", "\n");
            if(string.IsNullOrEmpty(stats)) { return default(string); }

            string current_path = CsvPath;
            using (FileStream fs = new FileStream(current_path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(stats);
                }
            }

            return current_path;
        }
    }
}
