using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseSceneGameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        audioSource.Play();     //‘I‘ð‰æ–Ê‚ÌBGM‚ð—¬‚·
    }
}
