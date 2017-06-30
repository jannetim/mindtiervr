using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerFunctions : NetworkBehaviour {
    public Camera Cam;
    public AudioListener AudioListener;
    GameObject bridge;

    // Use this for initialization
    void Start () {
        /* if (GameObject.Find("Session Manager").GetComponent<SessionManager>().SingleUserSession)
         {
             if (gameObject.name != "PlayerSim")
             {
                 Cam.enabled = true;
                 AudioListener.enabled = true;
             }

         }
         else */
        if (!isLocalPlayer)
        {
            return;
        }
        Cam.enabled = true;
        AudioListener.enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (!SceneManager.GetActiveScene().name.Equals("ForestShrine16_network"))
        {
            transform.gameObject.GetComponent<PlayerFAScript>().enabled = false;
        }
        /*Renderer renderer = bridge.GetComponent<Renderer>();
        Material material = renderer.materials[1];
        float faValue = Mathf.Abs(Mathf.Sin(Time.time * 0.2f)) + 0.5f;

        material.SetColor("_EmissionColor", new Color(faValue, faValue, faValue));
        */
        /*
        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

    */

        /*if (Input.GetKeyDown("r"))
        {
            Debug.Log("pressed r");
            GameObject.Find("Network Manager").GetComponent<NetworkManager>().StopHost();
            GameObject.Find("Network Manager").GetComponent<NetworkManager>().StopServer();
            //GameObject.Find("Network Manager").GetComponent<NetworkManager>().ServerChangeScene("LaunchManager");
            SceneManager.LoadScene(3);
        }*/
    }
}
