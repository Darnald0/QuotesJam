using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Anything sound related
/// </summary>
public class SoundManager : MonoBehaviour
{
    #region Singleton

    public static SoundManager Instance;

    #endregion

    #region Classes and Enums

    /// <summary>
    /// A single sound effect (And related behaviours)
    /// </summary>
    [Serializable]
    public class SoundEffect
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioClip File { get; private set; }

        protected SoundManager currentManager;
        protected AudioSource currentSource;
        protected float currentVolume;

        // Events
        protected event Action OnEndOfSound;

        // Coroutines
        protected Coroutine COR_WaitForEndOfSound;

        /// <summary>
        /// Play sound effect
        /// </summary>
        /// <param name="manager">Calling manager</param>
        /// <param name="source">Assigned audiosource</param>
        /// <param name="volume">Volume</param>
        /// <param name="callback">Callback on end of playback</param>
        public virtual void Play(SoundManager manager, AudioSource source, float volume, Action callback)
        {
            Stop(false);

            currentManager = manager;
            currentSource = source;
            currentVolume = volume;

            if (callback != null)
            {
                OnEndOfSound += callback;
                if (File != null)
                {
                    COR_WaitForEndOfSound = manager.StartCoroutine(COR_EndOfSound(File.length));
                }
                else
                {
                    OnEndOfSound?.Invoke();
                    OnEndOfSound = null;
                }
            }

            if (File != null)
            {
                source.volume = volume;
                source.clip = File;
                source.Play();
            }
        }

        /// <summary>
        /// Stop playback
        /// </summary>
        /// <param name="hard">If true, does not invoke end of sound callbacks</param>
        public virtual void Stop(bool hard)
        {
            if (hard) OnEndOfSound = null; // Remove any callbacks

            if (currentSource != null) currentSource.Stop();

            OnEndOfSound?.Invoke(); // Play all subscribed actions

            RemoveCallbacks();
        }

        /// <summary>
        /// Remove assigned callbacks to current playback
        /// </summary>
        public virtual void RemoveCallbacks()
        {
            OnEndOfSound = null; // Remove all actions from list

            // Remove potential delayed callback coroutines
            if (COR_WaitForEndOfSound != null)
            {
                currentManager.StopCoroutine(COR_WaitForEndOfSound); // Remove duplicate behavior, that would usually occur on SFX end
                COR_WaitForEndOfSound = null;
            }
        }

