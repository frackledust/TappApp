using TappData;

namespace TappService
{
    public static class UserService
    {
        public static void DeactiveTranslator(int id)
        {
            //Delete unfinished translations and set translator id as NULL
            ProjectMapper.DeleteTranslations(id);

            //Set Person-active as false
            PersonGateway.Deactive(id);
        }
    }
}
