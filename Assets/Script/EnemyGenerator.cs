using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    public Transform[] enemyTran;    //�G�𐶐�����ꏊ

    [SerializeField]
    private int radius;    //���a[radius]�̉~�̒����烉��������

    [SerializeField]
    private GameManager gameManager;     //gameManager�ƘA�g����(�������̃X�N���v�g�ł͐����̏��������邾���B���ۂɍ��̂�gameManager�X�N���v�g)

    private int currentWaveIndex;    //���݂�Wave���扽Wave��������

    /// <summary>
    /// �G�𐶐����鏈���̏���������
    /// </summary>
    public void PrepareEnemyGenerator()
    {
        //�܂��n�܂����Ƃ���gameManager��Wave��wave1�ɂ���B
        gameManager.currentWave = GameManager.Wave.wave1;
    }

    /// <summary>
    /// �G��|���I������炱�̃��\�b�h�����čX�V����B
    /// </summary>
    public void UpdateNextWave()
    {
        //���݂�wave��1���₷�iwave1����wave2�ցAwave2����wave3�ցj
        if(gameManager.currentWave == GameManager.Wave.wave1)
        {
            gameManager.currentWave = GameManager.Wave.wave2;
        }

        if(gameManager.currentWave == GameManager.Wave.wave2)
        {
            gameManager.currentWave = GameManager.Wave.wave3;
        }

        MatchWave();   //currentWave�𔽉f������B
    }

    /// <summary>
    /// ���݂�Wave����DataBasemanager�̃C���f�b�N�X�ԍ��ƈ�v�����邽�߂̃��\�b�h�B
    /// </summary>
    private void MatchWave()
    {
        switch (gameManager.currentWave)    //�������݂�Wave��
        {
            case (GameManager.Wave.wave1):�@�@//Wave1�Ȃ�

                currentWaveIndex = 0;    //���̏����ɂ����StageData���̃C���f�b�N�X�ԍ��Ƒ扽Wave������v�����Ă���

                Debug.Log("Wave1�ł�");
                break;

            case (GameManager.Wave.wave2):�@�@//Wave2�Ȃ�

                currentWaveIndex = 1;    //���̏����ɂ����StageData���̃C���f�b�N�X�ԍ��Ƒ扽Wave������v�����Ă���

                Debug.Log("Wave2�ł�");
                break;

            case (GameManager.Wave.wave3):�@�@//Wave3�Ȃ�

                currentWaveIndex = 2;    //���̏����ɂ����StageData���̃C���f�b�N�X�ԍ��Ƒ扽Wave������v�����Ă���

                Debug.Log("Wave3�ł�");
                break;
        }
    }

    /// <summary>
    /// �G�𐶐����鏈���B
    /// </summary>
    public void EnemyGenerate()
    {     
        if (gameManager.currentWave == GameManager.Wave.wave1)
        {
            currentWaveIndex = 0;
        }
        if (gameManager.currentWave == GameManager.Wave.wave2)
        {
            currentWaveIndex = 1;
        }
        if (gameManager.currentWave == GameManager.Wave.wave3)
        {
            currentWaveIndex = 2;
        }

        //�����̐���
        for (int i = 0; i < DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].DinosaurCount; i++)     //��Stage�̃C���f�b�N�X�ԍ��͂P�ɂ��Ă���B��ł������ς���B
        {
            //------------------------------------�ʒu�̎w��------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.x,      //DataBaseManager�̒���StageDataSO�Ŏw�肵���ꏊ�̂��ꂼ��̍��W��enemyGenerateTran�̂��̂�������B

                    enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.y,

                    enemyGenerateTran.y + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.z

                    );

            //-----------------------------------��������-------------------------------------------------------------//

            GameObject enemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[0].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);
        }

        //���{�b�g�̐���
        for (int j = 0;j < DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].RobotCount; j++)
        {
            //------------------------------------�ʒu�̎w��------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.x,      //DataBaseManager�̒���StageDataSO�Ŏw�肵���ꏊ�̂��ꂼ��̍��W��enemyGenerateTran�̂��̂�������B

                    enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.y,

                    enemyGenerateTran.y + enemyTran[DataBaseManager.instance.stageDataSO.stageDataList[currentWaveIndex].GenerateEnemyTranIndex].position.z

                    );

            //------------------------------------��������------------------------------------------------------//

            GameObject robotEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[1].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);
        }
    }
}
