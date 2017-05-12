using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheOut : MonoBehaviour {
    GameObject avatar1;
    GameObject avatar2;
	public GameObject Player1WaveSpawn;
	public GameObject Player2WaveSpawn;
	Color auraColor;

    public float instantiateSpeed = 4.0f;
	// Use this for initialization
	void Start () {
		 avatar1 = GameObject.Find("NewWave1");
		avatar2 = GameObject.Find("NewWave2");


		// old waves
		// avatar1 = GameObject.Find("planewaveP1");
        //avatar2 = GameObject.Find("planewaveP2");


        //Debug.Log(avatar2.transform.position);
        InvokeRepeating("InstantiateWaves", 2.0f, 6.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void InstantiateWaves()
    {

		Player1WaveSpawn = GameObject.Find("Player1_WaveSpawn");
		GameObject newWave = Instantiate(avatar1, Player1WaveSpawn.transform.position, avatar1.transform.rotation);
		MoveWave mWave = newWave.GetComponent<MoveWave>();
		DestroyWave dWave = newWave.GetComponent<DestroyWave>();
		mWave.enabled = true;
		dWave.enabled = true;

		auraColor = GameObject.Find ("Player1_Manager").GetComponent<PlayerFAScript> ().PlayerColor;

        auraColor.a = 0.1f;// GameObject.Find("Player1_Manager").GetComponent<PlayerFAScript>().AuraColor.a / 5;// + 0.2f;
	//	newWave.GetComponent<Renderer> ().material.color = auraColor;
	//	newWave.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor); 
		newWave.GetComponent<ParticleSystem>().startColor = auraColor;





		Player2WaveSpawn = GameObject.Find("Player2_WaveSpawn");
		 
		newWave = Instantiate(avatar2, Player2WaveSpawn.transform.position, avatar2.transform.rotation);
		MoveWave2 mWave2 = newWave.GetComponent<MoveWave2>();
		dWave = newWave.GetComponent<DestroyWave>();
		mWave2.enabled = true;
		dWave.enabled = true;

		auraColor = GameObject.Find ("Player2_Manager").GetComponent<PlayerFAScript> ().PlayerColor;

        auraColor.a = 0.1f;// GameObject.Find("Player2_Manager").GetComponent<PlayerFAScript>().AuraColor.a / 5;// + 0.2f;
	//	newWave.GetComponent<Renderer> ().material.color = auraColor;
	//	newWave.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor); 
		newWave.GetComponent<ParticleSystem>().startColor = auraColor;



		//GameObject waveGeometry = newWave.transform.Find("wave1_side1").gameObject;
		//waveGeometry.GetComponent<Renderer> ().material.color = auraColor;
		//waveGeometry.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor); 

		//waveGeometry = newWave.transform.Find("wave1_side2").gameObject;
		//waveGeometry.GetComponent<Renderer> ().material.color = auraColor;
		//waveGeometry.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor);

	/* Old Waves
   /*   Player1WaveSpawn = GameObject.Find("Player1_WaveSpawn");
		GameObject newWave = Instantiate(avatar1, Player1WaveSpawn.transform.position, avatar1.transform.rotation);
        MoveWave mWave = newWave.GetComponent<MoveWave>();
        DestroyWave dWave = newWave.GetComponent<DestroyWave>();
        mWave.enabled = true;
        dWave.enabled = true;

		auraColor = GameObject.Find ("Player1_Manager").GetComponent<PlayerFAScript> ().PlayerColor;
		auraColor.a = GameObject.Find ("Player1_Manager").GetComponent<PlayerFAScript> ().AuraColor.a*2 + 0.2f;
		GameObject waveGeometry = newWave.transform.Find("wave1_side1").gameObject;
		waveGeometry.GetComponent<Renderer> ().material.color = auraColor;
		waveGeometry.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor); 

		waveGeometry = newWave.transform.Find("wave1_side2").gameObject;
		waveGeometry.GetComponent<Renderer> ().material.color = auraColor;
		waveGeometry.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor);



        Player2WaveSpawn = GameObject.Find("Player2_WaveSpawn");
        newWave = Instantiate(avatar2, Player2WaveSpawn.transform.position, avatar2.transform.rotation);
        MoveWave2 mWave2 = newWave.GetComponent<MoveWave2>();
        dWave = newWave.GetComponent<DestroyWave>();
        mWave2.enabled = true;
        dWave.enabled = true;
	
		auraColor = GameObject.Find ("Player2_Manager").GetComponent<PlayerFAScript> ().PlayerColor;
		auraColor.a = GameObject.Find ("Player2_Manager").GetComponent<PlayerFAScript> ().AuraColor.a*2 + 0.2f;
		waveGeometry = newWave.transform.Find("wave2_side1").gameObject;
		waveGeometry.GetComponent<Renderer> ().material.color = auraColor;
		waveGeometry.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor); 

		waveGeometry = newWave.transform.Find("wave2_side2").gameObject;
		waveGeometry.GetComponent<Renderer> ().material.color = auraColor;
		waveGeometry.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", auraColor); 
		*/

    }
}
