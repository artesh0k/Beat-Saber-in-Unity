using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Nezbytne pro praci s prvky UI
using EzySlice;
using UnityEngine.InputSystem;

public class Saber : MonoBehaviour
{
    public LayerMask layer;
    private Vector3 previousPos;

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public Material crossSectionMaterial;
    public float cutForce = 2000;
    public VelocityEstimator velocityEstimator;

    // Pridavame prefab pro jiskry
    public GameObject sparkEffectPrefab;

    public AudioClip sliceSoundClip;
    private AudioSource audioSource;

    // Pocitatdlo rezanych kostek
    private int score = 0;

    // Textovy prvek pro zobrazeni skore
    public Text scoreText;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Inicializace score na zacatku
        UpdateScoreText();
    }

    void Update()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, layer);
        
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target); 
            
            if (sliceSoundClip != null)
            {
                audioSource.PlayOneShot(sliceSoundClip);
            }

            // Tvorba jiskr v miste dopadu
            if (sparkEffectPrefab != null)
            {
                Instantiate(sparkEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            // Zvysyjeme skore a aktualizujme text
            score++;
            UpdateScoreText();
            
        }


    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        
        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);
            Destroy(target);
        }

        
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Your score: " + score;
        }
    }
}
