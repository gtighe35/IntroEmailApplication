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
      // We loop here so that the user can send multiple emails
      while (loop)
      {
        Console.WriteLine();
        Console.WriteLine("New Message");

        var mail = new MimeMessage();

        mail.From.Add(new MailboxAddress("Gage Tighe", "gtgtighe35@gmail.com")); // TODO: update to your own name and email address
        //This part is for taking in the information of the email from the user

        try //If there is a " " in the Console.ReadLine(), this will result in an exception
        {
          Console.Write("To: ");
          mail.To.Add(new MailboxAddress("", Console.ReadLine()));
        }
        catch (Exception ex)
        {
           Console.WriteLine("An error resulted from entering the email: ");
           Console.WriteLine(ex.Message);
           continue; //This will go to the next loop
        }
       

        Console.Write("Subject: ");
        mail.Subject = Console.ReadLine();

        Console.Write("Body: ");
        mail.Body = new TextPart("plain") { Text = Console.ReadLine() };

//After having all the info, we will try to send the message
  try{
        using (var client = new SmtpClient())
        {
          client.Connect("smtp.gmail.com", SmtpPort, IsSslConnection);
          client.Authenticate("gtgtighe35", "gsivgisbnamtuywa"); // TODO: update to your own username and APP PASSWORD (this is different from your normal password)
          client.Send(mail);
          client.Disconnect(true);

          Console.WriteLine("Message sent succesfully!");
        }
  }
  catch(Exception ex){
    Console.WriteLine("Error occurred while attempting to send email");
    Console.WriteLine(ex.Message);
    // I could have logic to make this go to the send another email prompt, but I am fine with the way it handles this exception
  }
        Console.WriteLine();
        Console.Write("Would you like to send another email? (Y/N): ");
        //This will instantly terminate if they press any other key
        if (Console.ReadKey().Key != ConsoleKey.Y)
        {
          loop = false;
        }
      }
    }
  }
}