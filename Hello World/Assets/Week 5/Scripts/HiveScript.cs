using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveScript : MonoBehaviour
{
    private float timer = 0;
    public int nectar = 0;
    [SerializeField] private int honey = 0;
    [SerializeField] private float makeHoney = 2;
    [SerializeField] private int honeyNeededForNewBee = 5;

    [SerializeField] private GameObject Bee;
    [SerializeField] private int startingBeesAmnt = 2;
    public int numOfBees = 0;
    

    [SerializeField] private AudioSource enterSFX;
    [SerializeField] private AudioSource exitSFX;
    [SerializeField] private AudioSource madeHoneySFX;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite beeHive;
    [SerializeField] private Sprite beeHiveWithHoney;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startingBeesAmnt; i++) 
        {
            spawnBee();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nectar >= 1)
        {
            spriteRenderer.color = Color.white;
            if (timer < makeHoney)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                nectar--;
                timer = 0;
                honey++;
                madeHoneySFX.Play();
            }

            if (honey >= honeyNeededForNewBee)
            {
                spawnBee();
                honey = 0;
            }
        }

        if (nectar < 1 && numOfBees > 0)
        {
            spriteRenderer.color = new Color32(125, 125, 125, 255);
        }

        if (honey > 0)
        {
            spriteRenderer.sprite = beeHiveWithHoney;
        }
        else
        {
            spriteRenderer.sprite = beeHive;
        }

        if (numOfBees <= 0 && nectar < 1)
        {
            spriteRenderer.color = new Color32(125, 0, 0, 255);
        }
    }

    void spawnBee()
    {
        float xPos = this.transform.position.x;
        float yPos = this.transform.position.y;
        Vector3 SpawnPosition = new Vector3(Random.Range(xPos -1, xPos +1), Random.Range(yPos -1, yPos +1), 0);

        //spawn obstacle
        GameObject newBee = Instantiate(Bee, SpawnPosition, transform.rotation);
        newBee.GetComponent<BeeScript>().Init(this);
        numOfBees++;

        exitSFX.Play();
    }

    public void giveNectar()
    {
        enterSFX.Play();
        nectar++;
    }
}
