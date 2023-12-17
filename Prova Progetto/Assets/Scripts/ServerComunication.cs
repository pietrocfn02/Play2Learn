using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
public class ServerComunication : MonoBehaviour
{
    private static DatabaseReference dbReference;
    private static ServerComunication _instance;
    // string id = SystemInfo.deviceUniqueIdentifier; 

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
}
