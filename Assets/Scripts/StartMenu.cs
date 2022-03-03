using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    [Header ("References")]
    public AudioSource audioSource;
    public GameObject pauseMenu;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        
    }
    public void startGame(){
        StartCoroutine(loadDelay("Level1"));
        

    }

    public void restartLevel(){
        Scene scene = SceneManager.GetActiveScene();
        StartCoroutine(loadDelay(scene.name));
    }

    public void startLevel1(){
         StartCoroutine(loadDelay("Level1"));
        
    }


     public void startLevel2(){
          StartCoroutine(loadDelay("Level2"));
        
    }

    
     public void startLevel3(){
          StartCoroutine(loadDelay("Level3"));
        
    }

    
     public void startLevel4(){
          StartCoroutine(loadDelay("Level4"));
        
    }

    public void startLevel5(){
          StartCoroutine(loadDelay("Level5"));
        
    }


     public void returnToMain(){
        StartCoroutine(loadDelay("StartMenu"));

    }



    public void pause(){
        pauseMenu = transform.GetChild(2).gameObject;
        if (pauseMenu.activeSelf){
            pauseMenu.SetActive(false);
        } else {
            pauseMenu.SetActive(true);
        }

        playUISound();

     
    }



    void playUISound(){
        audioSource.Play();
    }



    IEnumerator loadDelay(string sceneName){
        playUISound();
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneName);

    }



}
