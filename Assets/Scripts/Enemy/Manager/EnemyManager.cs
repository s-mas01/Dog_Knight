using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
*Enemyのアニメーションのバグ修正
* ・Idle：遠い：7以上：speedを0
* ・Run：やや近い：7以下：speedを2
* ・Attack：近い：2以下：speedを0
*/
public class EnemyManager : MonoBehaviour
{
    public GameObject GameClearText;
    public GameObject GetKeyText;
    public GameObject Canvas;

    bool At;

    //動き系
    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    private bool isDie;


    //当たり判定系
    public Collider weaponCollider;

    //ステータス
    [SerializeField] EnemyStatusSO enemyStatusSO;
    [SerializeField] PlayerStatusSO playerStatusSO;
    public EnemyUIManager enemyUIManager;
    public int hp;
    private int damage;


    //GameSystem



    void Start()
    {
        hp = enemyStatusSO.enemyStatusList[0].hp;
        enemyUIManager.Init(this);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        HideColliderWeapon();
        At = false;
        isDie = false;
        damage = 0; 
    }

    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);

        if (Input.GetKeyDown(KeyCode.E))
        {
            At = true;
        }
        if (isDie == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("PickMap");
        }
    }
    
    //武器当たり判定
    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }

    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }



    void Damage(int damage)
    {
        if(At==true)
        {
            damage = playerStatusSO.attack2;
            At = false;
        }
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
            GameClearText.SetActive(true);
            GetKeyText.SetActive(true);
            Canvas.SetActive(true);
            isDie = true;
            //Destroy(gameObject, 3f);


        }
        enemyUIManager.UpdateHP(hp);
        //Debug.Log("Enemy残りHP：" + hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            damage = playerStatusSO.attack1;
            if (damage != 0)
            {
                // ダメージを与えるものにぶつかったら
                animator.SetTrigger("Hurt");
                Damage(damage);
            }
        }

    }
}

