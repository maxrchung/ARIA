using UnityEngine;
using System.Collections;

public class BoatController : MonoBehaviour {
	public float rotationSpeed;
	public float acceleration;

	public string upKey;
	public string leftKey;
	public string rightKey;
	private int horizontalDir;

	// Use this for initialization
	void Start () {
		horizontalDir = 0;
	}

	void FixedUpdate () {
		if(Input.GetKey(leftKey)) {
			horizontalDir = -1;
		}
		else if(Input.GetKey(rightKey)) {
			horizontalDir = 1;
		}
		else {
			horizontalDir = 0;
		}

		if (Input.GetKey(upKey)) {
			gameObject.GetComponent<Rigidbody2D> ().AddRelativeForce (new Vector2 (0, acceleration));
		}
		if (horizontalDir != 0) {
			float force = Mathf.Sqrt(2*(Mathf.Pow(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude,2F))-2*(Mathf.Pow(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude,2F))*Mathf.Cos((Mathf.PI/4)*Input.GetAxis ("Horizontal")));
			float angle = (Mathf.PI-(Mathf.PI/4)*horizontalDir)/2;
			gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-transform.TransformVector(new Vector2 (Mathf.Cos (angle)*force,/*-Mathf.Sin(angle)*force*/0)), transform.localPosition);
		}
		//print (new Vector2 (-transform.InverseTransformVector(gameObject.GetComponent<Rigidbody2D>().velocity).x, 0));
		if (transform.InverseTransformVector(gameObject.GetComponent<Rigidbody2D>().velocity).x!=0){
			Vector2 force = transform.InverseTransformVector(gameObject.GetComponent<Rigidbody2D>().velocity);
			//print (force.x);
			gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(-new Vector2 (force.x, 0));
		}
	}
}