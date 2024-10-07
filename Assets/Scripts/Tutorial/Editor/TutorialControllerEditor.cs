using UnityEditor;
using UnityEngine;

namespace Tutorial
{
    [CustomEditor(typeof(TutorialController))]
    public class TutorialControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TutorialController tutorialController = (TutorialController) target;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Next Step"))
            {
                tutorialController.NextStep();
            }
            if (GUILayout.Button("Skip Tutorial"))
            {
                tutorialController.SkipTutorial();
            }
            GUILayout.EndHorizontal();
        }
    }
}