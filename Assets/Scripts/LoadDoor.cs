using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDoor : MonoBehaviour
{
    [Header ("References")]
    public GameManager gameManger;
    public Dialog dialog;
    public AudioSource audioSource;

    [Header ("Load Zone")]
    public string sceneName;
    public bool clackityIn = false;
    public bool clickityIn = false;
    // Start is called before the first frame update
    void Start()
    {
          gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
          dialog = GetComponent<Dialog>();
          audioSource = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Clackity")){
            clackityIn = true;

            if (!clackityIn || !clickityIn){
                dialog.speak();
            }
        }

        if (other.gameObject.CompareTag("Clickity")){
            clickityIn = true;
             if (!clackityIn || !clickityIn){
                dialog.speak();
            }

        }


        if (clackityIn && clickityIn){
            audioSource.Play();
            gameManger.loadLevel(sceneName);
        }
        
    }



    private void OnTriggerExit2D(Collider2D other) {
         if (other.gameObject.CompareTag("Clackity")){
            clackityIn = false;
         }

        if (other.gameObject.CompareTag("Clickity")){
            clickityIn = false;
        }

        
    }




    

}
