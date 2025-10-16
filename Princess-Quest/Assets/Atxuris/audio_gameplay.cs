using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audio_gameplay : MonoBehaviour
{
    AudioSource audioSource;
    List<EnemyAI> enemiesChasing = new();

    [SerializeField]
    AudioClip NormalMusic;

    [SerializeField]
    AudioClip CombatMusic;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = NormalMusic;
        audioSource.Play();
    }

    public void OnEnemyChasing(EnemyAI enemyAI)
    {
        if (!enemiesChasing.Contains(enemyAI))
        {
            enemiesChasing.Add(enemyAI);
            if (audioSource.clip != CombatMusic)
            {
                audioSource.clip = CombatMusic;
                audioSource.Play();
            }
        }
    }

    public void OnEnemyStoppedChasing(EnemyAI enemyAI)
    {
        if (enemiesChasing.Contains(enemyAI))
        {
            enemiesChasing.Remove(enemyAI);
        }

        if (enemiesChasing.Count == 0)
        {
            audioSource.clip = NormalMusic;
            audioSource.Play();
        }
    }
}
