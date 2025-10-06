namespace Neudesic.Agents.Patterns.Shared.DotNetInfra.Configuration;

public class AppSettings
{
    internal AppSettings(string azureOpenAIEndpoint, string azureOpenAIDeploymentName, string azureOpenAIKey)
    {
        AzureOpenAIEndpoint = azureOpenAIEndpoint;
        AzureOpenAIDeploymentName = azureOpenAIDeploymentName;
        AzureOpenAIKey = azureOpenAIKey;
    }

    public string AzureOpenAIEndpoint { get; }
    public string AzureOpenAIDeploymentName { get; }
    public string AzureOpenAIKey { get; }
}