using System.IO;
using System.Text;
using UnityEngine;

public class SaveData
{
    public string Save(Player activePlayer)
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
        foreach(Player player in GameManager.Instance.players)
        {
            string loadedString = JsonUtility.ToJson(player);
            var mySaveData = JsonUtility.FromJson<Player>(loadedString);
            Debug.Log(mySaveData.Name);
        }
    }
}
