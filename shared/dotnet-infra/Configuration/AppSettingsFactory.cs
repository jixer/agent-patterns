namespace Neudesic.Agents.Patterns.Shared.DotNetInfra.Configuration;

using Neudesic.Agents.Patterns.Shared.DotNetInfra;

public class AppSettingsFactory
{
    public static AppSettings LoadFromDotEnv()
    {
        DotEnv.TryLoad();

        var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        var deploymentName = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_NAME");
        var key = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");

        return new AppSettings(
            azureOpenAIEndpoint: endpoint ?? throw new InvalidOperationException("AZURE_OPENAI_ENDPOINT is not set in .env"),
            azureOpenAIDeploymentName: deploymentName ?? throw new InvalidOperationException("AZURE_OPENAI_DEPLOYMENT_NAME is not set in .env"),
            azureOpenAIKey: key ?? throw new InvalidOperationException("AZURE_OPENAI_KEY is not set in .env")
        );
    }
}