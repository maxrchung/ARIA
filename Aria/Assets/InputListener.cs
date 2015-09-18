using UnityEngine;
using System.Collections;

public class InputListener : MonoBehaviour {

	private GameObject cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectsWithTag("MainCamera");		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
