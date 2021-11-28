using System;
using System.Collections.Generic;
using System.Text;
using TappModels;

namespace TappService.Filters
{
    internal class TranslatedFilter : IFilter
    {
        public static string Command { get => "translated"; }

        public bool IsMatch(IItem item)
        {
            if(item is Project p)
            {
                if(p.IsTranslated)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
