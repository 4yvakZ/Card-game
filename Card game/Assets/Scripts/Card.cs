using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Card FrontCard { get; set; }

    public Card BackCard { get;private set; }
    public readonly string cardName;
    public readonly int index;

    public Card(Card backCard, string name, int index)
    {
        BackCard = backCard;
        FrontCard = null;
        cardName = name;
        this.index = index;
    }
}
