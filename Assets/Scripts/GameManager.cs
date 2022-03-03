using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("References")]
    public static GameManager instance;
    public GameObject redWitch;
    public GameObject blueWitch;
    public MapManager mapManager;
    public AudioSource audioSource;
    
    


    [Header ("States")]
    public string rotationDirection;
    public int rotationState;
    public bool frozen;
    public Vector2 storedVector1;
      public Vector2 storedVector2;


    private void Awake() {
        makeSinglton();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        redWitch = GameObject.Find("RedWitch");
        blueWitch = GameObject.Find("BlueWitch");
        mapManager = GameObject.Find("Map").GetComponent<MapManager>();
        audioSource = GetComponent<AudioSource>();

    }
    
    void makeSinglton(){
        if (instance != null){
            Destroy(gameObject);
        } else {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
    }




    //////////////////////////////////////////////////////////////////////////
    //
    // Functional Calls
    //
    //////////////////////////////////////////////////////////////////////////

    public void rotateForBlueWitch(){
        mapManager.setPivot(blueWitch.transform, redWitch.transform);
        StartCoroutine(rotateLeft());
    }


    public void rotateForRedWitch(){
        mapManager.setPivot(redWitch.transform, blueWitch.transform);
        StartCoroutine(rotateRight());
        }


    IEnumerator rotateLeft(){
         if (!frozen){
            mapManager.rotateDegree = 0;
            mapManager.canRotate = true;
            redWitch.GetComponent<GravityController>().gravityLeftWard();
            storedVector1 = redWitch.GetComponent<RedWitchJumpContoller>().freeze();
            storedVector2 = blueWitch.GetComponent<BlueWitchJumpController>().freeze();
            mapManager.rotateMapLeft();
            frozen = true;
            yield return new WaitForSeconds(1.8f);
            redWitch.GetComponent<RedWitchJumpContoller>().unFreeze(storedVector1);
            blueWitch.GetComponent<BlueWitchJumpController>().unFreeze(storedVector2);

            yield return new WaitForSeconds(1f);
            mapManager.canRotate = true;
            frozen = false;
        } 

    }

        IEnumerator rotateRight(){
         if (!frozen){
            mapManager.rotateDegree = 0;
            mapManager.canRotate = true;
            blueWitch.GetComponent<GravityController>().gravityRightWard();
            storedVector1 = redWitch.GetComponent<RedWitchJumpContoller>().freeze();
            storedVector2 = blueWitch.GetComponent<BlueWitchJumpController>().freeze();
            mapManager.rotateMapRight();
            frozen = true;
            yield return new WaitForSeconds(1.8f);
            redWitch.GetComponent<RedWitchJumpContoller>().unFreeze(storedVector1);
            blueWitch.GetComponent<BlueWitchJumpController>().unFreeze(storedVector2);

            yield return new WaitForSeconds(1f);
            mapManager.canRotate = true;
            frozen = false;
        } else {
            frozen = false;
        }

    }




    public void reloadLevel(){
        audioSource.Play();
        Scene scene = SceneManager.GetActiveScene();
        StartCoroutine(delayedLoad(scene.name, 1.0f));
    }


    public void loadLevel(string sceneName){
        StartCoroutine(delayedLoad(sceneName, 2.5f));
    }


    IEnumerator delayedLoad(string sceneName, float loadDelay){
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
    


}
