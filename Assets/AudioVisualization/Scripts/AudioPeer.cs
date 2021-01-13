using UnityEngine;

public class AudioPeer : MonoBehaviour {

    public AudioSource audioSource;
    public bool useBuffer = true;

    private float[] samples = new float[512];
    private float[] freqBands = new float[8];
    private float[] freqBandBuffers = new float[8];
    private float[] bufferDecrease = new float[8];

    private float[] freqBandsHighest = new float[8];
    private float[] audioBands = new float[8];
    private float[] audioBandBuffers = new float[8];

    void Update() {
        if (audioSource == null) {
            return;
        }
        ReadSamples();
        CalculateFrequencyBands();
        if (useBuffer) {
            BufferFrequencyBands();
        }
        CalculateAudioBands();
    }

    private void ReadSamples() {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
    public void CalculateFrequencyBands() {
        int totalSampleCount = 0;
        for (int freqIndex = 0; freqIndex < freqBands.Length; freqIndex++) {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, freqIndex) * 2;

            if (freqIndex == 7) {
                sampleCount += 2;
            }
            for (int sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++) {
                average += samples[totalSampleCount] * (totalSampleCount + 1);
                totalSampleCount++;
            }

            average /= totalSampleCount;

            freqBands[freqIndex] = average;
        }
    }

    private void BufferFrequencyBands() {
        for (int freqIndex = 0; freqIndex < freqBands.Length; freqIndex++) {
            // New Maximum reached
            if (freqBands[freqIndex] > freqBandBuffers[freqIndex]) {
                freqBandBuffers[freqIndex] = freqBands[freqIndex];
                bufferDecrease[freqIndex] = 0.005f;
            } else if (freqBands[freqIndex] < freqBandBuffers[freqIndex]) {
                freqBandBuffers[freqIndex] -= bufferDecrease[freqIndex];
                bufferDecrease[freqIndex] *= 1.2f;
            }
        }
    }

    public void CalculateAudioBands() {
        for (int freqIndex = 0; freqIndex < freqBands.Length; freqIndex++) {
            if (freqBands[freqIndex] > freqBandsHighest[freqIndex]) {
                freqBandsHighest[freqIndex] = freqBands[freqIndex];
            }
            audioBands[freqIndex] = freqBands[freqIndex] / freqBandsHighest[freqIndex];
            audioBandBuffers[freqIndex] = freqBandBuffers[freqIndex] / freqBandsHighest[freqIndex];
        }
    }

    public float GetSample(int sampleIndex) {
        return samples[sampleIndex];
    }
    public float GetFrequencyBand(int freqIndex) {
        return useBuffer ? freqBandBuffers[freqIndex] : freqBands[freqIndex];
    }

    // Normalized frequency band between 0 and 1
    public float GetAudioBand(int freqIndex) {
        return audioBands[freqIndex];
    }

}
