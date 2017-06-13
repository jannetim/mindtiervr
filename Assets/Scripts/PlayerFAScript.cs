﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class PlayerFAScript : NetworkBehaviour
{
    // THIS IS USED FOR CONTROLLING AURA COLOR
    // HAS MANUALLY PLACED OBJECTS, 
    // PLAYERMANAGER HANDLES INPUT TO HERE.


    public Gradient FAColorSlide = new Gradient();
    [SyncVar]
    public Color PlayerColor;
    public GameObject PlayerBridgeSides;
    public GameObject PlayerAura;
    public GameObject PlayerAura2;
    public GameObject[] PlayerLights;
    //public GameObject PlayerStatue; 
    [SyncVar]
    public float PlayerFA_Display = 0.0f;
    //public float PlayerFA_adjusted;
    public float OtherFA = 0.0f;
    [SyncVar]
    public Color AuraColor;
    public bool UseSyncGlow;
    float auraH;
    float auraS;
    float auraV;
    float glowS;
    float glowVMod;
    bool flickerS;
    bool flickerV;
    private NetworkIdentity objNetId;
    [SyncVar]
    private int playerNumber;

    private GameObject BridgeLayers;

    // Use this for initialization
    void Start()
    {
        if (transform.name == "Player(Clone)")
        {
            if (!isLocalPlayer)
            {
               // return;
            }
        }
        GameObject SpawnPoint1 = GameObject.Find("Spawn Point 1");
        GameObject SpawnPoint2 = GameObject.Find("Spawn Point 2");
        float dist1 = Vector3.Distance(this.transform.position, SpawnPoint1.transform.position);
        float dist2 = Vector3.Distance(this.transform.position, SpawnPoint2.transform.position);
        if (dist1 < dist2)
        {
            playerNumber = 2;
            PlayerBridgeSides = GameObject.Find("Player2_BridgeSides");
            PlayerAura = GameObject.Find("Aura_player2");
            PlayerAura2 = GameObject.Find("Aura_player2Expander");
            PlayerLights[0] = GameObject.Find("Light_Player2");
            PlayerLights[1] = GameObject.Find("Light_Player2 (2)");
            PlayerLights[2] = GameObject.Find("Light_Player2 (1)");
            PlayerLights[3] = GameObject.Find("Light_Player2 (3)");
            BridgeLayers = GameObject.Find("Player2_BridgeLayers");
        }
        else
        {
            playerNumber = 1;
            PlayerBridgeSides = GameObject.Find("Player1_BridgeSides");
            PlayerAura = GameObject.Find("Aura_player1");
            PlayerAura2 = GameObject.Find("Aura_player1Expander");
            PlayerLights[0] = GameObject.Find("Light_Player1");
            PlayerLights[1] = GameObject.Find("Light_Player1 (2)");
            PlayerLights[2] = GameObject.Find("Light_Player1 (1)");
            PlayerLights[3] = GameObject.Find("Light_Player1 (3)");
            BridgeLayers = GameObject.Find("Player1_BridgeLayers");
        }
        
        Debug.Log("playernumne " + playerNumber);
        //UseSyncGlow = false;
        glowS = 1.0f;

        // dynamic modifier for glow HDR
        glowVMod = 0.5f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.name == "Player(Clone)")
        {
            if (!isLocalPlayer)
            {
                return;
            }
        }
        
        PlayerColor = FAColorSlide.Evaluate(PlayerFA_Display);
        if (NetworkServer.active)
        {
            RpcSetPlayerColor();
        }
        else if (NetworkClient.active)
        {
            CmdSetPlayerColor();
        }
        Color.RGBToHSV(PlayerColor, out auraH, out auraS, out auraV);
        auraS = 0.72f;
        auraV = 0.35f;
        AuraColor = Color.HSVToRGB(auraH, auraS, auraV);
        AuraColor.a = 0.05f + PlayerFA_Display * 0.7f;

        Color BridgeSideColor = PlayerColor;
        BridgeSideColor.a = 0.4f;

        if (NetworkServer.active)
        {
            RpcSetColors(AuraColor, BridgeSideColor);
        }
        else if (NetworkClient.active)
        {
            CmdSetColors(AuraColor, BridgeSideColor);
        }


        if (NetworkServer.active)
        {
            RpcPlayerLight(PlayerColor, PlayerFA_Display);

        }
        else if (NetworkClient.active)
        {
            CmdPlayerLight(PlayerColor, PlayerFA_Display);

        }

        if (UseSyncGlow)
        { 
            // calculates the sync, 0 -> sync and 1 -> !sync 
            float fasync = Mathf.Abs(PlayerFA_Display - OtherFA);


            // Lower emission saturation according to FA-level when FA-levels in sync

            if (fasync < 0.1)
            {
                if (glowS > 0.75f && !flickerS)
                {
                    glowS -= 0.001f;
                } else
                {
                    flickerS = true;
                }
                if (glowS < 0.9 && flickerS)
                {
                    glowS += 0.001f;
                } else
                {
                    flickerS = false;
                }
                // emission brightness correlates with FA-sync
                auraV = glowVMod - fasync;
                if (glowVMod < 2.0f && !flickerV)
                {
                    glowVMod += 0.001f;
                } else
                {
                    flickerV = true;
                }
                if (glowVMod > 1.0f && flickerV)
                {
                    glowVMod -= 0.001f;
                } else
                {
                    flickerV = false;
                }
            } else
            {
                auraV = 0.5f - fasync * 2f;
                if (auraV <= 0f)
                {
                    auraV = 0f;
                }
                // When falls out of sync, incrementally rise saturation to maximum
                if (glowS < 1)
                {
                    glowS += 0.05f;
                }
                if (glowVMod > 0.5f)
                {
                    glowVMod -= 0.05f;
                }
            }


            Color emissionColor = Color.HSVToRGB(auraH, glowS, auraV);
            if (NetworkServer.active)
            {
                RpcEmission(emissionColor);
            }
            else if (NetworkClient.active)
            {
                CmdEmission(emissionColor);
            }
        }
        // used with gradient shader
        //PlayerBridgeSides.GetComponent<Renderer>().material.SetFloat("_Threshold", 1.0f - PlayerFA_Display);
    }


    [Command]
    void CmdSetPlayerColor()
    {
        RpcSetPlayerColor();
    }

    [ClientRpc]
    void RpcSetPlayerColor()
    {
        PlayerColor = FAColorSlide.Evaluate(PlayerFA_Display);
       /* try { 
            BridgeLayers.GetComponent<BreathLayerer>().planeColor = PlayerColor;
        } catch (NullReferenceException e)
        {
            Debug.Log(e);
        }*/
    }



    [Command]
    void CmdPlayerLight(Color PlayerColor, float PlayerFA_Display)
    {
        for (int i = 0; i < PlayerLights.Length; i++)
        {
           // CmdAssignLocalAuthority(PlayerLights[i]);
        }
        RpcPlayerLight(PlayerColor, PlayerFA_Display);
        
        for (int i = 0; i < PlayerLights.Length; i++)
        {
           // CmdRemoveLocalAuthority(PlayerLights[i]);
        }
    }

    [ClientRpc]
    void RpcPlayerLight(Color PlayerColor, float PlayerFA_Display)
    {
        try
        { 
            for (int i = 0; i < PlayerLights.Length; i++)
            {
                PlayerLights[i].GetComponent<Light>().color = PlayerColor;
                PlayerLights[i].GetComponent<Light>().intensity = 0.05f + PlayerFA_Display * 1.1f;

            }
        } catch (NullReferenceException e)
        {
            Debug.Log("Playerlights probably not initialized " + e);
        }
    }


    [Command]
    void CmdEmission(Color emissionColor)
    {
        RpcEmission(emissionColor);
    }

    [ClientRpc]
    void RpcEmission(Color emissionColor)
    {
        try
        {
            PlayerBridgeSides.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColor);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log(e);
        }
    }

    [Command]
    void CmdSetColors(Color AuraColor, Color BridgeSideColor)
    {
        CmdAssignLocalAuthority(PlayerBridgeSides);
        CmdAssignLocalAuthority(PlayerAura);
        CmdAssignLocalAuthority(PlayerAura2);
        RpcSetColors(AuraColor, BridgeSideColor);
        CmdRemoveLocalAuthority(PlayerBridgeSides);
        CmdRemoveLocalAuthority(PlayerAura);
        CmdRemoveLocalAuthority(PlayerAura2);
    }

    [ClientRpc]
    void RpcSetColors(Color AuraColor, Color BridgeSideColor)
    {
        try { 
            PlayerAura.GetComponent<Renderer>().material.SetColor("_TintColor", AuraColor);
            AuraColor.a = AuraColor.a * 0.4f;
            PlayerAura2.GetComponent<Renderer>().material.SetColor("_TintColor", AuraColor);

            PlayerBridgeSides.GetComponent<Renderer>().material.color = BridgeSideColor;
        } catch (UnassignedReferenceException e)
        {
            Debug.Log(e);
        }
    }


    [Command]
    void CmdAssignLocalAuthority(GameObject obj)
    {
        objNetId = obj.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
    }

    [Command]
    void CmdRemoveLocalAuthority(GameObject obj)
    {
        objNetId = obj.GetComponent<NetworkIdentity>();
        objNetId.RemoveClientAuthority(connectionToClient);
    }
}
