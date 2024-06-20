using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour 
{
    public GameObject LevelTransitioner;
    public LevelTransition script;
    public float timer;
    public float finalTime = 0;
    public int buildidx;

    public Auth authScript;

    void Start(){
    }

    private void OnTriggerEnter(Collider Info)
    {
        if(Info.tag == "NextScene")
        {
            //authScript.transition();
        }
    }
    public void sceneTransition(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
