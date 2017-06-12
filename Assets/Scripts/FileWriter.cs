using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriter : MonoBehaviour {

	public bool FileWriting = true;
	public float FileWriteFreq = 1f;

	/*
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("SaveFileNameStored")) {
			SaveFileName = PlayerPrefs.GetString ("SaveFileNameStored");		
			Debug.Log (SaveFileName + "SaveFileName from save");
		}

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//File Writing happens here.

		// first the variables
		if (SimulationOrAdaptation == false) {
			simulationToWrite = " Simulated data";
		} else {
			simulationToWrite = " ";
		}

		SaveFileName = "testname.txt";  // for now, we should get these from save file.
		path1 = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Desktop) + "/RelaWorldData";
		path2 = path1 + "/" + SaveFileName;



		if (FileWriting == true) {

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

					headerToWrite = "Starting recording a new test: " + MeditationTestType + " " + saveTimeNow + simulationToWrite + Environment.NewLine;
					//			Debug.Log (headerToWrite);
					System.IO.File.AppendAllText (path2, headerToWrite);
					headerWritten = true;

				} else {  //HERE we write actual data

					saveTimeNow = System.DateTime.Now;
					saveTimeNow.ToString ("HHmmss");

					var heightTemp = AdaptationLevitationHeight.ToString ();
					var whiteTemp = AdaptationBubbleStrength.ToString ();

					stateToWrite = MeditationTestType + " " + saveTimeNow + " " + heightTemp + " " + whiteTemp + simulationToWrite + Environment.NewLine;
					//		Debug.Log (MeditationTestType + path2 + stateToWrite);
					System.IO.File.AppendAllText (path2, stateToWrite);
				}

				writeTimer -= writeTimer;	
			}
		} 


	}*/
}
