using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using UnityEngine.SceneManagement;
using static Firebase.Extensions.TaskExtension;


public class Auth : MonoBehaviour
{
    public TextMeshProUGUI emailInput;
    public TextMeshProUGUI passwordInput;

    public TextMeshProUGUI ErrorText;

    public DatabaseReference dbReference;

    private static float UTime0, UTime1, UTime2, UTime3, UTime4;
    private static float High0, High1, High2, High3, High4;
    private static string HighScoreUser0, HighScoreUser1, HighScoreUser2, HighScoreUser3, HighScoreUser4;
    private static string displayedName0, displayedName1, displayedName2, displayedName3, displayedName4;
    private static string UID;
    private static float finalTime;
    private static int bidx;
    public LevelTransition transScript;
    public YouWin YouWin;

    public TextMeshProUGUI timeCounter;
    private static float timer;
    private static bool TimerOn;

    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser player;
    private Firebase.Auth.AuthResult result;

    private bool LogInFailed;
    //private DataSnapshot snapshot;

    private string DATA_URL = "https://fpsplatformer-default-rtdb.asia-southeast1.firebasedatabase.app/";

    void Start(){
        //Define Database reference
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(gameObject);
        ErrorText.text = "";
    }
     //Register user to Firebase Authentication
    public void RegisterUser(){
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWith(task => {
            if(task.IsCanceled){
                Debug.LogError("Registering User Was Canceled");
                return;
            }
            if(task.IsFaulted) {
                Debug.LogError("Registering User Encountered An Error" + task.Exception);
                return;
            }
            if(task.IsCompleted){
                var player = FirebaseAuth.DefaultInstance.CurrentUser;
                Firebase.Auth.AuthResult result = task.Result;
                Debug.Log("Firebase User Registered Successfully "+ result.User.UserId.ToString());
                User user = new User(999.9f, 999.9f, 999.9f, 999.9f, 999.9f);
                string json = JsonUtility.ToJson(user);
                dbReference.Child("User").Child(player.UserId).SetRawJsonValueAsync(json);
                ErrorText.text = "Successfully registered User";
                return;
            }
            

        });
    }

    //Use Firebase Authentication to log in a user and get their time scores from Database
    public async void LogInUser(){
        await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWith( task => {
            if(task.IsCanceled) {
                Debug.LogError("Sign In Was Canceled");
                LogInFailed = true;
                return;
            }
            if(task.IsFaulted){
                Debug.LogError("Sign In Encountered An Error");
                LogInFailed = true;
                return;
            }
            if (task.IsCompleted){
                Firebase.Auth.AuthResult result = task.Result;
                Debug.LogFormat("User Signed In Successfully", result.User.DisplayName, result.User.UserId);
                LogInFailed = false;
                var player = FirebaseAuth.DefaultInstance.CurrentUser;
                UID = player.UserId;
            }
                });
            if(!LogInFailed){
                GetTimeValues();
            }
        
    }
    private async void GetTimeValues(){

        //Get snapshot of User's time scores
        var level0T = await dbReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level0Time").GetValueAsync();
        var level1T = await dbReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level1Time").GetValueAsync();
        var level2T = await dbReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level2Time").GetValueAsync();
        var level3T = await dbReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level3Time").GetValueAsync();
        var level4T = await dbReference.Child("User").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("level4Time").GetValueAsync();

        //Get snapshot of highscores
        var HS0 = await dbReference.Child("Times").Child("level0High").GetValueAsync();
        var HS1 = await dbReference.Child("Times").Child("level1High").GetValueAsync();
        var HS2 = await dbReference.Child("Times").Child("level2High").GetValueAsync();
        var HS3 = await dbReference.Child("Times").Child("level3High").GetValueAsync();
        var HS4 = await dbReference.Child("Times").Child("level4High").GetValueAsync();
        bool worked = true;
        if(worked){
            //Get float value of User's time scores
            UTime0 = float.Parse(level0T.Value.ToString());
            UTime1 = float.Parse(level1T.Value.ToString());
            UTime2 = float.Parse(level2T.Value.ToString());
            UTime3 = float.Parse(level3T.Value.ToString());
            UTime4 = float.Parse(level4T.Value.ToString());

            //Get float value of highscores
            High0 = float.Parse(HS0.Value.ToString());
            High1 = float.Parse(HS1.Value.ToString());
            High2 = float.Parse(HS2.Value.ToString());
            High3 = float.Parse(HS3.Value.ToString());
            High4 = float.Parse(HS4.Value.ToString());
        }
        //Transfer to menu from login screen
        SceneManager.LoadScene("StartMenu");
    }

    //Function calls when entering new scene: resets timer and gets buildIndex
    public void GetIndex(int idx){
        timer = 0;
        bidx = idx;
        //If buildIndex corresponds to a level start timer
        if(bidx > 1)
        {
            timer = 0f;
            TimerOn = true;
            if(TimerOn)
            {
                StartCoroutine(UpdateTimer());
            }
        }
    }

