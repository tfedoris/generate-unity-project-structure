using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CreateFolderStructure : EditorWindow
    {
        private static string _projectName = Application.productName;
        
        [MenuItem("Assets/Custom Tools/Create Folder Structure")]
        private static void SetUpFolders()
        {
            var window = CreateInstance<CreateFolderStructure>();
            window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 400f, 150f);
            window.ShowPopup();
        }
        
        private static void CreateFolders()
        {
            var rootFolders = new List<string>
            {
                "_Developers"
            };
            
            foreach (string folder in rootFolders.Where(folder => !Directory.Exists("Assets/" + folder)))
            {
                Directory.CreateDirectory("Assets/" + folder);
            }

            var projectFolders = new List<string>
            {
                "Characters",
                "FX",
                "Gameplay",
                "_Levels",
                "Lighting",
                "MaterialLibrary",
                "Objects",
                "Objects/Architecture",
                "Objects/Props",
                "Scripts",
                "Sound",
                "UI"
            };
            
            foreach (string folder in projectFolders.Where(folder => !Directory.Exists("Assets/" + _projectName + "/" + folder)))
            {
                Directory.CreateDirectory("Assets/" + _projectName + "/" + folder);
            }
            
            AssetDatabase.Refresh();
        }
        
        private void OnGUI()
        {
            EditorGUILayout.LabelField("Enter the Project name used as the root folder.");
            _projectName = EditorGUILayout.TextField("Project Name: ", _projectName);
            
            Repaint();

            GUILayout.Space(70);

            if (!GUILayout.Button("Generate")) return;
            
            CreateFolders();
            Close();
        }
    }
}