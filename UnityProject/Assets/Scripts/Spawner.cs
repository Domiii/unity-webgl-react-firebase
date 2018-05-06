using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject prefab;
	public float variance = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//SpawnOne ();

	}

	public void SpawnOne() {
		var pos = transform.position + Vector3.right * Random.Range (-variance, variance);
		var go = (GameObject)Instantiate (prefab, pos, Quaternion.identity);
		Destroy (go, 10);
	}

	public void Spawn(int n = 1) {
		for (var i = 0; i < n; ++i) {
			
		}
	}
}
