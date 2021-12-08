using System.Collections.ObjectModel;
using TappModels;
using TappService.Filters;

namespace TappService
{
    /// <summary>
    /// Use Case 2 - manual filtering of objects
    /// </summary>
    public static class FilterService<T> where T : IFilterable
    {
        /// <summary>
        /// Checks if <paramref name="item"/> fits for all <paramref name="filters"/>
        /// </summary>
        private static bool FitsFilters(Collection<IFilter> filters, T item)
        {
            for (int i = 0; i < filters.Count; i++)
            {
                if (filters[i].IsMatch(item) == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a filtered collection from <paramref name="all_items"/> based on <paramref name="filters"/>
        /// </summary>
        private static Collection<T> Filter(Collection<IFilter> filters, Collection<T> all_items)
        {
            if (filters.Count == 0)
            {
                return all_items;
            }


            Collection<T> result = new Collection<T>();
            for (int i = 0; i < all_items.Count; i++)
            {
                if (FitsFilters(filters, all_items[i]))
                {
                    result.Add(all_items[i]);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns filtered collection from <paramref name="all_items"/> based on <paramref name="filters_commands"/>
        /// </summary>
        public static Collection<T> Filter(string filters_commands, Collection<T> all_items)
        {
            if (filters_commands == null || filters_commands.Length == 0) { return all_items; }

            Collection<IFilter> filters = new Collection<IFilter>();

            //Translated command add
            if (filters_commands.Contains(TranslatedFilter.Command))
            {
                filters.Add(new TranslatedFilter());
            }

            //Languages command add
            if (filters_commands.Contains(LanguageFilter.Command))
            {
                string command = filters_commands[filters_commands.IndexOf(LanguageFilter.Command)..];
                var parameters = command.Split(new char[] { '-', '_' });

                if (parameters.Length >= 2)
                {
                    bool of_translated = parameters.Length == 3 && parameters[2] == "T";

                    filters.Add(new LanguageFilter(parameters[1], of_translated));
                }
            }

            return Filter(filters, all_items);
        }
    }
}
