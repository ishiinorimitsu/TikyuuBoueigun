using UnityEngine;

[System.Serializable]
public class WeaponData 
{
    public string weaponName;   //����̖��O
    public int weaponNo;   //����̔ԍ�
    public Sprite charaSprite;   //����̌�����

    public int weaponAttackPower;   //����̈ꔭ���������̃_���[�W�i�_���[�W�j
    public int attackRange;   //����̎˒��i���j
    public int maxAttackCount;   //����Ɉ�x�̃����[�h�ŉ����܂őłĂ邩�i���j
    public BulletController bulletPrefab;   //���e
    public int fuelEnergy;  //���Ղ���G�l���M�[�i���j
    public float bulletSpeed;  //�e�̑��x

    [Multiline]
    public string discription;   //����̐���
}
