using System.Collections.Generic;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Database;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance { get { return _instance; } }

    public delegate void OnLoadedDelegate(string json);
    public delegate void OnSaveDelegate();

    FirebaseDatabase database;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        database = FirebaseDatabase.DefaultInstance;
    }

    public void LoadData(string path, OnLoadedDelegate onLoadedDelegate)
    {
        database.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            onLoadedDelegate(task.Result.GetRawJsonValue());
        });
    }

    public void SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        database.RootReference.Child(path).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            onSaveDelegate?.Invoke();
        });
    }

    public string GetKey(string path)
    {
        return database.RootReference.Child(path).Push().Key;
    }
}
