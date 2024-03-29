﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlackSlashCommandBase.Services.Slack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackSlashCommandBase.Test.Services
{
    [TestClass]
    public class HmacSha256HashServiceTest
    {
        [TestMethod]
        public void GetHash_ValidRequest_ReturnsCorrectHash()
        {
            var requestSignature = "v0:1531420618:token=xyzz0WbapA4vBCDEFasx0q6G&team_id=T1DC2JH3J&team_domain=testteamnow&channel_id=G8PSS9T3V&channel_name=foobar&user_id=U2CERLKJA&user_name=roadrunner&command=%2Fwebhook-collect&text=&response_url=https%3A%2F%2Fhooks.slack.com%2Fcommands%2FT1DC2JH3J%2F397700885554%2F96rGlfmibIGlgcZRskXaIFfN&trigger_id=398738663015.47445629121.803a0bc887a14d10d2c447fce8b6703c";
            var signatureSecret = "8f742231b10e8888abcd99yyyzzz85a5";

            var hashService = new HmacSha256HashService();
            var hash = hashService.GetHash(requestSignature, signatureSecret);

            Assert.AreEqual("a2114d57b48eac39b9ad189dd8316235a7b4a8d21a10bd27519666489c69b503", hash);
        }

        [TestMethod]
        public void GenerateVersionedHash_ValidRequest_ReturnsCorrectVersionedHash()
        {
            var hash = "a2114d57b48eac39b9ad189dd8316235a7b4a8d21a10bd27519666489c69b503";
            var versionNumber = "v0";

            var hashService = new HmacSha256HashService();
            var versionedHash = hashService.GenerateVersionedHash(hash, versionNumber);

            Assert.AreEqual("v0=a2114d57b48eac39b9ad189dd8316235a7b4a8d21a10bd27519666489c69b503", versionedHash);
        }
    }
}
