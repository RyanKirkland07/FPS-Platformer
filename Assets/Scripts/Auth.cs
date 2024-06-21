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
    public TextMeshProUGUI usernameInput;
    public DatabaseReference dbReference;

    private static float UTime0, UTime1, UTime2, UTime3, UTime4;
    public static string UID;
    public static float finalTime;
    public static int bidx;
    public LevelTransition transScript;
    public YouWin YouWin;

    public TextMeshProUGUI timeCounter;
    public static float timer;
    public static bool TimerOn;

    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser player;
    private Firebase.Auth.AuthResult result;

    private bool LogInFailed;
    //private DataSnapshot snapshot;

    private string DATA_URL = "https://fpsplatformer-default-rtdb.asia-southeast1.firebasedatabase.app/";

    void Start(){
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        DontDestroyOnLoad(gameObject);
    }

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
            }
            

        });
    }
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
    public void LogOutUser(){
        FirebaseAuth.DefaultInstance.SignOut();
    }
    private async void GetTimeValues(){
        var level0T = await dbReference.Child("User").Child(UID).Child("level0Time").GetValueAsync();
        var level1T = await dbReference.Child("User").Child(UID).Child("level1Time").GetValueAsync();
        var level2T = await dbReference.Child("User").Child(UID).Child("level2Time").GetValueAsync();
        var level3T = await dbReference.Child("User").Child(UID).Child("level3Time").GetValueAsync();
        var level4T = await dbReference.Child("User").Child(UID).Child("level4Time").GetValueAsync();
        bool worked = true;
        if(worked){
            UTime0 = float.Parse(level0T.Value.ToString());
            UTime1 = float.Parse(level1T.Value.ToString());
            UTime2 = float.Parse(level2T.Value.ToString());
            UTime3 = float.Parse(level3T.Value.ToString());
            UTime4 = float.Parse(level4T.Value.ToString());
        }
        SceneManager.LoadScene("StartMenu");
    }

    public void GetIndex(int idx){
        timer = 0;
        bidx = idx;
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
    public IEnumerator UpdateTimer(){
        while(TimerOn){
            yield return new WaitForSeconds(0.01f);
            timer += (1 * Time.deltaTime);
            timeCounter.text = timer.ToString();
        }
    }

    public void Transition()
    {
        Debug.LogError(UTime0);
        TimerOn = false;
        finalTime = timer;
        Debug.LogError(finalTime);
        timer = 0;
        CheckTimerValues();
    }
    public async void CheckTimerValues()
    {
        Debug.LogError("finalTime: " + finalTime);
        switch(bidx)
        {
            case <2:
                Debug.LogError("How?!");
                break;
            case 2:
                if(finalTime < UTime0)
                {
                    UTime0 = finalTime;
                    Debug.LogError("New Time 0 = " + UTime0);
                    User user = new User(UTime0, UTime1, UTime2, UTime3, UTime4);
                    string json = JsonUtility.ToJson(user);
                    await FirebaseDatabase.DefaultInstance.RootReference.Child("User").Child(UID).SetRawJsonValueAsync(json);
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
