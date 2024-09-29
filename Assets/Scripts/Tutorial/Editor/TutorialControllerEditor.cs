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
            if (GUILayout.Button("Next Step"))
            {
                tutorialController.NextStep();
            }
        }
    }
}