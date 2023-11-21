using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;

// Firebase �ҷ�����

public class GameManager : MonoBehaviour
{
    private DatabaseReference databaseReference;
    void Start()
    {
        Debug.Log("Firebase Initialization Started");

        // Firebase �ʱ�ȭ
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

            if (databaseReference != null)
            {
                Debug.Log("Firebase Initialization Completed");
                //AddDataToDatabase(); // Firebase �ʱ�ȭ�� �Ϸ�� �� �����ͺ��̽� �۾� ����
            }
            else
            {
                Debug.LogError("Failed to initialize Firebase properly.");
            }
        });

        Debug.Log("Firebase Initialization Finished");
        // �����ͺ��̽� ��θ� ������ �ν��Ͻ��� �ʱ�ȭ
        // Database�� Ư�������� ����ų �� �ִµ�, �� �� RootReference�� ����Ŵ

        Rank rank = new Rank("���ϵ�", 100, 1574940551);
        string json = JsonUtility.ToJson(rank);
        // �����͸� json���·� ��ȯ

        string key = databaseReference.Child("rank").Push().Key;
        // firebase�� key���� 0 ���ο�, 1 Ȳ����, 2 ������.
        // root�� �ڽ� rank�� key ���� �߰����ִ� ����

        databaseReference.Child("rank").Child(key).SetRawJsonValueAsync(json);
        // ������ Ű�� �ڽ����� json�����͸� ����
    }

    class Rank
    {
        // ���� ������ ��� Rank Ŭ����
        // Firebase�� �����ϰ� name, score, timestamp�� ������ �ؾ���
        public string name;
        public int score;
        public int timestamp;
        // JSON ���·� �ٲ� ��, ������Ƽ�� ������ �ȵ�. ������Ƽ�� X

        public Rank(string name, int score, int timestamp)
        {
            // �ʱ�ȭ�ϱ� ���� ������ ���
            this.name = name;
            this.score = score;
            this.timestamp = timestamp;
        }
    }

    public DatabaseReference reference { get; set; }
    // ���̺귯���� ���� �ҷ��� FirebaseDatabase ���ð�ü�� �����ؼ� ���

}
