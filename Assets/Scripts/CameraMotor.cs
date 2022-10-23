using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt; // Player that the camera will look at
    public Vector3 offset = new Vector3(0, 5.0f, -10.0f);
    public Vector3 rotation = new Vector3(35, 0, 0);

    public GameManager gameManager;

    public bool IsMoving { set; get; }

    private void Start() 
    {
        gameManager.audioBackGround.Play();
    }
    
    private void LateUpdate() 
    {
        if(!IsMoving)
            return;
            
        gameManager.audioBackGround.Stop();
        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
    }
}
