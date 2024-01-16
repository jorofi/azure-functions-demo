using Azure.Messaging.ServiceBus;
using FunctionsDemo.Abstractions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FunctionsDemo.Functions;

public class SendEmailFunction
{
    private readonly ILogger<SendEmailFunction> _logger;
    private readonly ISendGridClient _sendGridClient;

    public SendEmailFunction(ILogger<SendEmailFunction> logger, ISendGridClient sendGridClient)
    {
        _logger = logger;
        _sendGridClient = sendGridClient;
    }

    [Function(nameof(SendEmailFunction))]
    public async Task Run([ServiceBusTrigger(Constants.EmailQueue, Connection = Constants.ServiceBusConnectionName)] ServiceBusReceivedMessage busMessage)
    {
        _logger.LogInformation("Message ID: {id}", busMessage.MessageId);

        EmailMessages emailMessages = busMessage.Body.ToObjectFromJson<EmailMessages>();

        foreach (var emailMessage in emailMessages.Messages)
        {
            _logger.LogInformation("Sending email to {email}", emailMessage.To);

            var message = new SendGridMessage();
            message.SetFrom(new EmailAddress(Constants.SupportEmail, Constants.SupportName));
            message.AddTo(emailMessage.To);
            message.SetSubject(emailMessage.Subject);
            message.AddContent("text/html", emailMessage.Body);

            var response = await _sendGridClient.SendEmailAsync(message);
            var responseString = response.Body.ReadAsStringAsync();

            _logger.LogInformation("Email sent with status code {statusCode} and response {response}", response.StatusCode, responseString);
        }


    }
}
