using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameDeckControll : MonoBehaviour
{

    [SerializeField] GameObject SpritesProvider;
    

    Image myImage;
    [SerializeField] int num;

    void Start()
    {
        myImage = GetComponent<Image>();
        GameObject[] allSprite=GameObject.FindGameObjectsWithTag("TransferDeck");
        myImage.sprite=allSprite[num].GetComponent<TroopInDeck>().DeckTroopImage;
        GetComponent<TroopInDeck>().DeckTroop=allSprite[num].GetComponent<TroopInDeck>().DeckTroop;
        GetComponent<TroopInDeck>().Cost=allSprite[num].GetComponent<TroopInDeck>().Cost;

    }


    void Update()
    {
        if (myImage.sprite == null)
        {
            if (SpritesProvider.GetComponent<Image>().sprite != null)
            {
                myImage.sprite = SpritesProvider.GetComponent<Image>().sprite;
                GetComponent<TroopInDeck>().DeckTroop=SpritesProvider.GetComponent<TroopInDeck>().DeckTroop;
                GetComponent<TroopInDeck>().Cost=SpritesProvider.GetComponent<TroopInDeck>().Cost;
                SpritesProvider.GetComponent<Image>().sprite = null;
                SpritesProvider.GetComponent<TroopInDeck>().DeckTroop= null;
                SpritesProvider.GetComponent<TroopInDeck>().Cost= 0;


            }
        }
    }
}
