using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Variables
    public static AudioManager instance; 
    public Sound[] listSounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        foreach (Sound sound in listSounds)
        {
            // Añadimos a la lista un audioSource y un clip para cada componente
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;

            // Accedemoa a cada audioSource y hacemos referencia al volumen, el loop y el playOnAwake para
            // poder modificar estos valores
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.playOnAwake = sound.playOnAwake;
        }
    }

    public void PlaySong(string name)
    {
        foreach (Sound sound in listSounds)
        {
            if (name == sound.name)
            {
                sound.audioSource.Play();
            }
        }
    }

    public void StopSong(string name)
    {
        foreach (Sound sound in listSounds)
        {
            if (name == sound.name)
            {
                sound.audioSource.Stop();
            }
        }
    }
}
