using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	GameObject SessionManager;
	GameObject DataHolder;
	GameObject SpawnPoint1;
	GameObject SpawnPoint2;
	public int PlayerNumber;
	float breatheNow = 0f;
	float breathePast = 0f;
	bool inBreathStart = false;
	bool inBreathContinues = false;
	bool outBreathStart = false;
	bool outBreathContinues = false;

	// Use this for initialization
	void Start () {

		SessionManager = GameObject.Find ("SessionManager");
		DataHolder = GameObject.Find ("DataHolder");

		SpawnPoint1 = GameObject.Find ("Spawn Point 1");
		SpawnPoint2 = GameObject.Find ("Spawn Point 2");

		//find out which player this one is.
		float dist1 = Vector3.Distance(this.transform.position, SpawnPoint1.transform.position);
		float dist2 = Vector3.Distance(this.transform.position, SpawnPoint2.transform.position);
		if (dist1 < dist2) { 
			PlayerNumber = 1; 
			Debug.Log ("Player 1 found");
		}
		else{
			PlayerNumber = 2; 
			Debug.Log ("Player 2 found");
		}


	}




	
	// Update is called once per frame
	void Update () {


		if (PlayerNumber == 1) {
			breathePast = breatheNow;
			breatheNow = DataHolder.GetComponent<SimulationData> ().P1Breathing;
		
		}
		if (PlayerNumber == 2) {
			breathePast = breatheNow;
			breatheNow = DataHolder.GetComponent<SimulationData> ().P1Breathing;
		
		}


/*


		if (breatheNow < breathePast) {
			Debug.Log ("Player " + PlayerNumber + " breathing out");
			outBreathStart = true;
			if (outBreathContinues == true;
			

			outBreathContinues = false;
			inBreathContinues = false;
			inBreathStart = false;

			if (outBreathStart == true) {
				//initialize waves
				outBreathStart = 
			} 
		
		}*/







		if (breatheNow => breathePast) {


			Debug.Log ("Player " + PlayerNumber + " breathing in");
			outBreathStart = false;
			inBreathStart = true;

		}


		
	}
}
