using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrocerySituation
{
    Right, WrongSelection, WrongRepetition
}

public class GroceryStatus
{
    public int grocerySpriteId;
    public GrocerySituation Situation;

    public GroceryStatus(int grocerySpriteId, GrocerySituation situation)
    {
        this.grocerySpriteId = grocerySpriteId;
        this.Situation = situation;
    }
}
