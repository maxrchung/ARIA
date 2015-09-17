using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private float totalTime;
	private bool gameStart;

	// Use this for initialization
	void Start () {
		totalTime = 0;
		gameStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;
		if(!gameStart) {
			CountDown();
		}
	}

	void CountDown() {

	}
}