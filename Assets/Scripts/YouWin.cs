using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class YouWin : MonoBehaviour
{
    public GameObject Winner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider Info)
    {
        if(Info.tag == "Win")
        {
            Winner.SetActive(true);
            StartCoroutine(Return());
            IEnumerator Return()
            {
                yield return new WaitForSeconds(5);
                SceneManager.LoadScene("StartMenu");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
