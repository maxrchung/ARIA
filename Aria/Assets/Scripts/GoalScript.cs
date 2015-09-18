using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {
	public int team;
	public Camera mainCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Ball")){
			//Change Score
			mainCamera.GetComponent<GameManager>().AddScore(team);
			//Reset Players
			//Reset Ball
		}
	}
}
