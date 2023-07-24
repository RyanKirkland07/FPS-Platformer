using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    public GameObject player;
    public AudioClip Song1;
    public AudioClip Song2;
    public AudioSource SongSource;
    // Start is called before the first frame update
    void Start()
    {
        SongSource.clip = Song1;
        SongSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
