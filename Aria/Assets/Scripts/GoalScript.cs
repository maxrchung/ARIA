using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {
	public int team;
	private GameObject mainCamera;
	private CameraShakeScript shake;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
		shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		
		if (other.gameObject.transform.tag.Equals("Cat")){
			shake.screenSlam(0.2f,0.5f);
			StartCoroutine (Wait (2.0f));
			//Change Score
			mainCamera.GetComponent<GameManager>().AddScore(team);
		}
	}

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		//Reset Players
		mainCamera.GetComponent<GameManager>().NewRound();
	}
}
