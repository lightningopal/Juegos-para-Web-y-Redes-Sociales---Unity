using System;
using UnityEngine;

/// <summary>
/// Clase AudioManager, que controla el audio en el juego
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Singleton")]
    [HideInInspector]
    public static AudioManager instance;

    [Tooltip("All music in the game")]
    public Audio[] music;
    [Tooltip("All sounds in the game")]
    public Audio[] sounds;
    #endregion

    #region MétodosUnity
    /// <summary>
    /// Método Awake, that executes on script load
    /// </summary>
    void Awake()
    {
        // Se instancia a si misma
        if (instance == null)
        {
            instance = this;

            // Se impide que se destruya al cargar
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Se establece la información de cada audio
        foreach (Audio a in music)
        {
            a.source = gameObject.AddComponent<AudioSource>(); ;
            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }

        foreach (Audio a in sounds)
        {
            a.source = gameObject.AddComponent<AudioSource>(); ;
            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
        }
    }

    /// <summary>
    /// Método Start, que se llama antes del primer frame
    /// </summary>
    private void Start()
    {
        //PlayAudio(mainTheme);
    }
    #endregion

    #region MétodosClase
    /// <summary>
    /// Método ManageAudio, que controla el audio
    /// </summary>
    /// <param name="name">Nombre del audio</param>
    /// <param name="type">Tipo del audio (music/sound)</param>
    /// <param name="action">Acción a realizar</param>
    public void ManageAudio(string name, string type, string action)
    {
        if (type == "music")
        {
            Audio a = Array.Find(music, sound => sound.name == name);
            if (a == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (action == "play")
                a.source.Play();
            else if (action == "stop")
                a.source.Stop();
            else if (action == "pause")
                a.source.Pause();
            else if (action == "unpause")
                a.source.UnPause();
        }
        else if (type == "sound")
        {
            Audio a = Array.Find(sounds, sound => sound.name == name);
            if (a == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (action == "play")
                a.source.Play();
            else if (action == "stop")
                a.source.Stop();
            else if (action == "pause")
                a.source.Pause();
            else if (action == "unpause")
                a.source.UnPause();
        }
    }

    #region MétodosUnitarios
    #region Music
    /// <summary>
    /// Método PlayMusic, que reproduce un audio de tipo música
    /// </summary>
    /// <param name="name">Música a reproducir</param>
    public void PlayMusic(string name)
    {
        ManageAudio(name, "music", "play");
    }

    /// <summary>
    /// Método StopMusic, que para un audio de tipo música
    /// </summary>
    /// <param name="name">Música a parar</param>
    public void StopMusic(string name)
    {
        ManageAudio(name, "music", "stop");
    }

    /// <summary>
    /// Método PauseMusic, que pausa un audio de tipo música
    /// </summary>
    /// <param name="name">Música a pausar</param>
    public void PauseMusic(string name)
    {
        ManageAudio(name, "music", "pause");
    }

    /// <summary>
    /// Método UnpauseMusic, que despausa un audio de tipo música
    /// </summary>
    /// <param name="name">Música a despausar</param>
    public void UnpauseMusic(string name)
    {
        ManageAudio(name, "music", "unpause");
    }
    #endregion

    #region Sound
    /// <summary>
    /// Método PlaySound, que reproduce un audio de tipo sonido
    /// </summary>
    /// <param name="name">Sonido a reproducir</param>
    public void PlaySound(string name)
    {
        ManageAudio(name, "sound", "play");
    }

    /// <summary>
    /// Método PlaySoundAtPoint, que reproduce un audio de tipo sonido en un lugar concreto
    /// </summary>
    /// <param name="name">Sonido a reproducir</param>
    public void PlaySoundAtPoint(string name, Vector3 position)
    {
        Audio a = Array.Find(sounds, sound => sound.name == name);
        if (a == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        AudioSource.PlayClipAtPoint(a.clip, position);
    }

    /// <summary>
    /// Método StopSound, que para un audio de tipo sonido
    /// </summary>
    /// <param name="name">Sonido a parar</param>
    public void StopSound(string name)
    {
        ManageAudio(name, "sound", "stop");
    }

    /// <summary>
    /// Método PauseSound, que pausa un audio de tipo sonido
    /// </summary>
    /// <param name="name">Sonido a pausar</param>
    public void PauseSound(string name)
    {
        ManageAudio(name, "sound", "pause");
    }

    /// <summary>
    /// Método UnpauseSound, que despausa un audio de tipo sonido
    /// </summary>
    /// <param name="name">Sonido a despausar</param>
    public void UnpauseSound(string name)
    {
        ManageAudio(name, "sound", "unpause");
    }
    #endregion

    #endregion

    #endregion
}
