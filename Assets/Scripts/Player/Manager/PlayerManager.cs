using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //ゲーム[勝ち・負け]UI
    public GameObject GameOverText;
    public GameObject GetKeyText;
    public GameObject Canvas;

    //playerの動き系
    public float moveSpeed = 0.035f; // 歩行速度
    public float rotateSpeed = 0.5f; // 回転速度
    private float horizontalInput, verticalInput;

    //music
    public AudioSource audioSource;
    public AudioClip AttackSound;
    public AudioClip DefanceSound;
    public AudioClip HurtSound;

    //当たり判定系
    private Rigidbody rb;
    private Animator animator;
    public Collider weaponCollider;
    public Collider shieldCollider;


    //ステータス
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

        //動き
        if (verticalInput != 0)
        {
            transform.position += transform.forward * moveSpeed * verticalInput;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        //攻撃
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

        //ガード
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


    //武器当たり判定
    public void HideColliderWeapon()　//なし
    {
        weaponCollider.enabled = false;
    }

    public void ShowColliderWeapon() //あり
    {
        weaponCollider.enabled = true;
    }

    //シールと当たり判定
    public void HideColliderShield()　//なし
    {
        shieldCollider.enabled = false;
        animator.SetBool("Damage", false);
    }

    public void ShowColliderShield() //あり
    {
        shieldCollider.enabled = true;
        animator.SetBool("Damage", true);
    }


    //攻撃効果音
    public void StartAudio() //あり
    {
        audioSource.PlayOneShot(AttackSound);
    }
    public void StartAudio2() //あり
    {
        audioSource.PlayOneShot(HurtSound);
    }




    void Damage(int damage)
    {
        //シールドのColliderに攻撃が当たったら
        if (shieldCollider.enabled == true)
        {
            audioSource.PlayOneShot(DefanceSound);
            damage = 0;
        }
        //シールドのColliderに攻撃が当たってダメージが0になったら状態継続用
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
        //Debug.Log("残りHP:" + hp);
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
                // ダメージを与えるものにぶつかったら
                Damage(damage);
        }
           
    }
}

