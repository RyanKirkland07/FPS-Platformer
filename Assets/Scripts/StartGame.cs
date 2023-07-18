using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public AudioClip Song;
    public AudioSource SongSource;
    // Start is called before the first frame update
    void Start()
    {
        SongSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
