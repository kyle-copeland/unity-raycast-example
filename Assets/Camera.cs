using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Transform player;
	public Vector3 distance;
	void Start() {
		distance = - player.position + transform.position;
	}
	void Update () {
		transform.Translate(distance + player.position - transform.position);
	}
}