    //Function to update timer and display time on screen
    public IEnumerator UpdateTimer(){
        while(TimerOn){
            yield return new WaitForSeconds(0.01f);
            timer = Time.timeSinceLevelLoad;
            timeCounter.text = timer.ToString();
        }
    }

    //Function called when transition levels
    public void Transition()
    {
        Debug.LogError(UTime0);
        TimerOn = false;
        finalTime = timer;
        Debug.LogError(finalTime);
        timer = 0;
        CheckTimerValues();
    }

    //Checks if new time score is faster than highscore
    public async void CheckTimerValues()
    {
        Debug.LogError("finalTime: " + finalTime);
        switch(bidx)
        {
            case <2:
                Debug.LogError("How?!");
                break;
            case 2:
                //If new time is faster than old time change time to new time
                if(finalTime < UTime0)
                {
                    UTime0 = finalTime;
                    Debug.LogError("New Time 0 = " + UTime0);
                    User user = new User(UTime0, UTime1, UTime2, UTime3, UTime4);
                    string json = JsonUtility.ToJson(user);
                    await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(UID).SetRawJsonValueAsync(json);
                    //If new time score is faster than highscore time change highscore and highscore user in Database
                    if(UTime0 < High0)
                    {
                        High0 = UTime0;
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("level0High").SetValueAsync(UTime0);
                        HighScoreUser0 = FirebaseAuth.DefaultInstance.CurrentUser.Email;
                        displayedName0 = HighScoreUser0.Substring(0, 4);
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("HighScoreUser0").SetValueAsync(displayedName0);
                    }
                    transScript.sceneTransition();
                }
                else
                {
                    transScript.sceneTransition();
                }
                break;
            case 3:
                if(finalTime < UTime1)
                {
                    UTime1 = finalTime;
                    Debug.LogError("New Time 1 = " + UTime1);
                    User user = new User(UTime0, UTime1, UTime2, UTime3, UTime4);
                    string json = JsonUtility.ToJson(user);
                    await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(UID).SetRawJsonValueAsync(json);
                    if(UTime1 < High1)
                    {
                        High1 = UTime1;
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("level1High").SetValueAsync(UTime1);
                        HighScoreUser1 = FirebaseAuth.DefaultInstance.CurrentUser.Email;
                        displayedName1 = HighScoreUser1.Substring(0, 4);
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("HighScoreUser1").SetValueAsync(displayedName1);
                    }
                    transScript.sceneTransition();
                }
                else
                {
                    transScript.sceneTransition();
                }
                break;
            case 4:
                if(finalTime < UTime2)
                {
                    UTime2 = finalTime;
                    Debug.LogError("New Time 2 = " + UTime2);
                    User user = new User(UTime0, UTime1, UTime2, UTime3, UTime4);
                    string json = JsonUtility.ToJson(user);
                    await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(UID).SetRawJsonValueAsync(json);
                    if(UTime2 < High2)
                    {
                        High2 = UTime2;
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("level2High").SetValueAsync(UTime2);
                        HighScoreUser2 = FirebaseAuth.DefaultInstance.CurrentUser.Email;
                        displayedName2 = HighScoreUser2.Substring(0, 4);
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("HighScoreUser2").SetValueAsync(displayedName2);
                    }
                    transScript.sceneTransition();
                }
                else
                {
                    transScript.sceneTransition();
                }
                break;
            case 5:
                if(finalTime < UTime3)
                {
                    UTime3 = finalTime;
                    Debug.LogError("New Time 3 = " + UTime3);
                    User user = new User(UTime0, UTime1, UTime2, UTime3, UTime4);
                    string json = JsonUtility.ToJson(user);
                    await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(UID).SetRawJsonValueAsync(json);
                    if(UTime3 < High3)
                    {
                        High3 = UTime3;
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("level3High").SetValueAsync(UTime3);
                        HighScoreUser3 = FirebaseAuth.DefaultInstance.CurrentUser.Email;
                        displayedName3 = HighScoreUser3.Substring(0, 4);
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("HighScoreUser3").SetValueAsync(displayedName3);
                    }
                    transScript.sceneTransition();
                }
                else
                {
                    transScript.sceneTransition();
                }
                break;
            case 6:
                if(finalTime < UTime4)
                {
                    UTime4 = finalTime;
                    Debug.LogError("New Time 4 = " + UTime4);
                    User user = new User(UTime0, UTime1, UTime2, UTime3, UTime4);
                    string json = JsonUtility.ToJson(user);
                    await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(UID).SetRawJsonValueAsync(json);
                    if(UTime4 < High4)
                    {
                        High4 = UTime4;
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("level4High").SetValueAsync(UTime4);
                        HighScoreUser4 = FirebaseAuth.DefaultInstance.CurrentUser.Email;
                        displayedName4 = HighScoreUser4.Substring(0, 4);
                        await FirebaseDatabase.DefaultInstance.RootReference.Child("Times").Child("HighScoreUser4").SetValueAsync(displayedName4);
                    }
                    YouWin.LastScene();
                }
                else
                {
                    YouWin.LastScene();
                }
                break;
        }   
    }
}
