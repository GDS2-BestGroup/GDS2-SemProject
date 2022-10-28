using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCredit : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim.Play();
        animator.Play("Entry", -1 );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
