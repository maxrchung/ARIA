using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class GameManager : MonoBehaviour {

	private float totalTime;
	private float previousTime;
	private bool gameStart;

	private int team1Score;
	private int team2Score;

	public float countDownTime;
	public Text countDownTxt;
	private bool countDownOver;

	public float gameTime;
	public Text gameTimer;

	private GameObject cat;
	private Vector3 catPos;

	private List<GameObject> players;
	private List<Vector3> spawnPts;

	private XmlDocument xmlDoc;
	private XmlElement root;
	private XmlNodeList redTeam;
	private XmlNodeList blueTeam;
	private XmlNodeList posRed;
	private XmlNodeList posBlue;

	public GameObject redBoat;
	public GameObject blueBoat;
	public GameObject redDriver;
	public GameObject blueDriver;

	// Use this for initialization
	void Awake () {
		if(GameObject.FindGameObjectsWithTag("MainCamera").Length > 1) {
			Destroy(this);
		}
		else {
			LoadXML();
		}
		SetupGame();
		gameStart = true;
	}
	
	// Update is called once per frame
	void Update () {
		totalTime -= Time.deltaTime;
		if(gameStart) {
			if(!countDownOver) {
				CountDown();
			}
			else {
				UpdateTimer();
			}
		}
	}

	void StopMov(GameObject p) {
		p.GetComponent<BoatController>().enabled = false;
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
			countDownOver = true;
			totalTime = previousTime;
			countDownTxt.GetComponent<Animator>().SetBool("counterOn", !countDownOver);
			foreach(GameObject p in players) {
				StartMov(p);
			}
		}
	}

	void UpdateTimer() {
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
			Vector3 temp = new Vector3(float.Parse(positions.Item(i).Attributes["x"].Value), float.Parse(positions.Item(i).Attributes["y"].Value), 0f);

			GameObject pBoat = (GameObject) Instantiate(boatPrefab, temp, Quaternion.identity);
			
			XmlNode controls = xmlDoc.DocumentElement.SelectNodes("p" + team.Item(i).Attributes["num"].Value).Item(0);
			pBoat.GetComponent<BoatController>().upKey = controls.Attributes["up"].Value;
			
			pBoat.GetComponent<BoatController>().leftKey = controls.Attributes["left"].Value;
			pBoat.GetComponent<BoatController>().downKey = controls.Attributes["down"].Value;
			pBoat.GetComponent<BoatController>().rightKey = controls.Attributes["right"].Value;


			GameObject pDriver = (GameObject) Instantiate(driverPrefab);
			pDriver.GetComponent<DriverController>().parent = pBoat;
			pDriver.GetComponent<DriverController>().driverPos = pBoat.transform.GetChild(0).gameObject;

			players.Add(pBoat);
			spawnPts.Add(temp);
		}
		
	}

	// called each "round" in the game (ie when a goal is scored)
	public void NewRound() {
		SetPositions();

		// Resetting count down
		countDownOver = false;
		previousTime = totalTime;
		totalTime = countDownTime;
		countDownTxt.GetComponent<Animator>().SetBool("counterOn", !countDownOver);
		countDownTxt.enabled = true;

	}

	void SetPositions() {
		for(int i = 0; i < players.Count; i++) {
			players[i].transform.position = spawnPts[i];
			players[i].GetComponent<BoatController>().Stop();
		}
		cat.transform.position = catPos;
		cat.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		cat.GetComponent<Rigidbody2D>().angularVelocity = 0f;
	}

	// only called once per game
	void SetupGame() {
		ReadXML();

		// Set up players		
		players = new List<GameObject>();
		spawnPts = new List<Vector3>();
		CreateTeams(redTeam, posRed, redBoat, redDriver);
		CreateTeams(blueTeam, posBlue, blueBoat, blueDriver);
		foreach(GameObject p in players) {
			StopMov(p);
		}

		// Set up cat 
		cat = GameObject.FindGameObjectsWithTag("Cat")[0];
		catPos = new Vector3(cat.transform.position.x, cat.transform.position.y, 0);
		SetPositions();

		// set up timer
		countDownTime += 1;
		totalTime = gameTime;
		UpdateTimer();
		previousTime = gameTime;
		totalTime = countDownTime;
		countDownOver = false;
		countDownTxt.enabled = true;
		countDownTxt.GetComponent<Animator>().SetBool("counterOn", !countDownOver);

		// set up team score		
		team1Score=0;
		team2Score=0;
	}

	// Get the necessary information to spawn and create the players
	void ReadXML() {
		redTeam = root.SelectNodes("playerRed");
		blueTeam = root.SelectNodes("playerBlue");
		string gameType = "oneVone";

		if(redTeam.Count == 2) {
			gameType = "twoVtwo";
		}

		posRed = root.SelectNodes(gameType + "/redTeam/pos");
		posBlue = root.SelectNodes(gameType + "/blueTeam/pos");
	}

	void LoadXML() {
		string filePath = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\XML\\main.xml";
		if (File.Exists (filePath)) {
			xmlDoc = new XmlDocument();

			try {
				xmlDoc.Load (filePath);
			} catch (FileNotFoundException) {
				Debug.Log ("The file for loading the XML was not found");
				return;
			}

			root = xmlDoc.DocumentElement;


		}
		else {
			Debug.Log("Can't find xml");
		}
	}
}