using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class BreathLayerer : NetworkBehaviour
{
    //public GameObject Plane, Plane2, Plane3, Plane4;
    public GameObject[] planes;
    private Color planeColor;
    public GameObject Player;
    public GameObject OtherPlane;
    public GameObject Plane, Plane2;
    [Range(0, 1)]
    public float PlaneTransparency = 0.2f;
    private float origAlpha;
    private bool isFadingIn;
    private bool isFadingOut;
    private float ownH, ownS, ownV;
    private float otherH, otherS, otherV;
    private float ownOrigV, otherOrigV;
    public BreathLayerer OtherScript;
    // Use this for initialization
    void Start()
    {
        System.Array.Reverse(planes);
        origAlpha = PlaneTransparency;
        // InvokeRepeating("InitBreatheBar", 2.0f, 5.0f);
    }

    public void InitBreatheBar()
    {

        StartCoroutine("Fades");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        planeColor = Player.GetComponent<PlayerFAScript>().PlayerColor;
    }

    IEnumerator Fades()
    {
        if (isFadingIn)
        {
            StopCoroutine("FadeIn");
        }
        else if (isFadingOut)
        {
            StopCoroutine("FadeOut");
        }
        yield return StartCoroutine("FadeIn");
        isFadingIn = false;

        if (OtherPlane.activeInHierarchy && OtherPlane.GetComponent<Renderer>().material.color.a > 0.1f)
        {
            StartCoroutine("SyncGlow");
            OtherScript.StartSyncGlow();
        }


        yield return StartCoroutine("FadeOut");
        isFadingOut = false;
    }

    IEnumerator FadeIn()
    {
        isFadingIn = true;




        //old logic
        foreach (GameObject o in planes)
        {
            if (NetworkServer.active)
            {
                RpcInitColor();
            }
            else if (NetworkClient.active)
            {
                CmdInitColor();
            }
            //planeColor.a = 0;
            //o.GetComponent<Renderer>().material.SetColor("_Color", planeColor);
            //o.SetActive(false);
        }
        if (planes[0].name == "Plane5") { 
            System.Array.Reverse(planes);
        }
        foreach (GameObject o in planes)
        {/*
            if (NetworkServer.active)
            {
                RpcSetState(o, true);
            }
            else if (NetworkClient.active)
            {
                CmdSetState(o, true);
            }*/

            //o.SetActive(true);

            Color color = o.GetComponent<Renderer>().material.color;
            for (float f = 0; f <= origAlpha; f += 0.05f)
            {

                color.a = f;
                //o.GetComponent<Renderer>().material.SetColor("_Color", color);
                if (NetworkServer.active)
                {
                    RpcSetColor(o, color);
                }
                else if (NetworkClient.active)
                {
                    CmdSetColor(o, color);
                }
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
                //o.GetComponent<Renderer>().material.SetColor("_Color", color);
                if (NetworkServer.active)
                {
                    RpcSetColor(o, color);
                }
                else if (NetworkClient.active)
                {
                    CmdSetColor(o, color);
                }
                yield return null;
            }
            /*if (NetworkServer.active)
            {
                RpcSetState(o, false);
            }
            else if (NetworkClient.active)
            {
                CmdSetState(o, false);
            }*/
            //o.SetActive(false);
        }
    }

    public void StartSyncGlow()
    {
        StartCoroutine("SyncGlow");
    }

    IEnumerator SyncGlow()
    {
        yield return StartCoroutine("SyncGlowIn");
        yield return StartCoroutine("SyncGlowOut");
    }

    IEnumerator SyncGlowIn()
    {
        Color colorOwn = Plane.GetComponent<Renderer>().material.color;
        Color color2 = Plane2.GetComponent<Renderer>().material.color;

        Color.RGBToHSV(colorOwn, out ownH, out ownS, out ownV);
        ownOrigV = ownV;
        for (float f = 0; f < 1.0f; f += 0.03f)
        {
            Color color = Color.HSVToRGB(ownH, ownS, ownV + f);
            color.a = origAlpha + f / 3;
            color2.a = origAlpha + f / 4;
            if (NetworkServer.active)
            {
                RpcSetSyncColor(color, color2);
            }
            else if (NetworkClient.active)
            {
                CmdSetSyncColor(color, color2);
            }
            //plane.GetComponent<Renderer>().material.SetColor("_Color", color);
            //plane2.GetComponent<Renderer>().material.SetColor("_Color", color2);
            yield return null;
        }
    }

    IEnumerator SyncGlowOut()
    {
        Color colorOwn = Plane.GetComponent<Renderer>().material.color;
        Color color2 = Plane2.GetComponent<Renderer>().material.color;
        colorOwn.a = 0.0f;
        Color.RGBToHSV(colorOwn, out ownH, out ownS, out ownV);
        for (float f = 1; f > 0; f -= 0.02f)
        {
            Color color = Color.HSVToRGB(ownH, ownS, ownOrigV + f);
            color.a = origAlpha + f / 3;
            color2.a = origAlpha + f / 4;
            if (NetworkServer.active)
            {
                RpcSetSyncColor(color, color2);
            }
            else if (NetworkClient.active)
            {
                CmdSetSyncColor(color, color2);
            }
            //plane.GetComponent<Renderer>().material.SetColor("_Color", color );
            //plane2.GetComponent<Renderer>().material.SetColor("_Color", color2);
            yield return null;
        }
    }


    [Command]
    void CmdInitColor()
    {
        RpcInitColor();
    }

    [ClientRpc]
    void RpcInitColor()
    {
        foreach (GameObject o in planes)
        {
            planeColor.a = 0;
            o.GetComponent<Renderer>().material.SetColor("_Color", planeColor);
        }
    }

    [Command]
    void CmdSetColor(GameObject o, Color color)
    {
        RpcSetColor(o, color);
    }

    [ClientRpc]
    void RpcSetColor(GameObject o, Color color)
    {
        o.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    [Command]
    void CmdSetSyncColor(Color color, Color color2)
    {
        RpcSetSyncColor(color, color2);
    }

    [ClientRpc]
    void RpcSetSyncColor(Color color, Color color2)
    {
        Plane.GetComponent<Renderer>().material.SetColor("_Color", color);
        Plane2.GetComponent<Renderer>().material.SetColor("_Color", color2);
    }
    /*
    [Command]
    void CmdSetState(GameObject o, bool state)
    {
        RpcSetState(o, state);
    }

    [ClientRpc]
    void RpcSetState(GameObject o, bool state)
    {
        o.SetActive(state);
    }*/
}