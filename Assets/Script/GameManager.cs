using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private WeaponGenerator weaponGenerator;

    void Start()
    {
        weaponGenerator.AddWeaponData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
