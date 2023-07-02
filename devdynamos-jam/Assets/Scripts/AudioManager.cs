using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioSource MainAudioSource;

  void Start()
  {
    MainAudioSource=GetComponent<AudioSource>();
  }

  public static void PlayRandomSoundWithRandomPitch(AudioClip[] ac, float minPitch,float maxPitch)
  {
    var selectedClip = ac[Random.Range(0,ac.Length)];
    PlaySoundRandomPitch(selectedClip,minPitch,maxPitch);
  }

  public static void PlaySoundRandomPitch(AudioClip ac,float minPitch,float maxPitch)
  {
    float randomizedPitch = Random.Range(minPitch,maxPitch);
    PlaySoundPitched(ac,randomizedPitch);
  }

  public static void PlaySoundPitched(AudioClip ac,float pitch)
  {
    MainAudioSource.pitch=pitch;
    PlaySound(ac);
  }

  public static void PlayFromRandomClips(AudioClip[] acs)
  {
    int randomIndex = Random.Range(0,acs.Length);
    PlaySound(acs[randomIndex]);
  }

  public static void PlaySound(AudioClip ac)
  {
    MainAudioSource.PlayOneShot(ac);
    ResetSource();
  }

  private static void ResetSource()
  {
    MainAudioSource.clip=null;
    MainAudioSource.pitch=1.0f;
  }
}
