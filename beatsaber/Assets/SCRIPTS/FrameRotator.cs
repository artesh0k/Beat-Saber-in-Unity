using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRotator : MonoBehaviour
{
    // Maximalni uhel rotace
    public float maxAngle = 15f;

    // Rychlost rotace
    public float rotationSpeed = 0.5f;

    public float smoothTime = 2f;

    // Aktualni uhel
    private float currentAngle = 0f;

    // Cilovy uhel pro plynulou zmenu
    private float targetAngle = 0f;

    void Update()
    { 
        targetAngle = Mathf.Sin(Time.time * rotationSpeed) * maxAngle;
         
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * smoothTime);
         
        transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
    }
}
