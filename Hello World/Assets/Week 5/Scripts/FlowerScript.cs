using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    [SerializeField] private float gainNectarTime = 20;
    [SerializeField] private float plantDeathTime = 60;
    [SerializeField] private float timer = 0;

    public bool hasNectar = true;
    [SerializeField] private int timesGained = 0;
    [SerializeField] private GameObject flower;

    [SerializeField] private AudioSource flowerDeathSFX;

    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        setNecterToTrue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasNectar)
        {
            spriteRenderer.color = new Color32(125, 125, 125, 255);
            if (timer < gainNectarTime)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                timer = 0;
                setNecterToTrue();
            }
        }

        if (hasNectar) 
        {
            if (timer < plantDeathTime)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                spriteRenderer.color = new Color32(125, 0, 0, 255);
                flowerDeathSFX.Play();
                Destroy(gameObject, flowerDeathSFX.clip.length);
            }
        }
    }

    void setNecterToTrue()
    {
        hasNectar = true;
        spriteRenderer.color = Color.white;
        timer = 0;
    }

    public void GetNectar()
    {
        hasNectar = false;
        timesGained++;
        timer = 0;

        if (timesGained >= 3)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8, 8), Random.Range(-3, 5), 0);
            timesGained = 0;
            Instantiate(flower, SpawnPosition, transform.rotation);
        }
    }
}
