namespace Neudesic.Agents.Patterns.ToolCalling;

using System;
using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using OpenAI;
using Neudesic.Agents.Patterns.Shared.DotNetInfra;
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
        // Load environment variables from .env file if it exists
        DotEnv.TryLoad();

        // Ensure required environment variables are set
        var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        var deploymentName = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME");
        var key = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");
        if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(deploymentName) || string.IsNullOrEmpty(key))
        {
            Console.WriteLine("Please set the AZURE_OPENAI_ENDPOINT, AZURE_OPENAI_DEPLOYMENT_NAME, and AZURE_OPENAI_KEY environment variables.");
            return;
        }

        var cred = new AzureKeyCredential(key);
#pragma warning disable OPENAI001 // Suppress 'evaluation purposes only' warning
        var client = new AzureOpenAIClient(new Uri(endpoint), cred)
                        .GetChatClient(deploymentName)
                        .CreateAIAgent(name: "ToolBot",
                                    instructions: "You are an upbeat assistant that helps users.",
                                    tools: [AIFunctionFactory.Create(Add)]);
#pragma warning restore OPENAI001


        var resp = await client.RunAsync("What is 1 + 2?");
        Console.WriteLine("Response: {0}", resp);
        Console.WriteLine("Messages: {0}", JsonSerializer.Serialize(resp.Messages, new JsonSerializerOptions { WriteIndented = true }));
    }
}