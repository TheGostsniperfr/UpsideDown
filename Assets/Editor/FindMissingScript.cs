using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FindMissingScript
{
    [MenuItem("Tools/Find Missing Scripts In projects")]
    static void FindMissingScriptsInProjectMenuItem()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();

        foreach (string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            foreach (Component component in prefab.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.Log("Prefab found with missing script " + path, prefab);
                    break;
                }
            }
        }
    }

    [MenuItem("Tools/Find Missing Scripts In Scene")]
    static void FindMissingScriptsInSceneMenuItem()
    {

        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
        {
            foreach (Component component in gameObject.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.Log("GameObject found with missing script " + gameObject.name, gameObject);
                    break;
                }
            }
        }
    }
}
