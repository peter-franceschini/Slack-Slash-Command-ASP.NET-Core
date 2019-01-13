using SlackSlashCommandBase.Models.Slack;
using SlackSlashCommandBase.Services.Slack;
using SlackSlashCommandBase.Utilities;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using SlackSlashCommandBase.Services;

namespace SlackSlashCommandBase.Controllers
{
    [Route("api/[controller]")]
    public class SlashCommandController : Controller
    {
        private SlackSettings SlackSettings { get; set; }
        private ISignatureValidationService SignatureValidationService { get; set; }

        public SlashCommandController(IOptions<SlackSettings> slackSettings, ISignatureValidationService signatureValidationService)
        {
            SlackSettings = slackSettings.Value;
            SignatureValidationService = signatureValidationService;
            
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> ImmediateResponsePost()
        {
            var streamHelper = new StreamHelper();
            var requestBody = streamHelper.ReadAsString(Request.Body).Result;

            var slashCommandPayload = new SlashCommandPayload(requestBody);

            // Verify Slack request signature
            if (!SignatureValidationService.SignatureValid(Request.Headers["X-Slack-Signature"], Request.Headers["X-Slack-Request-Timestamp"], requestBody, SlackSettings.SignatureSecret))
            {
                return BadRequest();
            }

            // Do stuff here

            // Return an immediate response to slack
            var slashCommandErrorResponse = new SlashCommandResponse()
            {
                ResponseType = "in_channel",
                Text = "It's 80 degrees right now."
            };

            return Ok(slashCommandErrorResponse);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> DelayedResponsePost()
        {
            var streamHelper = new StreamHelper();
            var requestBody = streamHelper.ReadAsString(Request.Body).Result;

            var slashCommandPayload = new SlashCommandPayload(requestBody);

            // Verify Slack request signature
            if (!SignatureValidationService.SignatureValid(Request.Headers["X-Slack-Signature"], Request.Headers["X-Slack-Request-Timestamp"], requestBody, SlackSettings.SignatureSecret))
            {
                return BadRequest();
            }

            // Queue background task
            var exampleDelayedWorkService = new ExampleDelayedWorkService(new SlackDelayedResponseService());
            BackgroundJob.Enqueue(() => exampleDelayedWorkService.DoWork(slashCommandPayload));

            // Return an immediate response to Slack while the backgroundjob is still processing
            var slashCommandResponse = new SlashCommandResponse()
            {
                ResponseType = "ephemeral",
                Text = "Processing your request."
            };

            return Ok(slashCommandResponse);
        }
    }
}
