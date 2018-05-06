using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomMoveLeftRight))]
public class RandomMoveLeftRightEditor : Editor {
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var obj = (RandomMoveLeftRight) target;

		if (GUILayout.Button ("Hi!")) {
			// do something with obj when button is clicked
			Debug.Log("Button pressed for: " + obj.name);
			EditorGUIUtility.PingObject (obj);
		}

	}
}