using UnityEngine;

public class FilePaths
{
    private const string HOME_DIRECTORY_SYMBOL = "~/";

    public static readonly string root = $"{Application.dataPath}/gameData/";

    //Resource Paths
    public static readonly string resources_dialogueFiles = $"DialogueFiles/";


/// <summary>
/// Returns the path of the resource using the default path or the root of the resource if the resource name begins with the Home Directory Symbol
/// </summary>
/// <param name="defaultPath"></param>
/// <param name="resourceName"></param>
/// <returns></returns>
    public static string GetPathToResource(string defaultPath, string resourceName)
    {
        if (resourceName.StartsWith(HOME_DIRECTORY_SYMBOL))
        {
            return resourceName.Substring(HOME_DIRECTORY_SYMBOL.Length);
        }

        return defaultPath + resourceName;
    }
}