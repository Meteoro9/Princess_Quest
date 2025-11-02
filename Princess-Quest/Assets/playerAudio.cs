using UnityEngine;

public class playerAudio : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip pasos;

    [SerializeField]
    AudioClip vozPrincesaAtaque;

    [SerializeField]
    AudioClip saltoPrincesa;

    [SerializeField]
    PlayerMovement playerMovement;

    [SerializeField]
    AttackComponent attackComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        attackComponent.AttackStarted += PlayAttack;
    }

    // Update is called once per frame
    private void Update()
    {
        //float x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Horizontal") && playerMovement.IsGrounded)
        {
            audioSource.clip = pasos;
            audioSource.Play();
        }
        if (Input.GetButtonDown("Jump") && playerMovement.IsGrounded)
        {
            audioSource.clip = saltoPrincesa;
            audioSource.Play();
        }
        /*         if (IsAttackKeyPressed() && !attackComponent.IsAttacking)
                {
                    audioSource.clip = vozPrincesaAtaque;
                    audioSource.Play();
                } */
    }

    void PlayAttack(AttackSO attackSO)
    {
        audioSource.clip = vozPrincesaAtaque;
        audioSource.Play();
    }

    bool IsAttackKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Z)
            || Input.GetKeyDown(KeyCode.X)
            || Input.GetKeyDown(KeyCode.J)
            || Input.GetKeyDown(KeyCode.K);
    }
}
