using System;
using System.Collections.Generic;
using UnityEngine;

public enum DropType
{
    Empty,
    Money,
    Fire
}

[CreateAssetMenu(fileName = "DropTabe", menuName = "Drop")]
public class DropTable_SO : ScriptableObject
{
    [Serializable]
    public class Drop
    {
        public DropType drop;
        public int probability;
    }

    public List<Drop> table;
}
