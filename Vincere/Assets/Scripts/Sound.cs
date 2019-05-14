using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum Type
    {
        Player1_Sounds,
        Player2_Sounds,
        Environment_Sound,
        Menu_Sound
    }

    public AudioClip clip;

    public string name;
    public Type type;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool looping;

    [Range(-1f, 1f)]
    public float stereo;

    //[HideInInspector]
    public AudioSource source;

}
