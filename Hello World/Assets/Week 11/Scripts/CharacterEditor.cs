using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CharacterEditor
{
    public class CharacterEditor : MonoBehaviour
    {
        [SerializeField] Button nextMaterial;
        [SerializeField] Button nextBodyPart;
        [SerializeField] Button loadGame;

        [SerializeField] Character character;

        int id;
        public BodyTypes bodyType = BodyTypes.Head;

        private void Awake()
        {
            //TODO: Setup some button listeners to call the NextMaterial, NextBodyPart, and LoadGame functions
            //nextMaterial.Invoke("NextMaterial", 0);
            //nextBodyPart.Invoke("NextBodyPart", 0);
            //loadGame.Invoke("LoadGame", 0);
        }

        public void NextMaterial()
        {
            //TODO: Add 1 to the value of id and if it is 3 or more then reset back to 0
            id++;
            if (id == 3) { id = 0; };
            //TODO: Make a switch case for each BodyType and save the value of id to the correct PlayerPref
            switch (bodyType)
            {
                case BodyTypes.Arm: 
                    PlayerPrefs.SetInt("ArmColor", id);
                    break;
                case BodyTypes.Leg: 
                    PlayerPrefs.SetInt("LegColor", id);
                    break;
                case BodyTypes.Head: 
                    PlayerPrefs.SetInt("HeadColor", id);
                    break;
                case BodyTypes.Body: 
                    PlayerPrefs.SetInt("BodyColor", id);
                    break;
                default: 
                    PlayerPrefs.SetInt("ArmColor", id);
                    break;
            }
            //TODO: Tell the character to load to get the updated body piece
            character.Load();
        }

        public void NextBodyPart()
        {
            //TODO: Setup a switch case that will go through the different body types
            //      ie if the current type is Head and we click next then set it to Body
            switch (bodyType)
            {
                case BodyTypes.Arm:
                    bodyType = BodyTypes.Leg;
                    break;
                case BodyTypes.Leg:
                    bodyType = BodyTypes.Head;
                    break;
                case BodyTypes.Head:
                    bodyType = BodyTypes.Body;
                    break;
                case BodyTypes.Body:
                    bodyType = BodyTypes.Arm;
                    break;
                default:
                    bodyType = BodyTypes.Arm;
                    break;
            }
            //TODO: Then setup another switch case that will get the current saved value
            //      from the player prefs for the current body type and set it to id
            switch (bodyType)
            {
                case BodyTypes.Arm:
                    id = PlayerPrefs.GetInt("ArmColor");
                    break;
                case BodyTypes.Leg:
                    id = PlayerPrefs.GetInt("LegColor");
                    break;
                case BodyTypes.Head:
                    id = PlayerPrefs.GetInt("HeadColor");
                    break;
                case BodyTypes.Body:
                    id = PlayerPrefs.GetInt("BodyColor");
                    break;
                default:
                    id = PlayerPrefs.GetInt("ArmColor");
                    break;
            }
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("CharacterCreator");
        }
    }
}