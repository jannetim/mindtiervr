using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFAScript : MonoBehaviour
{
    // THIS IS USED FOR CONTROLLING AURA COLOR
    // HAS MANUALLY PLACED OBJECTS, 
    // PLAYERMANAGER HANDLES INPUT TO HERE.





    public Gradient FAColorSlide = new Gradient();
    public Color PlayerColor;

    public GameObject PlayerBridgeSides;
    public GameObject PlayerAura;
    public GameObject PlayerAura2;
    public GameObject[] PlayerLights;
    public GameObject PlayerStatue;
    public float PlayerFA_Display = 0.0f;
    public float PlayerFA_adjusted;
    public float OtherFA = 0.0f;
    public Color AuraColor;
    public bool UseSyncGlow;
    float auraH;
    float auraS;
    float auraV;
    float glowS;
    float glowVMod;
    bool flickerS;
    bool flickerV;
    // Use this for initialization
    void Start()
    {


    //    GradientColorKey[] gck = new GradientColorKey[2];
    //    GradientAlphaKey[] gak = new GradientAlphaKey[2];

        UseSyncGlow = true;
        glowS = 1.0f;

        // dynamic modifier for glow HDR
        glowVMod = 0.5f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerColor = FAColorSlide.Evaluate(PlayerFA_Display);

        Color.RGBToHSV(PlayerColor, out auraH, out auraS, out auraV);
        auraS = 0.72f;
        auraV = 0.35f;
        AuraColor = Color.HSVToRGB(auraH, auraS, auraV);
        AuraColor.a = 0.05f + PlayerFA_Display * 0.7f;
        PlayerAura.GetComponent<Renderer>().material.SetColor("_TintColor", AuraColor);
        AuraColor.a = AuraColor.a * 0.4f;
        PlayerAura2.GetComponent<Renderer>().material.SetColor("_TintColor", AuraColor);

        for (int i = 0; i < PlayerLights.Length; i++)
        {

            Light l = PlayerLights[i].GetComponent<Light>();
            l.color = PlayerColor;
            l.intensity = 0.05f + PlayerFA_Display * 1.1f;
        }

		Color BridgeSideColor = PlayerColor;
		BridgeSideColor.a = 0.4f;
		PlayerBridgeSides.GetComponent<Renderer>().material.color = BridgeSideColor;
      //  PlayerBridgeSides.GetComponent<Renderer>().material.color = PlayerColor;

        if (UseSyncGlow)
        { 
            // calculates the sync, 0 -> sync and 1 -> !sync 
            float fasync = Mathf.Abs(PlayerFA_Display - OtherFA);
      //      print("FA-sync: " + fasync + "own FA: " + PlayerFA_Display + ", other FA: " + OtherFA);

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
                //glowS = Mathf.Clamp01(1.5f - (PlayerFA_Display + OtherFA) / 2);
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
		    PlayerBridgeSides.GetComponent<Renderer>().material.SetColor("_EmissionColor", emissionColor);
        }
        // used with gradient shader
        //PlayerBridgeSides.GetComponent<Renderer>().material.SetFloat("_Threshold", 1.0f - PlayerFA_Display);


    }
}
