using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ChatApp.Services
{
    public class SmsSender
    {
        private readonly string accountSid = "YOUR_TWILIO_ACCOUNT_SID";
        private readonly string authToken = "YOUR_TWILIO_AUTH_TOKEN";
        private readonly string fromPhone = "YOUR_TWILIO_PHONE_NUMBER";

        public async Task SendSms(string toPhone, string message)
        {
            TwilioClient.Init(accountSid, authToken);
            var messageResponse = await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(fromPhone),
                to: new PhoneNumber(toPhone)
            );
        }
    }
}
