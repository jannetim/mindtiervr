using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{



	[Header("Session Parameters")]
	public float StartDelay = 30f;
    public bool SingleUserSession = true;
    public bool SimulateSelf = true;
    public bool SimulateOther = true;
	public Color NeutralEEGColor;
	public float SessionLength = 200f;
	public float StartTimerLength = 10f;
	public bool StartTimerDone = false;
	public bool FileWriting = false;

	/*
	public bool SimulateSelfFA = true;
	public bool SimulateSelfResp = true;
	public bool SimulateOtherFA = true;
	public bool SimulateOtherResp = true;
	*/


	[Header("AdaptationControls")]

	public bool RespSelf = true;
	public bool EegSelf = true;
	public bool EegOther = true;
	public bool RespOther = true;
	[Header("OldControls")]
	public bool Waves = false;
	public bool BridgeMeter = true;

	/*
	public bool BridgeMeterSelf = true;
	public bool BridgeSidesSelf = true;

	public bool AuraVisibleSelf = true;
	//public bool AuraColorSelf = true;
	public bool AuraScalingSelf = true;
	public bool PlayerLightsSelf = true;
	public bool StatueBreathingSelf = true;

	public bool BridgeMeterOth = true;
	public bool BridgeSidesOth = true;
	public bool AuraColorOth = true;
	public bool AuraVisibleOth = true;
	public bool AuraScalingOth = true;
	public bool PlayerLightsOth = true;
	*/




    public GameObject SimPlayer;

    public GameObject[] activePlayers;
    bool otherInitialized = false;
    public GameObject activePlayer;

    // Use this for initialization


    void Awake() { 
        /*
		//Loading parameters from the playerrefs.
		if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
			SingleUserSession = PlayerPrefsX.GetBool ("Param_SingleUserSession");		
			//if (SingleUserSession) { Debug.Log( "single user session");
			//} else { Debug.Log ("multi user session");
		}

		if (PlayerPrefs.HasKey ("Param_RespSelf")) {
			RespSelf = PlayerPrefsX.GetBool ("Param_RespSelf");		
			//if (SingleUserSession) { Debug.Log( "single user session");
			//} else { Debug.Log ("multi user session");
		}
			
		if (PlayerPrefs.HasKey ("Param_RespOther")) {
				RespOther = PlayerPrefsX.GetBool ("Param_RespOther");		
				//if (SingleUserSession) { Debug.Log( "single user session");
				//} else { Debug.Log ("multi user session");
			}

		if (PlayerPrefs.HasKey ("Param_RespOther")) {
				RespOther = PlayerPrefsX.GetBool ("Param_RespOther");		
				//if (SingleUserSession) { Debug.Log( "single user session");
				//} else { Debug.Log ("multi user session");
			}

		if (PlayerPrefs.HasKey ("Param_EegOther")) {
					EegOther = PlayerPrefsX.GetBool ("Param_EegOther");		
					//if (SingleUserSession) { Debug.Log( "single user session");
					//} else { Debug.Log ("multi user session");
				}

    */

	
	}


	void Start(){
		StartCoroutine ("SessionTimer");
		StartCoroutine ("StartTimer");
	}



    // Update is called once per frame
    void Update()
    {



        // we initialize the simulated player 2 opposite of the user.
        if ((SingleUserSession == true) && (otherInitialized != true))
        {

            if (activePlayers == null)
            {
                activePlayers = GameObject.FindGameObjectsWithTag("Player");
            }

            if (activePlayers.Length == 0)
            {
                activePlayers = GameObject.FindGameObjectsWithTag("Player");
            } else
            {

                activePlayer = activePlayers[0];
                int playerNumber = activePlayer.GetComponent<PlayerManager>().PlayerNumber;
                SimPlayer.SetActive(true);

                if (playerNumber == 2)
                {
                    SimPlayer.GetComponent<PlayerManager>().PlayerNumber = 1;
                    SimPlayer.GetComponent<PlayerManager>().AuraController = GameObject.Find("Player1_Manager");
                    SimPlayer.GetComponent<PlayerManager>().AuraExpander = GameObject.Find("Aura_player1Expander");
                    SimPlayer.GetComponent<PlayerManager>().BridgeBars = GameObject.Find("Player1_BridgeLayers");
					SimPlayer.GetComponent<PlayerManager>().StatueAnimator = GameObject.Find ("Statue_Player1");
					SimPlayer.GetComponent<PlayerManager>().AuraAnimator = GameObject.Find ("AuraNew_Player1");

					SimPlayer.GetComponent<PlayerManager> ().IsNPC = true;

                }
                else
                {
                    SimPlayer.GetComponent<PlayerManager>().PlayerNumber = 2;
                    SimPlayer.GetComponent<PlayerManager>().AuraController = GameObject.Find("Player2_Manager");
                    SimPlayer.GetComponent<PlayerManager>().AuraExpander = GameObject.Find("Aura_player2Expander");
                    SimPlayer.GetComponent<PlayerManager>().BridgeBars = GameObject.Find("Player2_BridgeLayers");
					SimPlayer.GetComponent<PlayerManager>().StatueAnimator = GameObject.Find ("Statue_Player2");
					SimPlayer.GetComponent<PlayerManager>().AuraAnimator = GameObject.Find ("AuraNew_Player2");
				
					SimPlayer.GetComponent<PlayerManager> ().IsNPC = true;
                }
                otherInitialized = true;

            }

        }



        if (Input.GetKeyDown(KeyCode.I)) { SimPlayer.SetActive(true); }
    }

	IEnumerator SessionTimer()
	{
		//Debug.Log ("Session Timer Launched");
		yield return new WaitForSeconds(SessionLength);
		//Debug.Log ("return to main menu");
		Application.LoadLevel (0);
	}

	IEnumerator StartTimer()
	{
		//Debug.Log ("Session Timer Launched");
		yield return new WaitForSeconds(StartTimerLength);
		StartTimerDone = true;
	}


}
