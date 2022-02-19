using System.IO;
using System.Text;
using UnityEngine;

public class SaveData
{
    public string Save(PlayerData activePlayer)
    {
        string jsonString = JsonUtility.ToJson(activePlayer);
        SaveToFile(activePlayer.Name, jsonString);
        return jsonString;
    }

    private void SaveToFile(string fileName, string jsonString)
    {
        using (var stream = File.OpenWrite(fileName))
        {
            stream.SetLength(0);
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    public void Load()
    {
        foreach(PlayerData player in LocalGameManager.Instance.players)
        {
            string loadedString = JsonUtility.ToJson(player);
            var mySaveData = JsonUtility.FromJson<PlayerData>(loadedString);
            Debug.Log(mySaveData.Name);
        }
    }
}
