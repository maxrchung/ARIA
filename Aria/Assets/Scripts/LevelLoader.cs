using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	private string lvlName;
	public string gameScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectLevel(string name) {
		lvlName = name;
		GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Transition>().StartRoutine(lvlName);
	}


}
