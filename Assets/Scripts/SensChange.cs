using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensChange : MonoBehaviour
{
    public CameraTurn CameraTurn;
    public void SetSensitivity (float Sens)
    {
        Debug.Log(Sens);
        CameraTurn.ChangeSens(Sens);

    }
}
