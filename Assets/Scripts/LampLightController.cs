using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LampLightController : MonoBehaviour
{
    [Header ("References")]
    public Light2D lampPointLight;

    public BoxCollider2D boxCollider2D;
    public AudioSource audioSource;

    [Header ("Light State")]
    public string lightColor;
    public int colorState;

    public Color purple = new Color(0.9266f, 0.26f, 0.86f);

    public Color lightBlue = new Color(0f, 0.83f, 0.82f);
    public Color pink = new Color( 1f, 0.32f, 0.51f);
    // Start is called before the first frame update
    void Start()
    {
        lampPointLight = GetComponentInChildren<Light2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Clickity")){
            colorState--;
            changeColor();
        }

        if (other.gameObject.CompareTag("Clackity")){
            colorState++;
            changeColor();
       

        }

        boxCollider2D.enabled = false;
        StartCoroutine(resetBoxCollider());



    }


    void changeColor(){
        if (colorState > 2){
            colorState = 2;
        }

        if (colorState < -2){
            colorState = -2;
        }

        switch(colorState){
             case 0:
                lampPointLight.color  = purple;
                lightColor = "purple";
                break;
            case -1:
                lampPointLight.color  = pink;
                lightColor = "pink";
                break;
            case 1:
                lampPointLight.color  = lightBlue;
                lightColor = "aquwa";
                break;
            case -2:
                lampPointLight.color  = Color.red;
                lightColor = "red";
                break;
            case 2:
                lampPointLight.color  = Color.blue;
                lightColor = "blue";
                break;
        }
    }



    IEnumerator resetBoxCollider(){
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        boxCollider2D.enabled = true;
    }

}

