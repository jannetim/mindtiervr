using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

// session 1, one user, no adaptations.
	public  void LaunchSession1(){
		if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
			PlayerPrefsX.SetBool ("Param_SingleUserSession", true);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

		}

		if (PlayerPrefs.HasKey ("Param_RespSelf")) {
			PlayerPrefsX.SetBool ("Param_RespSelf", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespSelf", b);

		}

		if (PlayerPrefs.HasKey ("Param_EegSelf")) {
			PlayerPrefsX.SetBool ("Param_EegSelf", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespSelf", b);

		}

		if (PlayerPrefs.HasKey ("Param_RespOther")) {
			PlayerPrefsX.SetBool ("Param_RespOther", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespOther", b);

		}

		if (PlayerPrefs.HasKey ("Param_EegOther")) {
			PlayerPrefsX.SetBool ("Param_EegOther", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespOther", b);

		}


		Debug.Log ("Session1 parameters loaded - one user, no adaptations");
		Application.LoadLevel (1);

	}


// session 2, one user, respiration only
	public  void LaunchSession2(){
		if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
			PlayerPrefsX.SetBool ("Param_SingleUserSession", true);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

		}

		if (PlayerPrefs.HasKey ("Param_RespSelf")) {
			PlayerPrefsX.SetBool ("Param_RespSelf", true);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_RespSelf", b);

		}

		if (PlayerPrefs.HasKey ("Param_EegSelf")) {
			PlayerPrefsX.SetBool ("Param_EegSelf", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespSelf", b);

		}

		if (PlayerPrefs.HasKey ("Param_RespOther")) {
			PlayerPrefsX.SetBool ("Param_RespOther", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespOther", b);

		}

		if (PlayerPrefs.HasKey ("Param_EegOther")) {
			PlayerPrefsX.SetBool ("Param_EegOther", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespOther", b);

		}
		Debug.Log ("Session2 parameters loaded - one user, respiration only");
		Application.LoadLevel (1);
		}
		





// session 3, one user, eeg only
	public  void LaunchSession3(){
	if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
		PlayerPrefsX.SetBool ("Param_SingleUserSession", true);
	} else {
		bool b = true;
		PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

	}

	if (PlayerPrefs.HasKey ("Param_RespSelf")) {
		PlayerPrefsX.SetBool ("Param_RespSelf", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespSelf", b);

	}

	if (PlayerPrefs.HasKey ("Param_EegSelf")) {
		PlayerPrefsX.SetBool ("Param_EegSelf", true);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespSelf", b);

	}

	if (PlayerPrefs.HasKey ("Param_RespOther")) {
		PlayerPrefsX.SetBool ("Param_RespOther", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespOther", b);

	}

	if (PlayerPrefs.HasKey ("Param_EegOther")) {
		PlayerPrefsX.SetBool ("Param_EegOther", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespOther", b);

	}


		Debug.Log ("Session3 parameters loaded - one user, eeg only");
		Application.LoadLevel (1);

	}



// session 4 - two users, no adaptations.
public  void LaunchSession4(){
	if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
		PlayerPrefsX.SetBool ("Param_SingleUserSession", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

	}

	if (PlayerPrefs.HasKey ("Param_RespSelf")) {
		PlayerPrefsX.SetBool ("Param_RespSelf", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespSelf", b);

	}

	if (PlayerPrefs.HasKey ("Param_EegSelf")) {
		PlayerPrefsX.SetBool ("Param_EegSelf", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespSelf", b);

	}

	if (PlayerPrefs.HasKey ("Param_RespOther")) {
		PlayerPrefsX.SetBool ("Param_RespOther", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespOther", b);

	}

	if (PlayerPrefs.HasKey ("Param_EegOther")) {
		PlayerPrefsX.SetBool ("Param_EegOther", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespOther", b);

	}

		Debug.Log ("Sessio4 parameters loaded - two users, no adaptations.");
	Application.LoadLevel (1);
	}


// session 5, two users, respiration only
public  void LaunchSession5(){
	if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
		PlayerPrefsX.SetBool ("Param_SingleUserSession", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

	}

	if (PlayerPrefs.HasKey ("Param_RespSelf")) {
		PlayerPrefsX.SetBool ("Param_RespSelf", true);
	} else {
		bool b = true;
		PlayerPrefsX.SetBool ("Param_RespSelf", b);

	}

	if (PlayerPrefs.HasKey ("Param_EegSelf")) {
		PlayerPrefsX.SetBool ("Param_EegSelf", false);
	} else {
		bool b = false;
		PlayerPrefsX.SetBool ("Param_RespSelf", b);

	}

	if (PlayerPrefs.HasKey ("Param_RespOther")) {
		PlayerPrefsX.SetBool ("Param_RespOther", true);
	} else {
		bool b = true;
		PlayerPrefsX.SetBool ("Param_RespOther", b);

	}

	if (PlayerPrefs.HasKey ("Param_EegOther")) {
		PlayerPrefsX.SetBool ("Param_EegOther", true);
	} else {
		bool b = true;
		PlayerPrefsX.SetBool ("Param_RespOther", b);

	}


		Debug.Log ("Session5 parameters loaded - two users, respiration only");
	Application.LoadLevel (1);
	}


//session 6 - two users, eeg only
	public  void LaunchSession6(){
		if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
			PlayerPrefsX.SetBool ("Param_SingleUserSession", false);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

		}

		if (PlayerPrefs.HasKey ("Param_RespSelf")) {
			PlayerPrefsX.SetBool ("Param_RespSelf", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespSelf", b);

		}

		if (PlayerPrefs.HasKey ("Param_EegSelf")) {
			PlayerPrefsX.SetBool ("Param_EegSelf", true);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_RespSelf", b);

		}

		if (PlayerPrefs.HasKey ("Param_RespOther")) {
			PlayerPrefsX.SetBool ("Param_RespOther", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_RespOther", b);

		}

		if (PlayerPrefs.HasKey ("Param_EegOther")) {
			PlayerPrefsX.SetBool ("Param_EegOther", true);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_RespOther", b);

		}


		Debug.Log ("Session6 parameters loaded -two users, eeg only");
		Application.LoadLevel (1);
	}




}
