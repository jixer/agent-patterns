namespace Neudesic.Agents.Patterns.Shared.DotNetInfra;

/// <summary>
/// Utility class for loading environment variables from .env files
/// </summary>
public static class DotEnv
{
    /// <summary>
    /// Loads environment variables from a .env file
    /// </summary>
    /// <param name="filePath">Path to the .env file. Defaults to ".env" in the current directory</param>
    /// <param name="overrideExisting">Whether to override existing environment variables. Defaults to false</param>
    public static void Load(string filePath = ".env", bool overrideExisting = false)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($".env file not found at path: {filePath}");
        }

        foreach (var line in File.ReadAllLines(filePath))
        {
            ProcessLine(line, overrideExisting);
        }
    }

    /// <summary>
    /// Loads environment variables from a .env file if it exists
    /// </summary>
    /// <param name="filePath">Path to the .env file. Defaults to ".env" in the current directory</param>
    /// <param name="overrideExisting">Whether to override existing environment variables. Defaults to false</param>
    /// <returns>True if file was found and loaded, false otherwise</returns>
    public static bool TryLoad(string filePath = ".env", bool overrideExisting = false)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        Load(filePath, overrideExisting);
        return true;
    }

    /// <summary>
    /// Loads environment variables from multiple .env files
    /// </summary>
    /// <param name="filePaths">Array of paths to .env files</param>
    /// <param name="overrideExisting">Whether to override existing environment variables. Defaults to false</param>
    public static void LoadMultiple(string[] filePaths, bool overrideExisting = false)
    {
        foreach (var filePath in filePaths)
        {
            Load(filePath, overrideExisting);
        }
    }

    /// <summary>
    /// Gets an environment variable value
    /// </summary>
    /// <param name="key">The environment variable key</param>
    /// <returns>The environment variable value, or null if not found</returns>
    public static string? Get(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }

    /// <summary>
    /// Gets an environment variable value or returns a default value if not found
    /// </summary>
    /// <param name="key">The environment variable key</param>
    /// <param name="defaultValue">The default value to return if the key is not found</param>
    /// <returns>The environment variable value, or the default value if not found</returns>
    public static string GetOrDefault(string key, string defaultValue)
    {
        return Environment.GetEnvironmentVariable(key) ?? defaultValue;
    }

    /// <summary>
    /// Checks if an environment variable exists
    /// </summary>
    /// <param name="key">The environment variable key</param>
    /// <returns>True if the environment variable exists, false otherwise</returns>
    public static bool Exists(string key)
    {
        return Environment.GetEnvironmentVariable(key) != null;
    }

    private static void ProcessLine(string line, bool overrideExisting)
    {
        // Skip empty lines and comments
        var trimmedLine = line.Trim();
        if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine.StartsWith("#"))
        {
            return;
        }

        // Find the first '=' character
        var separatorIndex = trimmedLine.IndexOf('=');
        if (separatorIndex == -1)
        {
            return; // Skip lines without '='
        }

        var key = trimmedLine.Substring(0, separatorIndex).Trim();
        var value = trimmedLine.Substring(separatorIndex + 1).Trim();

        // Skip invalid keys
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        // Remove quotes from value if present
        value = UnquoteValue(value);

        // Set environment variable if it doesn't exist or if override is enabled
        if (overrideExisting || string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }

    private static string UnquoteValue(string value)
    {
        // Remove surrounding quotes (single or double)
        if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
            (value.StartsWith("'") && value.EndsWith("'")))
        {
            if (value.Length >= 2)
            {
                value = value.Substring(1, value.Length - 2);
            }
        }

        // Handle escape sequences
        value = value.Replace("\\n", "\n")
                     .Replace("\\r", "\r")
                     .Replace("\\t", "\t")
                     .Replace("\\\\", "\\");

        return value;
    }
}
