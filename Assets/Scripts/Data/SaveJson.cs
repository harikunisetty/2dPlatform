using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SaveJson
{
    private static readonly string fileName = "SaveGameData01.Sav";//File + File Path
    public static string GetFileName()
    {
        return Application.persistentDataPath + "/" + fileName;
    }
    public void SaveDataToFile(SaveData data)
    {
        data.hashvalue = string.Empty;

        string json = JsonUtility.ToJson(data);
        string file = GetFileName();

        data.hashvalue = GetSHAH256(json);
        json = JsonUtility.ToJson(data);

        FileStream fileStream = new FileStream(file, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }
    public bool LoadDataToFile(SaveData data)   // Get File
    {
        string loadFile = GetFileName();
        if (File.Exists(loadFile))
        {
            using (StreamReader reader = new StreamReader(loadFile))
            {
                string json = reader.ReadToEnd();
                JsonUtility.FromJsonOverwrite(json, data);

                if (CheckData(json))
                    JsonUtility.FromJsonOverwrite(json, data);
                else
                    Debug.LogError("Data Edited");
            }
            return true;
        }
        return false;
    }
    public bool CheckData(string json)
    {
        SaveData tempData = new SaveData();
        JsonUtility.FromJsonOverwrite(json, tempData);

        string oldHash = tempData.hashvalue;
        tempData.hashvalue = string.Empty;

        string tempJson = JsonUtility.ToJson(tempData);
        string newHash = GetSHAH256(tempJson);

        return (oldHash == newHash);
    }
    public void DeleteFile() // Delete File
    {
        File.Delete(GetFileName());
    }
    #region Cryptography
    public string GetHexStringFromHash(byte[] hash)
    {
        string hexString = string.Empty;
        foreach (byte b in hash)
        {
            hexString += b.ToString("x2");
        }
        return hexString;
    }
    public string GetSHAH256(string text)
    {
        byte[] textToBytes = Encoding.UTF8.GetBytes(text);
        SHA256Managed myShah256 = new SHA256Managed();
        byte[] hasvalues = myShah256.ComputeHash(textToBytes);
        return GetHexStringFromHash(hasvalues);
    }
    #endregion
}
