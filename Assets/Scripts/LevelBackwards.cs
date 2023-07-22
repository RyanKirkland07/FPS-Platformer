using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBackwards : MonoBehaviour 
{
    public GameObject LevelTransitioner;
    private void OnTriggerEnter(Collider BackwardsInfo)
    {
        Debug.Log("Working");
        if(BackwardsInfo.tag == "LastScene")
        {
            SceneBackwards();
        }
    }
    public void SceneBackwards()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
