using System;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CaseManagement.Business
{
    public class NotificationBL
    {
        public bool SendSMS(string phoneNumber, string message)
        {
            try
            {
                string accountSid = ConfigurationManager.AppSettings["Twilio:AccountSid"];
                string authToken = ConfigurationManager.AppSettings["Twilio:AuthToken"];
                string twilioPhoneNumber = ConfigurationManager.AppSettings["Twilio:PhoneNumber"];

                if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken))
                {
                    // If Twilio not configured, just log it
                    System.Diagnostics.Debug.WriteLine($"SMS would be sent to {phoneNumber}: {message}");
                    return true;
                }

                TwilioClient.Init(accountSid, authToken);

                var messageResource = MessageResource.Create(
                    body: message,
                    from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

                return messageResource != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending SMS: " + ex.Message);
            }
        }

        public bool SendWhatsApp(string phoneNumber, string message)
        {
            try
            {
                string accountSid = ConfigurationManager.AppSettings["Twilio:AccountSid"];
                string authToken = ConfigurationManager.AppSettings["Twilio:AuthToken"];
                string twilioPhoneNumber = ConfigurationManager.AppSettings["Twilio:PhoneNumber"];

                if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken))
                {
                    System.Diagnostics.Debug.WriteLine($"WhatsApp would be sent to {phoneNumber}: {message}");
                    return true;
                }

                TwilioClient.Init(accountSid, authToken);

                var messageResource = MessageResource.Create(
                    body: message,
                    from: new Twilio.Types.PhoneNumber("whatsapp:" + twilioPhoneNumber),
                    to: new Twilio.Types.PhoneNumber("whatsapp:" + phoneNumber)
                );

                return messageResource != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending WhatsApp: " + ex.Message);
            }
        }
    }
}
