using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;
using TappData;
using TappModels;

namespace TappService
{
    public static class ContactService
    {
        private static string GetEmailText(string requester_username, Project project)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("---------------------------");
            sb.AppendLine("Hi @username");
            sb.AppendLine($"We saw that you speak {project.Original_language} and {project.Translate_language}");
            sb.AppendLine("Would you like to help translate one project?");
            sb.AppendLine("You can find the project by searching:");
            sb.AppendLine($"[{requester_username}-{project.Name}]");
            sb.AppendLine();
            sb.AppendLine("See you soon!");
            sb.AppendLine("Tapp Team");

            return sb.ToString();
        }

        private static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static List<string> ValidateEmails(DataTable data)
        {
            List<string> emails = new List<string>();

            foreach (DataRow row in data.Rows)
            {
                string email = row[0].ToString();

                if (IsValidEmail(email))
                {
                    emails.Add(email);
                }
            }

            return emails;
        }
        public static int ReachTranslators(string requester_name, Project project)
        {
            if (project == null) { return 0; }

            //Get emails from database
            var data = PersonGateway.GetEmails(new string[] { project.Original_language, project.Translate_language });


            var emails = ValidateEmails(data);

            string email_text = GetEmailText(requester_name, project);

            //>> Send emails
            int emails_sent_count = 0;

            EmailSender es = new EmailSender();
            foreach (var email in emails)
            {
                try
                {
                    if (es.Send("TappAppTeam@gmail.cz", email, email_text))
                    {
                        emails_sent_count++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return emails_sent_count;
        }
    }
}
