using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWave : MonoBehaviour {
	public float WaveDuration = 4.5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, WaveDuration);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
