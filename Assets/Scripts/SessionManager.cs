using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour {
	public bool SingleUserSession = true;
	public bool SimulateSelf = true;
	public bool SimulateOther = true;
	public GameObject SimPlayer;
	public GameObject[] activePlayers;
	bool otherInitialized = false;
	public GameObject activePlayer;

	// Use this for initialization
	void Start () {}
		

		
		

	
	// Update is called once per frame
	void Update () {



		// we initialize the simulated player 2 opposite of the user.
		if ((SingleUserSession == true) && (otherInitialized != true)) {

				if (activePlayers == null) {
					activePlayers = GameObject.FindGameObjectsWithTag("Player");


				} 

			if (activePlayers.Length == 0) {
				activePlayers = GameObject.FindGameObjectsWithTag("Player");
			}


					else {

					 activePlayer = activePlayers [0];
					int playerNumber =  activePlayer.GetComponent<PlayerManager> ().PlayerNumber;
					SimPlayer.SetActive (true);

				if (playerNumber == 2) {
					SimPlayer.GetComponent<PlayerManager> ().PlayerNumber = 1;
					SimPlayer.GetComponent<PlayerManager> ().AuraController = GameObject.Find ("Player1_Manager");
					SimPlayer.GetComponent<PlayerManager> ().AuraExpander = GameObject.Find ("Aura_player1Expander");
					SimPlayer.GetComponent<PlayerManager> ().BridgeBars = GameObject.Find ("Player1_BridgeLayers");
				} else {
					SimPlayer.GetComponent<PlayerManager> ().PlayerNumber = 2;
					SimPlayer.GetComponent<PlayerManager> ().AuraController = GameObject.Find ("Player2_Manager");
					SimPlayer.GetComponent<PlayerManager> ().AuraExpander = GameObject.Find ("Aura_player2Expander");
					SimPlayer.GetComponent<PlayerManager> ().BridgeBars = GameObject.Find ("Player2_BridgeLayers");
					}
					otherInitialized = true;

				}

	}
		


		if (Input.GetKeyDown (KeyCode.I)) {SimPlayer.SetActive (true);}
	}
}
