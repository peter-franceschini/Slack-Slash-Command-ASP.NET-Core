using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SlackSlashCommandBase.Models.Slack;

namespace SlackSlashCommandBase.Services.Slack
{
    public class SlackDelayedResponseService : ISlackDelayedResponseService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public void SendDelayedResponse(string responseUrl, SlashCommandResponse response)
        {
            var content = new StringContent(JsonConvert.SerializeObject(response), Encoding.UTF8, "application/json");

            var result = HttpClient.PostAsync(responseUrl, content).Result;
        }
    }
}
