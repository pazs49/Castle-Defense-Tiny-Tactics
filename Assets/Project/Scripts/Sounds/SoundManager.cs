using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager Instance;

  public AudioSource audioSource;
  public List<SoundGroup> soundClips = new List<SoundGroup>();

  private void Awake()
  {
    if (!Instance)
    {
      Instance = this;
    }
  }

  public void PlaySound(string name)
  {
    SoundGroup soundGroup = soundClips.Find(sound => sound.name == name);
    if (soundGroup != null && soundGroup.soundClips.Count > 0)
    {
      AudioClip clip = soundGroup.soundClips[Random.Range(0, soundGroup.soundClips.Count)];
      audioSource.PlayOneShot(clip);
    }
  }
}
