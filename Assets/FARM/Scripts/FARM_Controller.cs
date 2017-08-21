using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FARM_Controller : MonoBehaviour {

    [SerializeField]
    ParticleSystem[] asap_particles;

    [SerializeField]
    Animator part1animator;
    [SerializeField]
    Animator part2animator;
    [SerializeField]
    Animator part3animator;

    Animator animator;
    float HIDE_COOLDOWN = 0.5f;
    float IDLE_COOLDOWN = 0.5f;
    float hideCoolDown;
    float idleCoolDown;

    [Header("Debugging")]
    public bool DoHide;
    public bool DoIdle;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
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

    public void HidePart1()
    {
        part1animator.Play("FARM_STATE2_PART1_HIDE");
    }

    public void HidePart2()
    {
        part2animator.Play("FARM_STATE2_PART2_HIDE");
    }

    public void HidePart3()
    {
        part3animator.Play("FARM_STATE2_PART3_HIDE");
    }

    public void UnhidePart1()
    {
        part1animator.Play("FARM_STATE2_PART1_UNHIDE");
    }

    public void UnhidePart2()
    {
        part2animator.Play("FARM_STATE2_PART2_UNHIDE");
    }

    public void UnhidePart3()
    {
        part3animator.Play("FARM_STATE2_PART3_UNHIDE");
    }

    public void ImmediateHidePart1()
    {
        part1animator.Play("FARM_STATE2_PART1_IMMEDIATEHIDE");
    }

    public void ImmediateHidePart2()
    {
        part2animator.Play("FARM_STATE2_PART2_IMMEDIATEHIDE");
    }

    public void ImmediateHidePart3()
    {
        part3animator.Play("FARM_STATE2_PART3_IMMEDIATEHIDE");
    }

    /// <summary>
    /// Menutup farm, hanya berjalan saat pabrik sudah idle
    /// </summary>
    public void PlayHide()
    {
        // pastikan state sekarang benar agar tidak bisa dispam
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_idle"))
        {
            if (hideCoolDown <= 0)
            {
                hideCoolDown = HIDE_COOLDOWN;
                animator.SetTrigger("hide");
            }
        }
    }

    /// <summary>
    /// Memunculkan farm lagi, hanya berjalan saat farm sudah state terbuka
    /// </summary>
    public void PlayIdle()
    {
        // pastikan state sekarang benar agar tidak bisa dispam
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FARM_showing"))
        {
            if (idleCoolDown <= 0)
            {
                idleCoolDown = IDLE_COOLDOWN;
                animator.SetTrigger("idle");
            }
        }
        animator.SetTrigger("idle");
    }
}
