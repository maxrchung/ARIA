using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InputListener : MonoBehaviour {

	private GameObject cam;
	public List<string> leftKeys;
	public List<string> rightKeys;

	private bool xmlUpdated;

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
		xmlUpdated = false;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < rightKeys.Count; i++) {
			if(Input.GetKeyDown(rightKeys[i])) {
				if(!red.Contains(i) && red.Count < teamSize) {
					if(blue.Contains(i)) {
						blue.Remove(i);
					}
					red.Add(i);
					GameObject.FindGameObjectsWithTag(leftKeys[i] + rightKeys[i])[0].GetComponent<Image>().color = Color.red;
				}
			}
		}
		for(int i = 0; i < leftKeys.Count; i++) {
			if(Input.GetKeyDown(leftKeys[i])) {
				if(!blue.Contains(i) && blue.Count < teamSize) {
					if(red.Contains(i)) {
						red.Remove(i);
						Debug.Log(red.Count	);
					}
					blue.Add(i);
					GameObject.FindGameObjectsWithTag(leftKeys[i] + rightKeys[i])[0].GetComponent<Image>().color = Color.blue;
				}
			}
		}
		if(blue.Count == teamSize && red.Count == teamSize && !xmlUpdated) {
			for(int i = 0; i < blue.Count; i++) {
				cam.GetComponent<GameManager>().AddTeamMember("teamOne", (red[i] + 1).ToString(), "playerRed");
				cam.GetComponent<GameManager>().AddTeamMember("teamTwo", (blue[i] + 1).ToString(), "playerBlue");		
			}
			startGame.enabled = true;
			xmlUpdated = true;
		}
	}
}
