using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWave : MonoBehaviour {
    GameObject wave;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(1,0,-1) * Time.deltaTime);
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);

    }
}
