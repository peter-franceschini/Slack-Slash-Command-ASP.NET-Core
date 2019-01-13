using SlackSlashCommandBase.Models.Slack;
using SlackSlashCommandBase.Services.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackSlashCommandBase.Services
{
    public class ExampleDelayedWorkService
    {
        private ISlackDelayedResponseService SlackDelayedResponseService { get; set; }

        public ExampleDelayedWorkService(ISlackDelayedResponseService slackDelayedResponseService)
        {
            SlackDelayedResponseService = slackDelayedResponseService;
        }

        public void DoWork(SlashCommandPayload slashCommandPayload)
        {
            // Do stuff here

            // Build Slack response model
            var responseModel = new SlashCommandResponse()
            {
                ResponseType = "in_channel",
                Text = "It's 80 degrees right now."
            };

            // Send delayed response to Slack 
            SlackDelayedResponseService.SendDelayedResponse(slashCommandPayload.ResponseUrl, responseModel);
        }
    }
}
