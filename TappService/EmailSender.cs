using System;
using System.IO;

namespace TappService
{
    /// <summary>
    /// Handles logic of email transactions from the app
    /// </summary>
    internal class EmailSender
    {
        private static string log_path = @"assets\email_log.txt";

        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="from"> Requester's email address </param>
        /// <param name="to"> Reciever's email address </param>
        /// <param name="text"> Text of the email </param>
        internal bool Send(string from, string to, string text)
        {
            //>> Domain logic for sending emails

            SendLog(from, to);
            return true;
        }

        /// <summary>
        /// Logs sent emails into file at <see cref="log_path"/>
        /// </summary>
        /// <param name="from"> Requester's email address </param>
        /// <param name="to"> Reciever's email address </param>
        public static void SendLog(string from, string to)
        {
            using (StreamWriter sw = new StreamWriter(log_path, append: true))
            {
                sw.WriteLine($"{DateTime.Now} - sent email from: {from} to: {to}");
            }
        }
    }
}
