using UnityEngine;
using UnityEditor;

namespace Coalesce
{
    [CustomEditor(typeof(PhysicsSimulation))]
    public class PhysicsSimulationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PhysicsSimulation sim = (PhysicsSimulation)target;

            if (GUILayout.Button("Simulate"))
            {
                sim.RunSimulation();
            }

            if (GUILayout.Button("Reset"))
            {
                sim.ResetAllBodies();
            }
        }
    }
}
