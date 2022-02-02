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

    public int generatedEnemyCount;    //���������G�̐�

    public int knockDownEnemyCount;    //�|�����G�̐��i�ewave���ƂɃ��Z�b�g����j

    private int currentWaveDinosaurCount;�@�@//���̃E�F�[�u�ō�鋰���̐�

    private int currentWaveInsectCount;     //���̃E�F�[�u�Ő������鍩���̐�

    private int currentWaveBossDinosaurCount;�@�@//���̃E�F�[�u�ō�鋰���̐�

    private int currentWaveBossInsectCount;     //���̃E�F�[�u�Ő������鍩���̐�

    private int currentWaveGenerateTran;     //���̃E�F�[�u�Ő�������ꏊ

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
        }else if(gameManager.currentWave == GameManager.Wave.wave2)
        {
            gameManager.currentWave = GameManager.Wave.wave3;
        }

        MatchWave();   //currentWave�𔽉f������B
    }

    /// <summary>
    /// ���݂�Wave����DataBasemanager�̃C���f�b�N�X�ԍ��ƈ�v�����邽�߂̃��\�b�h�B
    /// </summary>
    public void MatchWave()
    {
        switch (gameManager.currentWave)    //�������݂�Wave��
        {
            case (GameManager.Wave.wave1):�@�@//Wave1�Ȃ�

                //���݂̃E�F�[�u�̂��ꂼ��̓G�̐��A��������ꏊ��currentWaveDinosaurCount�ȂǂɑΉ�������B
                currentWaveDinosaurCount = GameData.instance.currentStageData.wave1DinosaurCount;
                currentWaveInsectCount = GameData.instance.currentStageData.wave1InsectCount;
                currentWaveBossDinosaurCount = GameData.instance.currentStageData.wave1BossDinosaur;
                currentWaveBossInsectCount = GameData.instance.currentStageData.wave1BossInsect;
                currentWaveGenerateTran = GameData.instance.currentStageData.wave1GenerateEnemyTranIndex;

                Debug.Log("Wave1�ł�");
                break;

            case (GameManager.Wave.wave2):�@�@//Wave2�Ȃ�

                //���݂̃E�F�[�u�̂��ꂼ��̓G�̐��A��������ꏊ��currentWaveDinosaurCount�ȂǂɑΉ�������B
                currentWaveDinosaurCount = GameData.instance.currentStageData.wave2DinosaurCount;
                currentWaveInsectCount = GameData.instance.currentStageData.wave2InsectCount;
                currentWaveBossDinosaurCount = GameData.instance.currentStageData.wave2BossDinosaur;
                currentWaveBossInsectCount = GameData.instance.currentStageData.wave2BossInsect;
                currentWaveGenerateTran = GameData.instance.currentStageData.wave2GenerateEnemyTranIndex;

                Debug.Log("Wave2�ł�");
                break;

            case (GameManager.Wave.wave3):�@�@//Wave3�Ȃ�

                //���݂̃E�F�[�u�̂��ꂼ��̓G�̐��A��������ꏊ��currentWaveDinosaurCount�ȂǂɑΉ�������B
                currentWaveDinosaurCount = GameData.instance.currentStageData.wave3DinosaurCount;
                currentWaveInsectCount = GameData.instance.currentStageData.wave3InsectCount;
                currentWaveBossDinosaurCount = GameData.instance.currentStageData.wave3BossDinosaur;
                currentWaveBossInsectCount = GameData.instance.currentStageData.wave3BossInsect;
                currentWaveGenerateTran = GameData.instance.currentStageData.wave3GenerateEnemyTranIndex;

                Debug.Log("Wave3�ł�");
                break;
        }
    }

    /// <summary>
    /// �G�𐶐����鏈���B
    /// </summary>
    public void EnemyGenerate()
    {     
        //�����̐���
        for (int i = 0; i < currentWaveDinosaurCount; i++)     
        {
            //------------------------------------�ʒu�̎w��------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManager�̒���StageDataSO�Ŏw�肵���ꏊ�̂��ꂼ��̍��W��enemyGenerateTran�̂��̂�������B

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //-----------------------------------��������-------------------------------------------------------------//

            GameObject enemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[0].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            enemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //���̏����ō����EnemyController����EnemyGenerator������B

            generatedEnemyCount++;     //���������G�̐��𐔂���
        }


        //���̐���
        for (int j = 0;j < currentWaveInsectCount; j++)
        {
            //------------------------------------�ʒu�̎w��------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManager�̒���StageDataSO�Ŏw�肵���ꏊ�̂��ꂼ��̍��W��enemyGenerateTran�̂��̂�������B

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //------------------------------------��������------------------------------------------------------//

            GameObject insectEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[1].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            insectEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //���̏����ō����EnemyController����EnemyGenerator������B

            generatedEnemyCount++;     //���������G�̐��𐔂���
        }


        //�{�X�����̐���
        for (int j = 0; j < currentWaveBossDinosaurCount; j++)
        {
            //------------------------------------�ʒu�̎w��------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManager�̒���StageDataSO�Ŏw�肵���ꏊ�̂��ꂼ��̍��W��enemyGenerateTran�̂��̂�������B

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //------------------------------------��������------------------------------------------------------//

            GameObject bossDinosaurEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[2].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            bossDinosaurEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //���̏����ō����EnemyController����EnemyGenerator������B

            generatedEnemyCount++;     //���������G�̐��𐔂���
        }


        //�{�X���̐���
        for (int j = 0; j < currentWaveBossInsectCount; j++)
        {
            //------------------------------------�ʒu�̎w��------------------------------------------------------//

            Vector2 enemyGenerateTran = radius * Random.insideUnitCircle;      //���a[radius]�̉~�̒����烉���_���Ŏ擾�ł����B(x,y)��x���W��Vector3�ł�x���W�ɁAy���W��Vector3��z���W�ɉ�����

            Vector3 trueEnemyGenerateTran = new Vector3(

                    enemyGenerateTran.x + enemyTran[currentWaveGenerateTran].position.x,      //DataBaseManager�̒���StageDataSO�Ŏw�肵���ꏊ�̂��ꂼ��̍��W��enemyGenerateTran�̂��̂�������B

                    enemyTran[currentWaveGenerateTran].position.y,

                    enemyGenerateTran.y + enemyTran[currentWaveGenerateTran].position.z

                    );

            //------------------------------------��������------------------------------------------------------//

            GameObject bossInsectEnemy = Instantiate(DataBaseManager.instance.enemyDataSO.enemyDataList[3].enemyPrefab, trueEnemyGenerateTran, Quaternion.identity);

            bossInsectEnemy.GetComponent<EnemyController>().GetEnemyGenerator(this);    //���̏����ō����EnemyController����EnemyGenerator������B

            generatedEnemyCount++;     //���������G�̐��𐔂���
        }
    }

    /// <summary>
    /// GameManager��CountUpKnockOutEnemyCount���\�b�h��EnemyController������o����悤�ɂ���
    /// </summary>
    public void SendCountUpKnockOutEnemyCount()
    {
        gameManager.CountUpKnockOutEnemyCount();
    }
}
