using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audio_gameplay : MonoBehaviour
{
    //AudioSource audioSource;
    List<EnemyAI> enemiesChasing = new();

    [SerializeField]
    AudioSource NormalMusic;

    [SerializeField]
    AudioSource CombatMusic;

    void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = NormalMusic;
        //audioSource.Play();

        CombatMusic.Play();
        NormalMusic.Play();
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

            NormalMusic.volume = 0;
            CombatMusic.volume = 0.3f;
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

            CombatMusic.volume = 0;
            NormalMusic.volume = 0.3f;
        }
    }
}
