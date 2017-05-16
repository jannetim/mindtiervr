using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraScaler : MonoBehaviour {
	float scalefactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scalefactor = (0.40f + 0.15f * (Mathf.Sin (Time.time * 0.8f)*0.5f + 1f));
		this.transform.localScale = new Vector3 ( scalefactor, scalefactor, scalefactor) ;

		
	}
}
