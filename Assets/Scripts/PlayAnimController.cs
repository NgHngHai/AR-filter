using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.eulerAngles.z) > 15)
        {
            animator.SetBool("isTilting", true);
        } else
        {
            animator.SetBool("isTilting", false);
        }
    }
}
