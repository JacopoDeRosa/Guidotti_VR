using UnityEditor;
using UnityEngine;

namespace Molecules
{
    [CustomEditor(typeof(Invokan))]
    public class InvokanEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Invokan invokan = (Invokan) target;
            
            if (GUILayout.Button("Try Plug Receptor"))
            {
                invokan.TryPlugReceptor();
            }
        }
    }
}