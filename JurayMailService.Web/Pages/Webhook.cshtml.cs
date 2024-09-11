using Application.Commands.EmailResponseStatusCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace JurayMailService.Web.Pages
{
     [IgnoreAntiforgeryToken]
    public class WebhookModel : PageModel
    {
        private readonly IMediator _mediator;

        public WebhookModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            string payload;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                payload = await reader.ReadToEndAsync();
            }

            // Deserialize the JSON payload into a dictionary
            var payloadDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);

            // Construct the formatted string
            var formattedPayload = new StringBuilder();
            foreach (var kvp in payloadDict)
            {
                formattedPayload.Append($"{kvp.Key}: {kvp.Value}***<br>");
            }

            // Example usage of specific properties
            var recordType = payloadDict.ContainsKey("RecordType") ? payloadDict["RecordType"].ToString() : "N/A";
            var messageID = payloadDict.ContainsKey("MessageID") ? payloadDict["MessageID"].ToString() : "N/A"; 

            // Example of using the formatted string
            var webhookLog = formattedPayload.ToString();
             
            UpdateMailWebhookCommand wcommand = new UpdateMailWebhookCommand(messageID, webhookLog, recordType, DateTime.UtcNow.AddHours(1));
            await _mediator.Send(wcommand);
             

            return Page();
        }
       
    }

    public class WebhookPayload
    {
        public string RecordType { get; set; }
        public int ServerID { get; set; }
        public string MessageStream { get; set; }
        public string MessageID { get; set; }
        public string Recipient { get; set; }
        public string Tag { get; set; }
        public DateTime DeliveredAt { get; set; }
        public string Details { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public string example { get; set; }
        public string example_2 { get; set; }
    }
}
