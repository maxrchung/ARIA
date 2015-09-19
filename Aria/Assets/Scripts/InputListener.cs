using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InputListener : MonoBehaviour {

	private GameObject cam;
	public List<string> leftKeys;
	public List<string> rightKeys;

	public Button startGame;
	private List<int> red;
	private List<int> blue;
	private int teamSize;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];		
		red = new List<int>();
		blue = new List<int>();
		teamSize = leftKeys.Count/2;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < leftKeys.Count; i++) {
			if(Input.GetKeyDown(leftKeys[i])) {
				if(!red.Contains(i) && red.Count < teamSize) {
					cam.GetComponent<GameManager>().AddTeamMember("teamOne", (i + 1).ToString(), "playerRed");
					red.Add(i);
					GameObject.FindGameObjectsWithTag(leftKeys[i] + rightKeys[i])[0].GetComponent<Image>().color = Color.red;
				}
			}
		}
		for(int i = 0; i < rightKeys.Count; i++) {
			if(Input.GetKeyDown(rightKeys[i])) {
				if(!blue.Contains(i) && blue.Count < teamSize) {
					cam.GetComponent<GameManager>().AddTeamMember("teamTwo", (i + 1).ToString(), "playerBlue");
					blue.Add(i);
					GameObject.FindGameObjectsWithTag(leftKeys[i] + rightKeys[i])[0].GetComponent<Image>().color = Color.blue;
				}
			}
		}
		if(blue.Count == teamSize && red.Count == teamSize) {
			startGame.gameObject.SetActive(true);
		}
	}
}
