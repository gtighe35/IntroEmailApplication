using MailKit.Net.Smtp;
using MimeKit;

namespace IntroEmailApplication
{
  internal class Program
  {
    private const int SmtpPort = 465;
    private const bool IsSslConnection = true;

    static void Main(string[] args)
    {
      Console.WriteLine("Welcome to the command line email client!");

      var loop = true;
  
      while (loop)
      {
        Console.WriteLine();
        Console.WriteLine("New Message");

        var mail = new MimeMessage();

        mail.From.Add(new MailboxAddress("Gage Tighe", "gtgtighe35@gmail.com")); // TODO: update to your own name and email address
        try
        {
          Console.Write("To: ");
          mail.To.Add(new MailboxAddress("", Console.ReadLine()));
        }
        catch (Exception ex)
        {
           Console.WriteLine("An error resulted from entering the email: ");
           Console.WriteLine(ex.Message);
           continue;
        }
       

        Console.Write("Subject: ");
        mail.Subject = Console.ReadLine();

        Console.Write("Body: ");
        mail.Body = new TextPart("plain") { Text = Console.ReadLine() };

        using (var client = new SmtpClient())
        {
          client.Connect("smtp.gmail.com", SmtpPort, IsSslConnection);
          client.Authenticate("gtgtighe35", "gsivgisbnamtuywa"); // TODO: update to your own username and APP PASSWORD (this is different from your normal password)
          client.Send(mail);
          client.Disconnect(true);

          Console.WriteLine("Message sent succesfully!");
        }

        Console.WriteLine();
        Console.Write("Would you like to send another email? (Y/N): ");

        if (Console.ReadKey().Key != ConsoleKey.Y)
        {
          loop = false;
        }
      }
    }
  }
}