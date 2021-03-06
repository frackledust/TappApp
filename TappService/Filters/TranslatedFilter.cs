using TappModels;

namespace TappService.Filters
{
    internal class TranslatedFilter : IFilter
    {
        public static string Command { get => "translated"; }

        public bool IsMatch(IFilterable item)
        {
            if (item is Project p)
            {
                if (p.HasTranslation)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
