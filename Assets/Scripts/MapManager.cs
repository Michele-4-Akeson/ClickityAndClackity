using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header ("References")]
    public Transform pivot; // the transform of the gameObject the map will rotate around
    public Transform other;


    [Header ("State")]
    public string rotationDirection;
    public int rotationState;
    public bool canRotate = true;

    public int rotateDegree;
    public int rotationInterval;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void setPivot(Transform target, Transform attched){
        pivot = target;
        other = attched;
    }

    public void rotateMapRight(){
        if (canRotate){
            rotateDegree = 0;
            InvokeRepeating("rotationRight",  1f, 0.01f);
        }
    }


     public void rotateMapLeft(){
        if (canRotate){
            rotateDegree = 0;
            InvokeRepeating("rotationLeft",  1f, 0.01f);
        }
    }



    private void rotationRight(){
          if (rotateDegree < rotationInterval){
               transform.RotateAround(pivot.position, Vector3.forward, 1); // vector3.forward = (0, 0, 1)
               other.RotateAround(pivot.position, Vector3.forward, 1); // vector3.forward = (0, 0, 1)
               rotateDegree++;
               if (rotateDegree == rotationInterval){
                   canRotate = false;
                   CancelInvoke("rotationRight");
               }
          }

    }



    
    private void rotationLeft(){
          if (rotateDegree < rotationInterval){
               transform.RotateAround(pivot.position, Vector3.forward, -1); // vector3.forward = (0, 0, 1)
               other.RotateAround(pivot.position, Vector3.forward, -1); // vector3.forward = (0, 0, 1)
               rotateDegree++;
               if (rotateDegree == rotationInterval){
                   canRotate = false;
                   CancelInvoke("rotationLeft");
               }
          }

    }





}
