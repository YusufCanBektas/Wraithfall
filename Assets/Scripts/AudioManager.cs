using UnityEngine;

namespace schmup
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Musik")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioClip menuMusic;
        [SerializeField] AudioClip levelMusic;

        [Header("SFX")]
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioClip shootSfx;
        [SerializeField] AudioClip hitSfx;
        [SerializeField] AudioClip explosionSfx;
        [SerializeField] AudioClip bossDefeatedSfx;
        [SerializeField] AudioClip buttonClickSfx;

        void Awake()
        {
            // Singleton: nur eine Instanz über alle Szenen hinweg
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMenuMusic() => PlayMusic(menuMusic);
        public void PlayLevelMusic() => PlayMusic(levelMusic);

        void PlayMusic(AudioClip clip)
        {
            if (clip == null || musicSource == null) return;
            if (musicSource.clip == clip && musicSource.isPlaying) return;

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayShoot() => PlaySfx(shootSfx);
        public void PlayHit() => PlaySfx(hitSfx);
        public void PlayExplosion() => PlaySfx(explosionSfx);
        public void PlayBossDefeated() => PlaySfx(bossDefeatedSfx);
        public void PlayButtonClick() => PlaySfx(buttonClickSfx);

        void PlaySfx(AudioClip clip)
        {
            if (clip == null || sfxSource == null) return;
            sfxSource.PlayOneShot(clip);
        }
    }
}