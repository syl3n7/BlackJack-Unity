using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Button dealButton;
    public Button hitBttn;
    public Button standBttn;
    public Button betBttn;
    public Player player, dealer;

    private int standClicks = 0;
    
    void Start()
    {
        GameObject.Find("Deck").GetComponent<Deck>().Shuffle();
        //onclick listeners
        standBttn.onClick.AddListener(() => Stand());
        hitBttn.onClick.AddListener(() => Hit());
        dealButton.onClick.AddListener(() => Deal());
    }

    private void Stand()
    {
        standClicks++;
        if (standClicks > 1) Debug.Log("end function");
        HitDealer();
        standBttn.GetComponent<TMP_Text>().text = "CAll";
    }

    private void HitDealer()
    {
        while (dealer.handValue < 16 && dealer.cardIndex < 10)
        {
            dealer.GetCard();
            //store dealer score
            
        }
    }

    private void Hit()
    {
        //check that there is room on the table
        if(player.GetCard() <= 10)
        {
            player.GetCard();
        }
    }
    
    private void Deal()
    {
        GameObject.Find("Deck").GetComponent<Deck>().Shuffle();
        player.GetHand();
        dealer.GetHand();
    }
}
