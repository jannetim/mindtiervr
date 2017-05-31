using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerFunctions : NetworkBehaviour {
    public Camera Cam;
    GameObject bridge;
    // Use this for initialization
    void Start () {
        //bridge = GameObject.Find("sceneholder2/Environment assets/Bridge");
        if (GameObject.Find("Session Manager").GetComponent<SessionManager>().SingleUserSession == true)
        {
            // if real player then camera enabled
            if (gameObject.name != "PlayerSim")
            {
                Cam.enabled = true;
            }

        }
        else if (!isLocalPlayer)
        {
            return;
        }
        else
        {
            Cam.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        /*Renderer renderer = bridge.GetComponent<Renderer>();
        Material material = renderer.materials[1];
        float faValue = Mathf.Abs(Mathf.Sin(Time.time * 0.2f)) + 0.5f;

        material.SetColor("_EmissionColor", new Color(faValue, faValue, faValue));
        */

        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);



        // player sends wave
        if (Input.GetKeyDown("r"))
        {
            Debug.Log("pressed r");
            //GameObject.Find("ParticlePillar").GetComponent<ParticleSystem>().Stop();
        }
    }
}
