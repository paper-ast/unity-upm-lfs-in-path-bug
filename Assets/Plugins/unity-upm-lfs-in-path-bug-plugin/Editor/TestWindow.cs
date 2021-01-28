using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestWindow : EditorWindow
{
    private static string packageName = "com.test.upm-lfs-in-path-bug";
    private static string resourcesDir = "PackageResources";
    private static string resourcesPath;
    private static string textureName = "unity-logo.png";
    private static Texture2D texture2D;
    
    [MenuItem("Tools/Test Window")]
    static void Init()
    {
        
        string[] dirs = Directory.GetDirectories(Environment.CurrentDirectory, resourcesDir, SearchOption.AllDirectories);
        resourcesPath = dirs[0].Replace(Environment.CurrentDirectory + Path.DirectorySeparatorChar, "") + Path.DirectorySeparatorChar;

        if (resourcesPath.Contains(packageName))
        {
            resourcesPath = Path.Combine("Packages", packageName, resourcesDir) + Path.DirectorySeparatorChar;
        }
        
        texture2D = AssetDatabase.LoadAssetAtPath(resourcesPath + textureName, typeof(Texture2D)) as Texture2D;
        
        TestWindow window = (TestWindow)EditorWindow.GetWindowWithRect(typeof(TestWindow), new Rect(0, 0, 450, 470));
        window.Show();
    }
    
    void OnGUI()
    {
        GUILayout.Label("Here MUST be Texture", EditorStyles.boldLabel);

        if (texture2D != null)
        {
            //GUI.DrawTexture(Rect.zero, texture2D);
            EditorGUI.DrawTextureTransparent(new Rect(0, 20, 450, 450), texture2D);
        }
        else
        {
            GUILayout.Label("But texture2D == null ", EditorStyles.boldLabel);
            GUILayout.Label("because in we see in" + textureName + " \n", EditorStyles.boldLabel);

            var fullPath = Path.GetFullPath(resourcesPath + textureName);

            if (File.Exists(fullPath))
            {
                var text = File.ReadAllText(fullPath);
                GUILayout.Label(text, EditorStyles.label); 
            }
        }
    }
    
}
