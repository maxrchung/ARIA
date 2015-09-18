using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class GameManager : MonoBehaviour {

	private float totalTime;
	private bool gameStart;

	private int team1Score;
	private int team2Score;

	public float countDownTime;
	public Text countDownTxt;

	public float gameTime;
	public Text gameTimer;

	private List<GameObject> players;

	private XmlDocument xmlDoc;
	private XmlNodeList redTeam;
	private XmlNodeList blueTeam;
	private XmlNodeList posRed;
	private XmlNodeList posBlue;

	public GameObject redBoat;
	public GameObject blueBoat;
	public GameObject redDriver;
	public GameObject blueDriver;

	// Use this for initialization
	void Start () {
		LoadXML();
		countDownTime += 1;
		totalTime = gameTime;
		RunTimer();
		totalTime = countDownTime;
		gameStart = false;

		SetupGame();
		Debug.Log("PLAYER COUNT _--------------------------------------------" + players.Count);
		/*
		foreach(GameObject p in players) {
			StopMov(p);
		}
		*/
		team1Score=0;
		team2Score=0;
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

	public void AddScore(int team){
		if (team==1){
			team1Score++;
		}
		else if (team==2){
			team2Score++;
		}
	}

	void CreateTeams(XmlNodeList team, XmlNodeList positions, GameObject boatPrefab, GameObject driverPrefab) {
		
		for(int i = 0; i < team.Count; i++) {
			Debug.Log(positions.Item(i).Attributes["x"].Value);
			Vector3 temp = new Vector3(float.Parse(positions.Item(i).Attributes["x"].Value), float.Parse(positions.Item(i).Attributes["y"].Value), 0f);

			GameObject pBoat = (GameObject) Instantiate(boatPrefab, temp, Quaternion.identity);
			
			XmlNode controls = xmlDoc.DocumentElement.SelectNodes("p" + team.Item(i).Attributes["num"].Value).Item(0);
			Debug.Log(controls);
			pBoat.GetComponent<BoatController>().upKey = controls.Attributes["up"].Value;
			
			pBoat.GetComponent<BoatController>().leftKey = controls.Attributes["left"].Value;
			pBoat.GetComponent<BoatController>().downKey = controls.Attributes["down"].Value;
			pBoat.GetComponent<BoatController>().rightKey = controls.Attributes["right"].Value;


			GameObject pDriver = (GameObject) Instantiate(driverPrefab);
			pDriver.GetComponent<DriverController>().parent = pBoat;
			pDriver.GetComponent<DriverController>().driverPos = pBoat.transform.GetChild(0).gameObject;

			players.Add(pBoat);
			Debug.Log("adding - -- " + players.Count);
		}
		Debug.Log("adding - -- --------------- END OF FOR LOOP" + players.Count);
		
	}

	void SetupGame() {
		players = new List<GameObject>();
		CreateTeams(redTeam, posRed, redBoat, redDriver);
		CreateTeams(blueTeam, posBlue, blueBoat, blueDriver);
	}

	void LoadXML() {
		//Debug.Log("Loading XML for levels");
		string filePath = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\XML\\main.xml";
		print(filePath);
		if (File.Exists (filePath)) {
			//Debug.Log("Found XML file");
			xmlDoc = new XmlDocument();
			try {
				xmlDoc.Load (filePath);
			} catch (FileNotFoundException) {
				Debug.Log ("The file for loading the XML was not found");
				return;
			}

			redTeam = xmlDoc.GetElementsByTagName ("playerRed");
			blueTeam = xmlDoc.GetElementsByTagName ("playerBlue");
			string gameType = "oneVone";

			if(redTeam.Count == 2) {
				gameType = "twoVtwo";
			}

			posRed = xmlDoc.DocumentElement.SelectNodes(gameType + "/redTeam/pos");
			posBlue = posRed = xmlDoc.DocumentElement.SelectNodes(gameType + "/blueTeam/pos");

		}
		else {
			Debug.Log("Can't find xml");
		}
	}

}