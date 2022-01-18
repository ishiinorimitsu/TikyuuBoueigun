using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour     //‚±‚±‚Ì’†‚É“ü‚Á‚Ä‚«‚½“G‚ğ’e‚Éî•ñ‚Æ‚µ‚Ä—^‚¦‚é
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
    /// ”ÍˆÍ“à‚É“ü‚Á‚Ä‚«‚½“G‚ğenemy‚Ì’†‚É“ü‚ê‚éB
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
