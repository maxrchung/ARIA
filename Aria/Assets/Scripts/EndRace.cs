using UnityEngine;
using System.Collections;

public class EndRace : MonoBehaviour {

	public bool finished;
	public string winner;
	public GameObject player1;
	public GameObject player2;

	// Use this for initialization
	void Start () {
		finished = false;
		winner = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!finished && (other.gameObject.tag == player1.tag || other.gameObject.tag == player2.tag)) {
			finished = true;
			winner = other.gameObject.tag;
			Debug.Log(winner + " won");
		}
	}
}
