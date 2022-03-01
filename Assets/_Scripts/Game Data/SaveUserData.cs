using Firebase.Auth;
using UnityEngine;

public class SaveUserData : MonoBehaviour
{
	public static UserData data;
	public static string userPath;

	private void Start()
	{
		FindObjectOfType<FirebaseManager>().OnSignIn += OnSignIn;
		userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
	}

	void OnSignIn()
	{
		userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		SaveManager.Instance.LoadData(userPath, OnLoadData);
	}

	void OnLoadData(string json)
	{
		if (json != null)
		{
			data = JsonUtility.FromJson<UserData>(json);
		}
		else
		{
			data = new UserData();
			data.Name = "User " + FirebaseAuth.DefaultInstance.CurrentUser.UserId.Substring(0, 6);
			SaveData();
		}

		FindObjectOfType<FirebaseManager>()?.PlayerDataLoaded();
	}

	public static void SaveData()
	{
		SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(data));
	}
}