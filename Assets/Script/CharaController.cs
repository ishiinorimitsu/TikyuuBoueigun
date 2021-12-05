using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    //------------------------�A�j���[�V����-------------------------------------//

    [SerializeField]
    private Animator anim;

    //-------------------------�ړ��֌W---------------------------------------//

    [SerializeField]
    private float jumpForce;  //�W�����v��

    [SerializeField]
    private float moveSpeed;  //���������ւ̈ړ���

    private float x;  //x�������ւ̈ړ���

    private float z;  //z�������ւ̈ړ���

    private Rigidbody rb;  //Rigidbody�ɗ͂�������̂ŁA���������B

    //-------------------------�e�̔��ˊ֌W---------------------------------------//

    [SerializeField]
    private BulletController bulletPrefab;  //�e�̃v���t�@�u������B

    public Transform bulletStartPosition;  //�e�𐶐�����n�_�i�e���̏ꏊ�j

    [SerializeField]
    private float bulletPower;   //�ǂ̂��炢�̃p���[�Œe�����ł�����

    [SerializeField]
    private List<WeaponData> weaponDataList = new List<WeaponData>();

    [SerializeField]
    private float currentBullet;    //���̒e��������B

    [SerializeField]
    private float maxBullet;    //�ő�e��������B

    [SerializeField]

    private float minBullet = 0;   //�ŏ�����

    private bool isReload;  //�����[�h����

    //---------------------------�G�l���M�[�֌W------------------------------------//

    public float maxEnergy;     //�ő�G�l���M�[��

    public float minEnergy;     //�ŏ��G�l���M�[�ʁi�̂��͈͎̔w��Ŏg���j

    public float currentEnergy;       //���݂̃G�l���M�[��

    public float jumpEnergy; �@�@//���W�����v���邲�Ƃɏ����G�l���M�[�A1����Ƃ��ā��Ōv�Z��������A�����_�̂悤��100�����āA10������݂����Ȃق������₷��

    public float attackEnergy;�@�@//���U�����邲�ƂɎg���G�l���M�[

    public float cureEnergy;    //�n�ʂɂ���Ԃɉ񕜂���G�l���M�[

    public UIManager UIManager;   //UIManager�Ƀf�[�^�𑗂��悤�ɂ���B

    public void GameStart()
    {
        rb = GetComponent<Rigidbody>();�@�@�@//Rigidbody�������Ă���

        currentEnergy = maxEnergy;    //�Q�[�����J�n�����Ƃ��ɍő�G�l���M�[�ʂɂ��Ă����B

        UIManager.SetEnergySliderValue(maxEnergy);   //�G�l���M�[�Ɋւ�����̂̃Z�b�g

        UIManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxAttackCount);   //�ő�e�����Z�b�g

        UIManager.SetSelectedWeapon();

        maxBullet = GameData.instance.equipWeaponData.maxAttackCount;  //�ő勅���𑕔����Ă��镐�킩�瓾��

        currentBullet = maxBullet;   //�Q�[�����J�n�����Ƃ��ɍő�e���ɂ��Ă����B

        bulletPower = GameData.instance.equipWeaponData.bulletSpeed;�@�@//�e�̑��x�͑������Ă��镐��̑��x
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");   //���������̈ړ�������ꍇ�A�P����������

        z = Input.GetAxis("Vertical");  //���������̈ړ�������ꍇ�A�P����������

        if (Input.GetButtonDown("Jump")�@& currentEnergy >= jumpEnergy)  //�X�y�[�X�L�[���������Ƃ��Ƀ��\�b�h�����������B
        {
            anim.SetTrigger("Jump");

            Jump();
        }


        //-----------------------------------------�e�𔭎˂���----------------------------------------------------//

        if (currentBullet > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                BulletController createBullet = Instantiate(bulletPrefab, bulletStartPosition.position, bulletStartPosition.rotation);   //�e�e�𐶐�����

                createBullet.Shot(this);

                anim.SetTrigger("Shot");

                currentBullet--;    //���̋����������т�1�����炵�Ă���

                currentBullet = Mathf.Clamp(currentBullet, minBullet, maxBullet);   //���̋����͈̔͂��w�肷��

                UIManager.UpdateDisplayBullet(currentBullet);   //�e���̏����𔽉f������
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
            GameData.instance.ChangeWeapon();

            Debug.Log(GameData.instance.equipWeaponData.weaponName);

            UIManager.SetWeaponSliderValue(GameData.instance.equipWeaponData.maxAttackCount);

            UIManager.SetSelectedWeapon();
        }
    }

    private void FixedUpdate()
    {
        //�ړ�����
        Move();

        //�J�����̌�������L�����̌�����ς���B
        LookRotation();
    }

    //---------------------------------�ړ��Ɋւ��鏈��----------------------------------------------------------//
    //���������ւ̈ړ�
    public void Move()
    {
        //rb.velocity = new Vector3(x * moveSpeed,rb.velocity.y, z * moveSpeed);

        if(x!=0 || z != 0)
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

    private void LookRotation()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * z + Camera.main.transform.right * x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    //----------------------------------�G�l���M�[�Ɋւ��鏈��----------------------------------------------------------//

    private void JumpEnergyDecrease()
    {
        currentEnergy -= jumpEnergy;�@�@//���݂̃G�l���M�[�ʂ���W�����v�G�i�W�[�����炷�B
    }
    
    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "ground")
        {
            currentEnergy += cureEnergy;

            currentEnergy = Mathf.Clamp(currentEnergy, minEnergy, maxEnergy);

            UIManager.UpdateDisplayEnergy(currentEnergy);   //�G�l���M�[�l���X�V����
        }
    }

    //-----------------------------------�e�𔭎˂��鏈���Ɋւ��郁�\�b�h----------------------------------------------------//
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
        currentBullet = maxBullet;

        UIManager.UpdateDisplayBullet(currentBullet);

        isReload = false;
    }
}


