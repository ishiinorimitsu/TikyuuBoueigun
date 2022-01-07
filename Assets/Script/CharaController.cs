using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //------------------------�A�j���[�V����-------------------------------------//

    [SerializeField]
    private Animator anim;

    //public WeaponData GameData.instance.equipWeaponData;

    //-------------------------�ړ��֌W---------------------------------------//

    [SerializeField]
    private float jumpForce;  //�W�����v��

    [SerializeField]
    private bool isGround;     //���n�ʂƐڂ��Ă��邩

    [SerializeField]
    private float moveSpeed;  //���������ւ̈ړ���

    private float x;  //x�������ւ̈ړ���

    private float z;  //z�������ւ̈ړ���

    private Rigidbody rb;  //Rigidbody�ɗ͂�������̂ŁA���������B

    //[SerializeField]
    //private GameObject flyFire;    //��ԂƂ��Ƀ��P�b�g�̂��K����o�鉊

    //[SerializeField]
    //private Transform fireGenerateTran;     //�����o���ꏊ

    //-------------------------�e�̔��ˊ֌W---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //�e�̃v���t�@�u������B

    public Transform bulletStartPosition;  //�e�𐶐�����n�_�i�e���̏ꏊ�j

    [SerializeField]
    private float bulletPower;   //�ǂ̂��炢�̃p���[�Œe�����ł�����

    //[SerializeField]
    //private List<WeaponData> weaponDataList = new List<WeaponData>();

    [SerializeField]
    private List<int> currentBulletList = new List<int>();�@�@//����̍��̒e���̃��X�g

    //[SerializeField]
    //private int currentBullet;    //���̒e��������B

    private int minBullet = 0;   //�ŏ�����

    private bool isReload;  //�����[�h����

    //---------------------------�G�l���M�[�֌W------------------------------------//

    public float maxEnergy;     //�ő�G�l���M�[��

    public float minEnergy;     //�ŏ��G�l���M�[�ʁi�̂��͈͎̔w��Ŏg���j

    public float currentEnergy;       //���݂̃G�l���M�[��

    public float jumpEnergy; �@�@//���W�����v���邲�Ƃɏ����G�l���M�[�A1����Ƃ��ā��Ōv�Z��������A�����_�̂悤��100�����āA10������݂����Ȃق������₷��

    public float attackEnergy;�@�@//���U�����邲�ƂɎg���G�l���M�[

    public float cureEnergy;    //�n�ʂɂ���Ԃɉ񕜂���G�l���M�[

    public UIManager UIManager;   //UIManager�Ƀf�[�^�𑗂��悤�ɂ���B


    //-------------------------------HP�֌W-----------------------------------------//

    [SerializeField]
    private int maxHp;    //�ő�HP

    private int minHp = 0;    //�ŏ�HP

    [SerializeField]
    public int currentHp;    //���݂�HP

    //---------------------------------�Q�[�����--------------------------------------//
    [SerializeField]
    private EnemyGenerator enemyGenerator;


    //---------------------------------�T�E���h�G�t�F�N�g�̓��e-----------------------------------//

    [SerializeField]
    private AudioSource audioSource;    //�I�[�f�B�I�\�[�X������B

    [SerializeField]
    private AudioClip reloadGunSE;    //�e�������[�h���鉹

    [SerializeField]
    private AudioClip shotGunSE;     //�e������




    public void GameStart()
    {
        rb = GetComponent<Rigidbody>();   //Rigidbody�������Ă���

        currentEnergy = maxEnergy;    //�Q�[�����J�n�����Ƃ��ɍő�G�l���M�[�ʂɂ��Ă����B

        currentHp = maxHp;   //�Q�[�����J�n�����Ƃ��ɍő�̗͂ɂ��Ă����B

        UIManager.SetEnergySliderValue(maxEnergy);   //�G�l���M�[�Ɋւ�����̂̃Z�b�g

        for (int i = 0; i < GameData.instance.chooseWeaponData.Count; i++)
        {
            //currentBullet�̃��X�g�Ɏ��Ă镐�핪�́ucurrentBullet�v�����
            currentBulletList.Add(GameData.instance.chooseWeaponData[i].maxBullet);
        }

        UIManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxBullet, currentBulletList[GameData.instance.currentEquipWeaponNo]);   //�ő�e�����Z�b�g�i�����́A���I�΂�Ă��镐��̍ő�e���j

        UIManager.SetSelectedWeapon();�@�@//���I�΂�Ă��镐��̖��O�Ɖ摜���Z�b�g

        UIManager.SetHpSliderValue(maxHp);    //�ő�HP���Z�b�g����

        bulletPower = GameData.instance.equipWeaponData.bulletSpeed;�@�@//�e�̑��x�͑������Ă��镐��̑��x
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp > 0)
        {
            if (gameManager.currentGameState == GameManager.GameState.play)   //GameState��play�̎�����
            {
                x = Input.GetAxis("Horizontal");   //���������̈ړ�������ꍇ�A�P����������

                z = Input.GetAxis("Vertical");  //���������̈ړ�������ꍇ�A�P����������

                if (isGround)
                {
                    if (Input.GetButtonDown("Jump") & currentEnergy >= jumpEnergy)  //�X�y�[�X�L�[���������Ƃ��Ƀ��\�b�h�����������B
                    {
                        anim.SetTrigger("Jump");

                        Jump();
                    }
                }


                //-----------------------------------------�e�𔭎˂���----------------------------------------------------//

                //���I��ł��镐��̌��݂̒e�����O���傫��������i�܂��e�������Ă�����j
                if (currentBulletList[GameData.instance.currentEquipWeaponNo] > 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        BulletController createBullet = Instantiate(bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);   //�e�e�𐶐�����

                        createBullet.Shot(this);

                        anim.SetTrigger("Attack");

                        audioSource.PlayOneShot(shotGunSE);�@�@�@//�e��������炷�B

                        currentBulletList[GameData.instance.currentEquipWeaponNo]--;    //���̋����������т�1�����炵�Ă���

                        currentBulletList[GameData.instance.currentEquipWeaponNo] = Mathf.Clamp(currentBulletList[GameData.instance.currentEquipWeaponNo], minBullet, GameData.instance.equipWeaponData.maxBullet);   //���̋����͈̔͂��w�肷��

                        UIManager.UpdateDisplayBullet(currentBulletList[GameData.instance.currentEquipWeaponNo]);   //�e���̏����𔽉f������
                    }
                }

                else
                {
                    if (isReload == false)
                    {
                        isReload = true;   //�e���Ȃ��̂ɘA�ł��ꂽ�Ƃ��p

                        //�������Ă��镐��́u�����[�h���ԁv���������ĂȂ��悤�ɂ���
                        StartCoroutine(ReloadWeapon());
                    }
                }

                //---------------------------------------�����ς���-------------------------------------------------------------//
                if (Input.GetButtonDown("ChangeWeapon"))
                {
                    GameData.instance.ChangeWeapon();  //�����Ă��镐���ς��鏈��

                    audioSource.PlayOneShot(reloadGunSE);

                    //currentBullet = currentBulletList[GameData.instance.currentEquipWeaponNo];

                    Debug.Log(GameData.instance.equipWeaponData.weaponName);

                    UIManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxBullet, currentBulletList[GameData.instance.currentEquipWeaponNo]);  //

                    UIManager.SetSelectedWeapon();   //���ݑI�΂�Ă��镐��̖��O�A�C���X�g��ς���
                }

                //if (Input.GetButtonDown("EnemyGenerate"))
                //{
                //    UIManager.GameClear();
                //}
            }
        }
    }   

    private void FixedUpdate()
    {
        if (gameManager.currentGameState == GameManager.GameState.play)
        {
            
            
             //�ړ�����
             Move();
            

            //�J�����̌�������L�����̌�����ς���B
            LookRotation();
        }
    }

    //---------------------------------�ړ��Ɋւ��鏈��----------------------------------------------------------//
    //���������ւ̈ړ�
    public void Move()
    {
        //rb.velocity = new Vector3(x * moveSpeed,rb.velocity.y, z * moveSpeed);

        if (x != 0 || z != 0)
        {
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        }
        else
        {
            anim.SetFloat("Run", 0);
            anim.SetBool("Idle", true);
        }
    }

    //�W�����v�̈ړ�
    public void Jump()
    {
        rb.AddForce(transform.up*jumpForce);   //�W�����v���鏈��

        JumpEnergyDecrease();   //�W�����v���邲�ƂɃG�l���M�[�����炷�B

        UIManager.UpdateDisplayEnergy(currentEnergy);   //�G�l���M�[�l���X�V����
    }


    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    private void LookRotation()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        //if (moveForward != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(moveForward);
        //}

        transform.rotation = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0);
    }

    //----------------------------------�G�l���M�[�Ɋւ��鏈��----------------------------------------------------------//

    private void JumpEnergyDecrease()
    {
        currentEnergy -= jumpEnergy;�@�@//���݂̃G�l���M�[�ʂ���W�����v�G�i�W�[�����炷�B
    }
    
    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            currentEnergy += cureEnergy;

            currentEnergy = Mathf.Clamp(currentEnergy, minEnergy, maxEnergy);

            UIManager.UpdateDisplayEnergy(currentEnergy);   //�G�l���M�[�l���X�V����

            isGround = true;
        }
    }

    //-----------------------------------�e�𔭎˂��鏈���Ɋւ��郁�\�b�h----------------------------------------------------//
    /// <summary>
    /// �e���Z�b�g����B�������Ă��镐�킪���u�ő�e���v��currentBulletList�ɓ����
    /// </summary>
    private void SetBullet()
    {
        for(int i = 0; i < GameData.instance.chooseWeaponData.Count; i++)  //���ݓo�^����Ă��镐��i�Q�j
        {
            currentBulletList.Add(GameData.instance.chooseWeaponData[i].maxBullet);�@�@//���ꂼ��̕���̍ő�e����currentBulletList�ɑ������
        }
    }
    
    /// <summary>
    /// �������Ă��镐��́u�����[�h���ԁv���������ĂȂ��悤�ɂ���
    /// </summary>
    private IEnumerator ReloadWeapon()
    {
        anim.SetTrigger("Reload");

        //�������Ă��镐��́u�����[�h�G�l���M�[�v�������G�l���M�[�����炷
        currentEnergy -= GameData.instance.equipWeaponData.reloadEnergy;

        UIManager.UpdateDisplayEnergy(currentEnergy);

        yield return new WaitForSeconds(GameData.instance.equipWeaponData.reloadTime);

        //�������Ă��镐��́ucurrentBullet�v���ő�ɂ���
        currentBulletList[GameData.instance.currentEquipWeaponNo] = GameData.instance.equipWeaponData.maxBullet;

        UIManager.UpdateDisplayBullet(currentBulletList[GameData.instance.currentEquipWeaponNo]);

        audioSource.PlayOneShot(reloadGunSE);

        isReload = false;
    }


    //-----------------------------------------�U�����ꂽ�Ƃ��̏���----------------------------------------------------------------//

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyBullet")
        {
            col.gameObject.TryGetComponent<EnemyBulletController>(out EnemyBulletController enemyBulletController);

            currentHp -= enemyBulletController.attackPower;�@�@//�U������邽�т�Hp�����炷

            currentHp = Mathf.Clamp(currentHp,minHp,maxHp);    //HP���}�C�i�X�ɂȂ������h��

            UIManager.UpdateDisplayHp(currentHp);    //HP�̃Q�[�W���X�V����

            if (currentHp <= 0)    //���̍U����HP��0�ɂȂ����玀�ʃA�j���[�V�����𗬂��B
            {
                anim.SetTrigger("Die");    //���ʃA�j���[�V�����𗬂�

                UIManager.GameOver();     //GameOver�̏�������������B
            }

            StartCoroutine(KillPlayer());�@�@�@//�L�����̃X�C�b�`��؂�Ȃ��Ǝ��񂾌�������̍U���ŉ��������ł��܂�����
        }
    }

    /// <summary>
    /// �R���[�`���ɂ��Ȃ��ƍŏ��̈��̎��ʃA�j���[�V�������Đ�����Ȃ�����B
    /// </summary>
    /// <returns></returns>
    private IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(1.0f);    //�P�b�҂�

        gameObject.SetActive(false);�@�@//�L�����̃X�C�b�`��؂�B
    }
}


