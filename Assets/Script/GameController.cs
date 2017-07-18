using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/*----------------------------------------------------------------------------------------------------------------------
    Touch:
    0 - Prolog
    1 - Prolog Sub
    2 - Factory
    3 - Store
    4 - Games
  
    5 - Farm Atas
    6 - Farm Bawah
    7 - Rumah Kiri
    8 - Rumah Kanan

    Swipe:
    0 - Farm atas 
    1 - Farm bawah
    2 - Rumah Kiri
    3 - Rumah Kanan

    Blow:
    0 - Anak
----------------------------------------------------------------------------------------------------------------------*/
[System.Serializable]
public class Interaction : System.Object
{
    public enum InteractionObject
    {
        Prolog,
        Prolog_Sub,
        Factory,
        Store,
        Games,
        Farm,
        Rumah,
        Anak,
        Size
    };

    public enum SensorType
    {
        Touch,
        Swipe,
        Blow
    };

    public enum OutputType
    {
        Direct,
        One_Stage,
        Two_Stage,
        Change_Scene
    };
    
    public string name;
    public GameObject game;
    public SensorType sensorType;
    public KeyCode keyCode;
    public OutputType outputType;
    public AudioClip sound_effect1;
    public float volume1;
    public AudioClip sound_effect2;
    public float volume2;
    public float timeOut;
    [HideInInspector]
    public bool value;
    [HideInInspector]
    public float counter;
    [HideInInspector]
    public bool audiostate;
    [HideInInspector]
    public bool audiostate2;
}

public class GameController : MonoBehaviour
{
    private Fading fading;
    public string sceneName;

    //private bool audiostate = true;
    //private bool audiostate2 = true;
    
    private int state = 0;

    public Interaction[] interaction = new Interaction[(int)Interaction.InteractionObject.Size];

    void PlaySoundEffect(int soundNumber, float volume, AudioClip soundEffect, AudioClip soundEffect2, AudioSource source)
    {
        switch (soundNumber)
        {
            case 1:
                    source.PlayOneShot(soundEffect, volume);
                break;

            case 2:
                    source.PlayOneShot(soundEffect2, volume);
                break;
        }

    }

    void NewScene()
    {
        float fadeTime = fading.BeginFade(1);
        SceneManager.LoadScene(sceneName);
    }

    // Use this for initialization
    void Start()
    {
        fading = GetComponent<Fading>();
        for (int i = 0; i < (int)Interaction.InteractionObject.Size; i++)
        {
            interaction[i].audiostate = false;
            interaction[i].audiostate2 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < (int)Interaction.InteractionObject.Size; i++)
        {
            // Read input from keyboard
            if (Input.GetKeyDown(interaction[i].keyCode))
            {
                Debug.Log("Key " + interaction[i].keyCode + " Pressed");
                if (interaction[i].sensorType == Interaction.SensorType.Blow)
                    interaction[i].value = true;
                else
                    interaction[i].value = !interaction[i].value; 
            }

            // Time out control
            if (interaction[i].value == true)
            {
                interaction[i].counter += Time.deltaTime;
                if (interaction[i].counter > interaction[i].timeOut)
                {
                    interaction[i].value = !interaction[i].value;
                    interaction[i].counter = 0;
                    Debug.Log(interaction[i].game + " is timeout");
                }
            }

            else if (interaction[i].value == false)
            {
                interaction[i].counter = 0;
            }

            // Read input from serial handler
            if (SerialHandler.serial_is_open)
            {
                interaction[i].value = SerialHandler.getSensorTrig(i);
            }

            
            switch (interaction[i].outputType)
            {
                case Interaction.OutputType.Direct:

                    break;

                case Interaction.OutputType.One_Stage:

                    if (interaction[i].value == true)
                    {
                        interaction[i].game.GetComponent<Animator>().SetInteger("AnimState", 1);
                        if (interaction[i].game.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PlaySoundEffect")) 
                            PlaySoundEffect(1, interaction[i].volume1, interaction[i].sound_effect1, interaction[i].sound_effect2, interaction[i].game.GetComponent<AudioSource>());
                        interaction[i].audiostate = false;
                    }

                    else
                    {
                        interaction[i].game.GetComponent<Animator>().SetInteger("AnimState", 0);
                        interaction[i].audiostate = true;
                    }

                    break;

                case Interaction.OutputType.Two_Stage:

                    if (interaction[i].value == true && state == 0)
                    {
                        interaction[i].game.GetComponent<Animator>().SetInteger("AnimState", 1);
                        if (interaction[i].game.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PlaySoundEffect"))
                            PlaySoundEffect(1, interaction[i].volume1, interaction[i].sound_effect1, interaction[i].sound_effect2, interaction[i].game.GetComponent<AudioSource>());
                        state = 1;
                        interaction[i].audiostate = false;
                    }

                    else if (interaction[i].value == false && state == 1)
                    {
                        interaction[i].game.GetComponent<Animator>().SetInteger("AnimState", 2);
                        if (interaction[i].game.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PlaySoundEffect"))
                            PlaySoundEffect(2, interaction[i].volume2, interaction[i].sound_effect1, interaction[i].sound_effect2, interaction[i].game.GetComponent<AudioSource>());
                        state = 2;
                        interaction[i].audiostate2 = false;
                    }

                    else if (interaction[i].value == true && state == 2)
                    {
                        interaction[i].game.GetComponent<Animator>().SetInteger("AnimState", 0);
                        interaction[i].audiostate = true;
                        interaction[i].audiostate2 = true;
                        state = 0;
                    }

                    break;

                case Interaction.OutputType.Change_Scene:
                    if (interaction[i].value == true)
                    {
                        interaction[i].value = false;
                        NewScene();
                    }
                    break;

            }

        }


    }
}


