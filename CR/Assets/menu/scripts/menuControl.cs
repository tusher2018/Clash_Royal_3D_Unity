using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class menuControl : MonoBehaviour
{

    [SerializeField] public GameObject[] myDeck;
    [SerializeField] public GameObject holdingobject;
    [SerializeField] public Sprite notHolldingimage;


    // Use this for initialization
    void Start()
    {
        //myDeck = GameObject.FindGameObjectsWithTag("MyDeck");
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite == notHolldingimage)
        {
            foreach (GameObject item in myDeck)
            {
                item.transform.GetComponent<Animation>().CrossFade("deckNormal", 0.0f);
            }
        }
        else
        {
            foreach (GameObject item in myDeck)
            {
                item.transform.GetComponent<Animation>().CrossFade("deckVivation", 0.0f);
            }
        }
    }

    public void makeHoldingNull()
    {
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =  notHolldingimage;
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop = null;
    }

    public void MyDeckClick(GameObject Deck)
    {
        DeckExit();
        if (holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite ==  notHolldingimage)
        {
            Deck.transform.GetChild(1).gameObject.SetActive(true);
            Deck.transform.GetComponent<Image>().transform.localScale = new Vector3(0.13f, 0.88f, 1f);
        }
        else
        {
            Deck.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =  notHolldingimage;

            Deck.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop =
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop;
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop = null;
        }
    }


    public void DeckClick(GameObject Deck)
    {
        DeckExit();
        Deck.transform.GetChild(1).gameObject.SetActive(true);
        Deck.transform.GetComponent<Image>().transform.localScale = new Vector3(1f, 1.2f, 1f);
    }

    public void DeckExit()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("UnLockDeck"))
        {
            item.transform.GetComponent<Image>().transform.localScale = new Vector3(.88f, 1.11f, 1f);
            item.transform.GetChild(1).gameObject.SetActive(false);
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("MyDeck"))
        {
            item.transform.GetComponent<Image>().transform.localScale = new Vector3(0.11f, 0.87f, 1f);
            item.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void Useclick(GameObject imageObject)
    {
        foreach (GameObject item in myDeck)
        {
            imageObject.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
            if (item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite == null)
            {
                item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                imageObject.transform.GetComponent<Image>().sprite;
                item.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop =
                imageObject.transform.GetComponent<TroopInDeck>().DeckTroop;

                DeckExit();
                return;
            }

        }
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
        imageObject.transform.GetComponent<Image>().sprite;
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop =
        imageObject.transform.GetComponent<TroopInDeck>().DeckTroop;
        DeckExit();

    }

    public void Removeclick(GameObject imageObject)
    {
        foreach (GameObject item in myDeck)
        {
            imageObject.transform.parent.parent.GetChild(1).gameObject.SetActive(false);
            item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = null;
            item.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop = null;
            DeckExit();
        }
    }


}
