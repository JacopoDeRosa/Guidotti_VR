
using UnityEditor;
using UnityEngine;

namespace GameLogic
{
    [CustomEditor(typeof(GameController))]
    public class GameControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameController controller = (GameController) target;
            if (GUILayout.Button("Refresh Spawner List"))
            {
                controller.RefreshSpawners();
            }
        }
    }
}