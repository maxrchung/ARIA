using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	private string lvl;
	public float transTime;
	private int direction;
	public float speed;

	private float alpha = 0f;
	public Texture2D fadeOutTexture;
	private string gameScene = "Bruce";

	// Use this for initialization
	void Awake() {
	}
	
	void OnGUI() {
		alpha += direction * speed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.DrawTexture( new Rect(0,0,Screen.width, Screen.height), fadeOutTexture);
	}

	public float Fade(int d, float t = 1f) {
		direction = d;
		transTime = t;
		Debug.Log(transTime);
		return transTime;
	}

	public void StartRoutine(string b) {
		StartCoroutine(Load(b));
	}

	public IEnumerator Load(string lvlName) {
		yield return new WaitForSeconds(Fade(1));
		Application.LoadLevel(lvlName);
		float time = Fade(-1);
		yield return new WaitForSeconds(.2f*time);
		if(lvlName.Equals(gameScene)) {
			GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<GameManager>().SetupGame();
		}
		yield return new WaitForSeconds(.8f*time);
		if(lvlName.Equals(gameScene)) {
			GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<GameManager>().StartNOW();
		}
	}


}