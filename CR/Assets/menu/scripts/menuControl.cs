using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class menuControl : MonoBehaviour
{

    [SerializeField] public GameObject[] myDeck;
    [SerializeField] public GameObject[] myDecktTransfarObject;
    [SerializeField] public GameObject holdingobject;
    [SerializeField] public Sprite notHolldingimage;



    void Start()
    {

    }


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
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = notHolldingimage;
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop = null;
    }

    public void MyDeckClick(GameObject Deck)
    {
        DeckExit();
        if (holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite == notHolldingimage)
        {
            Deck.transform.GetChild(1).gameObject.SetActive(true);
            Deck.transform.GetComponent<Image>().transform.localScale = new Vector3(0.13f, 0.88f, 1f);
        }
        else
        {
            Deck.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = notHolldingimage;

            Deck.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop =
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop;
            Deck.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().Cost =
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().Cost;
            holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop = null;
        }
    }


    public void DeckClick(GameObject Deck)
    {
        DeckExit();
        Deck.transform.GetChild(1).gameObject.SetActive(true);
        Deck.transform.GetComponent<Image>().transform.localScale = new Vector3(0.125f, 0.24f, 1f);
    }

    public void DeckExit()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("UnLockDeck"))
        {
            item.transform.GetComponent<Image>().transform.localScale = new Vector3(0.125f, 0.224f, 1f);
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
                item.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().Cost =
                imageObject.transform.GetComponent<TroopInDeck>().Cost;

                DeckExit();
                return;
            }

        }
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite =
        imageObject.transform.GetComponent<Image>().sprite;
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop =
        imageObject.transform.GetComponent<TroopInDeck>().DeckTroop;
        holdingobject.transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().Cost =
        imageObject.transform.GetComponent<TroopInDeck>().Cost;
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

    public void butlleClick(GameObject emtyMassage)
    {

        for (int i = 0; i < myDeck.Length; i++)
        {
            myDecktTransfarObject[i].GetComponent<TroopInDeck>().Cost = myDeck[i].transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().Cost;
            myDecktTransfarObject[i].GetComponent<TroopInDeck>().DeckTroop = myDeck[i].transform.GetChild(0).GetChild(0).GetComponent<TroopInDeck>().DeckTroop;
            myDecktTransfarObject[i].GetComponent<TroopInDeck>().DeckTroopImage = myDeck[i].transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
            myDecktTransfarObject[i].tag = "TransferDeck";
            if (myDecktTransfarObject[i].GetComponent<TroopInDeck>().DeckTroop != null)
            {
                DontDestroyOnLoad(myDecktTransfarObject[i]);
            }
            else
            {
                StartCoroutine(emtyMassageCoroutine(emtyMassage));
                return;
            }
        }
SceneManager.LoadScene("Game");
    }


    IEnumerator emtyMassageCoroutine(GameObject emtyMassage)
    {
        yield return emtyMassage.GetComponent<Text>().text = "deck is emety.please sellect deck and go battle";
        yield return new WaitForSeconds(1);
        yield return emtyMassage.GetComponent<Text>().text = "";
    }


}
