using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audio_gameplay : MonoBehaviour
{
    //AudioSource audioSource;
    List<EnemyAI> enemiesChasing = new();

    [SerializeField]
    AudioSource NormalMusic; // se cambio audio clip por audio source

    [SerializeField]
    AudioSource CombatMusic; // el mismo cambio

    [SerializeField] float fadeDuration = 2.0f;

    [SerializeField]
    [Range(0f, 1f)] float targetVolume = 0.3f;

    private Coroutine normalMusicFade;
    private Coroutine combatMusicFade;

    void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = NormalMusic;
        //audioSource.Play();

        // Asignamos volumen de inicio
        NormalMusic.volume = targetVolume;
        CombatMusic.volume = 0f;

        // Se reproducen ambas, una en silencio
        CombatMusic.Play(); // nuevo
        NormalMusic.Play(); // nuevo
    }

    public void OnEnemyChasing(EnemyAI enemyAI)
    {
        if (!enemiesChasing.Contains(enemyAI))
        {
            enemiesChasing.Add(enemyAI);
            /*if (audioSource.clip != CombatMusic) // Subir volumen música de combate y bajar exploración
            {
                //audioSource.clip = CombatMusic;
                
                audioSource.Play();
            }*/

            //NormalMusic.volume = 0; // nuevo
            //CombatMusic.volume = 0.3f; // nuevo

            StartFade(NormalMusic, 0f);
            StartFade(CombatMusic, targetVolume);

        }
    }

    public void OnEnemyStoppedChasing(EnemyAI enemyAI)
    {
        // Console shows error msg if this check isnt here
        /*if (!audioSource)
        {
            return;
        }*/
        if (enemiesChasing.Contains(enemyAI))
        {
            enemiesChasing.Remove(enemyAI);
        }

        if (enemiesChasing.Count == 0) // Bajar música de combate y subir exploración
        {
            //audioSource.clip = NormalMusic;
            //audioSource.Play();

            //CombatMusic.volume = 0; // nuevo
            //NormalMusic.volume = 0.3f; // nuevo

            StartFade(NormalMusic, targetVolume);
            StartFade(CombatMusic, 0f);
        }


    }

    void StartFade(AudioSource source, float targetVol)
    {
        // Detenemos cualquier corrutina actual:        
        if (source == NormalMusic && normalMusicFade != null)
        {
            StopCoroutine(normalMusicFade);
        }
        else if (source == CombatMusic && combatMusicFade != null)
        {
            StopCoroutine(combatMusicFade);
        }

        // Iniciamos la nueva corrutina correspondiente
        Coroutine newFade = StartCoroutine(FadeAudio(source, targetVol, fadeDuration));

        if (source == NormalMusic)
            normalMusicFade = newFade;
        else if (source == CombatMusic)
            combatMusicFade = newFade;

    }

    IEnumerator FadeAudio(AudioSource source, float targetVol, float duration)
    {
        float timer = 0f;
        float startVolume = source.volume;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = timer / duration;

            source.volume = Mathf.Lerp(startVolume, targetVol, progress);

            yield return null;
        }

        source.volume = targetVol;

        if (source == NormalMusic)
            normalMusicFade = null;
        else if (source == CombatMusic)
            combatMusicFade = null;
    }
}