        /// <summary>
        /// Call action callback after delay
        /// </summary>
        /// <param name="delay">Delay before callback</param>
        /// <returns></returns>
        protected IEnumerator COR_EndOfSound(float delay)
        {
            yield return new WaitForSeconds(delay);

            OnEndOfSound?.Invoke();
            OnEndOfSound = null;
        }
    }

    /// <summary>
    /// A single sound effect (With subtitles, specified behaviours)
    /// </summary>
    [Serializable]
    public class SoundEffectWithSubs : SoundEffect
    {
        [field: SerializeField] public List<Subtitles> Subs { get; private set; }

        public Coroutine COR_Subtitles;

        public override void Play(SoundManager manager, AudioSource source, float volume, Action callback)
        {
            base.Play(manager, source, volume, callback);

            COR_Subtitles = manager.StartCoroutine(COR_DisplaySubtitles(Subs));
        }

        public override void Stop(bool hard)
        {
            base.Stop(hard);

            if (currentManager != null && COR_Subtitles != null) // Remove running subtitles coroutines
            {
                currentManager.StopCoroutine(COR_Subtitles); // Stop the subtitles coroutine
                COR_Subtitles = null;
            }
        }

        /// <summary>
        /// Display of subtitles
        /// </summary>
        /// <param name="subs">Subtitles to be displayed</param>
        /// <returns></returns>
        protected IEnumerator COR_DisplaySubtitles(List<Subtitles> subs)
        {
            if (subs == null) yield break;

            int currentSubIndex = 0;

            while (currentSubIndex < subs.Count)
            {

                if (currentSubIndex + 1 < subs.Count)
                    yield return new WaitForSeconds(subs[currentSubIndex].Duration);
                else
                    yield break;

                currentSubIndex++;
            }
        }

        /// <summary>
        /// Subtitles serialization
        /// </summary>
        [Serializable]
        public class Subtitles
        {
            [field: SerializeField] public string Line { get; private set; }
            [field: SerializeField] public float Duration { get; private set; }
        }
    }

    /// <summary>
    /// Sound effect types
    /// </summary>
    public enum DefaultTypes
    {
        Any,
        Clips,
        SubbedClips
    }

    #endregion

    #region Members

    [SerializeField] private bool _volumeToggle;
    public bool VolumeToggle
    {
        get { return _volumeToggle; }
        private set
        {
            _volumeToggle = value;

            PlayerPrefs.SetInt("VolumeToggle", (_volumeToggle ? 1 : 0));

            if (VolumeToggle)
            {
                SFX.volume = 1f;
                Music.volume = 1f;
            }
            else
            {
                SFX.volume = 0f;
                Music.volume = 0f;
            }
        }
    }

    // Audio sources
    [SerializeField] private AudioSource Music;
    [SerializeField] private AudioSource SFX;

    // Clips by type
    [SerializeField] private List<SoundEffect> Clips;
    [SerializeField] private List<SoundEffectWithSubs> SubbedClips;

    // Currently playing SFX (One on this sprcific audiosource)
    private SoundEffect currentSFX;

    #endregion

    #region Monobehaviours

    private void Awake()
    {
        // DEBUG SETTINGS (Since we're not building with GameManager, as we dont have a menu)
        // Setting up frame rate
        //Application.targetFrameRate = 144;
        // Disabing vSync (Only uncomment if needed)
        //QualitySettings.vSyncCount = 0;

        // Prevent duplication of SoundManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        Instance = this;

        if (PlayerPrefs.HasKey("VolumeToggle"))
        {
            VolumeToggle = (PlayerPrefs.GetInt("VolumeToggle") == 1);
        }
        else
        {
            VolumeToggle = true;
            PlayerPrefs.SetInt("VolumeToggle", 1);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        Music.Play();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Get a sound by name and type
    /// </summary>
    /// <param name="soundName">Sound name</param>
    /// <param name="type">Sound type</param>
    /// <returns>SFX</returns>
    private SoundEffect GetSound(string soundName, DefaultTypes type = DefaultTypes.Any)
    {
        List<SoundEffect> searchList = new List<SoundEffect>();
        GetListByType(type, searchList);

        foreach (SoundEffect sfx in searchList)
        {
            if (sfx.Name.Trim().Equals(soundName.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return sfx;
            }
        }

        Debug.LogError("Sound not found - " + soundName);
        return null;
    }

    /// <summary>
    /// Get a random sound by type
    /// </summary>
    /// <param name="type">Sound type</param>
    /// <returns>SFX</returns>
    private SoundEffect GetRandomSound(DefaultTypes type)
    {
        List<SoundEffect> searchList = new List<SoundEffect>();
        GetListByType(type, searchList);

        if (searchList.Count > 0)
        {
            System.Random rng = new System.Random();
            int random = rng.Next(0, searchList.Count);

            return searchList[random];
        }

        Debug.LogError("Random sound not found for type - " + type);
        return null;
    }

    /// <summary>
    /// Get list corresponding to requested sound type
    /// </summary>
    /// <param name="type">Sound type</param>
    /// <param name="searchList">Sound list returned</param>
    private void GetListByType(DefaultTypes type, List<SoundEffect> searchList)
    {
        switch (type)
        {
            case DefaultTypes.Clips:
                searchList.AddRange(Clips);
                break;
            case DefaultTypes.SubbedClips:
                searchList.AddRange(SubbedClips);
                break;
            default:
                searchList.AddRange(Clips);
                break;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Stop currently playing SFX (Single sound at once on this audio source)
    /// </summary>
    /// <param name="hard">If false callbacks will be played as if it ended normally, otherwise they will be dismissed</param>
    public void StopSFX(bool hard)
    {
        if (currentSFX != null) currentSFX.Stop(hard);
    }

    /// <summary>
    /// Remove cazllbacks on current SFX
    /// </summary>
    public void RemoveCallbacksSFX()
    {
        if (currentSFX != null) currentSFX.RemoveCallbacks();
    }

    /// <summary>
    /// Play an SFX (Single sound at once on this audio source)
    /// </summary>
    /// <param name="soundName">Name of requested sound</param>
    /// <param name="callback">Callback on end of playback</param>
    /// <param name="type">Requested SFX type</param>
    /// <param name="volume">Volume</param>
    public void PlaySFX(string soundName, Action callback = null, DefaultTypes type = DefaultTypes.Any, float volume = 1f)
    {
        StopSFX(false);

        currentSFX = GetSound(soundName, type);

        if (currentSFX != null)
        {
            currentSFX.Play(this, SFX, volume, callback);
        }
        else
        {
            callback?.Invoke(); // Callback immediately since no sound was found
        }
    }

    /// <summary>
    /// Play a random sound of specific type that cannot be stopped (Up to 20ish sounds at once probably)
    /// </summary>
    /// <param name="type">Requested type</param>
    /// <param name="volume">Volume</param>
    public void PlayRandomSFXOneShot(DefaultTypes type, float volume = 1f)
    {
        SoundEffect sfx = GetRandomSound(type);

        if (sfx != null) SFX.PlayOneShot(sfx.File, volume);
    }

    /// <summary>
    /// Play a specific sound that cannot be stopped (Up to 20ish sounds at once probably)
    /// </summary>
    /// <param name="soundName">Name of requested sound</param>
    /// <param name="type">Requested type</param>
    /// <param name="volume">Volume</param>
    public void PlaySFXOneShot(string soundName, DefaultTypes type = DefaultTypes.Any, float volume = 1f)
    {
        SoundEffect sfx = GetSound(soundName, type);

        if (sfx != null) SFX.PlayOneShot(sfx.File, volume);
    }

    /// <summary>
    /// Toggle volume on/off (Flip switch)
    /// </summary>
    public void ToggleVolume()
    {
        VolumeToggle = !VolumeToggle;
    }

    /// <summary>
    /// Change music volume
    /// </summary>
    /// <param name="vol">Volume</param>
    public void ChangeVolumeMusic(float vol)
    {
        Music.volume = vol;
    }

    /// <summary>
    /// Change SFX volume (Single sound at once)
    /// </summary>
    /// <param name="vol">Volume</param>
    public void ChangeVolumeSFX(float vol)
    {
        SFX.volume = vol;
    }

    #endregion

    #region Callbacks

    /// <summary>
    /// Scene loading behavior
    /// </summary>
    /// <param name="sc">Scene that is being loaded</param>
    /// <param name="mode">Mode for loading</param>
    private void OnSceneLoaded(Scene sc, LoadSceneMode mode)
    {
        StopSFX(true); // Stops hard, removing callbacks
    }

    #endregion
}
