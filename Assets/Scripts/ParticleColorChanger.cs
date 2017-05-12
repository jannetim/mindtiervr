using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorChanger : MonoBehaviour {

    GameObject P1, P2;

	// Use this for initialization
	void Start () {
        P1 = GameObject.Find("Player1_Manager");
        P2 = GameObject.Find("Player2_Manager");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
