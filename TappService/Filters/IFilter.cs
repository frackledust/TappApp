using TappModels;

namespace TappService.Filters
{
    internal interface IFilter
    {
        public static string Command { get; }

        public virtual bool IsMatch(IFilterable product)
        {
            return false;
        }
    }
}
