using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private int numberOfSequences = 10;
    [SerializeField] private int numberOfBatches = 4;
    private int numberOfCardsOfOneSuit = 13;
    private int numberOfSuits = 4;
    readonly List<string> cardsNames = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K", "A" };

    private Card bankTopCard = null;
    private List<Card> batchesTopCards = new List<Card>();

    [Header("UI")]
    [SerializeField] private List<Texture> cardsTextures;


    void Awake()
    {
        InitBatches();
        GenerateCards();

        for (int i = 0; i < numberOfBatches; i++)
        {
            Debug.Log(batchesTopCards[i].cardName);
        }
    }

    void Start()
    {
        
    }

    private void OnGUI()
    {
        GUI.backgroundColor = new Color(0,0,0,0);
        Texture bankCardTexture = cardsTextures[bankTopCard.index];
        GUI.Button(new Rect(640, 480, bankCardTexture.width, bankCardTexture.height),
            bankCardTexture);
        //int startX = 
        for (Card card = bankTopCard.BackCard; card != null; card = card.BackCard)
        {

        }
    }
    private void InitBatches()
    {
        for (int i = 0; i < numberOfBatches; i++)
        {
            batchesTopCards.Add(null);
        }
    }
   

    private void GenerateCards()
    {
        for (int i = 0; i < numberOfSequences; i++)
        {
            var sequence = GenerateSequence(Random.Range(2, 8));
            int index = cardsNames.IndexOf(sequence[0]) + numberOfCardsOfOneSuit * Random.Range(0, numberOfSuits);
            bankTopCard = new Card(bankTopCard, sequence[0], index);
            if (bankTopCard.BackCard != null)
            {
                bankTopCard.BackCard.FrontCard = bankTopCard;
            }

            for (int j = 1; j < sequence.Length; j++)
            {
                int batchIndex = Random.Range(0, batchesTopCards.Count);
                index = cardsNames.IndexOf(sequence[j]) + numberOfCardsOfOneSuit * Random.Range(0, numberOfSuits);
                batchesTopCards[batchIndex] = new Card(batchesTopCards[batchIndex], sequence[j], index);
                if (batchesTopCards[batchIndex].BackCard != null)
                {
                    batchesTopCards[batchIndex].BackCard.FrontCard = batchesTopCards[batchIndex];
                }
            }
        }
    }

    private string[] GenerateSequence(int length)
    {
        string[] sequence = new string[length];
        int firstCardIndex = Random.Range(0, cardsNames.Count);

        int changeDirectionIndex = -1;


        bool isGoingUp = true;

        if (Random.Range(0, 100) < 35)
        {
            isGoingUp = false;
        }

        if (Random.Range(0, 100) < 15)
        {
            changeDirectionIndex = Random.Range(1, cardsNames.Count);
        }



        for (int i = 0; i < length; i++)
        {
            sequence[i] = cardsNames[firstCardIndex];
            if (changeDirectionIndex == i)
            {
                isGoingUp = !isGoingUp;
            }
            if (isGoingUp)
            {
                firstCardIndex++;
                if (firstCardIndex == cardsNames.Count)
                {
                    firstCardIndex = 0;
                }
            }
            else
            {
                firstCardIndex--;
                if (firstCardIndex == -1)
                {
                    firstCardIndex = cardsNames.Count - 1;
                }
            }
        }
        return sequence;
    }
}
