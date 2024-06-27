using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;

public class DisplayLeaderboard : MonoBehaviour
{
    public TextMeshProUGUI Highscore0, Highscore1, Highscore2, Highscore3, Highscore4;
    private string High0, High1, High2, High3, High4;
    private bool worked;

    async void Awake()
    {
        var dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        var HS0 = await dbReference.Child("User").Child("Level0High").GetValueAsync();
        var HS1 = await dbReference.Child("User").Child("Level1High").GetValueAsync();
        var HS2 = await dbReference.Child("User").Child("Level2High").GetValueAsync();
        var HS3 = await dbReference.Child("User").Child("Level3High").GetValueAsync();
        var HS4 = await dbReference.Child("User").Child("Level4High").GetValueAsync();
        worked = true;

        if(worked)
        {
            High0 = HS0.Value.ToString();
            High1 = HS1.Value.ToString();
            High2 = HS2.Value.ToString();
            High3 = HS3.Value.ToString();
            High4 = HS4.Value.ToString();

            Highscore0.text = High0;
            Highscore1.text = High1;
            Highscore2.text = High2;
            Highscore3.text = High3;
            Highscore4.text = High4;
        }
    }
}
