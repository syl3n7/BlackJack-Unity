using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Button dealButton;
    public Button hitBttn;
    public Button standBttn;
    public Button betBttn;
    public Player player, dealer;

    private int standClicks = 0;
    
    //scoreboard
    public TMPro.TMP_Text playerScore;
    public TMPro.TMP_Text dealerScore;
    public TMPro.TMP_Text bets;
    public TMPro.TMP_Text cash;

    public TMPro.TMP_Text standBtnText;
    
    //game state 
    public TMPro.TMP_Text gamestate;
    
    //card to hide the dealer card
    public GameObject hiddenCard;
    //bet amount
    public TMPro.TMP_Text amountBet;
    private int pot = 0;
    
    void Start()
    {
        GameObject.Find("Deck").GetComponent<Deck>().Shuffle();
        //onclick listeners
        standBttn.onClick.AddListener(() => Stand());
        hitBttn.onClick.AddListener(() => Hit());
        dealButton.onClick.AddListener(() => Deal());
        betBttn.onClick.AddListener(() => BetClicked());
    }

    private void Stand()
    {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standBtnText.text = "CALL";
    }

    private void HitDealer()
    {
        while (dealer.handValue < 16 && dealer.cardIndex < 10)
        {
            dealer.GetCard();
            dealerScore.text = "Hand: " + dealer.handValue.ToString();
            if(dealer.handValue > 20) RoundOver();
        }
    }

    private void Hit()
    {
        //check that there is room on the table
        if(player.cardIndex <= 10)
        {
            player.GetCard();
            playerScore.text = "Hand: " + player.handValue;
            if(player.handValue > 20) RoundOver();
        }
    }
    
    private void Deal()
    {
        //reset round
        player.ResetHand();
        dealer.ResetHand();
        //hide deal hand score at start
        gamestate.gameObject.SetActive(false);
        dealerScore.gameObject.SetActive(false); //control the presence of the hand score at the start of the deal
        GameObject.Find("Deck").GetComponent<Deck>().Shuffle();
        player.GetHand();
        dealer.GetHand();
        //Update the scoreboard
        playerScore.text = "Hand: " + player.handValue;
        dealerScore.text = "Hand: " + dealer.handValue;
        //enable to hide one of the dealers cards
        hiddenCard.GetComponent<Renderer>().enabled = true;
        //adjust the bttns 
        dealButton.gameObject.SetActive(false);
        hitBttn.gameObject.SetActive(true);
        standBttn.gameObject.SetActive(true);
        standBtnText.text = "Stand";
        // set pot size 
        pot = 40;
        bets.text = "Bets: $" + pot;
        player.adjustMoney(-20);
        cash.text = "$" + player.GetMoney();
    }
    
    //check for winner and loser, and is over
    void RoundOver()
    {
        //Booleans for bust
        bool playerBust = player.handValue > 21;
        bool dealerBust = dealer.handValue > 21;
        bool player21 = player.handValue == 21;
        bool dealer21 = dealer.handValue == 21;
        
        //if stand has been clicked less that twice, no 21 or busts quit
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;
        if (playerBust && dealerBust)
        {
            gamestate.text = "All Bust: Bets returned";
            player.adjustMoney(pot / 2);
            
        }
        //if player busts,dealer doesnt , or if dealer has more points, dealer wins
        else if (playerBust || (!dealerBust && dealer.handValue > player.handValue))
        {
            gamestate.text = "Dealer Wins !";
        }
        //if dealer busts, player doesnt, or player has more points, player wins
        else if (dealerBust || player.handValue > dealer.handValue)
        {
            gamestate.text = "You Win!";
            player.adjustMoney(pot);
        }
        //check for tie, return bets
        else if (player.handValue == dealer.handValue)
        {
            gamestate.text = "Push: Bets returned";
            player.adjustMoney(pot / 2);
        }
        else
        {
            roundOver = false;
        }
        if (roundOver) //update ui for the next move / hand / turn
        {
            hitBttn.gameObject.SetActive(false); 
            standBttn.gameObject.SetActive(false);
            dealButton.gameObject.SetActive(true);
            gamestate.gameObject.SetActive(true);
            dealerScore.gameObject.SetActive(true);
            hiddenCard.GetComponent<Renderer>().enabled = false;
            cash.text = "$" + player.GetMoney();
            standClicks = 0;
        }
    }
    //add money to pot if bet clicked
    void BetClicked()
    {
        int intBet = int.Parse(amountBet.text.Remove(0, 1));
        player.adjustMoney(-intBet);
        cash.text = "$" + player.GetMoney();
        pot += (intBet * 2);
        bets.text = "Bets: $" + pot;
    }
}
