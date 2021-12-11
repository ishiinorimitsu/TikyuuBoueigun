using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    [SerializeField]
    private bool isSearch;  //今策敵中かどうか

    [SerializeField]
    private EnemyController enemyController;   //敵を入れる

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
