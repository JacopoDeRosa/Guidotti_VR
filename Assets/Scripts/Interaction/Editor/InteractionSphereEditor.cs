using UnityEditor;
using UnityEngine;

namespace Interaction
{
    [CustomEditor(typeof(InteractionShere))]
    public class InteractionSphereEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            InteractionShere interactionSphere = (InteractionShere) target;
            if (GUILayout.Button("Interact"))
            {
                interactionSphere.OnInteract.Invoke();
            }
        }
    }
}