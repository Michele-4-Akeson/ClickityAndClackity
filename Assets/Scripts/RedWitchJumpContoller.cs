using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWitchJumpContoller : MonoBehaviour
{
    [Header ("References")]
    public GameManager gameManger;
    private Rigidbody2D playerRB;
    public GameObject blueWitch;
    public Animator animator;
    public GravityController gravityController;
    public Transform bottom;
    public LayerMask floor;
    public float checkRadius;

    public AudioSource auido;
    

    [Header ("Movement Attributes")]
    public bool onGround;
    public bool canJump;
    public bool charging;
    public float jumpForce;
    public float jumpForceMin;
    public float jumpForceMax;
    public float jumpForceIncreaseRate;
  
  
     [Header ("Movement Trajectory")]
    public Vector2 mouseTarget;
    public Vector2 direction;
    public Vector2 deltaVector;
    public GameObject trajectoryPoint;
    private GameObject[] trajectoryPointArray;
    public int count;
    public float trajectoryGap;

 
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
        blueWitch = GameObject.Find("BlueWitch");
        gravityController = GetComponent<GravityController>();
        auido = GetComponent<AudioSource>();


        trajectoryPointArray = new GameObject[count];

        // instaniates copies of the trajectoryPoint GameObejct to be used to indicate where player will jump to
        for (int i = 0; i< count; i++){
            trajectoryPointArray[i] = Instantiate(trajectoryPoint);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        updateAnimation();
        // if left mouse button is pressed, trajectoryPointArray will appear on screen and indicate, based on gravity,
        // where the character will jump
         onGround = Physics2D.OverlapCircle(bottom.position, checkRadius, floor);
         charging = Input.GetMouseButton(1) && canJump;
         flipSprite();

          if (onGround){
              canJump = true;
          }

        if (charging){
            mouseTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 charPosition =  transform.position;
            direction = mouseTarget - charPosition; 

            if (jumpForce < jumpForceMax){
                jumpForce += jumpForceIncreaseRate;
            }

            
            for (int i = 0; i < count; i++){
                trajectoryPointArray[i].SetActive(true);
                trajectoryPointArray[i].transform.position = indicatePath(i * trajectoryGap);
            }


        
        }


        if (Input.GetMouseButtonUp(1) && canJump){
            playerRB.velocity = Vector2.zero;
            deltaVector = new Vector2(mouseTarget.x - transform.position.x, mouseTarget.y - transform.position.y).normalized;
            playerRB.AddForce(deltaVector * jumpForce, ForceMode2D.Impulse);
            canJump = false;

            jumpForce = jumpForceMin; // resets jumpForce
            auido.Play();


            StartCoroutine(removeTrajectory());

        
        }
    }


      Vector2 indicatePath(float t){ 
          /*
          creates a vector2 that repersents the position of a trajectoryPoint, (t) that the player would travel through 
          */

            Vector2 pointPos = (Vector2) transform.position + (direction.normalized * jumpForce * t) + (0.5f * gravityController.gravityVector * 9.8f *(t * t));
            return pointPos;
    }

    
    IEnumerator removeTrajectory(){
        yield return new WaitForSeconds(2.5f);
         for (int i = 0; i < count; i++){
                trajectoryPointArray[i].SetActive(false);
            }
    }






    /////////////////////////////////////////////////////////////////////
    //
    // gravity/rotation methods
    //
    /////////////////////////////////////////////////////////////////////

    
     void flipSprite()
    { /*
       * flipSprite() rotates the x value of a Gameobjects transform component, flipping the object
       * to face the opposite direction if it is moving right, or left
       */
        Vector3 scale = transform.localScale;
        if (direction.x > 0)
        {
         
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
             
        } else if (direction.x < 0){

            scale.x = Mathf.Abs(scale.x) / -1;
            transform.localScale = scale;
        }
    }
    
    public Vector2 freeze(){
        Vector2 storedVector = playerRB.velocity; 
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        print(playerRB.velocity);
        return storedVector;
    }



    public void unFreeze(Vector2 storedVector){
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRB.velocity += storedVector;
    }












    ////////////////////////////////////////////////////////////////////////
    //
    // Trigger/Collision 2D
    //
    ////////////////////////////////////////////////////////////////////////

    
    private void OnTriggerEnter2D(Collider2D other) {

          if (other.gameObject.CompareTag("Trajectory Point")){
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("spike")){
        animator.SetBool("Dead", true);
        print("spikes");
        StartCoroutine(restartLevel());
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other) {
         if (other.gameObject.CompareTag("Lamp Head")){
            gameManger.rotateForRedWitch();
        }

        if (other.gameObject.CompareTag("spike")){
            animator.SetBool("Dead", true);
            print("spikes");
            StartCoroutine(restartLevel());
        }

        
    }






    ///////////////////////////////////////////////////////////////////////
    //
    // Animation
    //
    ///////////////////////////////////////////////////////////////////////

    void updateAnimation(){
        animator.SetBool("charging", charging);
        animator.SetBool("onGround", onGround);
        animator.SetFloat("hSpeed", Mathf.Abs(playerRB.velocity.x)); // overly complicated to work animation around change in gravity given 
        animator.SetFloat("vAbsSpeed", Mathf.Abs(playerRB.velocity.y));
        animator.SetFloat("vSpeed", playerRB.velocity.y);
    }

    
    IEnumerator restartLevel(){
        yield return new WaitForSeconds(1f);
        gameManger.reloadLevel();
    }
}
