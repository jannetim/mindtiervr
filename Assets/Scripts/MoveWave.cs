using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWave : MonoBehaviour {
    GameObject wave;

	public float WaveSpeed = 0.4f;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(new Vector3(1,0,-1) * Time.deltaTime * WaveSpeed);
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);

    }
}
