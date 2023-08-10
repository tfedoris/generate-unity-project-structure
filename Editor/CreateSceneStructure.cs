using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CreateSceneStructure : EditorWindow
    {
        [MenuItem("GameObject/Custom Tools/Create Scene Structure")]
        private static void CreateGameObjects()
        {
            CreateGameObject("Debug");
            CreateGameObject("Management");
            CreateGameObject("UI");
            CreateGameObject("Cameras", new []{ "Main Camera" });
            CreateGameObject("Lights", new []{ "Directional Light" });
            CreateGameObject("World", null, new []{ "Terrain", "Props" });
            CreateGameObject("Gameplay", null, new []{ "Actors", "Items" });
            CreateGameObject("_Dynamic");
        }

        private static void CreateGameObject(string parentObjectName, IEnumerable<string> existingChildObjectNames = null, IEnumerable<string> newChildObjectNames = null)
        {
            var parentObject = GameObject.Find(parentObjectName);
            if (!parentObject)
            {
                parentObject = new GameObject(parentObjectName);
            }

            var parentObjectTransform = parentObject.transform;

            CreateChildParentRelationships(parentObjectTransform, existingChildObjectNames, false);
            CreateChildParentRelationships(parentObjectTransform, newChildObjectNames, true);
        }

        private static void CreateChildParentRelationships(Transform parentObjectTransform, IEnumerable<string> childObjectNames,
            bool createNewObjectIfNotFound)
        {
            if (childObjectNames == null) return;
            foreach (string objectName in childObjectNames)
            {
                var gameObject = GameObject.Find(objectName);
                if (!gameObject && createNewObjectIfNotFound)
                {
                    gameObject = new GameObject(objectName);
                }

                if (gameObject)
                {
                    gameObject.transform.SetParent(parentObjectTransform);
                }
            }
        }
    }
}