using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource attack;
    [SerializeField] AudioSource damage;

    public enum SoundType
    {
        ATTACK = 0,
        RUN = 1,
        DAMAGE = 2
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    public static void PlaySound(SoundType s)
    {

    }
}
