using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    public int EmotionalState;
    public Image sprite1;
    public Image sprite2;
    public Image sprite3;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchImage(Sprite sprite)
    {
        switch (EmotionalState)
        {
            case 1:
                sprite1.sprite = sprite;
                animator.SetTrigger("SwitchFirst");
                break;
            case 2:
                sprite2.sprite = sprite;
                animator.SetTrigger("SwitchSecond");
                break;
            case 3:
                sprite3.sprite = sprite;
                animator.SetTrigger("SwitchThird");
                break;
                /*case 4:
                    sprite4.sprite = sprite;
                    animator.SetTrigger("SwitchFourth");
                    break;
                case 5:
                    sprite5.sprite = sprite;
                    animator.SetTrigger("SwitchFifth");
                    break;
                case 6:
                    sprite6.sprite = sprite;
                    animator.SetTrigger("SwitchSixth");
                    break;
                    */
        }
    }

    public void SetImage(Sprite sprite)
    {
        switch (EmotionalState)
        {
            case 1:
                sprite1.sprite = sprite;
                break;
            case 2:
                sprite2.sprite = sprite;
                break;
            case 3:
                sprite3.sprite = sprite;
                break;
                /*case 4:
                    sprite4.sprite = sprite;
                    break;
                case 5:
                    sprite5.sprite = sprite;
                    break;
                case 6:
                    sprite6.sprite = sprite;
                    break;
                    */
        }
    }
}