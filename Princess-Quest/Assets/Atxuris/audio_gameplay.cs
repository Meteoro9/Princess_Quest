using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audio_gameplay : MonoBehaviour
{
    //AudioSource audioSource;
    List<EnemyAI> enemiesChasing = new();

    private bool isShottingDown = false; // Para evitar crasheo

    [SerializeField]
    AudioSource NormalMusic; // se cambio audio clip por audio source

    [SerializeField]
    AudioSource CombatMusic; // el mismo cambio

    [SerializeField] float fadeDuration = 2.0f;

    [SerializeField]
    [Range(0f, 1f)] float targetVolume = 0.3f;

    private Coroutine normalMusicFade;
    private Coroutine combatMusicFade;

    private void OnDisable()
    {
        isShottingDown = true;
    }

    void Awake()
    {
        // Asignamos volumen de inicio
        NormalMusic.volume = targetVolume;
        CombatMusic.volume = 0f;

        // Se reproducen ambas, una en silencio
        CombatMusic.Play(); 
        NormalMusic.Play(); 
    }

    public void OnEnemyChasing(EnemyAI enemyAI)
    {
        if (isShottingDown) return;

        if (!enemiesChasing.Contains(enemyAI))
        {
            enemiesChasing.Add(enemyAI);
            
            StartFade(NormalMusic, 0f);
            StartFade(CombatMusic, targetVolume);

        }
    }

    public void OnEnemyStoppedChasing(EnemyAI enemyAI)
    {
        if (isShottingDown) return;

        if (enemiesChasing.Contains(enemyAI))
        {
            enemiesChasing.Remove(enemyAI);
        }

        if (enemiesChasing.Count == 0) // Bajar música de combate y subir exploración
        {
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
