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
    public PABRIK_Stopwatch_Controller StopwatchController;

    Animator animator;
    float HIDE_COOLDOWN = 0.5f;
    float IDLE_COOLDOWN = 0.5f;
    float hideCoolDown;
    float idleCoolDown;

    [Header("Debugging")]
    public bool DoHide;
    public bool DoIdle;
    public float HotThermometerValue;
    public bool DoSetHotThermometer;
    public bool DoSetStopwatchValue;//ini buat testing doang!
    public int StopwatchCurrentValue;
    public int StopwatchMaxValue;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoHide)
        {
            DoHide = false;
            PlayHide();
        }

        if (DoIdle)
        {
            DoIdle = false;
            PlayIdle();
        }

        if (DoSetHotThermometer)
        {
            DoSetHotThermometer = false;
            SetHotThermometerValue(HotThermometerValue);
        }

        // TESTING DOANG!!
		/*
        if (DoSetStopwatchValue)
        {
            DoSetStopwatchValue = false;
            StopwatchController.SetValue(StopwatchCurrentValue, StopwatchMaxValue);
        }*/

        if (hideCoolDown > 0)
            hideCoolDown -= Time.deltaTime;

        if (idleCoolDown > 0)
            idleCoolDown -= Time.deltaTime;

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
    }

    public void ImmediateHidePart2()
    {
        part2animator.Play("PABRIK_STATE2_PART2_IMMEDIATEHIDE");
    }

    public void ImmediateHidePart3()
    {
        part3animator.Play("PABRIK_STATE2_PART3_IMMEDIATEHIDE");
        SetHotThermometerValue(0);
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
        SetHotThermometerValue(0);
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

    /// <summary>
    /// Set besaran termometer di bagian ketiga. Animasi bakalan stop dulu
    /// dan gak jalan sebelum nilai ini diset >= 1
    /// </summary>
    /// <param name="fillAmount"></param>
    public void SetHotThermometerValue(float fillAmount)
    {
        HotThermometer.fillAmount = fillAmount;
        if (fillAmount >= 1)
        {
            part3animator.Play("3");
        }
    }

    /// <summary>
    /// Menutup pabrik, hanya berjalan saat pabrik sudah idle
    /// </summary>
    public void PlayHide()
    {
        // pastikan state sekarang benar agar tidak bisa dispam
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_idle"))
        {
            if (hideCoolDown <= 0)
            {
                hideCoolDown = HIDE_COOLDOWN;
                animator.SetTrigger("hide");
            }
        }
    }

    /// <summary>
    /// Memunculkan pabrik lagi, hanya berjalan saat pabrik sudah state terbuka
    /// </summary>
    public void PlayIdle()
    {
        // pastikan state sekarang benar agar tidak bisa dispam
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PABRIK_showing"))
        {
            if (idleCoolDown <= 0)
            {
                idleCoolDown = IDLE_COOLDOWN;
                animator.SetTrigger("idle");
            }
        }
    }
}
