using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {
    GameObject wave;
    Renderer renderer;
    Material material;
    Collider collider;
    bool fadeWave = false;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter collision");
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Stay in collision");
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit collision");
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(fadeWave);
        //wave = other.gameObject;
        wave = gameObject;
        fadeWave = true;
        //wave = GameObject.Find("sceneholder2/planewaveP2/Cube");

        Debug.Log("Enter trigger zone");

    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay in trigger zone");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit trigger zone");
    }

    void Update()
    {
        //Debug.Log("updating " + fadeWave);
        if (fadeWave)
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {

        renderer = wave.GetComponent<Renderer>();
        material = renderer.material;
        //Debug.Log("enter fadeout function");
        Color color = material.color;
        //Debug.Log(color.a);
        float origAlpha = color.a;
        for (float f = origAlpha; f >= 0; f -= 0.01f)
        {
            color.a = f;
            material.SetColor("_Color", color);
            yield return null;
        }

    }
}
