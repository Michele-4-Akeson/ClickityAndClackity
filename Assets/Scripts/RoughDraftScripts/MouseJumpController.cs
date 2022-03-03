using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseJumpController : MonoBehaviour
{
     [Header ("References")]
    private Rigidbody2D playerRB;
    public Transform bottom;
    public float radius;
    public LayerMask floor;
    

    [Header ("Movement Attributes/States")]
    public bool onGround;
    public float jumpForce;


    public bool canJump;

    public GravityController gravityController;

    public Vector2 mouseTarget;

    public GameObject pointPrefab;

    public GameObject[] points;

    public Vector2 direction;
    public int numberOfPoints;

    public float jumpForceHolder;

    public float jumpForceMax;
    public float jumpForceMin;

    public float jumpRateIncrease;
    public Vector2 deltaVector;
 
    // Start is called before the first frame update
    void Start()
    {


        playerRB = GetComponent<Rigidbody2D>();
        gravityController = GetComponent<GravityController>();


        points = new GameObject[numberOfPoints];


        for (int i = 0; i< numberOfPoints; i++){
            points[i] = Instantiate(pointPrefab);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
          onGround = Physics2D.OverlapCircle(bottom.position, radius, floor);

          if (onGround){
              canJump = true;
        
          }

        if (Input.GetMouseButton(0) && canJump){
            mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 charPosition =  transform.position;
            direction = mouseTarget - charPosition; 

            if (jumpForceHolder < jumpForceMax){
                jumpForceHolder += jumpRateIncrease;
            }

            
            for (int i = 0; i < numberOfPoints; i++){
                points[i].SetActive(true);
                points[i].transform.position = pointPosition(i * 0.3f);
            }


        
        }


        if (Input.GetMouseButtonUp(0) && canJump){
            playerRB.velocity = Vector2.zero;
            deltaVector = new Vector2(mouseTarget.x - transform.position.x, mouseTarget.y - transform.position.y).normalized;
            playerRB.AddForce(deltaVector * jumpForceHolder, ForceMode2D.Impulse);

            canJump = false;
            jumpForceHolder = jumpForceMin;

            for (int i = 0; i < numberOfPoints; i++){
                points[i].SetActive(false);
            }

            //Physics2D.gravity = new Vector2(9.8f, 0); -- sets all gravity to be direct right




        }


        
    }



    Vector2 pointPosition(float t){ 

        Vector2 pointPos = (Vector2) transform.position + (direction.normalized * jumpForceHolder * t) + (0.5f * gravityController.gravityVector * 9.8f *(t * t));
        return pointPos;
        /*
        Vector2 pointPos = (Vector2) transform.position + (direction.normalized * jumpForceHolder * t) + (0.5f * Physics2D.gravity *(t * t));
        return pointPos;

        */
    }
}
