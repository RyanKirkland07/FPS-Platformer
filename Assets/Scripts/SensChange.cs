using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensChange : MonoBehaviour
{
    //Finds CameraTurn script
    public CameraTurn CameraTurn;

    //Calls on the ChangeSens function in the CameraTurn script
    public void SetSensitivity (float Sens)
    {
        Debug.Log(Sens);
        CameraTurn.ChangeSens(Sens);

    }
}
