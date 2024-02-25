using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeeScript : MonoBehaviour
{
    HiveScript homeHive = null;
    private int flowerIndex;
    private FlowerScript[] flowerObjects = null;

    Vector3 hivePosition = Vector3.zero;

    private bool holdingNectar = false;

    [SerializeField] private int fail = 0;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bee;
    [SerializeField] Sprite beeWithNectar;

    [SerializeField] private AudioSource pollinateSFX1;
    [SerializeField] private AudioSource pollinateSFX2;
    [SerializeField] private AudioSource pollinateSFX3;
    [SerializeField] private AudioSource pollinateSFX4;

    [SerializeField] private AudioSource beeDeathSFX;

    private void randPollinateSFX()
    {
        int i = Random.Range(1, 4);
        if (i == 1) { pollinateSFX1.Play(); }
        if (i == 2) { pollinateSFX2.Play(); }
        if (i == 3) { pollinateSFX3.Play(); }
        if (i == 4) { pollinateSFX4.Play(); }
    }
    public HiveScript Init(HiveScript hive)
    {
        homeHive = hive;
        hivePosition = hive.transform.position;
        goToRandomFlower();

        return hive;
    }

    FlowerScript GetAnyFlower()
    {
        flowerObjects = FindObjectsOfType<FlowerScript>();

        flowerIndex = Random.Range(0, flowerObjects.Length);

        return flowerObjects[flowerIndex];
    }

    Vector3 GetFlowerPosition()
    {
        Vector3 randomFlowerPosition = GetAnyFlower().transform.position;

        return randomFlowerPosition;
    }

    float randSpeed()
    {
        float speed = Random.Range(3f, 5f);
        return speed;
    }

    void goToRandomFlower()
    {
        transform.DOMove(GetFlowerPosition(), randSpeed()).OnComplete(() =>
        {
            if (flowerObjects[flowerIndex].hasNectar == true)
            {
                flowerObjects[flowerIndex].GetNectar();
                holdingNectar = true;
                spriteRenderer.sprite = beeWithNectar;
                randPollinateSFX();
                fail = 0;
            }
            else if (flowerObjects[flowerIndex].hasNectar == false)
            {
                fail++;

                if (fail == 3) 
                {
                    spriteRenderer.color = new Color32(125, 0, 0, 255);
                    beeDeathSFX.Play();
                    Init(homeHive).numOfBees--;
                    GameObject.Destroy(gameObject, beeDeathSFX.clip.length);
                }
                goToRandomFlower();
            }

            if (holdingNectar)
            {
                transform.DOMove(hivePosition, randSpeed()).OnComplete(() =>
                {
                    holdingNectar = false;
                    homeHive.giveNectar();
                    spriteRenderer.sprite = bee;
                    goToRandomFlower();
                }).SetEase(Ease.Linear);
            }
        }).SetEase(Ease.Linear);
    }
}
