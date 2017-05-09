using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerFunctions : NetworkBehaviour {
    GameObject bridge;
    // Use this for initialization
    void Start () {
        //bridge = GameObject.Find("sceneholder2/Environment assets/Bridge");
    }
	
	// Update is called once per frame
	void Update () {
        /*Renderer renderer = bridge.GetComponent<Renderer>();
        Material material = renderer.materials[1];
        float faValue = Mathf.Abs(Mathf.Sin(Time.time * 0.2f)) + 0.5f;

        material.SetColor("_EmissionColor", new Color(faValue, faValue, faValue));
        */

        // TO-DO
        // Bridge materialin gradient
        /*var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);*/

        if (!isLocalPlayer)
        {
            return;
        }

        // player sends wave
        if (Input.GetKeyDown("r"))
        {
            print("pressed r");
        }
    }
}
