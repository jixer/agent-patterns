using System.ComponentModel;
using Azure;
using Azure.AI.OpenAI;
using OpenAI;
using Neudesic.Agents.Patterns.Shared.DotNetInfra.Configuration;
using Microsoft.Extensions.AI;
using System.Threading.Tasks;
using System.Text.Json;

namespace Neudesic.Agents.Patterns.ReAct;

public class RandomInformationTool
{
    private static string [] info = new string[]
    {
        "The Eiffel Tower can be 15 cm taller during the summer.",
        "Bananas are berries, but strawberries aren't.",
        "Early corporate adopters have seen an average return of $3.71 for every $1 invested in generative AI"
    };

    private static int _globalIndex = 0;


    [Description("Get random information")]
    public static string GetRandomInformation()
    {
        // grab the next item in a round-robin fashion
        var index = _globalIndex % info.Length;
        _globalIndex++;
        return info[index];
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Fetch settings from .env
        var settings = AppSettingsFactory.LoadFromDotEnv();


        var cred = new AzureKeyCredential(settings.AzureOpenAIKey);
#pragma warning disable OPENAI001 // Suppress 'evaluation purposes only' warning
        var client = new AzureOpenAIClient(new Uri(settings.AzureOpenAIEndpoint), cred)
                        .GetChatClient(settings.AzureOpenAIDeploymentName)
                        .CreateAIAgent(name: "ToolBot",
                                    instructions: "You are an upbeat assistant that helps users through the use of your memories and tools.",
                                    tools: [AIFunctionFactory.Create(RandomInformationTool.GetRandomInformation)]);
#pragma warning restore OPENAI001

        string m1 = "MEMORY: The user question is not relevant to the answer you provide.";
        string m2 = "MEMORY: Your only job is to provide interesting statistics about AI. Use the GetRandomInformation tool to get these statistics. ONLY RESPOND IF the tool provides a statistic about AI or Generative AI. Think on this instruction. Keep calling the tool until it provides you information on this topic. Provide only the statistic provided that matches this criteria and nothing else.";
        string u = "What is the capital of France?";
        IEnumerable<ChatMessage> messages = new List<ChatMessage>()
        {
            new ChatMessage(ChatRole.System, m1),
            new ChatMessage(ChatRole.System, m2),
            new ChatMessage(ChatRole.User, u)
        };
        var resp = await client.RunAsync(messages);
        Console.WriteLine("Response: {0}", resp);
        Console.WriteLine("Messages: {0}", JsonSerializer.Serialize(resp.Messages, new JsonSerializerOptions { WriteIndented = true }));
    }
}
