using System.Net.Mail;
namespace WebAPI
{
    public class EmailAuthProcess
    {
        public string emailAuthProcess(string Email)
        {
            try
            {
                MailMessage newMail = new MailMessage();

                // Gmail SMTP Host
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                // Set the sender and recipient
                newMail.From = new MailAddress("dhanasekar16.bit@gmail.com","Verification Code");
                newMail.To.Add(Email);

                Random random = new Random();
                int sixDigitNumber = random.Next(100000, 1000000);
                string sixDigitCode = sixDigitNumber.ToString();

                // Email subject and body
                newMail.Subject = "Reset Password Verification Code";
                newMail.IsBodyHtml = true;
                newMail.Body = $"<h5>Your Reset Password Verification Code is {sixDigitCode}</h5>";

                // Set SMTP client settings
                client.EnableSsl = true;       // Enable SSL for encryption
                client.Port = 587;             // Use port 587 for TLS
                client.Credentials = new System.Net.NetworkCredential("dhanasekar16.bit@gmail.com", "ijhk pswb yepk zdmo");

                // Send the constructed mail
                client.Send(newMail);

                return sixDigitCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex);
            }
            return "Failed";
        }
    }
}
