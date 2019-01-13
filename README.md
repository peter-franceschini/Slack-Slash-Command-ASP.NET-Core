# Slack Slash Command ASP.NET Core Base Implementation

This project is an ASP.NET Core 2.2 implementation of the [Slack Slash Command API](https://api.slack.com/slash-commands) to be used as the starting point for Slack Slash command projects.

## Features
* Slack request signature validation
* Immediate and delayed command response processing and delivery

## Slash Command Examples
### Immediate Response 
```SlashCommandController.ImmediateResponsePost()``` - An example of how to create an endpoint that returns an immediate response to a slash command.
### Delayed Response
```SlashCommandController.DelayedResponsePost()``` - An example of how to create an endpoint that returns an immediate and then later a delayed response to a Slash command.
```ExampleDelayedWorkService.cs``` - Example service that performs a task and then sends a delayed response to Slack.


## Installation / Usage

### Requirements
* .NET Core 2.2
* Slack team with administrative privileges

### Application Setup
1. Pull the latest code from the master branch of this repository
1. Build and publish the application to your desired web server
    * Web server must have [.Net Core 2.2 Runtime](https://dotnet.microsoft.com/download/dotnet-core/2.2) installed 

### Slack Setup
1. Setup a new Slack App in the [Slack API dashboard](https://api.slack.com/apps)
1. Update your application configuration file or secrets storage with your Slack Singing Secret from the App Credentials section of your new Slack App
1. Add the Slash Command feature to your new Slack App
1. Add a new Command with the following details:
    * Command: /your_command
    * Request URL: [Your Server URL]/api/SlashCommand/DelayedResponsePost or /api/SlashCommand/ImmediateResponsePost
1. Deploy your new Slack app to your Slack Team

### Production Setup
In order to handle delayed command responses, this project uses HangFire. By default in this application [Hangfire](https://www.hangfire.io/) is configured to use Memory Storage, which isn't suitable for production usage. It is recommended you change the Hangfire storage method in ```Startup.ConfigureServices()``` to another provider in production.