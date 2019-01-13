using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackSlashCommandBase.Services.Slack
{
    public interface IHashService
    {
        string GetHash(string requestSignature, string signatureSecret);
        string GenerateVersionedHash(string hash, string versionNumber);
    }
}
