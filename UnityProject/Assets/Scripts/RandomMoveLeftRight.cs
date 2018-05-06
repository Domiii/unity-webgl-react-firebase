using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveLeftRight : MonoBehaviour {
	public float maxRadius = 3;

	/// <summary>
	/// Speed in seconds
	/// </summary>
	public float speed = 3f;

	public float speedDeltaMax = .005f;

	Vector3 startPos;

	void Awake() {
		startPos = transform.position;
	}

	void FixedUpdate () {
//		var angle = Time.time / 2 * Mathf.PI * maxRadius;
//		var s = Mathf.Sin (angle);
		var t = Mathf.PingPong (Time.time * speed, 2 * maxRadius) - maxRadius;
		this.transform.position = startPos + t * Vector3.right;

		// randomize speed as well
		speed += Random.Range(-speedDeltaMax, speedDeltaMax);
	}
}
