using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Transform target;
    public Vector3 zAxis = new Vector3(0, 0, 1);
    public bool canRotate = true;

    public int rotateDegree = 1;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void rotate(){
          if (canRotate){
            InvokeRepeating("rotating", 1f, 0.01f);
        }
        
    
    }




    void rotating(){
            if (rotateDegree < 90 && canRotate){
               transform.RotateAround(target.position, zAxis, 1);
               rotateDegree++;
               if (rotateDegree == 90){
                   canRotate = false;
                   CancelInvoke("rotating");
                   
               }
        }

    }


    






    
}
