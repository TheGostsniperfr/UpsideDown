using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




[System.Serializable]
public class PlayerData
{
    [SerializeField] JSONSaving jSONSaving;

    [Header("KeyBinding settings")]
    public char useKey = 'f';
    public char switchGravityKey = 'e';
    

}


