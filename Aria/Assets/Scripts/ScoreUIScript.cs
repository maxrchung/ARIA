using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUIScript : MonoBehaviour {
	public Camera mainCamera;
	public int team;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text>().text="DERP";//mainCamera.GetComponent<GameManager>();
	}
}
