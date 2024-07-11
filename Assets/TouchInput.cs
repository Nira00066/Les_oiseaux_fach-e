using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private Camera mainCamera;

    public float releaseTime = 0.15f;
    public Rigidbody2D rb;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            OnTouchDown();
        }
        else
        {
            OnTouchUp();
        }

        if (isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            worldPosition.z = 0; // Ensure z position is 0 for 2D

            rb.position = worldPosition;
        }
    }

    private void OnTouchDown()
    {
        if (!isPressed)
        {
            isPressed = true;
            rb.isKinematic = true;
        }
    }

    private void OnTouchUp()
    {
        if (isPressed)
        {
            isPressed = false;
            rb.isKinematic = false;
            StartCoroutine(Release());
        }
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
    }
}
