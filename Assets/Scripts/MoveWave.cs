using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWave : MonoBehaviour {

	public float WaveSpeed = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * WaveSpeed);
    }
}
