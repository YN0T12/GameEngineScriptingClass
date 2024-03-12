using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Week8
{
    public class LogicScript : MonoBehaviour
    {
        // This is the players current score
        public int playerScore = 0;
        // This allows this script to talk to the score text
        public Text scoreText;
        [SerializeField] GameObject GameOverScreen;
        [SerializeField] GameObject startScreen;
        public bool startGameCheck = false;

        public float moveSpeed = 5;
        [SerializeField] float upgradeSpeedStrength = 0.05f;
        [SerializeField] int upgradeTimer = 0;
        [SerializeField] int levelToUpgrade = 10;
        [SerializeField] float maxTimeSpeed = 2;

        public BirdScript bird;

        [SerializeField] AudioSource scoreSFX;
        [SerializeField] AudioSource upgradeSFX;

        private void Start()
        {
            GameOverScreen.SetActive(false);
            startScreen.SetActive(true);
        }

        public void addScore(int scoreToAdd)
        {
            if (Time.timeScale < maxTimeSpeed)
            {
                // This adds to the player score by "scoreToAdd"
                playerScore = playerScore + scoreToAdd;

                if (upgradeTimer < levelToUpgrade)
                {
                    scoreSFX.Play();
                    upgradeTimer++;
                }

                // if your level is equal to levelToUpgrade
                else if (upgradeTimer == levelToUpgrade)
                {
                    upgradeSFX.Play();
                    Time.timeScale += upgradeSpeedStrength;
                    upgradeTimer = 0;
                    Debug.Log(Time.timeScale);
                }

                if (Time.timeScale > maxTimeSpeed)
                {
                    scoreSFX.Play();
                    Time.timeScale = maxTimeSpeed;
                    Debug.Log(Time.timeScale);
                }
            }
            else if (Time.timeScale >= maxTimeSpeed)
            {
                // This adds to the player score by "scoreToAdd"
                Time.timeScale = maxTimeSpeed;
                playerScore = playerScore + scoreToAdd;
            }

            //This sets the UI text showing the players score to the players current score according to the code
            scoreText.text = " " + playerScore.ToString();
        }

        public void startGame()
        {
            startScreen.SetActive(false);
            startGameCheck = true;
        }
        public void restartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void gameOver()
        {
            Time.timeScale = 1;
            upgradeTimer = 0;

            GameOverScreen.SetActive(true);
        }
    }
}
