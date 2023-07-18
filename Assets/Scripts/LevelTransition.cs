using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour 
{
    public GameObject LevelTransitioner;
    public LevelTransition script;
    private void OnTriggerEnter(Collider Info)
    {
        Debug.Log("Working");
        if(Info.tag == "NextScene")
        {
            SceneTransition();
        }
    }
    public void SceneTransition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
