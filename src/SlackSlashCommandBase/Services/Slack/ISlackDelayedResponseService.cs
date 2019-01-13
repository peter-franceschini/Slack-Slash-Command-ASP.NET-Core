using SlackSlashCommandBase.Models.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackSlashCommandBase.Services.Slack
{
    public interface ISlackDelayedResponseService
    {
        void SendDelayedResponse(string responseUrl, SlashCommandResponse response);
    }
}
