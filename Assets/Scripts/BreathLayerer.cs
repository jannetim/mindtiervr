﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathLayerer : MonoBehaviour {
    //public GameObject Plane, Plane2, Plane3, Plane4;
    public GameObject[] planes;
    private Color planeColor;
    private GameObject player;
    private GameObject otherPlane, otherPlane2;
    private GameObject plane, plane2;
    [Range(0, 1)]
    public float PlaneTransparency = 0.2f;
    private float origAlpha;
    private bool isFadingIn;
    private bool isFadingOut;
    private float ownH, ownS, ownV;
    private float otherH, otherS, otherV;
    // Use this for initialization
    void Start () {
        System.Array.Reverse(planes);
        if (gameObject.name == "Player1_BridgeLayers")
        {
            player = GameObject.Find("Player1_Manager");
            plane = GameObject.Find("Player1_BridgeLayers/Plane5");
            plane2 = GameObject.Find("Player1_BridgeLayers/Plane4");
            otherPlane = GameObject.Find("Player2_BridgeLayers/Plane5");
            otherPlane2 = GameObject.Find("Player2_BridgeLayers/Plane4");
        } else if (gameObject.name == "Player2_BridgeLayers")
        {
            player = GameObject.Find("Player2_Manager");
            plane = GameObject.Find("Player2_BridgeLayers/Plane5");
            plane2 = GameObject.Find("Player2_BridgeLayers/Plane4");
            otherPlane = GameObject.Find("Player1_BridgeLayers/Plane5");
            otherPlane2 = GameObject.Find("Player1_BridgeLayers/Plane4");
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

        if (otherPlane.activeInHierarchy && otherPlane.GetComponent<Renderer>().material.color.a > 0.1f)
        {
            StartCoroutine("SyncGlow");
        }
        yield return StartCoroutine("FadeOut");
        isFadingOut = false;
    }

    IEnumerator FadeIn()
    {
        isFadingIn = true;
        foreach (GameObject o in planes)
        {
            planeColor.a = 0;
            o.GetComponent<Renderer>().material.SetColor("_Color", planeColor);
            o.SetActive(false);
        }
        System.Array.Reverse(planes);
        foreach (GameObject o in planes)
        {
            o.SetActive(true);
            Color color = o.GetComponent<Renderer>().material.color;
            for (float f = 0; f <= origAlpha; f += 0.05f)
            {
                color.a = f;
                o.GetComponent<Renderer>().material.SetColor("_Color", color);
                yield return null;
            }
        }
    }

    IEnumerator FadeOut()
    {
        isFadingOut = true;
        yield return new WaitForSeconds(1.5f);
        System.Array.Reverse(planes);
        foreach (GameObject o in planes)
        { 
            Color color = o.GetComponent<Renderer>().material.color;
            for (float f = origAlpha; f >= 0; f -= 0.005f)
            {
                color.a = f;
                o.GetComponent<Renderer>().material.SetColor("_Color", color);
                yield return null;
            }
            o.SetActive(false);
        }
    }

    IEnumerator SyncGlow()
    {
        yield return StartCoroutine("SyncGlowIn");
        yield return StartCoroutine("SyncGlowOut");
    }

    IEnumerator SyncGlowIn()
    {
        Color colorOwn = plane.GetComponent<Renderer>().material.color;
        Color colorOther = otherPlane.GetComponent<Renderer>().material.color;

        /*float alpha = colorOwn.a;
        for (float f = alpha; f < alpha+0.4f; f += 0.001f)
        {
            colorOwn.a = f;
            plane.GetComponent<Renderer>().material.SetColor("_Color", colorOwn);
        }
        alpha = colorOther.a;
        for (float f = alpha; f < alpha + 0.4f; f += 0.001f)
        {
            colorOther.a = f;
            otherPlane.GetComponent<Renderer>().material.SetColor("_Color", colorOther);
        }*/
        Color.RGBToHSV(colorOwn, out ownH, out ownS, out ownV);
        Color.RGBToHSV(colorOther, out otherH, out otherS, out otherV);
        for (float f = 0; f < 1.0f; f += 0.01f ) {
            plane.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(ownH, ownS, ownV + f));
            otherPlane.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(otherH, otherS, otherV + f));
            yield return null;
        }
    }

    IEnumerator SyncGlowOut()
    {
        Color colorOwn = plane.GetComponent<Renderer>().material.color;
        Color colorOther = otherPlane.GetComponent<Renderer>().material.color;

        /*float alpha = colorOwn.a;
        for (float f = colorOwn.a; f >= origAlpha; f -= 0.001f)
        {
            colorOwn.a = f;
            plane.GetComponent<Renderer>().material.SetColor("_Color", colorOwn);
        }
        for (float f = colorOther.a; f >= origAlpha; f -= 0.001f)
        {
            colorOther.a = f;
            otherPlane.GetComponent<Renderer>().material.SetColor("_Color", colorOther);
        }*/

        Color.RGBToHSV(colorOwn, out ownH, out ownS, out ownV);
        Color.RGBToHSV(colorOther, out otherH, out otherS, out otherV);
        for (float f = 1.0f; f >= 0; f -= 0.01f)
        {
            plane.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(ownH, ownS, ownV - f));
            otherPlane.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(otherH, otherS, otherV - f));
            yield return null;
        }
    }
}
