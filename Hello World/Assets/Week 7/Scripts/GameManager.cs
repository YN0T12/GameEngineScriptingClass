using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Week7;

namespace Week10
{
    public class GameManager : MonoBehaviour
    {
        public UnityEvent GameOverEvent;
        public GameObject coins, enemies, doors;

        [ContextMenu("Game Over")]
        public void OnGameOver()
        {
            GameOverEvent.Invoke();
        }

        [TextAreaAttribute]
        public string Note;

        public void restartCoins()
        {
            
            foreach (Transform child in coins.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        public void restartEnemies()
        {
            foreach (Transform child in enemies.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Enemy>().RestartGame();
            }
        }
        public void restartDoors()
        {
            foreach (Transform child in doors.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Enemy>().RestartGame();
            }
        }
    }
}
