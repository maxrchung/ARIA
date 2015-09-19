using UnityEngine;
using System.Collections;

public class DriverController : MonoBehaviour {

	public GameObject parent;
	public GameObject driverPos;
	public GameObject driver;
	public Text playerIndicator;

	private bool keyPressed;
	private string keyUp;
	private string keyLeft;
	private string keyRight;
	private BoatController ctrlScript;

	private Animator anim;

	// Use this for initialization
	void Start () {
		ctrlScript = parent.GetComponent<BoatController>();
		keyPressed = false;
		keyUp = ctrlScript.upKey;
		keyLeft = ctrlScript.leftKey;
		keyRight = ctrlScript.rightKey;

		anim = driver.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePos();
		CheckKeyPressed();

		if(keyPressed) {
			anim.SetBool("moving", true);
			ChangeMovingAnim();
		}
		else {
			anim.SetBool("moving", false);
		}
	}

	public void SetNumber(string num) {
		playerIndicator.text = "p" + num;
	}

	void CheckKeyPressed() {
		if(!(Input.GetKey(keyUp) || Input.GetKey(keyRight) || Input.GetKey(keyLeft)) || !ctrlScript.enabled) {
			keyPressed = false;
		}
		else {
			keyPressed = true;
		}
	}

	void ChangeMovingAnim() {
			// start driver up anim
		if((65 > parent.transform.eulerAngles.z || parent.transform.eulerAngles.z >= 295) && anim.GetInteger("dir") != 1) {
			anim.SetInteger("dir", 1);
		}
		// start Turn driver right anim
		else if(225 <= parent.transform.eulerAngles.z && parent.transform.eulerAngles.z < 295 && anim.GetInteger("dir") != 2) {
			anim.SetInteger("dir", 2);
		}
		// down anim
		else if(135 <= parent.transform.eulerAngles.z && parent.transform.eulerAngles.z < 225 && anim.GetInteger("dir") != 3) {
			anim.SetInteger("dir", 3);
		}
		// left anim
		else if(65 <= parent.transform.eulerAngles.z && parent.transform.eulerAngles.z < 135 && anim.GetInteger("dir") != 4) {
			anim.SetInteger("dir", 4);
		}
	}

	void UpdatePos() {
		transform.position = new Vector2(driverPos.transform.position.x, driverPos.transform.position.y);
	}
}
