using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourne : MonoBehaviour
{
    public GameObject circle;

    void Start()
    {
        // Initialisation
        circle = GameObject.Find("Circle");
    }

    void Update()
    {
        // Logique de mise à jour
        if (circle != null)
        {
            // Exemple: Déplacer le cercle
            circle.transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
}
