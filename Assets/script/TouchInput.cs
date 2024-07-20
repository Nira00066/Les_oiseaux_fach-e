using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalleControleur : MonoBehaviour
{
    // Référence à la caméra principale
    private Camera mainCamera;
    // Référence au Rigidbody2D de la balle
    [SerializeField] private Rigidbody2D ballRigidbody2D;
    // Position du toucher sur l'écran
    private Vector2 touchPosition;
    // Position du toucher convertie en coordonnées du monde
    private Vector3 worldPosition;
    // Référence au SpringJoint2D de la balle
    [SerializeField] private SpringJoint2D ballSpringJoint2D;
    // Position initiale de la balle
    private Vector3 initialPosition;
    // Indique si l'écran a été touché
    private bool hasTouch = false;

    // Start est appelé avant la première frame update
    void Start()
    {
        // Récupère la caméra principale
        mainCamera = Camera.main;
        // Stocke la position initiale de la balle
        initialPosition = ballRigidbody2D.position;
    }

    // Update est appelé à chaque frame
    void Update()
    {
        // Vérifie si l'écran n'est pas touché
        if (!Touchscreen.current.primaryTouch.press.IsPressed())
        {
            // Si l'écran était touché précédemment
            if (hasTouch)
            {
                // Libère la balle
                ballRigidbody2D.isKinematic = false;
                // Lance la balle après un léger délai
                Invoke("LaunchBall", 0.15f);
            }
            return;
        }

        // Marque que l'écran est touché
        hasTouch = true;

        // Récupère la position du toucher
        touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        // Convertit la position du toucher en coordonnées du monde
        worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        Debug.Log(worldPosition);
        Debug.Log(touchPosition);

        // Fait en sorte que la balle suive le toucher
        ballRigidbody2D.GetComponent<Rigidbody2D>().isKinematic = true;
        ballRigidbody2D.position = worldPosition;
    }

    // Lance la balle en désactivant le SpringJoint2D
    private void LaunchBall()
    {
        ballSpringJoint2D.enabled = false;
        // Démarre une coroutine pour réinitialiser la balle après un délai de 2 secondes
        StartCoroutine(ResetBallAfterDelay(2.0f));
    }

    // Coroutine qui attend un certain délai avant de réinitialiser la balle
    private IEnumerator ResetBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetBall();
    }

    // Réinitialise la position, la vitesse et le joint de la balle
    private void ResetBall()
    {
        // Remet la balle à sa position initiale
        ballRigidbody2D.position = initialPosition;
        // Réinitialise la vitesse de la balle
        ballRigidbody2D.velocity = Vector2.zero;
        // Réactive le SpringJoint2D
        ballSpringJoint2D.enabled = true;
        // Réinitialise l'état de toucher
        hasTouch = false;
    }
}

