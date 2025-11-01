using UnityEngine;

public class playerAudio : MonoBehaviour
{
    
    AudioSource audioSource;
    
    [SerializeField] AudioClip pasos;

    [SerializeField] AudioClip vozPrincesaAtaque;

    [SerializeField] AudioClip saltoPrincesa;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        //float x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Horizontal"))
        {
            audioSource.clip = pasos;
            audioSource.Play();
        }
        if (Input.GetButtonDown("Jump"))
        {
            audioSource.clip = saltoPrincesa;
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            audioSource.clip = vozPrincesaAtaque;
            audioSource.Play();
        }

    }
}
