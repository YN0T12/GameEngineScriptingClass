using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week11
{
    public class DataSaver : MonoBehaviour
    {
        [SerializeField]
        private string playerName = "Bob";
        [SerializeField]
        private int level;
        [SerializeField]
        private float cash = 0.99f;

        [ContextMenu("Save Data")]
        void SaveData()
        {
            PlayerPrefs.SetInt("Levels Complete", level);
            PlayerPrefs.SetString("Name", playerName);
            PlayerPrefs.SetFloat("Money", cash);
        }

        [ContextMenu("Load Data")]
        void LoadData()
        {
            level = PlayerPrefs.GetInt("Levels Complete", 1);
            playerName = PlayerPrefs.GetString("Name", "bob");
            cash = PlayerPrefs.GetFloat("Money", 0f);
        }
    }
}
