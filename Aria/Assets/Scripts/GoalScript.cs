using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {
	public int team;
	private GameObject mainCamera;
	private CameraShakeScript shake;
	private bool scoreable;
	private ParticleSystem particles;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
		shake = mainCamera.GetComponent<CameraShakeScript>();
		particles = transform.GetChild (0).GetComponent<ParticleSystem>();
		scoreable=true;
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.transform.tag.Equals("Cat") && scoreable){
			print (":(");
			scoreable=false;
			shake.screenSlam(0.2f,1.0f);
			StartCoroutine (Wait (4.0f));
			if (!particles.isPlaying){
				particles.Play();
			}
			//Change Score
			mainCamera.GetComponent<GameManager>().AddScore(team);
		}
	}

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		//Reset Players
		mainCamera.GetComponent<GameManager>().NewRound();
		if (!particles.isStopped){
			particles.Stop();
		}
		scoreable=true;
	}
}
