using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour   //���ۂ̊J������ł́AGamaManagr�ɂ���Start�N���X�����C���[�W
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

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
        charaController.GameStart();      //charaController����start���\�b�h�����s����B

        enemyGenerator.PrepareEnemyGenerator();      //���̏����ŁA�܂����݂�Wave��1�ɂ���B

        enemyGenerator.MatchWave();    //�ŏ��͂����Ō��݂�Wave��EnemyGenerator����currentWaveIndex����v������B

        enemyGenerator.EnemyGenerate();     //Wave1��Start���\�b�h�ɏ����Ă����B
    }


    /// <summary>
    /// �|�����G�̐����J�E���g���AWave���N���A�������m�F����B
    /// </summary>
    public void CountUpKnockOutEnemyCount()
    {
        enemyGenerator.knockDownEnemyCount++;    //�|�����G�̐����{�P

        if (enemyGenerator.knockDownEnemyCount >= enemyGenerator.generatedEnemyCount)    //���������G�̐��𒴂�����(�S���|������)
        {
            Debug.Log("Wave�N���A");

            if(currentWave == Wave.wave3)     //�������݂�Wave���R�Ȃ�΁A�Q�[���N���A�Ƃ�B
            {
                GameClear();      //�Q�[���N���A�̃��\�b�h�����s����
            }
            else�@�@//
            {
                StartCoroutine(WaitWaveInterval(5.0f));     //�܂��T�b�̃C���^�[�o���A���ꂩ�玟��Wave�̃L�������������
            }
        }
    }


    private IEnumerator WaitWaveInterval(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //���݂�Wave��1��������2�ցA2��������3�ɕς���B
        enemyGenerator.UpdateNextWave();

        Debug.Log("Wave���X�V���܂����B");

        //knockDowaEnemyCount��generatedEnemyCount�����O�ɖ߂��B
        enemyGenerator.knockDownEnemyCount = 0;

        enemyGenerator.generatedEnemyCount = 0;

        Debug.Log("����������|�����G�̐�����񃊃Z�b�g");

        //���݂�Wave�����ƂɃf�[�^�x�[�X����w�肳�ꂽ���̓G�𐶐�����B

        enemyGenerator.EnemyGenerate();
    }


    private void GameClear()
    {
        Debug.Log("�Q�[���N���A");
    }
}
