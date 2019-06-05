using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookStore.BusinessLogic
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@fireplace.com";
        public string MailFromAddress = "bookstore@fireplace.com";
        public bool UseSSL = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public int ServerPort = 587;
        public string ServerName = "smtp.bookstore.com";
        public bool WriteAsFile = true;
        public string FileLocation = @"F:\Visual\BookStore\book_store_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings _emailSettings;
        public EmailOrderProcessor(EmailSettings settings) => _emailSettings = settings;

        public void ProcessOrder(CartDTO cartDto, ShippingDetailsDTO shippingDetailsDto)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSSL;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder bodyBuilder = new StringBuilder()
                    .AppendLine("New order processed")
                    .AppendLine("|||||||||||||||||||")
                    .AppendLine("Goods: ");

                foreach (var line in cartDto.Lines) 
                {
                    var subTotal = line.Book.Price * line.Quantity;
                    bodyBuilder.AppendFormat("{0} x {1} (total: {2:c}", line.Quantity, line.Book.Name, subTotal);
                }

                bodyBuilder.AppendFormat("Total cost: {0:c}", cartDto.TotalValue())
                    .AppendLine("|||||||||||||||||||")
                    .AppendLine("Delivery")
                    .AppendLine(shippingDetailsDto.Name)
                    .AppendLine(shippingDetailsDto.Addres_1)
                    .AppendLine(shippingDetailsDto.Addres_2 ?? "")
                    .AppendLine(shippingDetailsDto.City)
                    .AppendLine(shippingDetailsDto.Country)
                    .AppendLine("|||||||||||||||||||")
                    .AppendFormat("Gift Wrap: {0}", shippingDetailsDto.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    _emailSettings.MailFromAddress,
                    _emailSettings.MailToAddress,
                    "New order has been sent",
                    bodyBuilder.ToString()
                    );

                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
