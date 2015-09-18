using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private float totalTime;
	private bool gameStart;

	public float countDownTime;
	public Text countDownTxt;

	public float gameTime;
	public Text gameTimer;

	public List<GameObject> players;

	// Use this for initialization
	void Start () {
		countDownTime += 1;
		totalTime = gameTime;
		RunTimer();
		totalTime = countDownTime;
		gameStart = false;
		foreach(GameObject p in players) {
			StopMov(p);
		}
	}
	
	// Update is called once per frame
	void Update () {
		totalTime -= Time.deltaTime;
		if(!gameStart) {
			CountDown();
		}
		else {
			RunTimer();
		}
	}

	void StopMov(GameObject p) {
		p.GetComponent<BoatController>().enabled = false;;
	}

	void StartMov(GameObject p) {
		p.GetComponent<BoatController>().enabled = true;
	}

	void CountDown() {
		int t = (int) totalTime;
		if(t > 0) {
			countDownTxt.text = t.ToString();
		}
		else {
			countDownTxt.enabled = false;
			gameStart = true;
			totalTime = gameTime;
			foreach(GameObject p in players) {
				StartMov(p);
			}			
		}
	}

	void RunTimer() {
		int minutes = (int) (totalTime/60);
		string seconds = string.Format("{0:00.00}", totalTime - minutes*60);
		gameTimer.text = minutes.ToString() + ":" + seconds;
	}
}