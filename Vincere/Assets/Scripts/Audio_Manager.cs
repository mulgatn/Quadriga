using UnityEngine.Audio;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public Sound[] sounds;

    public static Audio_Manager instance;

    void Awake()
    {
        //if (instance == null)
        //    instance = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);


        foreach(Sound s in sounds)
        {
            if (s.type == Sound.Type.Menu_Sound)
                s.source = transform.Find("Menu_Sounds").gameObject.AddComponent<AudioSource>();
            else if (s.type == Sound.Type.Player1_Sounds)
                s.source = transform.Find("Player1_Sounds").gameObject.AddComponent<AudioSource>();
            else if (s.type == Sound.Type.Player2_Sounds)
                s.source = transform.Find("Player2_Sounds").gameObject.AddComponent<AudioSource>();
            else if (s.type == Sound.Type.Environment_Sound)
                s.source = transform.Find("Environment_Sounds").gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.looping;
        }
    }

    public void Play(string name, float stereo)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.source.panStereo = stereo;
                s.source.Play();
            }
        }
    }
}
