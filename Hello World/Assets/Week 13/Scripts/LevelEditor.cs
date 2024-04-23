using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.IO;

public class LevelEditor : MonoBehaviour
{
    [SerializeField]
    TMP_InputField lvlNumField;
    [SerializeField]
    TMP_InputField lvlNameField;
    [SerializeField]
    TMP_InputField coinNumField;
    [SerializeField]
    TMP_InputField enemyNumField;

    public int lvlNum;
    public string lvlName;
    public int coinNum;
    public int enemyNum;

    string path;

    public void Save()
    {
        lvlNum = int.Parse(lvlNumField.text);
        lvlName = lvlNameField.text;
        coinNum = int.Parse(coinNumField.text);
        enemyNum = int.Parse(enemyNumField.text);

        path = $"Assets/Resources/Levels/Level_{lvlNum.ToString("00")}.txt";

        LevelData levelData = new LevelData
        {
            lvlNum = lvlNum,
            lvlName = lvlName,
            coinNum = coinNum,
            enemyNum = enemyNum
        };

        string jsonData = JsonUtility.ToJson(levelData);

        //File.WriteAllText(path, jsonData);

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(JsonUtility.ToJson(levelData));
        writer.Close();

        AssetDatabase.ImportAsset(path);
    }
}
