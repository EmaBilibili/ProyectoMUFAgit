using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="Items/Player Data",order = 1)]

public class PlayerData : ScriptableObject
{
    [SerializeField] public int lifes;
}
