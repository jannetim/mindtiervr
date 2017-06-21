using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseLineManager : MonoBehaviour {

	public float BaseLineDuration = 30f;
	public bool StartTimerDone = false;
	bool BeginEndFade = false;
	float StartTimerLength = 10f;
	CanvasGroup CameraFadeCanvas;


	void Awake() { 
		//*//  
		//Loading parameters from the playerrefs.
		if (PlayerPrefs.HasKey ("Param_BaseLineDuration")) {
			BaseLineDuration = PlayerPrefs.GetFloat ("Param_BaseLineDuration");		
			//if (SingleUserSession) { Debug.Log( "single user session");
			//} else { Debug.Log ("multi user session");
		}
	}


	// Use this for initialization
	void Start () {
			CameraFadeCanvas = GameObject.Find ("FadeCanvas").GetComponent<CanvasGroup>();
			StartCoroutine ("SessionTimer");
			StartCoroutine ("StartTimer");
			StartCoroutine ("FadeToClear", 0.15f);
		
	}
	
	// Update is called once per frame
	void Update () {

			if (BeginEndFade) {
				StartCoroutine ("FadeToBlack", 0.15f);
			}
		
	}

		IEnumerator SessionTimer()
		{
			//Debug.Log ("Session Timer Launched");
			yield return new WaitForSeconds(BaseLineDuration-3f);
			BeginEndFade = true;
			yield return new WaitForSeconds(3f);
			//Debug.Log ("return to main menu");
			//NetworkManager nm = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		//	NetworkManager.singleton.StopHost();
			//NetworkManager.Shutdown();


			Application.LoadLevel (1);
		}

		IEnumerator StartTimer()
		{
			//Debug.Log ("Session Timer Launched");
			yield return new WaitForSeconds(StartTimerLength);
			StartTimerDone = true;
		}

		public IEnumerator FadeToBlack(float speed)
		{
			while (CameraFadeCanvas.alpha < 1f)
			{
				CameraFadeCanvas.alpha += speed * Time.deltaTime;

				yield return null;
			}
		}

		public IEnumerator FadeToClear(float speed)
		{
			while (CameraFadeCanvas.alpha > 0f)
			{
				CameraFadeCanvas.alpha -= speed * Time.deltaTime;

				yield return null;
			}
		}


}
