using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour   //���ۂ̊J������ł́AGamaManagr�ɂ���Start�N���X�����C���[�W
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;

    [SerializeField]
    private CharaController charaController;   //�X�^�[�g���\�b�h���ׂĂ������ɏ������߂Ɏ����Ă���

    [SerializeField]
    private UIManager uiManager;

    public AudioSource audioSource;      //�I�[�f�B�I�\�[�X�iBGM�j�p������B

    public enum Wave   //�Q�[���͑�̂̃X�e�[�W��wave3�܂łɂ���
    {
        wave1,     
        wave2,
        wave3
    }

    public Wave currentWave;     //���݂�Wave������B

    public enum GameState
    {
        prepare,    //�Q�[�����J�n����O�̏��
        play,�@�@�@//�Q�[���v���C�ł�����
        end     //�Q�[���N���A����Q�[���I�[�o�[���̑���ł��Ȃ����
    }

    public GameState currentGameState;    //���݂�GameState



    void Start()
    {
        SetGameState(GameState.prepare);     //�͂��߂�GameState��prepare�ɕς���B

        //weaponGenerator.AddWeaponData();
        charaController.GameStart();      //charaController����start���\�b�h�����s����B

        enemyGenerator.PrepareEnemyGenerator();      //���̏����ŁA�܂����݂�Wave��1�ɂ���B

        enemyGenerator.MatchWave();    //�ŏ��͂����Ō��݂�Wave��EnemyGenerator����currentWaveIndex����v������B

        enemyGenerator.EnemyGenerate();     //Wave1��Start���\�b�h�ɏ����Ă����B

        uiManager.returnHome.gameObject.SetActive(false);    //�����؂�Ȃ��ƁA�u�z�[���ɖ߂�v�{�^���͉����Ă��܂��B
        uiManager.gameClearSet.alpha = 0;    //�Q�[���N���A����UI���B���Ă����B

        uiManager.clearWindow.alpha = 0;    //�Q�[���N���A����UI���B���Ă����B

        uiManager.taikyaku.gameObject.SetActive(false);    //�����؂�Ȃ��ƁA�ދp��Ē���̃{�^���͉����Ă��܂��B

        uiManager.saityousen.gameObject.SetActive(false);    //�����؂�Ȃ��ƁA�ދp��Ē���̃{�^���͉����Ă��܂��B

        uiManager.gameOverSet.alpha = 0;    //�Q�[���I�[�o�[����UI���B���Ă����B�i���X�ɏo���Ƃ������o�̂��߁A���������Ă����j

        uiManager.gameEndBackGround.alpha = 0;   //�Q�[�����I������Ƃ��ɔw�i���ڂ������́B

        uiManager.gameOverWindow.alpha = 0;

        uiManager.returnHome.onClick.AddListener(ClickReturnHomeButton);    //�z�[���ɖ߂�{�^�����������Ƃ��̏�����ݒ肵�Ă���

        uiManager.saityousen.onClick.AddListener(ClickSaityousenButton);    //�Ē���{�^�����������Ƃ��̏�����ݒ肵�Ă���

        uiManager.taikyaku.onClick.AddListener(ClickTaikyakuButton);    //�ދp�{�^�����������Ƃ��̏�����ݒ肵�Ă���

        //�g�[�^���̋����̐����J�E���g����
        DataBaseManager.instance.stageDataSO.StageDataList[0].totalDinosaurCount = DataBaseManager.instance.stageDataSO.StageDataList[0].wave1DinosaurCount

                                                                                 + DataBaseManager.instance.stageDataSO.StageDataList[0].wave2DinosaurCount

                                                                                 + DataBaseManager.instance.stageDataSO.StageDataList[0].wave3DinosaurCount;

        //�g�[�^���̍����̐����J�E���g����
        DataBaseManager.instance.stageDataSO.StageDataList[0].totalInsectCount = DataBaseManager.instance.stageDataSO.StageDataList[0].wave1InsectCount

                                                                                + DataBaseManager.instance.stageDataSO.StageDataList[0].wave2InsectCount

                                                                                + DataBaseManager.instance.stageDataSO.StageDataList[0].wave3InsectCount;

        SetGameState(GameState.play);     //�v���C�ł����Ԃɂ���B

        if(currentGameState == GameState.play)
        {
            audioSource.Play();
        }
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
                uiManager.GameClear();    //�Q�[���N���A�̃��\�b�h�����s����
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

    private void ClickReturnHomeButton()
    {
        SceneManager.LoadScene("ChooseWeapon");
    }

    /// <summary>
    /// GameState�������̒��̂��̂ɕύX����
    /// </summary>
    /// <param name="nextGameState"></param>
    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;    
    }

    private void ClickSaityousenButton()
    {
        //GameScene��ǂݍ���
        SceneManager.LoadScene("GameScene");
    }

    private void ClickTaikyakuButton()
    {
        //ChooseScene��ǂݍ���
        SceneManager.LoadScene("ChooseWeapon");
    }
}
