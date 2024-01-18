//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AudioManager : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------------------------------ Audio Source ------------------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------------------------------------ Audio Clip ------------------------")]
    public AudioClip background;
    public AudioClip explosion;
    public AudioClip button;
    public AudioClip collect;
    public AudioClip win;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}