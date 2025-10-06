namespace Neudesic.Agents.Patterns.ToolCalling;

using System;
using Azure.AI.OpenAI;
using OpenAI;
using Neudesic.Agents.Patterns.Shared.DotNetInfra.Configuration;
using Azure;
using System.ComponentModel;
using Microsoft.Extensions.AI;
using System.Text.Json;

class Program
{
    [Description("Add two numbers together.")]
    static int Add([Description("The first number to add.")] int x, [Description("The second number to add.")] int y) {
        return x + y;
    }

    static async Task Main(string[] args)
    {
        // Fetch settings from .env
        var settings = AppSettingsFactory.LoadFromDotEnv();
        

        var cred = new AzureKeyCredential(settings.AzureOpenAIKey);
#pragma warning disable OPENAI001 // Suppress 'evaluation purposes only' warning
        var client = new AzureOpenAIClient(new Uri(settings.AzureOpenAIEndpoint), cred)
                        .GetChatClient(settings.AzureOpenAIDeploymentName)
                        .CreateAIAgent(name: "ToolBot",
                                    instructions: "You are an upbeat assistant that helps users.",
                                    tools: [AIFunctionFactory.Create(Add)]);
#pragma warning restore OPENAI001


        var resp = await client.RunAsync("What is 1 + 2?");
        Console.WriteLine("Response: {0}", resp);
        Console.WriteLine("Messages: {0}", JsonSerializer.Serialize(resp.Messages, new JsonSerializerOptions { WriteIndented = true }));
    }
}