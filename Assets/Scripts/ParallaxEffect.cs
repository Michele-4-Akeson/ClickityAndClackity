using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
   
    public Transform cameraTransform;
    [SerializeField]
    public float parallaxEffectMultiplier;
    private Vector3 initalCameraPosition;
    private Vector3 positionChange;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        initalCameraPosition = cameraTransform.position; // returns initial position of camera
        
        //parallaxEffectMultiplier = transform.position.z / 10; // create parallax multiplier based on z coordinate of landscape sprite
        
    }

    private void Update() {
        positionChange = (Vector3) cameraTransform.position - initalCameraPosition; // returns the change in position of the camera from frame to frame
        // Lerp to fade between positions
        transform.position += positionChange * parallaxEffectMultiplier; // moves landscape sprite by the amount the camera moved mulitpled by the landscape sprites parallax multiplier
        //transform.position = Vector2.Lerp(transform.position, cameraTransform.position, parallaxEffectMultiplier * Time.deltaTime);
        initalCameraPosition = cameraTransform.position;

    }

    public void positionOn(){
        transform.position = cameraTransform.position;
    }


}
