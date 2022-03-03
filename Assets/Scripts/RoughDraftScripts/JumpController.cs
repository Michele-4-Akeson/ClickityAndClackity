using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [Header ("References")]
    private Rigidbody2D playerRB;
    public Transform bottom;
    public Transform left;
    public Transform right;
    public float radius;
    public LayerMask floor;
    

    [Header ("Control Inputs")]
    public float horizontalInput;
    public float verticalInput;


    [Header ("Movement Attributes/States")]
    public bool onGround;
    public bool onWall;
    public bool onWallLeft;
    public bool onWallRight;
    public float jumpForce;
    public float dashForce;

    public bool canJump;
    public bool canDash;
    public bool isJumping;
    public bool isDashing;
    public float jumpTime;
    public float dashTime;

    public float jumpLimit;
    public float dashLimit;

    public int jumpCount = 2;

    public Manager manager;
    public Animator playerAnimator;



    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        playerAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(bottom.position, radius, floor);
        onWallLeft = Physics2D.OverlapCircle(left.position, radius, floor);
        onWallRight = Physics2D.OverlapCircle(right.position, radius, floor);
        onWall = (onWallLeft || onWallRight);
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (onGround){
            canJump = true;
            canDash = false;
            jumpTime = jumpLimit;
            isDashing = false;

            jumpCount = 2;
        }


        if (horizontalInput != 0){
            flip_sprite();
            //dash();
            dashX();
            dashingX();
          

        }



        if (verticalInput != 0){
            jump();
            jumping();

        }


    adjustAnimation();
        
    }


     void flip_sprite()
    { /*
       * flip_sprite() rotates the x value of a Gameobjects transform component, flipping the object
       * to face the opposite direction if it is moving right, or left
       */
        Vector3 scale = transform.localScale;
        if (horizontalInput < 0 && !isDashing)
        {
         
            scale.x = Mathf.Abs(scale.x);
            this.transform.localScale = scale;
             
        } else if (horizontalInput > 0 && !isDashing){

            scale.x = Mathf.Abs(scale.x) / -1;
            this.transform.localScale = scale;
        }
    }





    





    void jump(){
        if (canJump){
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            isJumping = true;

            canDash = true;
            

            dashTime = dashLimit;

        }

    }


    void jumping(){
        if (isJumping && jumpTime > 0){
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTime -= Time.deltaTime;
        } else {
            isJumping = false;
        }
    }



    void dash(){
        if (canDash){
            canJump = true;
            StartCoroutine("dashing");
        }
    }


    void dashX(){
          if (canDash){
            playerRB.velocity = new Vector2(0f, 0f); // stops motion
            playerRB.AddForce(new Vector2(horizontalInput * dashForce, 0f), ForceMode2D.Impulse);
            canDash = false;
            isDashing = true;
        }
    }

    void dashingX(){
         if (isDashing && dashTime > 0){
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
            playerRB.AddForce(new Vector2(horizontalInput * dashForce, 0f), ForceMode2D.Impulse);
            dashTime -= Time.deltaTime;
        } else {
            isDashing = false;
        }

    }


    IEnumerator dashing(){
        /*
        dashing() does a forced dash over a length of time and player has no control after hitting dash button;
        */
        playerRB.velocity = new Vector2(0f, 0f); // stops motion
        playerRB.AddForce(new Vector2(dashForce * horizontalInput, 0f), ForceMode2D.Impulse);
        float gravity = playerRB.gravityScale;
        playerRB.gravityScale = 0;
        canDash = false;
        isDashing = true;
        
        yield return new WaitForSeconds(dashTime);
        playerRB.gravityScale = gravity;
        isDashing = false;
    }



    public Vector2 freeze(){
        Vector2 v = playerRB.velocity; 
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        print(playerRB.velocity);
        return v;
    }



    public void unFreeze(Vector2 v){
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRB.velocity += v;
        print(playerRB.velocity);
        print(v);
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("redBlock")){
            Destroy(gameObject);
            //Destroy(other.gameObject);
        }
    }




    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Lamp Head")){
            manager.x();
        }
    }











    void adjustAnimation(){
        playerAnimator.SetFloat("verticalSpeed", playerRB.velocity.y);
        playerAnimator.SetBool("OnGround", onGround);
        playerAnimator.SetBool("Dashing", isDashing);
        playerAnimator.SetFloat("hSpeed", Mathf.Abs(playerRB.velocity.x));
    }

}
