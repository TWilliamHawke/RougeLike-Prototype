using UnityEditor;
using UnityEngine;

namespace Core
{
    public class InjectorsManager : MonoBehaviour
    {
        private void OnDestroy()
        {
            string[] assetNames = AssetDatabase.FindAssets("t:Injector", new[] { "Assets/Runtime/Scripts/Injectors" });
            foreach (string assetName in assetNames)
            {
                var path = AssetDatabase.GUIDToAssetPath(assetName);
                var injector = AssetDatabase.LoadAssetAtPath<Injector>(path);
                injector?.ClearDependency();
            }
        }
    }
}

