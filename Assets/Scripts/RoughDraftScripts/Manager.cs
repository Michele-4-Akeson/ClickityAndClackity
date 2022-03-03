using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public JumpController player;
    public MapController map;

    public bool frozen;
    public Vector2 v;

    private void Awake() {
        makeSinglton();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<JumpController>();
        map = GameObject.Find("Map").GetComponent<MapController>();
    }

    // Update is called once per frame
    void Update()
    {


    
        
    }

    public void x(){
        StartCoroutine(rotateWorld());
    }



    public void rotation(){
        if (!frozen){
            map.rotateDegree = 0;
            map.canRotate = true;
            v = player.freeze();
            map.rotate();
            StartCoroutine("freePlayer");
            frozen = true;
        } else {
            frozen = false;
        }
       
    }

    IEnumerator rotateWorld(){
        if (!frozen){
            map.rotateDegree = 0;
            map.canRotate = true;
            v = player.freeze();
            map.rotate();
            frozen = true;
            yield return new WaitForSeconds(1.8f);
            player.unFreeze(v);

            yield return new WaitForSeconds(1f);
            map.canRotate = true;
            frozen = false;
        } else {
            frozen = false;
        }
    }


    IEnumerator freePlayer(){
        yield return new WaitForSeconds(1.5f);
        player.unFreeze(v);
    }



    void makeSinglton(){
        if (instance != null){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    
}
