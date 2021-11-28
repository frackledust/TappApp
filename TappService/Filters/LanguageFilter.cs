using System;
using System.Collections.Generic;
using System.Text;
using TappModels;

namespace TappService.Filters
{
    internal class LanguageFilter : IFilter
    {
        bool of_translation;

        string languages;

        public static string Command { get => "language"; }

        public LanguageFilter(string _languages, bool _of_translation)
        {
            languages = _languages;
            of_translation = _of_translation;
        }

        public bool IsMatch(IItem item)
        {
            if (item is Project p)
            {
                if(of_translation)
                {
                    if(languages.Contains(p.Translate_language))
                    {
                        return true;
                    }

                }
                else
                {
                    if (languages.Contains(p.Original_language))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
