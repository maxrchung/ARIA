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
		StartCoroutine(Load());
	}

	IEnumerator Load() {
		yield return new WaitForSeconds(this.GetComponent<Transition>().Fade(1));
		Application.LoadLevel(lvlName);
		float time = this.GetComponent<Transition>().Fade(-1);
		yield return new WaitForSeconds(.2f*time);
		if(lvlName.Equals(gameScene)) {
			this.GetComponent<GameManager>().SetupGame();
		}
		yield return new WaitForSeconds(.8f*time);
		if(lvlName.Equals(gameScene)) {
			this.GetComponent<GameManager>().StartNOW();
		}
	}


}
