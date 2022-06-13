using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


public class spriteProviderControll : MonoBehaviour
{

    Image image;
    [SerializeField] GameObject[] OtherDeckAndProvider;
    public GameObject cloneImage;
    [SerializeField] Sprite[] having = new Sprite[4];
    [SerializeField] GameObject[] alldeck;
    public GameObject player;


    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (cloneImage.GetComponent<Image>().sprite != null)
        {
            cloneImage.transform.position = Input.mousePosition;
        }

        for (int i = 0; i < OtherDeckAndProvider.Length; i++)
        {
            if (OtherDeckAndProvider[i].GetComponent<Image>().sprite != null)
            {
                having[i] = OtherDeckAndProvider[i].GetComponent<Image>().sprite;
            }
        }
        if (transform.root.GetComponent<payerConorl>().BlueTeam) { alldeck = allSpritesArray(GameObject.FindGameObjectsWithTag("bluecard")); }
        if (!transform.root.GetComponent<payerConorl>().BlueTeam) { alldeck = allSpritesArray(GameObject.FindGameObjectsWithTag("redcard")); }

        if (image.sprite == null)
        {
            foreach (GameObject allSprite in alldeck)
            {
                if (having[0] != null)
                {
                    if (allSprite.GetComponent<TroopInDeck>().DeckTroopImage != having[0])
                    {
                        if (having[1] != null)
                        {
                            if (allSprite.GetComponent<TroopInDeck>().DeckTroopImage != having[1])
                            {
                                if (having[2] != null)
                                {
                                    if (allSprite.GetComponent<TroopInDeck>().DeckTroopImage != having[2])
                                    {
                                        if (having[3] != null)
                                        {
                                            if (allSprite.GetComponent<TroopInDeck>().DeckTroopImage != having[3])
                                            {
                                                if (image.sprite == null)
                                                {
                                                    image.sprite = allSprite.GetComponent<TroopInDeck>().DeckTroopImage;
                                                    transform.GetComponent<TroopInDeck>().DeckTroop = allSprite.GetComponent<TroopInDeck>().DeckTroop;
                                                    transform.GetComponent<TroopInDeck>().Cost = allSprite.GetComponent<TroopInDeck>().Cost;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    GameObject[] allSpritesArray(GameObject[] oldarray)
    {
        GameObject[] newArray = new GameObject[oldarray.Length];
        int size = oldarray.Length;
        for (int i = 0; i < size; i++)
        {
            int index = Random.Range(0, oldarray.Length);
            newArray[i] = oldarray[index];
            oldarray = RemoveIndices(oldarray, index);
        }
        return newArray;
    }

    private GameObject[] RemoveIndices(GameObject[] IndicesArray, int RemoveAt)
    {
        GameObject[] newIndicesArray = new GameObject[IndicesArray.Length - 1];
        int i = 0; int j = 0;
        while (i < IndicesArray.Length)
        {
            if (i != RemoveAt)
            {
                newIndicesArray[j] = IndicesArray[i];
                j++;
            }

            i++;
        }
        return newIndicesArray;
    }

    public void CmddeckClick(GameObject GiveThis)
    {
        if (player != null)
        {
            player.GetComponent<payerConorl>().Troop = GiveThis.transform.GetComponent<TroopInDeck>().DeckTroop;
            player.GetComponent<payerConorl>().TroopCost = GiveThis.transform.GetComponent<TroopInDeck>().Cost;
            player.GetComponent<payerConorl>().deck = GiveThis;
            cloneImage.GetComponent<RectTransform>().anchoredPosition = GiveThis.GetComponent<RectTransform>().anchoredPosition;
            cloneImage.GetComponent<Image>().sprite = GiveThis.GetComponent<Image>().sprite;
            player.GetComponent<payerConorl>().cloneImage = cloneImage.GetComponent<Image>();
        }
    }


}
