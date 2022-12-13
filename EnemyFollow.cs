using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public bool isAnimated = false;
    //public Animator anim;
    //public string animationStringFwd;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = this.gameObject.GetComponent<NavMeshAgent>();
        if(isAnimated)
        {
            //anim = this.gameObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        enemy.SetDestination(player.position);
        //anim.Play("WalkFront_Shoot_AR");
    }
}
