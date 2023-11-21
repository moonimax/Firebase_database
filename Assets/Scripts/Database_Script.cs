using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class Database_Script : MonoBehaviour
{   // Start is called before the first frame update


    public string PlayerName;
    public string level;
    public string gold;

    // Initialize DatabaseReference
    private DatabaseReference databaseReference;
    
    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public int level;
        public float gold;

        public PlayerData(string name, int level, float gold)
        {
            this.PlayerName = name;
            this.level = level;
            this.gold = gold;
        }

    }
    private void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    void Start()
    {
        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });


        SavePlayerData("Player1", 5, 10000);
    }


    public void SavePlayerData(string playerName, int level, float gold)
    {
        // 플레이어 데이터를 Firebase에 저장
        PlayerData playerData = new PlayerData(playerName, level, gold);
        string json = JsonUtility.ToJson(playerData);
        databaseReference.Child("rank").Child(playerName).SetRawJsonValueAsync(json);

    }
}