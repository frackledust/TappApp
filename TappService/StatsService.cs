using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using TappModels;

namespace TappService
{
    /// <summary>
    /// Use Case 3:
    /// Creating of statistics about projects
    /// </summary>
    public static class StatsService
    {
        /// <summary>
        /// Current DateTime in set format
        /// </summary>
        public static string TimeStamp { get => DateTime.Now.ToString("dd'_'MM'_'HH'_'mm"); }

        /// <summary>
        /// Name of new csv file of stats
        /// </summary>
        private static string CsvPath { get => $@".\stats\stats_{TimeStamp}_version.csv"; }

        /// <summary>
        /// Returns the number of elements in <paramref name="text"/> that ends with {. ? !}
        /// </summary>
        public static int SentenceCount(string text)
        {
            if (text == null || text.Length == 0) return 0;

            char[] split_chars = new char[] { '.', '?', '!' };
            return text.Split(split_chars, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Returns the number of elements in <paramref name="text"/> that end with space or new line
        /// </summary>
        public static int WordCount(string text)
        {
            if (text == null || text.Length == 0) return 0;

            char[] split_chars = new char[] { ' ', '\r', '\n' };
            return text.Split(split_chars, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Generates Header for table of WordCount and SentenceCounts stats of Project
        /// </summary>
        private static string GenerateHeader(string col_separator, string row_separator)
        {
            StringBuilder header = new StringBuilder();

            header.Append("Project name");
            header.Append(col_separator);

            header.Append("Original language");
            header.Append(col_separator);

            header.Append("Translation language");
            header.Append(col_separator);

            header.Append("Original words");
            header.Append(col_separator);

            header.Append("Translated words");
            header.Append(col_separator);

            header.Append("Original sentences");
            header.Append(col_separator);

            header.Append("Translated sentences");

            header.Append(row_separator);

            return header.ToString();
        }

        /// <summary>
        /// Generates table of statistics for WordCount and SentenceCounts stats for all <paramref name="projects"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GenerateStats(Collection<Project> projects, string col_separator, string row_separator)
        {
            if (projects is null) { throw new ArgumentNullException(nameof(projects)); }

            StringBuilder stats = new StringBuilder();

            //Header
            stats.Append(GenerateHeader(col_separator, row_separator));

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

        /// <summary>
        /// Saves generated stats of <paramref name="projects"/> into file
        /// </summary>
        public static string GenerateStatsToCSV(Collection<Project> projects)
        {
            string stats = GenerateStats(projects, ";", "\n");
            if (string.IsNullOrEmpty(stats)) { return default; }

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
