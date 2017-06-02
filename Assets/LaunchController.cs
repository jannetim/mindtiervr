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

	public  void LaunchSession1(){
		if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
			PlayerPrefsX.SetBool ("Param_SingleUserSession", true);
		} else {
			bool b = true;
			PlayerPrefsX.SetBool ("Param_SingleUserSession", b);

		}
		Debug.Log ("Session1 parameters loaded");
		Application.LoadLevel (1);

	}


	public  void LaunchSession2(){
		if (PlayerPrefs.HasKey ("Param_SingleUserSession")) {
			PlayerPrefsX.SetBool ("Param_SingleUserSession", false);
		} else {
			bool b = false;
			PlayerPrefsX.SetBool ("Param_SingleUserSession",b);
			Debug.Log ("Session2 parameters loaded");
		}
		Application.LoadLevel (1);

	}

}
