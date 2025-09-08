using UnityEngine;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
    private GameObject player;

    public AudioClip moveSoundClip;

    private bool moveSoundPlaying = false;
    public AudioSource moveSoundSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GetComponent<PlayerControl>().isMoveState) {
            if (!moveSoundPlaying) {
                moveSoundSource.clip = moveSoundClip;
                moveSoundSource.loop = true;
                moveSoundSource.Play();
                moveSoundPlaying = true;
            }
        } else {
            if (moveSoundPlaying) {
                moveSoundSource.Stop();
                moveSoundPlaying = false;
            }
        }
    }
}
