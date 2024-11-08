using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicScource;
    [SerializeField] AudioSource SFXScource;



    [Header("-------- Audio Clip --------")]
    public AudioClip Background;
    public AudioClip Race;
    public AudioClip Drift;
    public AudioClip Clicking;
}
