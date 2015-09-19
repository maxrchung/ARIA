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
		if (team==1){
			gameObject.GetComponent<Text>().text=mainCamera.GetComponent<GameManager>().GetTeam1Score().ToString();
		}
		else if (team==2){
			gameObject.GetComponent<Text>().text=mainCamera.GetComponent<GameManager>().GetTeam2Score().ToString();
		}
	}
}
