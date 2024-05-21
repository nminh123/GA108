using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBoss : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 3;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         player = GameObject.FindGameObjectWithTag("Player").transform;
         rb =animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookPlayer();
        Vector2 targer = new Vector2(player.position.x , rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, targer, speed * Time.fixedDeltaTime);
        float dis = Vector2.Distance(player.position, rb.position);
        if (dis > attackRange)
        rb.MovePosition(newPos);
        
            if (dis < attackRange)
        {
            rb.velocity = new Vector2(0, 0);           
            animator.SetTrigger("Attack");
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("Attack");
    }

}
