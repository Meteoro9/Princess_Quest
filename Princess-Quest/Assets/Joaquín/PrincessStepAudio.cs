using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PrincessStepAduio : MonoBehaviour
{
    PlayerMovement pm;
    AudioSource audioSource;

    [SerializeField]
    float minRandomPitch = 0.9f;

    [SerializeField]
    float maxRandomPitch = 1.1f;

    float pitchTimer;

    void OnEnable()
    {
        pm = transform.parent.GetComponent<PlayerMovement>();
        pm.OnWalking += OnPlayerWalking;
        pm.OnStoppedWalking += OnPlayerStoppedWalking;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pitchTimer += Time.deltaTime;
        if (pitchTimer >= audioSource.clip.length * 2)
        {
            audioSource.pitch = Random.Range(minRandomPitch, maxRandomPitch);
        }
    }

    void OnPlayerWalking()
    {
        if (!audioSource.isPlaying && pm.IsGrounded)
        {
            audioSource.Play();
        }
    }

    void OnPlayerStoppedWalking()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
