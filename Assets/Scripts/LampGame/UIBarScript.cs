using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarScript : MonoBehaviour
{
    public bool enableAnimation = false;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    // 
    public void Animate()
    {
        if (enableAnimation)
        {
            animator.enabled = true;
            return;
        }
        animator.enabled = false;
    }
}
