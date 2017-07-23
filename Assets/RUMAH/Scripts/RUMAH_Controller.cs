using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUMAH_Controller : MonoBehaviour {

    [SerializeField]
    ParticleSystem[] asap_particles;

    [SerializeField]
    Animator part1animator;
    
    Animator animator;
    float HIDE_COOLDOWN = 0.5f;
    float IDLE_COOLDOWN = 0.5f;
    float hideCoolDown;
    float idleCoolDown;

    [Header("Debugging")]
    public bool DoHide;
    public bool DoIdle;

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
        

        if (hideCoolDown > 0)
            hideCoolDown -= Time.deltaTime;

        if (idleCoolDown > 0)
            idleCoolDown -= Time.deltaTime;

    }

    public void PlayPart1()
    {
        part1animator.SetTrigger("play");
    }
    

    public void MoveToPart1()
    {
        animator.Play("RUMAH_state2_part1_show");
    }

    public void ImmediateHidePart1()
    {
        part1animator.Play("RUMAH_STATE2_PART1_IMMEDIATEHIDE");
    }
    
    public void HidePart1()
    {
        part1animator.Play("RUMAH_STATE2_PART1_HIDE");
    }

    public void UnhidePart1()
    {
        part1animator.Play("RUMAH_STATE2_PART1_UNHIDE");
    }
    
    /// <summary>
    /// Menutup pabrik, hanya berjalan saat pabrik sudah idle
    /// </summary>
    public void PlayHide()
    {
        // pastikan state sekarang benar agar tidak bisa dispam
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RUMAH_idle"))
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RUMAH_showing"))
        {
            if (idleCoolDown <= 0)
            {
                idleCoolDown = IDLE_COOLDOWN;
                animator.SetTrigger("idle");
            }
        }
    }
}
