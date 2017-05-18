using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathLayerer : MonoBehaviour {
    //public GameObject Plane, Plane2, Plane3, Plane4;
    public GameObject[] planes;
    private Color planeColor;
    private GameObject player;
    private GameObject otherPlayerLayers;
    [Range(0, 1)]
    public float PlaneTransparency = 0.2f;
    private float origAlpha;
    private bool isFadingIn;
    private bool isFadingOut;
    // Use this for initialization
    void Start () {
        System.Array.Reverse(planes);
        if (gameObject.name == "Player1_BridgeLayers")
        {
            player = GameObject.Find("Player1_Manager");
            otherPlayerLayers = GameObject.Find("Player2_BridgeLayers/planes");
        } else if (gameObject.name == "Player2_BridgeLayers")
        {
            player = GameObject.Find("Player2_Manager");
            otherPlayerLayers = GameObject.Find("Player1_BridgeLayers/planes");
        }

        origAlpha = PlaneTransparency;
       // InvokeRepeating("InitBreatheBar", 2.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        planeColor = player.GetComponent<PlayerFAScript>().PlayerColor;
    }

    public void InitBreatheBar()
    {
        StartCoroutine("Fades");
    }

    IEnumerator Fades()
    {
        if (isFadingIn)
        {
            StopCoroutine("FadeIn");
        } else if (isFadingOut)
        {
            StopCoroutine("FadeOut");
        }
        yield return StartCoroutine("FadeIn");
        isFadingIn = false;
        yield return StartCoroutine("FadeOut");
        isFadingOut = false;
    }

    IEnumerator FadeOut()
    {
        isFadingOut = true;
        yield return new WaitForSeconds(1.5f);
        //print("out");
        System.Array.Reverse(planes);
        foreach (GameObject o in planes)
        { 
            Color color = o.GetComponent<Renderer>().material.color;
            //Debug.Log(color.a);
            //float origAlpha = color.a;
            for (float f = origAlpha; f >= 0; f -= 0.005f)
            {
                color.a = f;
                o.GetComponent<Renderer>().material.SetColor("_Color", color);
                yield return null;
            }
            o.SetActive(false);
        }
    }

    IEnumerator FadeIn()
    {
        isFadingIn = true;
        //print("in");
        foreach (GameObject o in planes)
        {
            planeColor.a = 0;
            o.GetComponent<Renderer>().material.SetColor("_Color", planeColor);
            o.SetActive(false);
            //print(o.GetComponent<Renderer>().material.color);
        }
        System.Array.Reverse(planes);
        foreach (GameObject o in planes)
        {
            o.SetActive(true);
            //Debug.Log("enter fadeout function");
            Color color = o.GetComponent<Renderer>().material.color;
            //Debug.Log(color.a);
            //float origAlpha = color.a;
            for (float f = 0; f <= origAlpha; f += 0.05f)
            {
                color.a = f;
                o.GetComponent<Renderer>().material.SetColor("_Color", color);
                yield return null;
            }
        }
        
    }
}
