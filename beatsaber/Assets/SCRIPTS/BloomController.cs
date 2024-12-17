using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomController : MonoBehaviour
{
    public Volume volume;
    public AudioAnalyzer audioAnalyzer;

    private Bloom bloom;

    void Start()
    {
        
    }

    void Update()
    {
        if (bloom != null)
        {
            // Ziskani urovne hlasitosti z AudioAnalyzeru
            float intensity = audioAnalyzer.GetAverageFrequency();

            // Zvyseni intenzity Bloom a omezeni ji na rozsah
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, Mathf.Clamp(intensity * 2.0f, 2f, 15f), Time.deltaTime * 5f);

            // Zmena Thershold pro vice zare
            bloom.threshold.value = Mathf.Lerp(bloom.threshold.value, 1f, Time.deltaTime * 5f);
        }
    }
}
