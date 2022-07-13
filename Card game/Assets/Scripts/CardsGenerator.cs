using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsGenerator : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private int numberOfSequences = 10;
    [SerializeField] private int numberOfBatches = 4;
    readonly string[] cardsNames = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K", "A" };

    private Card bankTopCard = null;
    private List<Card> batchesTopCards;

    [Header("UI")]
    [SerializeField] private List<Sprite> cardsSprites;
    private Dictionary<string, List<Texture>> cardsTextures = new Dictionary<string, List<Texture>>();


    void Awake()
    {
        InitBatches();
        InitCardTextures();
        GenerateCards();

        for (int i = 0; i < numberOfBatches; i++)
        {
            Debug.Log(batchesTopCards[i].cardName);
        }
    }

    void Start()
    {

    }

    private void PlaceBank()
    {
        
    }
    private void InitBatches()
    {
        for (int i = 0; i < numberOfBatches; i++)
        {
            batchesTopCards.Add(null);
        }
    }
    private void InitCardTextures()
    {
        for (int i = 0; i < cardsNames.Length; i++)
        {
            var list = new List<Texture>();
            for (int j = i; j < cardsSprites.Count; j+=13)
            {
                list.Add(cardsSprites[j].texture);
            }
            cardsTextures.Add(cardsNames[i], list);
        }
        cardsTextures.Add("back", new List<Texture>() { cardsSprites[cardsSprites.Count - 1].texture });
    }

    private void GenerateCards()
    {
        for (int i = 0; i < numberOfSequences; i++)
        {
            var sequence = GenerateSequence(Random.Range(2, 8));
            bankTopCard = new Card(bankTopCard, sequence[0]);
            bankTopCard.FrontCard = bankTopCard;

            for (int j = 1; j < sequence.Length; j++)
            {
                int batchIndex = Random.Range(0, batchesTopCards.Count);
                batchesTopCards[batchIndex] = new Card(batchesTopCards[batchIndex], sequence[j]);
                batchesTopCards[batchIndex].BackCard.FrontCard = batchesTopCards[batchIndex];
            }
        }
    }

    private string[] GenerateSequence(int length)
    {
        string[] sequence = new string[length];
        int firstCardIndex = Random.Range(0, cardsNames.Length);

        int changeDirectionIndex = -1;


        bool isGoingUp = true;

        if (Random.Range(0, 100) < 35)
        {
            isGoingUp = false;
        }

        if (Random.Range(0, 100) < 15)
        {
            changeDirectionIndex = Random.Range(1, cardsNames.Length);
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
                if (firstCardIndex == cardsNames.Length)
                {
                    firstCardIndex = 0;
                }
            }
            else
            {
                firstCardIndex--;
                if (firstCardIndex == -1)
                {
                    firstCardIndex = cardsNames.Length - 1;
                }
            }
        }
        return sequence;
    }
}
