using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour     //�����̒��ɓ����Ă����G��e�ɏ��Ƃ��ė^����
{
    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private BulletController bullet;

    private void Start()
    {
        enemy = null;
    }

    /// <summary>
    /// �͈͓��ɓ����Ă����G��enemy�̒��ɓ����B
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            TryGetComponent<EnemyController>(out enemy);
        }

        //bullet.SendSearchEnemy(enemy);
    }
}
