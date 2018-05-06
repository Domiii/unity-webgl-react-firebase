using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.IO;
using System.Text.RegularExpressions;



/// <summary>
/// Adds a new "Add custom Editor" MenuItem to Script files in the Project view.
/// 
/// <see cref="https://stackoverflow.com/a/50203146/2228771"/>
/// <see cref="https://gist.github.com/Domiii/7df09313f3aae3d32fccae1434ea6e9c/edit"/>
/// </summary>
[InitializeOnLoad]
public static class AddCustomEditorMenuItem
{
	/// <summary>
	/// NOTE: Application.dataPath represents the Assets/ path in the Editor.
	/// </summary>
	public static readonly string AssetsPath = Path.GetFullPath(Application.dataPath);
	public static readonly string ProjectRoot = Path.GetFullPath(AssetsPath + "/../");

	// http://rextester.com/WTDR11798
	static readonly Regex scriptPathRegex = new Regex (@"^(.*[/\\]Scripts)[/\\](.*[/\\])?[\w_][\w\d_]+\.cs$");
	static readonly Regex editorScriptPathRegex = new Regex (@"^(.*[/\\]Editor)[/\\](.*[/\\])?[\w_][\w\d_]+\.cs$");

	static string BuildCustomEditorCode (string name)
	{
		return @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(" + name + @"))]
public class " + name + @"Editor : Editor {
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var obj = (" + name + @") target;

		if (GUILayout.Button (""Hi!"")) {
			// do something with obj when button is clicked
			Debug.Log(""Button pressed for: "" + obj.name);
			EditorGUIUtility.PingObject (obj);
		}

	}
}";
	}

	class ScriptPathInfo
	{
		/// <summary>
		/// Complete system path to script file
		/// </summary>
		public string scriptPath;

		/// <summary>
		/// The path to the Scripts/ folder.
		/// </summary>
		public string scriptsPath;

		/// <summary>
		/// Path of file, relative to Scripts/ folder
		/// </summary>
		public string scriptRelativePath;

		public string scriptName;

		public string editorFileName;
		public string editorPath;
		public string editorRelativeAssetPath;

		public ScriptPathInfo(Object scriptAsset) {
			scriptName = scriptAsset.name;
			
			// get system file path
			scriptPath = Path.GetFullPath(ProjectRoot + AssetDatabase.GetAssetPath (scriptAsset));

			// get file name of the editor file
			editorFileName = GetEditorFileNameFor (scriptName);

			// split the script path
			var results = scriptPathRegex.Matches (scriptPath).GetEnumerator ();
			results.MoveNext ();
			var match = (Match)results.Current;
			scriptsPath = match.Groups [1].Value;
			scriptRelativePath = match.Groups [2].Value;

			// re-combine editor path
			editorPath = Path.Combine (scriptsPath, "Editor");
			editorPath = Path.Combine (editorPath, scriptRelativePath);
			editorPath = Path.Combine (editorPath, editorFileName);

			// nicely formatted file path
			editorPath = Path.GetFullPath(editorPath);
			editorRelativeAssetPath = editorPath.Substring(ProjectRoot.Length);
		}

		public void WriteCustomEditorFile ()
		{
			// create all missing directories in the hierarchy
			Directory.CreateDirectory (Path.GetDirectoryName (editorPath));

			// write file
			File.WriteAllText (editorPath, BuildCustomEditorCode(scriptName));

			// let Asset DB pick up the new file
			AssetDatabase.Refresh();

			// highlight in GUI
			var os = AssetDatabase.LoadAllAssetsAtPath(editorRelativeAssetPath);
			EditorGUIUtility.PingObject (os[0]);

			// log
			Debug.Log("Created new custom Editor at: " + editorRelativeAssetPath);
		}

		static string GetEditorFileNameFor (string scriptName)
		{
			return scriptName + "Editor.cs";
		}
	}

	/// <summary>
	/// The menu item entry
	/// </summary>
	[MenuItem ("Assets/Add Custom Editor %#e", false, 0)]
	public static void AddCustomEditor ()
	{
		var scriptAsset = Selection.activeObject;

		// figure out paths
		var scriptPathInfo = new ScriptPathInfo (scriptAsset);

		// write file
		scriptPathInfo.WriteCustomEditorFile ();
	}

	[MenuItem ("Assets/Add Custom Editor %#e", true, 0)]
	public static bool ValidateAddCustomEditor ()
	{
		var scriptAsset = Selection.activeObject;

		if (scriptAsset == null) {
			// nothing selected? (should probably not happen)
			return false;
		}

		var path = ProjectRoot + AssetDatabase.GetAssetPath (scriptAsset);

		if (!scriptPathRegex.IsMatch (path)) {
			// not a Script in the Scripts folder
			return false;
		}

		if (editorScriptPathRegex.IsMatch (path)) {
			// we are not interested in Editor scripts
			return false;
		}
			

		if (Directory.Exists (path)) {
			// it's a directory, but we want a file
			return false;
		}

		var scriptPathInfo = new ScriptPathInfo (scriptAsset);

//		Debug.Log (scriptPathInfo.scriptPath);
//		Debug.Log (Path.GetFullPath(AssetsPath + "/../"));
//		Debug.Log (scriptPathInfo.editorRelativeAssetPath);
//		Debug.Log (scriptPathInfo.editorPath);

		if (File.Exists (scriptPathInfo.editorPath)) {
			// editor has already been created
			return false;
		}

		// all good!
		return true;
	}


	#region More Editor Script Examples
	//	// add editor keyboard shortcuts!
	//	static EditorHotkeysTracker()
	//	{
	//		SceneView.onSceneGUIDelegate += view =>
	//		{
	//			var e = Event.current;
	//			if (e != null && e.keyCode != KeyCode.None)
	//				Debug.Log("Key pressed in editor: " + e.keyCode);
	//		};
	//	}

	//	/// Add a context menu named "Do Something" in the inspector
	//	/// of the attached script.
	//	[ContextMenu ("Do Something")]
	//	void DoSomething ()
	//	{
	//		Debug.Log ("Do something with GO!");
	//	}
	#endregion
}
