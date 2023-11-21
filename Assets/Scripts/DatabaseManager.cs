using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField Gold;

    public Text NameText;
    public Text GoldText;

    private string userID;
    private DatabaseReference dbreference;
    // Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbreference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public void CreatorUser()
    {

        User newUser = new User(Name.text, int.Parse(Gold.text));
        string Json = JsonUtility.ToJson(newUser);

        dbreference.Child("users").Child(userID).SetRawJsonValueAsync(Json);

    }

    public IEnumerator GetName(Action<string> onCallback)
    {

        var userNameData = dbreference.Child("users").Child(userID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }


    public IEnumerator GetGold(Action<int> onCallback)
    {
        var userGoldData = dbreference.Child("users").Child(userID).Child("gold").GetValueAsync();

        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if(userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));

        }

    }

    public void GetUserInfo()
    {
        StartCoroutine(GetName ((string name) =>
        {
            NameText.text = "Name: " + name;
        }));


        StartCoroutine(GetGold((int gold) =>
        {
            GoldText.text = "Gold: " + gold.ToString(); 
        }));


    }

    public void UpdateName()
    {
        dbreference.Child("users").Child(userID).Child("name").SetValueAsync(Name.text);
    }

    public void UpdateGold()
    {
        dbreference.Child("users").Child(userID).Child("gold").SetValueAsync(Gold.text);
    }




}
