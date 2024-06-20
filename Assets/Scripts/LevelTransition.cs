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

    private static bool done;

    public Auth authScript;

    void Awake(){
        done = false;
    }

    private void OnTriggerEnter(Collider Info)
    {
        if(Info.tag == "NextScene")
        {
            if(!done){
                done = true;
                authScript.Transition();
            }
        }
    }
    public void sceneTransition(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
