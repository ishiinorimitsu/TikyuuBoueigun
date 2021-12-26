using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour   //���ۂ̊J������ł́AGamaManagr�ɂ���Start�N���X�����C���[�W
{
    [SerializeField]
    private WeaponGenerator weaponGenerator;

    [SerializeField]
    private CharaController charaController;   //�X�^�[�g���\�b�h���ׂĂ������ɏ������߂Ɏ����Ă���

    public enum Wave   //�Q�[���͑�̂̃X�e�[�W��wave3�܂łɂ���
    {
        wave1,     
        wave2,
        wave3
    }

    public Wave currentWave;     //���݂�Wave������B

    void Start()
    {
        //weaponGenerator.AddWeaponData();
        charaController.GameStart();
    }
}
