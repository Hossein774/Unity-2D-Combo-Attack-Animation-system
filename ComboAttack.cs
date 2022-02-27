using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Combat : MonoBehaviour

{

    public Animator anim;

    public float btwtime;

    public float Remainingtime = 0.8f;

    public int attacknum = 1;

    private bool canAttack = true;

    private PlayerController2D playerController;

    public Transform attackpos;
    public float attackRange;
    public LayerMask enemieslayer;
    public int Damage;

    private float starttoAttack = 0.4f;
    public float btwToAttack;

    public int countToNotAttack = 0;

    void Start()
    {

        anim = GetComponent<Animator>();

        playerController = GetComponent<PlayerController2D>();

        canattack = true;

    }

    void Update()
    {
        
        if (playerController.IsGrounded && Input.GetMouseButtonDown(0) && canAttack)
        {
            countToNotAttack++;
            if (attacknum == 1)
            {
                anim.SetTrigger("attack");
                btwtime = Remainingtime;
                attacknum++;
            }
            else if (attacknum == 2)
            {
                anim.SetTrigger("atack2");
                attacknum = 1;
                
            }
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackpos.position, attackRange, enemieslayer);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<Enemy>().takedamage(Damage);
            }
        }

        if (attacknum == 2) { StartCoroutine("attackTimer"); }
            
        if(countToNotAttack >= 2)
        {
            if (btwToAttack <= 0)
            {
                canAttack = true;
                countToNotAttack = 0;
                btwToAttack = starttoAttack;
            }
            else
            {
                canAttack = false;
                btwToAttack -= Time.deltaTime;
            }
        }

        if (!canAttack)
        {
            if(btwToAttack <= 0)
            {
                canAttack = true;
                btwToAttack = starttoAttack;
            }
            else
            {
                btwToAttack -= Time.deltaTime;
            }
        }

    }

    IEnumerator attackTimer()
    {
        if (btwtime <= 0 && attacknum >= 2)
        {
            attacknum = 1;
            btwtime = Remainingtime;
            yield return null;
            countToNotAttack = 0;
        }
        else
        {
            btwtime -= Time.deltaTime;
        }
        
    }

}