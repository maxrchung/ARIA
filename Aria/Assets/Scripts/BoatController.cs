using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour {
	public float rotationSpeed;
	public float acceleration;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		if (Input.GetAxis ("Vertical") > 0) {
			gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, acceleration*Input.GetAxis ("Vertical")));
		}
		if (Input.GetAxis ("Horizontal") != 0) {
			gameObject.GetComponent<Rigidbody2D>().AddTorque(Input.GetAxis ("Horizontal"));
		}
	}
}