using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
public class ServerComunication : MonoBehaviour
{
    private static DatabaseReference dbReference;
    private static ServerComunication _instance;
    private string jsonData = "";
    private int cont = 0;
    public static ServerComunication GetInstance()
    {
        if (_instance == null)
        {
            GameObject databaseManager = new GameObject("DatabaseManager");
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            _instance = databaseManager.AddComponent<ServerComunication>();
            DontDestroyOnLoad(databaseManager);
        }
        return _instance;
    }
    
    async void DeleteEntity(DatabaseReference entityRef) {
        await entityRef.SetRawJsonValueAsync(null);
    }
    public void WriteData(Collectable collectable)
    {
        try
        {
            string json = JsonUtility.ToJson(collectable);
            dbReference.Child(collectable.GetName()).SetRawJsonValueAsync(json);
        }
        catch (Exception e)
        {
            Debug.LogError("Distrutto tutto: " + e.Message);
        }
    }
    public string ReciveData(string name, string child)
    {
            FirebaseDatabase.DefaultInstance.GetReference(name).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.Log("Failed to retrieve data: " + task.Exception);
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists){
                    jsonData = snapshot.Child(child).GetRawJsonValue();
                }
                else
                {
                    Debug.LogWarning("No data found at specified path.");
                }
            }
            });
      return jsonData;
    }
}
