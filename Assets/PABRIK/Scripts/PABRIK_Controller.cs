using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PABRIK_Controller : MonoBehaviour {

    [SerializeField]
    ParticleSystem[] asap_particles;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
