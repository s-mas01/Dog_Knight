using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //�Q�[��[�����E����]UI
    public GameObject GameOverText;
    public GameObject GetKeyText;
    public GameObject Canvas;

    //player�̓����n
    public float moveSpeed = 0.035f; // ���s���x
    public float rotateSpeed = 0.5f; // ��]���x
    private float horizontalInput, verticalInput;

    //music
    public AudioSource audioSource;
    public AudioClip AttackSound;
    public AudioClip DefanceSound;
    public AudioClip HurtSound;

    //�����蔻��n
    private Rigidbody rb;
    private Animator animator;
    public Collider weaponCollider;
    public Collider shieldCollider;


    //�X�e�[�^�X
    [SerializeField] PlayerStatusSO playerStatusSO;
    [SerializeField] EnemyStatusSO enemyStatusSO;
    public int hp;
    public PlayerUIManager PlayerUIManager;
    bool isDie;
    private int damage = 0;


    // Start is called before the first frame update
    void Start()
    {
        hp = playerStatusSO.hp;
        PlayerUIManager.Init(this);
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        HideColliderWeapon();
        HideColliderShield();
        
    }

    void Update()
    {
        if (isDie == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("PickMap");
        }
        if (isDie)
        {
            
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //����
        if (verticalInput != 0)
        {
            transform.position += transform.forward * moveSpeed * verticalInput;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        //�U��
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Attack", true);

        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Attack", false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("Attack2", true);
       
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("Attack2", false);
        }

        //�K�[�h
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Defend", true);
            ShowColliderShield();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Defend", false);
            HideColliderShield();
        }


        transform.Rotate(new Vector3(0, rotateSpeed * horizontalInput, 0));

        

    }


    //���퓖���蔻��
    public void HideColliderWeapon()�@//�Ȃ�
    {
        weaponCollider.enabled = false;
    }

    public void ShowColliderWeapon() //����
    {
        weaponCollider.enabled = true;
    }

    //�V�[���Ɠ����蔻��
    public void HideColliderShield()�@//�Ȃ�
    {
        shieldCollider.enabled = false;
        animator.SetBool("Damage", false);
    }

    public void ShowColliderShield() //����
    {
        shieldCollider.enabled = true;
        animator.SetBool("Damage", true);
    }


    //�U�����ʉ�
    public void StartAudio() //����
    {
        audioSource.PlayOneShot(AttackSound);
    }
    public void StartAudio2() //����
    {
        audioSource.PlayOneShot(HurtSound);
    }




    void Damage(int damage)
    {
        //�V�[���h��Collider�ɍU��������������
        if (shieldCollider.enabled == true)
        {
            audioSource.PlayOneShot(DefanceSound);
            damage = 0;
        }
        //�V�[���h��Collider�ɍU�����������ă_���[�W��0�ɂȂ������Ԍp���p
        else if (damage == 0)
        {
            
        }
        else 
        {
            damage = 10;
            animator.SetTrigger("Hurt");
        }


        hp -= damage;
        if (hp <= 0)
        {

            hp = 0;
            isDie = true;
            animator.SetTrigger("Die");
            GameOverText.SetActive(true);
            GetKeyText.SetActive(true);
            Canvas.SetActive(true);
        }
        //Debug.Log("�c��HP:" + hp);
        PlayerUIManager.UpdateHP(hp);
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (isDie)
        {
            return;
        }
        damage = enemyStatusSO.enemyStatusList[0].attack1;

        if (other.gameObject.CompareTag("Axe"))
        {
                // �_���[�W��^������̂ɂԂ�������
                Damage(damage);
        }
           
    }
}

