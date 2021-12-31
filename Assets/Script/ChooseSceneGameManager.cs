using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSceneGameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        audioSource.Play();     //選択画面のBGMを流す
    }
}
