using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System;

public class FileWriter : MonoBehaviour {

	public bool FileWriting = true;
	public float FileWriteFreq = 1f;
	public string SaveFileName;
	DateTime saveTimeNow;
	string headerToWrite;
	string path1;
	string path2;
	string stateToWrite;
	float writeTimer = 0.0f;
	bool headerWritten = false;
	string sessionName;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("SaveFileNameStored")) {
			SaveFileName = PlayerPrefs.GetString ("SaveFileNameStored");		
			Debug.Log ("FileName parameter loaded: "+ SaveFileName );
		} else { SaveFileName = "Testfile.txt";
		}

		if (PlayerPrefs.HasKey ("Param_SessionID")) {
			sessionName = PlayerPrefs.GetString ("Param_SessionID");		
			Debug.Log ("Session loaded:" + sessionName);
		} else { sessionName = "TestSession";
		}
	
	}


void FixedUpdate () {
		//File Writing happens here.


		path1 = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Desktop) + "/MindTierData";
		path2 = path1 + "/" + SaveFileName;


		//only write the file once the start timer has ended.
		if (GetComponent<SessionManager>().StartTimerDone) {

			writeTimer += Time.deltaTime;  		//data is written on a frequencey, default, once per second.
			if (writeTimer > FileWriteFreq) {  

				if (headerWritten == false) {

					//create folder if it doesn't exist
					if (Directory.Exists (path1)) {
					} else {
						DirectoryInfo di = Directory.CreateDirectory (path1);
					}


					// write the header
					saveTimeNow = System.DateTime.Now;				
					saveTimeNow.ToString ("yyyyMMddHHmmss");

					headerToWrite = "Starting recording a new test: " + sessionName  +" "+ saveTimeNow + Environment.NewLine;//+ " " + simulationToWrite ;
					//			Debug.Log (headerToWrite);
					System.IO.File.AppendAllText (path2, headerToWrite);
					headerWritten = true;

				} else {  //HERE we write actual data

					saveTimeNow = System.DateTime.Now;
					saveTimeNow.ToString ("HHmmss");

					//var heightTemp = AdaptationLevitationHeight.ToString ();
					//var whiteTemp = AdaptationBubbleStrength.ToString ();

					stateToWrite = sessionName + " " + saveTimeNow + Environment.NewLine;//+ " "+ heightTemp + " " + whiteTemp + simulationToWrite 
					//		Debug.Log (MeditationTestType + path2 + stateToWrite);
					System.IO.File.AppendAllText (path2, stateToWrite);
				}

				writeTimer -= writeTimer;	
			}
		} 


	}
}
