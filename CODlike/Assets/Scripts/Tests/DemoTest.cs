using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTest : MonoBehaviour
{

    [SerializeField] private Animator weaponAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            ActivateAnim(AnimType.Run, true);
        }
        if (Input.GetKeyUp(KeyCode.W)) 
        {
            ActivateAnim(AnimType.Run, false);
        }

    }
    public void ActivateAnim(AnimType type, bool? setActive)
    {
        if (type == AnimType.Run)
        {
            weaponAnimator.SetBool("IsRunning", (bool)setActive);
        }

        else if (type == AnimType.Shoot)
        {
            if ((bool)setActive)
            {

            }
            else
            {

            }
        }

        else if (type == AnimType.Reload)
        {
            weaponAnimator.SetTrigger("Reload");
        }
    }
}
