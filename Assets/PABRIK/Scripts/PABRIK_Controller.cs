using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PABRIK_Controller : MonoBehaviour
{

    [SerializeField]
    ParticleSystem[] asap_particles;

    [SerializeField]
    Animator part1animator;
    [SerializeField]
    Animator part2animator;
    [SerializeField]
    Animator part3animator;
    [SerializeField]
    Animator part4animator;


    [Header("Part 3 components")]
    [SerializeField]
    Image HotThermometer;
    [SerializeField]
    RectTransform Clock;


    [Header("Debugging")]
    public string Part3AnimationState;
    public bool DoPart3_PlayAnimation;
    public float Part3HotThermometerValue;
    public bool DoPart3_SetHotThermometerValue;
    public float Part3TimerValue;
    public bool DoPart3_SetTimerValue;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoPart3_PlayAnimation)
        {
            DoPart3_PlayAnimation = false;
            Part3_PlayAnimation(Part3AnimationState);
        }

        if (DoPart3_SetHotThermometerValue)
        {
            DoPart3_SetHotThermometerValue = false;
            Part3_SetHotThermometerValue(Part3HotThermometerValue);
        }

        if (DoPart3_SetTimerValue)
        {
            DoPart3_SetTimerValue = false;
            Part3_SetTimerValue(Part3TimerValue);
        }
    }

    public void Play_Asap()
    {
        foreach (ParticleSystem ps in asap_particles)
        {
            ParticleSystem.EmissionModule em = ps.emission;
            em.enabled = true;
        }
    }

    public void Pause_Asap()
    {
        foreach (ParticleSystem ps in asap_particles)
        {
            ParticleSystem.EmissionModule em = ps.emission;
            em.enabled = false;
        }
    }

    public void PlayPart1()
    {
        part1animator.SetTrigger("play");
    }

    public void PlayPart2()
    {
        part2animator.SetTrigger("play");
    }

    public void PlayPart3()
    {
        part3animator.SetTrigger("play");
    }

    public void PlayPart4()
    {
        part4animator.SetTrigger("play");
    }

    public void MoveToPart1()
    {
        animator.Play("PABRIK_state2_part1_show");
    }

    public void MoveToPart2()
    {
        animator.Play("PABRIK_state2_part2_show");
    }

    public void MoveToPart3()
    {
        animator.Play("PABRIK_state2_part3_show");
    }

    public void MoveToPart4()
    {
        animator.Play("PABRIK_state2_part4_show");
    }

    public void ImmediateHidePart1()
    {
        part1animator.Play("PABRIK_STATE2_PART1_IMMEDIATEHIDE");
        Part3_SetHotThermometerValue(0);
    }

    public void ImmediateHidePart2()
    {
        part2animator.Play("PABRIK_STATE2_PART2_IMMEDIATEHIDE");
    }

    public void ImmediateHidePart3()
    {
        part3animator.Play("PABRIK_STATE2_PART3_IMMEDIATEHIDE");
    }

    public void ImmediateHidePart4()
    {
        part4animator.Play("PABRIK_STATE2_PART4_IMMEDIATEHIDE");
    }

    public void HidePart1()
    {
        part1animator.Play("PABRIK_STATE2_PART1_HIDE");
    }

    public void HidePart2()
    {
        part2animator.Play("PABRIK_STATE2_PART2_HIDE");
    }

    public void HidePart3()
    {
        part3animator.Play("PABRIK_STATE2_PART3_HIDE");
    }

    public void HidePart4()
    {
        part4animator.Play("PABRIK_STATE2_PART4_HIDE");
    }

    public void UnhidePart1()
    {
        part1animator.Play("PABRIK_STATE2_PART1_UNHIDE");
    }

    public void UnhidePart2()
    {
        part2animator.Play("PABRIK_STATE2_PART2_UNHIDE");
    }

    public void UnhidePart3()
    {
        part3animator.Play("PABRIK_STATE2_PART3_UNHIDE");
    }

    public void UnhidePart4()
    {
        part4animator.Play("PABRIK_STATE2_PART4_UNHIDE");
    }

    public void Part3_SetHotThermometerValue(float fillAmount)
    {
        HotThermometer.fillAmount = fillAmount;

        if (fillAmount >= 1)
        {
            Part3_PlayAnimation("3");
        }
    }

    public void Part3_SetTimerValue(float rotation)
    {
        Clock.localEulerAngles = new Vector3(0, 0, rotation);
    }


    public void Part3_PlayAnimation(string animationState)
    {
        part3animator.Play(animationState);
    }
}
