using UnityEngine;

[System.Serializable]
public class WeaponData 
{
    public string weaponName;   //����̖��O
    public int weaponNo;   //����̔ԍ�
    public Sprite weaponSprite;   //����̌�����

    public int weaponAttackPower;   //����̈ꔭ���������̃_���[�W�i�_���[�W�j
    public int attackRange;   //����̎˒��i���j
    public int maxBullet;   //����Ɉ�x�̃����[�h�ŉ����܂őłĂ邩�i���j
    public BulletController bulletPrefab;   //���e
    public int reloadEnergy;  //�����[�h����Ƃ��ɏ��Ղ���G�l���M�[�i���j
    public int reloadTime;  //�����[�h�ɂ����鎞�ԁi�b�j
    public float bulletSpeed;  //�e�̑��x
    public bool rapidFire;    //�A�˂ł��邩�ۂ�
    public int rapidFireTimer;   //�A�˂ł��镐�킾�����ꍇ�A�ǂꂭ�炢�̊Ԋu�ŘA�˂ł���悤�ɂ��邩

    [Multiline]
    public string discription;   //����̐���
}
