using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerInstantiator : MonoBehaviour {

    NetworkManager manager;

	// Use this for initialization
	void Start () {
        manager = transform.GetComponent<NetworkManager>();

        manager.StartHost();
        /*NetM = transform.GetComponent<NetworkManager>();
        var spawn = NetM.GetStartPosition();

        Debug.Log(NetM.playerPrefab);
        Debug.Log(NetM.GetStartPosition());

        NetworkManager.Instantiate(NetM.playerPrefab, spawn.position, spawn.rotation);*/
    }



    // Update is called once per frame
    void Update () {


    }
}
