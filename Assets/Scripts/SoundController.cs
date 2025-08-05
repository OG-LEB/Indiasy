using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    public static SoundController GetInstance() { return instance; }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private AudioSource ShootSound;
    [SerializeField] private AudioSource MeeleAtackSound;
    [SerializeField] private AudioSource ButtonSound;
    [SerializeField] private AudioSource TextSound;
    [SerializeField] private AudioSource DropInKotel;
    [SerializeField] private AudioSource SwordPickUp;
    [SerializeField] private AudioSource KotelDamage;
    [SerializeField] private AudioSource FinalDisk;
    [SerializeField] private AudioSource PickUp;
    [SerializeField] private AudioSource Jump;

    [Header("SoundTrack")]
    [SerializeField] private AudioSource SoundTrackObj;
    [SerializeField] private AudioClip SoundTrackMain;
    [SerializeField] private AudioClip SoundTrackFight;
    [SerializeField] private AudioClip SoundTrackChill;
    [SerializeField] private bool SoundTrackIsActive;
    [SerializeField] private float MainSoundTrackVolume;
    [SerializeField] private float FightSoundTrackVolume;

    public void PlayeShootSound() { ShootSound.Play(); }
    public void PlayeMeeleAtack() { MeeleAtackSound.Play(); }
    public void PlayeButtonSound() { ButtonSound.Play(); }
    public void PlayeTextSound() { TextSound.Play(); }
    public void PlayeDropInKotel() { DropInKotel.Play(); }
    public void PlayeSwordPickUp() { SwordPickUp.Play(); }
    public void PlayKotelDamage() { KotelDamage.Play(); }
    public void PlayFinalDisk() { FinalDisk.Play(); }
    public void PlayPickUp() { PickUp.Play(); }
    public void PlayJump() { Jump.Play(); }

    //SoundController.GetInstance().PlayeTextSound();
    

    //
    public void PlayMainSoundTrack() 
    {
        SoundTrackObj.clip = SoundTrackMain;
        SoundTrackObj.volume = MainSoundTrackVolume;
        SoundTrackObj.Play();
    }
    public void PlayFightSoundTrack() 
    {
        SoundTrackObj.clip = SoundTrackFight;
        SoundTrackObj.volume = FightSoundTrackVolume;
        SoundTrackObj.Play();
    }
    public void PlayChillSoundTrack()
    {
        SoundTrackObj.clip = SoundTrackChill;
        SoundTrackObj.volume = MainSoundTrackVolume;
        SoundTrackObj.Play();
    }

    private void Update()
    {
        if (SoundTrackIsActive && !SoundTrackObj.isPlaying)
        {
            SoundTrackObj.Play();

        }
        else if (!SoundTrackIsActive && SoundTrackObj.isPlaying) 
        {
            SoundTrackObj.Stop();

        }
    }
}
