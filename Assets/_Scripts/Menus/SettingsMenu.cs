using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TMP_InputField userNameInput;
    private TMP_Text[] texts;
    void Start()
    {
        texts = userNameInput.GetComponentsInChildren<TMP_Text>();
        if (SaveUserData.data != null)
            texts[0].text = SaveUserData.data.Name;
    }

    public void SaveName()
    {
        UserData userData = SaveUserData.data;
        userData.Name = texts[1].text;

        string data = JsonUtility.ToJson(userData);

        SaveManager.Instance.SaveData(SaveUserData.userPath, data);
    }
}
