using UnityEngine;

public class AudioAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;
    public float[] spectrumData = new float[64];
    public float intensityMultiplier = 10.0f; // Multiplikator pro nastaveni citlivosti

    public float GetAverageFrequency()
    {
        // Ziskavame zvukove spektrum
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        float sum = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            sum += spectrumData[i];
        }

        return sum * intensityMultiplier;
    }
}
