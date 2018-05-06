using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor {
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var obj = (Spawner) target;

		if (GUILayout.Button ("Spawn One")) {
			obj.SpawnOne ();
		}

	}
}
