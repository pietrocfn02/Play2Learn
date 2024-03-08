using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Storage;
using Firebase.Auth;
public class ServerComunication : MonoBehaviour
{
    private static DatabaseReference dbReference;
    private static ServerComunication _instance;
    private string jsonData = "";
    private int cont = 0;
    private static Dictionary<string, string> explanationCache;
    private static Dictionary<string, string> answerCache;
    private static Dictionary<string, string> storageCache;
    private static FirebaseStorage storage;
    private static StorageReference storageRef;
    private static string storageURL = "gs://tesi-244c3.appspot.com";
    
    public static ServerComunication GetInstance()
    {
        if (_instance == null)
        {
            GameObject databaseManager = new GameObject("DatabaseManager");

            explanationCache = new Dictionary<string, string>();
            answerCache = new Dictionary<string, string>();
            storageCache = new Dictionary<string, string>();

            
            storage = FirebaseStorage.DefaultInstance;
            storageRef = storage.GetReferenceFromUrl(storageURL);
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
        if ((child == "answer" && !answerCache.ContainsKey(name)) || 
            (child == "explanation" && !explanationCache.ContainsKey(name))){
            FirebaseDatabase.DefaultInstance.GetReference(name).GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                Debug.Log("Failed to retrieve data: " + task.Exception);
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists){
                    if (child == "answer"){
                        jsonData = snapshot.Child(child).GetRawJsonValue();
                        answerCache.Add(name,jsonData);
                    } else if (child == "explanation") {
                        jsonData = snapshot.Child(child).GetRawJsonValue();
                        explanationCache.Add(name,jsonData);
                    }
                }
                else
                {
                    Debug.LogWarning("No data found at specified path.");
                }
            }
            });
            return jsonData;
        }else if (child == "answer"){
            return answerCache[name];
        } else {
            return explanationCache[name];
        }
    }
    public void DownloadFromStorage(string fileName, Action<byte[]> onComplete)
    {
        if (!storageCache.ContainsKey(fileName))
        {
            StorageReference fileRef = storageRef.Child(fileName);

            fileRef.GetBytesAsync(long.MaxValue).ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Failed to retrieve data from Firebase Storage: " + task.Exception);
                    onComplete(null);
                    return;
                }

                byte[] fileData = task.Result;
                storageCache.Add(fileName, Convert.ToBase64String(fileData)); // Converti in Base64 per memorizzare i dati nel cache
                onComplete(fileData);
            });
        }
        else
        {
            byte[] cachedData = Convert.FromBase64String(storageCache[fileName]); // Converte i dati in Base64 per recuperarli dal cache
            onComplete(cachedData);
        }
    }

}
