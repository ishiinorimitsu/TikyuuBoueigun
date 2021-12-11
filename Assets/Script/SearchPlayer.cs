using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    [SerializeField]
    private bool isSearch;  //�����G�����ǂ���

    [SerializeField]
    private EnemyController enemyController;   //�G������

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isSearch = true;
            Debug.Log("OK1");
            enemyController.player = col.gameObject;
            Debug.Log("OK2");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearch = false;

            enemyController.player = null;
        }
    }
}
