using UnityEngine;
using System.Collections;

public class PlayerControllerTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.RightArrow)) {
			this.transform.position = new Vector2((float) (transform.position.x + .01), (float) transform.position.y);
		}
	}
}
