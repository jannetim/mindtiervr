using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFAScript : MonoBehaviour {
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
	public Color AuraColor;
	float auraH;
	float auraS;
	float auraV;



	// Use this for initialization
	void Start () {

	
		GradientColorKey[] gck = new GradientColorKey[2];
		GradientAlphaKey[] gak = new GradientAlphaKey[2];




	}
	
	// Update is called once per frame
	void FixedUpdate () {




		PlayerColor = FAColorSlide.Evaluate(PlayerFA_Display);

		Color.RGBToHSV(PlayerColor, out auraH, out auraS, out auraV);
		auraS = 0.72f;
		auraV = 0.35f;
		AuraColor = Color.HSVToRGB(auraH,auraS,auraV);
		AuraColor.a = 0.05f + PlayerFA_Display*0.7f;
		PlayerAura.GetComponent<Renderer> ().material.SetColor ("_TintColor", AuraColor); 
		AuraColor.a = AuraColor.a*0.4f;
		PlayerAura2.GetComponent<Renderer> ().material.SetColor ("_TintColor", AuraColor);



        for (int i = 0; i < PlayerLights.Length; i++) {
			
			Light l = PlayerLights[i].GetComponent<Light> ();
			l.color = PlayerColor;
			l.intensity = 0.05f + PlayerFA_Display*1.1f;
			}

		PlayerBridgeSides.GetComponent<Renderer> ().material.color = PlayerColor;
        PlayerBridgeSides.GetComponent<Renderer>().material.SetFloat("_Threshold", 1.0f - PlayerFA_Display);


    }
}
