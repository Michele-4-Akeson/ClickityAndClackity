using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header ("References")]
    public Transform characterTransform;
    public Rigidbody2D chracterRB;

    [Header ("States")]
    public string gravityDirection;
    public int gravityState = 0;
    public int t = 0;
    public Vector2 gravityVector;


    // Start is called before the first frame update
    void Start()
    {

        chracterRB = GetComponent<Rigidbody2D>();
        characterTransform = GetComponent<Transform>();
        chracterRB.gravityScale = 0;
        setGravityState(0);


        
    }


    private void FixedUpdate() {

        chracterRB.AddForce(gravityVector * 9.8f * chracterRB.mass);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setGravityState(int state){
        /*
        setGravityState() changes the direction of a GameObject - state: 0 => gravity down, state:1 => gravity right
        state: 2 => gravity up, state:3 => gravity down

        therefore - increasing state by 1 will rotate a gameObjects gravity rightward around a map
                  - decreasing the state by 1 will rotate a gameObjects gravity leftWard around a map
        */
        gravityState = state;

        switch(gravityState){
            case 0:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.down;
                gravityDirection = "down";
                break;
            case 1:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.right;
                gravityDirection = "right";
                break;
            case 2:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.up;
                gravityDirection = "up";
                break;
            case 3:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.left;
                gravityDirection = "left";
                break;
            
        }
    }


    public void gravityRightWard(){
        gravityState++;
        if (gravityState > 3){
              gravityState = 0;
          }

        //setRotation();

        switch(gravityState){
            case 0:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.down;
                gravityDirection = "down";
                break;
            case 1:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.right;
                gravityDirection = "right";
                break;
            case 2:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.up;
                gravityDirection = "up";
                break;
            case 3:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.left;
                gravityDirection = "left";
                break;
            
        }

          
    }




    public void gravityLeftWard(){
          gravityState--;

          if (gravityState < 0){
              gravityState = 3;
          }

        //setRotation();

        switch(gravityState){
            case 0:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.down;
                gravityDirection = "down";
                break;
            case 1:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.right;
                gravityDirection = "right";
                break;
            case 2:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.up;
                gravityDirection = "up";
                break;
            case 3:
                //traditional downward gravity towards bottom of screen;
                gravityVector = Vector2.left;
                gravityDirection = "left";
                break;
            
        }

    }


    void setRotation(){
          switch(gravityState){
            case 0:
                characterTransform.eulerAngles = Vector3.forward * 0;
                break;
            case 1:
                characterTransform.eulerAngles = Vector3.forward * 90;
                break;
            case 2:
                characterTransform.eulerAngles = Vector3.forward * 180;
                break;
            case 3:
                characterTransform.eulerAngles = Vector3.forward * -90;
                break;
            
        }

    }
}
