using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundSource, gameOverScene, sfxSource, playBackGround,settingSource;
    [SerializeField]
    AudioClip backgroundClip, playSceneClip, bossFightClip, playerShootClip, bossShootClip, boomExplosionClip, bossHitClip, bossLaugh, enemyShoot, bossDeath,
                                enemyDeath, enemyDeathbyG, playerDeath, mission1, reload, gettingHitClip, knifeDeath, gettingHitByKnife,victoryClip,settingClip;
                                
    public static AudioManager instance;

    private void Awake()
    {
        backgroundSource.PlayOneShot(backgroundClip);
        sfxSource.Play();
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySettingClip()
    {
        settingSource.PlayOneShot(settingClip);
    }
    public void StopSettingClip()
    {
        settingSource.Stop();
    }
    public void StopBackgroundMusic()
    {
        if (backgroundSource != null)
            backgroundSource.Stop();
        else
            Debug.LogWarning("backgroundSource chưa được gán.");
    }

    public void StopPlaySceneClip()
    {
        playBackGround.Stop();
    }
    public void PlaySceneBackGround()
    {
        playBackGround.PlayOneShot(playSceneClip);
    }

    public void VictorySound()
    {
        PlaySFX(victoryClip);
    }
    public void GettingHit()
    {
        PlaySFX(gettingHitClip);
    }
    public void Reload()
    {
        PlaySFX(reload);
    }
    public void BossDeath()
    {
        PlaySFX(bossDeath);
    }
    public void BossFight()
    {
        PlaySFX(bossFightClip);
    }

    public void PlayerShootClip()
    {
        PlaySFX(playerShootClip);
    }

    public void BossShootClip()
    {
        PlaySFX(bossShootClip);
    }



    public void BoomExplosionClip()
    {
        PlaySFX(boomExplosionClip);
    }

    public void BossHitClip()
    {
        PlaySFX(bossHitClip);
    }



    public void BossLaugh()
    {
        PlaySFX(bossLaugh);
    }

    public void EnemyShoot()
    {
        PlaySFX(enemyShoot);
    }
    public void EnemyDeath()
    {
        PlaySFX(enemyDeath);
    }

    public void EnemyDeathByG()
    {
        PlaySFX(enemyDeathbyG);
    }
    public void PlayerDeath()
    {
        PlaySFX(playerDeath);
    }

    public void Mission1()
    {
        PlaySFX(mission1);
    }
    public void KnifeDeath()
    {
        PlaySFX(knifeDeath);
    }
    public void GettingHitByKnife()
    {
        PlaySFX(gettingHitByKnife);
    }
  

}
