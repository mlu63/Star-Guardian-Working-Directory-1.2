using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioScript : MonoBehaviour
{
    public bool musicEnabled;
    public AudioSource source;

    // Use this for initialization
    void Start () {
        
        source = source.GetComponent<AudioSource>();
        musicEnabled = GlobalControl.Instance.musicOn;

        if (!musicEnabled)
        {
            source.Stop();
        } else if (musicEnabled)
        {
            source.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
