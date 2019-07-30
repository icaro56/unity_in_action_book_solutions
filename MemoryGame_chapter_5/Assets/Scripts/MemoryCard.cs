using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField]
    private GameObject cardBack;

    [SerializeField]
    private SceneController controller;

    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void SetCard(int cardId, Sprite image)
    {
        _id = cardId;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
