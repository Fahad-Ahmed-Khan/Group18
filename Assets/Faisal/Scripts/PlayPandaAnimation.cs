using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPandaAnimation : MonoBehaviour
{
    Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Anim.Play("ArmatureAction_002", -1,0f);
        //Animator.Play("same state you are", -1, 0f);
    }
}
