using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {
	public int team;
	private GameObject mainCamera;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log("Sad face?");
		
		if (other.gameObject.transform.tag.Equals("Cat")){
			Debug.Log("Sad face?");
			//Change Score
			mainCamera.GetComponent<GameManager>().AddScore(team);
			//Reset Players
			mainCamera.GetComponent<GameManager>().NewRound();
		}
	}
}
