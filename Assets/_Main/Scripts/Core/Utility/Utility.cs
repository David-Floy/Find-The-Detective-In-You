
using UnityEngine;

public class Utility 
{
    public static GameObject FindByTagName(GameObject[] gameObjects, string name)
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.name == name)
            {
                return gameObject;
            }
        }

        return null;
    }
}
