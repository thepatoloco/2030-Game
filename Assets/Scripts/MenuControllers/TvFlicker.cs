using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TvFlicker : MonoBehaviour
{
    public Sprite[] spritesTv;
    public float changeInterval = 0.5f;

    private Image imageComponent;
    private float timeSinceLastChange = 0f;

    private void Start()
    {
        imageComponent= GetComponent<Image>();
    }
    void Update()
    {
        // Actualiza el tiempo transcurrido desde el último cambio de sprite
        timeSinceLastChange += Time.deltaTime;

        // Si ha pasado el intervalo de cambio, cambia el sprite
        if (timeSinceLastChange >= changeInterval)
        {
            timeSinceLastChange = 0f;
            ChangeStaticSprite();
        }
    }

    void ChangeStaticSprite()
    {
        // Escoge aleatoriamente un sprite de estática del array
        int randomIndex = Random.Range(0, spritesTv.Length);
        imageComponent.sprite = spritesTv[randomIndex];
    }

}
