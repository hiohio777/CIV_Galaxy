using UnityEngine;

public class SoundEffect : NoRegisterMonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;
    public void Play() => PlaySoundEffect(soundEffect);
}