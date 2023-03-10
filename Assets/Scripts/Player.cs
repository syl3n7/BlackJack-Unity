using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used in both the player and the dealer!!

public class Player : MonoBehaviour
{
    // Get other scripts
    public Card card;   
    public Deck deck;

    // Total value of player/dealer's hand
    public int handValue = 0;

    // Betting money
    private int money = 1000;

    // Player's Hand
    public GameObject[] hand;

    // Index of the next card (Hit button)
    public int cardIndex = 0;
    
    // AceList to switch the value of the aces when needed
    List<Card> aceList = new List<Card>();

    public void GetHand()
    {
        GetCard();
        GetCard();
    }

    // Add a hand to the player/dealer's hand
    public int GetCard()
    {
        // Get a card, use deal card to assign sprite and value to card on table
        int cardValue = deck.DealCard(hand[cardIndex].GetComponent<Card>());

        hand[cardIndex].GetComponent<Renderer>().enabled = true;

        // Add card value to running total of the hand
        handValue += cardValue;

        // If value is 1, it is an ace
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<Card>());
        }
        //check if we should use 11 instead of 1
        aceCheck();
        cardIndex++;
        return handValue;
    }

    public void aceCheck() //search for the ace conversions, 1 to 11
    {
        foreach (Card ace in aceList) //for each ace in the list check
        {
            if (handValue + 10 < 22 && ace.GetValue() == 1)
            {
                // if converting , adjust card object value and hand
                ace.SetValue(11);
                handValue += 10;
            }
            else if (handValue > 21 && ace.GetValue() == 11)
            {
                // if converting , adjust card object value and hand
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }
    
    public void adjustMoney(int amount) // adds or subtracts from money
    {
        money += amount;
    }

    public int GetMoney() //gets the current money 
    {
        return money;
    }

    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<Card>().FlipCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }

        cardIndex = 0;
        handValue = 0;
        aceList = new List<Card>();
    }

}
