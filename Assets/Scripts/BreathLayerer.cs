using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathLayerer : MonoBehaviour {
    //public GameObject Plane, Plane2, Plane3, Plane4;
    public GameObject[] planes;
    private Color planeColor;
    GameObject player1;
    // Use this for initialization
    void Start () {
        System.Array.Reverse(planes);
        player1 = GameObject.Find("Player1_Manager");
        InvokeRepeating("InitBreatheBar", 2.0f, 6.0f);
	}
	
	// Update is called once per frame
	void Update () {
        planeColor = player1.GetComponent<PlayerFAScript>().PlayerColor;
    }

    void InitBreatheBar()
    {
        StartCoroutine("Fades");
    }

    IEnumerator Fades()
    {
        yield return StartCoroutine("FadeIn");
        yield return StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        print("out");
        System.Array.Reverse(planes);
        foreach (GameObject o in planes)
        { 
            //Debug.Log("enter fadeout function");
            Color color = o.GetComponent<Renderer>().material.color;
            //Debug.Log(color.a);
            float origAlpha = color.a;
            for (float f = origAlpha; f >= 0; f -= 0.01f)
            {
                color.a = f;
                o.GetComponent<Renderer>().material.SetColor("_Color", color);
                yield return null;
            }
        }
    }

    IEnumerator FadeIn()
    {
        print("in");
        foreach (GameObject o in planes)
        {
            planeColor.a = 0;
            o.GetComponent<Renderer>().material.SetColor("_Color", planeColor);
            //print(o.GetComponent<Renderer>().material.color);
        }
        System.Array.Reverse(planes);
        foreach (GameObject o in planes)
        {
            //Debug.Log("enter fadeout function");
            Color color = o.GetComponent<Renderer>().material.color;
            //Debug.Log(color.a);
            float origAlpha = color.a;
            for (float f = 0; f <= 1; f += 0.1f)
            {
                color.a = f;
                o.GetComponent<Renderer>().material.SetColor("_Color", color);
                yield return null;
            }
        }
    }
}
