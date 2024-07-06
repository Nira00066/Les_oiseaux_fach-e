using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private Camera mainCamera;
   [SerializeField] private Rigidbody2D ballRigidbody;
    [SerializeField] private SpringJoint2D springJoint2;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Out the method if not touching the screen
        if (!Touchscreen.current.primaryTouch.press.isPressed())
        {
            return;
        }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        Debug.Log("TOUCH POSITION: " + touchPosition);
        Debug.Log("WORLD POSITION: " + worldPosition);

        // Déplacer la balle à la position du toucher
        ballRigidbody.position = new Vector2(worldPosition.x, worldPosition.y);
   
 }
}
