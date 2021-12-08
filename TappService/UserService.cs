﻿using TappData;

namespace TappService
{
    /// <summary>
    /// Use Case 5:
    /// Domain login handling User's parameters
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Deletes unfinished translations and sets acivity status in database to false
        /// </summary>
        public static void DeactiveTranslator(int id)
        {
            ProjectMapper.DeleteTranslations(id);

            PersonGateway.Deactive(id);
        }
    }
}
