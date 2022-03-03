using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{

    public GameObject blueWitch;
    public GameObject redWitch;
    public CinemachineVirtualCamera cmv1;
  

    public bool followingBlue = true;
    public bool followingRed = false;


    public float transitionDuration = 2.5f;
    

    public bool bothVisible = true;
    public SpriteRenderer blueSR;
    public SpriteRenderer redSR;


    // Start is called before the first frame update
    void Start()
    {

        blueWitch = GameObject.Find("BlueWitch");
        redWitch =  GameObject.Find("RedWitch");
        blueSR = blueWitch.GetComponent<SpriteRenderer>();
        redSR = redWitch.GetComponent<SpriteRenderer>();
        cmv1 = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    
    }

    // Update is called once per frame
    void Update()
    {
        bothVisible = (blueSR.isVisible && redSR.isVisible);

        if (!bothVisible){
            
            if (Input.GetMouseButton(1) && ! followingRed){
                //StartCoroutine(Transition(blueWitch.transform));
                changeFollow(redWitch.transform);
                followingBlue = false;
                followingRed = true;
            }

            if (Input.GetMouseButtonDown(0) && !followingBlue){
                //StartCoroutine(Transition(redWitch.transform));
                changeFollow(blueWitch.transform);
                followingBlue = true;
                followingRed = false;
            }
        }
    }


    void changeFollow(Transform target){
        cmv1.Follow = target;
        cmv1.transform.rotation = target.rotation;
    }


    public void followClackity(){
         if (!followingBlue){
                //StartCoroutine(Transition(redWitch.transform));
                changeFollow(blueWitch.transform);
                followingBlue = true;
                followingRed = false;
            }

    }


    public void followClickity(){
         if (!followingRed){
                //StartCoroutine(Transition(blueWitch.transform));
                changeFollow(redWitch.transform);
                followingBlue = false;
                followingRed = true;
            }

    }







    IEnumerator Transition(Transform target)
    {
        cmv1.Follow = null;
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 2.0f)
        {
            t += Time.deltaTime * (Time.timeScale/transitionDuration);


        transform.position = Vector3.Lerp(startingPos, target.position, t);
        yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        cmv1.Follow = target;
        //parallaxEffect.positionOn();

        
    }
}
