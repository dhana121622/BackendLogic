using System.Net.Mail;

namespace WebAPI
{
    public class Email_2_UserName_Psd
    {
        public bool email_2_UserName_Psd(string Email, string psd, string userName)
        {
            try
            {
                MailMessage newMail = new MailMessage();

                // Gmail SMTP Host
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                // Set the sender and recipient
                newMail.From = new MailAddress("dhanasekar16.bit@gmail.com", "Login Crenditals");
                newMail.To.Add(Email);

                // Email subject and body
                newMail.Subject = $"Welcome {userName}";
                newMail.IsBodyHtml = true;
                newMail.Body = $"<h3>Your Login Crenditals for WorkStatus Update</h3>" +
                    $"<h4>UserName : {Email}</h4>" +
                    $"<h4>Password : {psd}</h4>";

                // Set SMTP client settings
                client.EnableSsl = true;       // Enable SSL for encryption
                client.Port = 587;             // Use port 587 for TLS
                client.Credentials = new System.Net.NetworkCredential("dhanasekar16.bit@gmail.com", "ijhk pswb yepk zdmo");

                // Send the constructed mail
                client.Send(newMail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex);
            }
            return false;
        }
    }
}