using System.Collections.ObjectModel;
using TappModels;
using TappService.Filters;

namespace TappService
{
    public class FilterService<T> where T : IItem
    {

        private bool FitsFilters(Collection<IFilter> filters, T item)
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

        private Collection<T> Filter(Collection<IFilter> filters, Collection<T> all_items)
        {
            if (filters.Count == 0)
            {
                return all_items;
            }


            Collection<T> result = new Collection<T>();
            for (int i = 0; i < all_items.Count; i++)
            {
                if(FitsFilters(filters, all_items[i]))
                {
                    result.Add(all_items[i]);
                }
            }

            return result;
        }

        public Collection<T> Filter(string filters_commands, Collection<T> all_items)
        {
            if(filters_commands.Length == 0) { return all_items; }

            Collection<IFilter> filters = new Collection<IFilter>();

            if(filters_commands.Contains(TranslatedFilter.Command))
            {
                filters.Add(new TranslatedFilter());
            }

            if(filters_commands.Contains(LanguageFilter.Command))
            {
                string command = filters_commands.Substring(filters_commands.IndexOf(LanguageFilter.Command));
                var parameters = command.Split('-');

                bool of_translated = (parameters.Length < 3 || parameters[2] == "O") ? false : true;

                filters.Add(new LanguageFilter(parameters[1], of_translated));
            }

            return Filter(filters, all_items);
        }
    }
}
