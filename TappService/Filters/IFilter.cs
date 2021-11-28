using TappModels;

namespace TappService.Filters
{
    internal interface IFilter
    {
        public static string Command { get;}

        public virtual bool IsMatch(IItem product)
        {
            return false;
        }
    }
}
