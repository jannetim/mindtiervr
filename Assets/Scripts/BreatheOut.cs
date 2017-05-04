using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheOut : MonoBehaviour {
    GameObject avatar1;
    GameObject avatar2;
    public float instantiateSpeed = 4.0f;
	// Use this for initialization
	void Start () {
        avatar1 = GameObject.Find("planewaveP1");
        avatar2 = GameObject.Find("planewaveP2");
        //Debug.Log(avatar2.transform.position);
        InvokeRepeating("InstantiateWaves", 2.0f, 4.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void InstantiateWaves()
    {

        GameObject newWave = Instantiate(avatar1, new Vector3(1.0f, 0.05f, -1.6f), avatar1.transform.rotation);
        MoveWave mWave = newWave.GetComponent<MoveWave>();
        DestroyWave dWave = newWave.GetComponent<DestroyWave>();
        mWave.enabled = true;
        dWave.enabled = true;
        newWave = Instantiate(avatar2, new Vector3(1.0f, 1.0f, 2.2f), avatar2.transform.rotation);
        MoveWave2 mWave2 = newWave.GetComponent<MoveWave2>();
        dWave = newWave.GetComponent<DestroyWave>();
        mWave2.enabled = true;
        dWave.enabled = true;
    }
}
