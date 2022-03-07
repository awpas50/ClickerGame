using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundList
{
    ButtonClicked,
    BuildingPlaced,
    Cancel,
    explosion1,
    explosion2,
    explosion3,
    SelectHouse,
    SelectFactory,
    SelectPark,
    SelectTurret,
    SelectAirport,
    AsteroidPassBy,
    GetAirportGoods,
    GetCleanAir,
    Repair,
    Error,
    TurretShoot,
    SelectLogisticCenter,
    ButtonClicked2,
    ButtonClicked3,
    Slide,
    BuildingComplete,
    BuildingSold,
    BuildingUpgrade,
    GetRandomStuff,
    PopUp1,
    PopUp2,
    PopUp3,
    Hover1,
    Hover2,
    Hover3,
    GetMoney1,
    GetMoney2,
    GetMoney3,
    ButtonClicked4,
    ButtonClicked5
}

[System.Serializable]
public class Sound
{
    // as we are going to add audio in the AudioManager gameObject, we need a reference
    public AudioClip clip;
    public SoundList soundList;
    [HideInInspector] public AudioSource source;
    [HideInInspector] public float originalVolume;
    [Range(0f, 2f)] public float volume;
    [Range(0.1f, 3f)] public float pitch;
    // determine 2D or 3D sound.
    [Range(0f, 1f)] public float reverbZoneMix;
}

