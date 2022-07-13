using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Card FrontCard { get; set; }

    public Card BackCard { get;private set; }
    public readonly string cardName;

    public Card(Card backCard, string name)
    {
        BackCard = backCard;
        FrontCard = null;
        this.cardName = name;
    }
}
