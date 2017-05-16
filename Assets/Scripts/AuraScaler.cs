using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraScaler : MonoBehaviour {
	public float ExpandSpeed = 0.1f;
	public float maxSize = 0.55f;
	public float minSize = 0.4f;
	float scalefactor = 0.4f;
	public bool expand = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//scalefactor = (0.40f + 0.15f * (Mathf.Sin (Time.time * 0.8f)*0.5f + 1f));
		// eli, tossa on sinikäyrä välillä 0-1, (ajanmukaan, tulee respiraatiosta).
		//0.4 on minimiskaala
		//0.65 on maksimiksaala.

		if ((expand == true) && (scalefactor <= maxSize)) {
			scalefactor += Time.deltaTime * ExpandSpeed;
			this.transform.localScale = new Vector3 ( scalefactor, scalefactor, scalefactor) ;
		
		}
		if ((expand == false) && (scalefactor >= minSize)){
				scalefactor -= Time.deltaTime * ExpandSpeed;
			this.transform.localScale = new Vector3 ( scalefactor, scalefactor, scalefactor) ;

		} 




		
	}
}
