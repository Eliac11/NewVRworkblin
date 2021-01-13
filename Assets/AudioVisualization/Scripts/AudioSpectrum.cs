using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour {

    public AudioPeer audioPeer;
    public GameObject bumperPrefab;

    private List<AudioBumper> bumpers = new List<AudioBumper>();

    // Start is called before the first frame update
    void Start() {
        for (int bumperIndex = 0; bumperIndex < 8; bumperIndex++) {
            AudioBumper bumper = Instantiate<GameObject>(bumperPrefab, transform).GetComponent<AudioBumper>();
            bumper.transform.position += Vector3.right * (-1.4f + bumperIndex * 0.3f);
            bumper.audioPeer = audioPeer;
            bumper.frequencyBand = bumperIndex;
            bumper.multiplier = 3;
            bumpers.Add(bumper);
        }
    }

}
