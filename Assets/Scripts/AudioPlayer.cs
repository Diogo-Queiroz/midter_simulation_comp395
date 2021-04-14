using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip clip;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(13.54f,0,-1.5f));
    }
}
