using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class DisplayTimes : MonoBehaviour
{
    public TextMeshProUGUI Time0, Time1, Time2, Time3, Time4;
    private string UTime0, UTime1, UTime2, UTime3, UTime4;
    private bool worked;
    async void Awake()
    {
        //Get snapshot of current User's time scores
        var level0T = await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level0Time").GetValueAsync();
        var level1T = await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level1Time").GetValueAsync();
        var level2T = await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level2Time").GetValueAsync();
        var level3T = await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level3Time").GetValueAsync();
        var level4T = await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level4Time").GetValueAsync();
        worked = true;
        if(worked)
        {
            //Get string value of User's time scores
            UTime0 = (level0T.Value.ToString());
            UTime1 = (level1T.Value.ToString());
            UTime2 = (level2T.Value.ToString());
            UTime3 = (level3T.Value.ToString());
            UTime4 = (level4T.Value.ToString());

            //Displays User's time scores in scene selection tab
            Time0.text = UTime0;
            Time1.text = UTime1;
            Time2.text = UTime2;
            Time3.text = UTime3;
            Time4.text = UTime4;
        }
    }

}
