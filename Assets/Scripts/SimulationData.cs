using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationData : MonoBehaviour {

	public float P1Breathing;
	public float P2Breathing;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		P1Breathing = Mathf.Sin(Time.time) * 0.2f);
		P2Breathing = 1f + Mathf.Sin(Time.time) * 0.2f);


	}
}
