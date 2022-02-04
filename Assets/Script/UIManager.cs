using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //----------------------------------------�G�l���M�[�֌W----------------------------------------------------//

    //[SerializeField]
    //private Image energySlider;     //�G�l���M�[�Q�[�W������

    //private float maxEnergyCount;    //�ő�G�l���M�[�ʂ�����

    //-----------------------------------------�e���֌W---------------------------------------------------//

    [SerializeField]
    private Image bulletSlider;     //�c��e���Q�[�W������

    [SerializeField]
    private Text lastBullet;    //�c���Ă���e��������B

    [SerializeField]
    private Text maxBullet;    //�ő�e��������B(Text)

    private float maxBulletCount;   //�ő�e��������(float)

    [SerializeField]
    public int reloadLastBullet;   //�e�����J�E���g�A�b�v����Ƃ��p�̒e

    public int ReloadLastBullet
    {
        set
        {
            reloadLastBullet = value;

            lastBullet.text = reloadLastBullet.ToString();
        }
        get
        {
            return reloadLastBullet;
        }
    }

    //------------------------------------------����֌W--------------------------------------------------//

    [SerializeField]
    private Image selectedWeapon;  //�I�΂�Ă��镐��̉摜������

    [SerializeField]
    private Text selectedWeaponText;  //�I�΂�Ă��镐��̃e�L�X�g������

    //------------------------------------------HP�֌W--------------------------------------------------//

    [SerializeField]
    private Image HPSlider;    //HP�Q�[�W������

    [SerializeField]
    private Image reloadSlider;     //�����[�h���̃Q�[�W������

    [SerializeField]
    private Text lastHP;    //�c���Ă���HP������B

    [SerializeField]
    private Text maxHP;    //�ő�HP������B(Text)

    private float maxHPCount;    //�ő�HP������(float)

    //------------------------------------------�N���A����UI�֌W--------------------------------------------------//

    public CanvasGroup gameClearSet;     //�Q�[���N���A�������ɏo�����{��������

    public CanvasGroup clearWindow;     //�Q�[���̃��U���g��ʂ�����

    public CanvasGroup gameOverSet;     //GameOver���̕���������B

    public CanvasGroup gameEndBackGround;    //�Q�[�����I������Ƃ��̔w�i���ڂ�������

    public CanvasGroup gameOverWindow;   //�Q�[���I�[�o�[���̑ދp���Ē��킩��I�ԃE�B���h�E������B

    public Button taikyaku;    //�ދp�̃{�^���i�̂��ɂ������������Ƃ���ChooseScene��ǂݍ��ށj

    public Button saityousen;    //�Ē���̃{�^���i�̂��ɂ������������Ƃ���GameScene��ǂݍ��ށj

    public Button returnHome;    //�N���A������́u�z�[���ɖ߂�v�{�^��

    public Text defeatDinosaurCount;    //�|���������̐�   

    public Text defeatInsectCount;   //�|�������̐�


    //-------------------------------------���ʉ��̐ݒ�-------------------------------------//

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip fanfare;     //�Q�[�����N���A�����Ƃ��̃t�@���t�@�[��

    [SerializeField]
    private AudioClip clearBGM;    //�t�@���t�@�[���̌�ɗ���BGM

    [SerializeField]
    private AudioClip failBGM;     //�Q�[���I�[�o�[�̎���BGM


    //----------------------------------------�G�l���M�[�Q�[�W�̏���------------------------------------------------------//
    /// <summary>
    /// �G�l���M�[�Q�[�W�̏���
    /// </summary>
    /// <param name="maxEnergy"></param>
    //public void SetEnergySliderValue(float maxEnergy)
    //{
    //    this.maxEnergyCount = maxEnergy;

    //    energySlider.fillAmount = maxEnergy;     //�X���C�_�[�̒���maxValue��maxEnergy�ƈꏏ�ɂ���B

    //    UpdateDisplayEnergy(maxEnergy);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B
    //}

    //public void UpdateDisplayEnergy(float currentEnergy)
    //{
    //    //Debug.Log(currentEnergy);

    //    energySlider.fillAmount = currentEnergy/maxEnergyCount;  //currentEnergy�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxEnergy�ł����B�j

    //    energySlider.DOFillAmount(currentEnergy / maxEnergyCount, 1.0f);
    //}


    //------------------------------------------����̒e�����̏���---------------------------------------------------------//

    /// <summary>
    /// �X���C�_�[�̃Q�[�W�Ɋւ��鏈��
    /// </summary>
    /// <param name="maxBullet"></param>
    public void SetWeaponSliderValue(int maxBullet,int currentBullet)     //�܂�����
    {
        this.maxBulletCount = maxBullet;

        bulletSlider.fillAmount = maxBullet;     //�X���C�_�[�̒���maxValue��maxBullet�ƈꏏ�ɂ���B

        UpdateDisplayBullet(currentBullet);    //�܂��ŏ���value�̒l��maxEnergy�Ɠ����ł����B

        this.maxBullet.text = maxBullet.ToString();    //UI�̍ő�e���𑕔����Ă��镐��̍ő�e���ɂ���B

       reloadSlider.fillAmount = 0;    //�����[�h����Ƃ��i0����100��S������̂ŁA�����[�h�̍ő�͂O�ɂ���j
    }

    /// <summary>
    /// �X���C�_�[�ɕύX���������ꍇ
    /// </summary>
    /// <param name="currentBulletCount"></param>
    public void UpdateDisplayBullet(int currentBulletCount)�@�@�@//�ω����������Ƃ��̏���
    {
        bulletSlider.fillAmount�@=�@currentBulletCount/maxBulletCount;  //currentBulletCount�܂�value�𓮂����i�ŏ��̈����̒l��maxBulletCount�ł����B�j

        lastBullet.text = currentBulletCount.ToString();   //���̋����𔽉f������B�����̍X�V
    }

    /// <summary>
    /// ����������[�h����Ƃ��̃X���C�_�[�̏���
    /// </summary>
    public void ReloadWeaponSlider ()
    {
        //���܂ł̕���Q�[�W�������Ȃ�����B
        bulletSlider.gameObject.SetActive(false);

        //�Q�[�W�����X�ɉ񕜂���
        reloadSlider.DOFillAmount(maxBulletCount/maxBulletCount,GameData.instance.equipWeaponData.reloadTime);

        //����̎c�苅�������X�ɕω�������B�i�܂��e�L�X�g�ɔ��f�����Ă��Ȃ��j
        DOTween.To(()=>ReloadLastBullet,num=>ReloadLastBullet=num,GameData.instance.equipWeaponData.maxBullet,GameData.instance.equipWeaponData.reloadTime);

        //����Q�[�W���Ăь�����悤�ɂ���
        bulletSlider.gameObject.SetActive(true);

        //�����[�h�Q�[�W��FillAmount���A�����[�h�c�e�����O�ɖ߂�
        StartCoroutine(waitReloadTime());
    }

    private IEnumerator waitReloadTime()
    {
        yield return new WaitForSeconds(GameData.instance.equipWeaponData.reloadTime);

        reloadSlider.fillAmount = 0;

        reloadLastBullet = 0;
    }
    //------------------------------------------���̕���̖��O�Ɖ摜�Ɋւ��鏈��-------------------------------------------------//
    public void SetSelectedWeapon()
    {
        selectedWeapon.sprite = GameData.instance.equipWeaponData.weaponSprite;    //���ݑI�΂�Ă��镐��̉摜������

        selectedWeaponText.text = GameData.instance.equipWeaponData.weaponName;    //���ݑI�΂�Ă��镐��̖��O������
    }

    //------------------------------------------------------HP�Q�[�W�̏���--------------------------------------------------------//
    public void SetHpSliderValue(int maxHp)
    {
        this.maxHPCount = maxHp;

        HPSlider.fillAmount = maxHp;     //�X���C�_�[�̒���maxValue��maxHp�ƈꏏ�ɂ���B

        UpdateDisplayHp(maxHp);    //�܂��ŏ���value�̒l��maxHp�Ɠ����ł����B

        maxHP.text = maxHp.ToString();    //UI�̍ő�HP��\������B
    }

    public void UpdateDisplayHp(int currentHp)
    {
        HPSlider.fillAmount = currentHp/maxHPCount; //currentHp�܂�1.0�b�����ē������i�ŏ��̈����̒l��maxHp�ł����B�j

        lastHP.text = currentHp.ToString();   //����HP�𔽉f������B�����̍X�V
    }

    //-----------------------------------------------------�Q�[�����I������Ƃ��̏���---------------------------------------------//

    
    /// <summary>
    /// �Q�[���N���A�̏����𖞂������Ƃ���GameClear�̉摜���t�@���t�@�[���ƂƂ��ɏo���B
    /// </summary>
    public void GameClear()
    {
        //�w�i��܂点��
        gameEndBackGround.DOFade(1.0f, 1.0f);

        gameManager.SetGameState(GameManager.GameState.end);   //����ł��Ȃ��悤�ɂ���

        //������Ă���Ȃ��~�߂�
        gameManager.audioSource.Stop();

        //�t�@���t�@�[���𗬂�
        audioSource.PlayOneShot(fanfare);

        //GameClear�̃��{�����o��
        gameClearSet.DOFade(endValue: 1.0f, duration: 2.0f);     //2�b�Ԃ����ă��{����������悤�ɂ���

        //�T�b���炢�҂�
        StartCoroutine(WaitClearTime());
    }

    private IEnumerator WaitClearTime()
    {
        yield return new WaitForSeconds(5.0f);

        //���U���g��ʂ��o��
        ShowClearResult();
    }

    private void ShowClearResult()
    {
        //�|�����G�̐��𐔂���
        CountDefeatEnemy();

        //returnHome�̃X�C�b�`������B
        returnHome.gameObject.SetActive(true);
        
        //ClearResult���o��
        clearWindow.DOFade(1.0f,1.0f);

        audioSource.PlayOneShot(clearBGM);
    }

    public void GameOver()
    {
        //�w�i��܂点��
        gameEndBackGround.DOFade(1.0f,1.0f);

        gameManager.SetGameState(GameManager.GameState.end);   //����ł��Ȃ��悤�ɂ���

        //������Ă���Ȃ��~�߂�
        gameManager.audioSource.Stop();

        //���s��BGM�𗬂�
        audioSource.PlayOneShot(failBGM);

        //�؂��Ă����ދp�A�Ē���̃X�C�b�`������
        taikyaku.gameObject.SetActive(true);
        saityousen.gameObject.SetActive(true);

        gameOverSet.DOFade(endValue: 1.0f, duration: 2.0f);     //2�b�Ԃ�����GameOver��������悤�ɂ���

        StartCoroutine(WaitGameOverTime());
    }

    private IEnumerator WaitGameOverTime()
    {
        yield return new WaitForSeconds(5.0f);

        //�uGameOver�v�̉�ʂ̃X�C�b�`��؂�i�\�������Ȃ��j
        gameOverSet.gameObject.SetActive(false);

        //���U���g��ʂ��o��
        ShowGameOverWindow();
    }

    public void ShowGameOverWindow()
    {
        gameOverWindow.DOFade(1.0f, 1.0f);
    }

    private void CountDefeatEnemy()
    {
        //���v�Z�������̂��e�L�X�g�ɓ����B
        defeatDinosaurCount.text = GameData.instance.currentStageData.totalDinosaurCount.ToString();

        defeatInsectCount.text = GameData.instance.currentStageData.totalInsectCount.ToString();
    }
}
