using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class TouchInput : MonoBehaviour
{


[SerializeField] private GameObject ballPrefab;
[SerializeField] public Rigidbody2D rbPivot;
[SerializeField] private float releaseTime = 0.15f;
[SerializeField] private float repopTime = 5.0f;
    
    private Camera mainCamera;
    private bool isDragging = false;

private Rigidbody2D ballPrest;
private SpringJoint2D ballSpringJoin;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
          spawnBall();  
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
           if(isDragging)
            {
             LauncheBall();
             Invoke(nameof(spawnBall),repopTime);
             isDragging = false ;
           } 
           return;
        }
       isDragging = true;
        
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            worldPosition.z = 0; // Ensure z position is 0 for 2D
            rbPivot.isKinematic = true; 
            rbPivot.position = worldPosition;
        
    }

private void spawnBall()
{
    if (ballPrefab != null)
    {
        if(ballPrest!= null)
        {
            Destroy(ballPrest);
        }
         ballPrest = Instantiate(ballPrefab, rbPivot.transform.position);
        rbPivot = ballPrest.GetComponent<Rigidbody2D>();
        ballSpringJoin = ballPrest.GetComponent<SpringJoint2D>();
        ballSpringJoin.connectedBody = rbPivot;
    }
}

    private Rigidbody2D Instantiate(GameObject ballPrefab, Vector3 position)
    {
        throw new NotImplementedException();
    }

    private  void LauncheBall()
{ if(rbPivot != null) {
    rbPivot.isKinematic = false;
    rbPivot = null ;
    Invoke(nameof(DetachBall),releaseTime );
}

}
private void DetachBall()
{
    if(rbPivot != null)
    {
        rbPivot.isKinematic = false;
        rbPivot = null;
        Invoke(nameof(DetachBall),DetachBall);
    }
}

    private void Invoke(string v, Action detachBall)
    {
        throw new NotImplementedException();
    }


}
