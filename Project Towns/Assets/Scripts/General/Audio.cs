﻿using UnityEngine;

/// <summary>
/// Clase Audio, que contiene toda la información
/// necesaria de un archivo de audio
/// </summary>
[System.Serializable]
public class Audio
{
    [Tooltip("Nombre del audio")]
    public string name;
    [Tooltip("Clip del audio")]
    public AudioClip clip;

    [Tooltip("Volumen")]
    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Tooltip("Pitch")]
    [Range(.1f, 3f)]
    public float pitch = 1.0f;

    [Tooltip("Bool que indica si es un loop")]
    public bool loop;

    [HideInInspector]
    [Tooltip("AudioSource para reproducir el audio")]
    public AudioSource source;

    public override string ToString()
    {
        return name + ": [Clip: " + clip.name + ", Volume: " + volume +
            ", Pitch: " + pitch + ", Loop: " + loop + ", Source: " + source.ToString() + "]";
    }
}