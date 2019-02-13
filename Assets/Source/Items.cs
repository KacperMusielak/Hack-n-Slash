using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public List<Armor> armorInspector;
    public static List<Armor> armor;

    void Start()
    {
        armor = armorInspector;
    }
}
