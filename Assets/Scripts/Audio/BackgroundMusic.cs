using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public static BackgroundMusic Main
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackgroundMusic();
            }
            return _instance;
        }
    }

    private bool musicPlaying = true;
    private static float randomSongThresholdDistance = 90f;
    private static float maxMusicVol = .65f;


    public AudioClip currentSong = null;

    private AudioClip homeBaseSong;
    private AudioClip fortSong;
    private AudioClip swampSong;
    private AudioClip farmSong;
    private AudioClip victorySong;
    private AudioClip defeatSong;
    private ArrayList genericSongs = new ArrayList();

    private GameObject blueBase;
    private GameObject BlueBase
    {
        get
        {
            if (blueBase == null)
                blueBase = GameObject.Find("Blue Base Control Point");
            return blueBase;
        }
    }
    private GameObject fortress;
    private GameObject Fortress
    {
        get
        {
            if (fortress == null)
                fortress = GameObject.Find("Fortress Control Point");
            return fortress;
        }
    }
    private GameObject farm;
    private GameObject Farm
    {
        get
        {
            if (farm == null)
                farm = GameObject.Find("Farm Control Point");
            return farm;
        }
    }
    private GameObject swamp;
    private GameObject Swamp
    {
        get
        {
            if (swamp == null)
                swamp = GameObject.Find("Swamp Control Point");
            return swamp;
        }
    }
    private GameObject redBase;
    private GameObject RedBase
    {
        get
        {
            if (redBase == null)
                redBase = GameObject.Find("Red Base Control Point");
            return redBase;
        }
    }

    private GameObject homeBase;
    private GameObject HomeBase
    {
        get
        {
            if (Util.MyLocalPlayerTeam == 1) 
                return BlueBase;
            else
                return RedBase;
        }
    }

    private GameObject enemyBase;
    private GameObject EnemyBase
    {
        get
        {
            if (Util.MyLocalPlayerTeam == 1)
                return RedBase;
            else
                return BlueBase;
        }
    }

    void Start()
    {
        fortSong = Resources.Load("BGM/Pitched Conflict") as AudioClip;
        swampSong = Resources.Load("BGM/Swamp") as AudioClip;
        farmSong = Resources.Load("BGM/But Where Are The Farmers") as AudioClip;

        genericSongs.Add(Resources.Load("BGM/Main Background Rhythm") as AudioClip);
        genericSongs.Add(Resources.Load("BGM/Far Too Many Trees") as AudioClip);
        genericSongs.Add(Resources.Load("BGM/Among Mountains") as AudioClip);

        victorySong = Resources.Load("BGM/Anxious Victory") as AudioClip;
        defeatSong = Resources.Load("BGM/Next Time We'll Win") as AudioClip;

        StartCoroutine(MonitorBackgroundMusic());
    }

    private IEnumerator MonitorBackgroundMusic()
    {
        while (musicPlaying)
        {
            while (Util.MyLocalPlayerObject == null)
                yield return null;
            if (!audio.isPlaying)
                audio.Play();

            yield return new WaitForSeconds(2.0f);
            GameObject closestControlPoint = FindClosestControlPoint(Util.MyLocalPlayerObject.transform.position);
            if (closestControlPoint == null) continue;

            if (Vector3.Distance(Util.MyLocalPlayerObject.transform.position, closestControlPoint.transform.position) >= randomSongThresholdDistance)
            {
                CrossfadeRandomSong();
            }
            else
            {
                CrossfadeAreaSong(closestControlPoint.name);
            }


        }
    }

    private void CrossfadeAreaSong(string closestPointName)
    {
        switch (closestPointName)
        {
            case "Red Base Control Point":
            case "Blue Base Control Point":
                CrossfadeRandomSong();
                break;
            case "Fortress Control Point":
                StartCoroutine(Crossfade(fortSong));
                break;
            case "Swamp Control Point":
                StartCoroutine(Crossfade(swampSong));
                break;
            case "Farm Control Point":
                StartCoroutine(Crossfade(farmSong));
                break;
            default:
                Debug.LogError("Invalid point name for song selection.");
                break;
        }
    }

    private IEnumerator Crossfade(AudioClip songToFade)
    {
        if (currentSong == songToFade) yield break;
        currentSong = songToFade;
        StartCoroutine(Util.FadeOutSoundInSeconds(audio, 2.0f));
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(Util.FadeInSoundInSeconds(audio, songToFade, maxMusicVol, 5.0f));
        yield break;
    }

    private void CrossfadeRandomSong()
    {
        if (genericSongs.Contains(currentSong)) return;

        int rand = Random.Range(0, genericSongs.Count);
        StartCoroutine(Crossfade(genericSongs[rand] as AudioClip));
    }

    private GameObject FindClosestControlPoint(Vector3 playerPosition)
    {
        if (HomeBase == null)
            return null;

        GameObject closestPoint = null;

        SortedList listOfDistances = new SortedList();
        listOfDistances.Add(Vector3.Distance(playerPosition, HomeBase.transform.position), HomeBase);
        listOfDistances.Add(Vector3.Distance(playerPosition, EnemyBase.transform.position), EnemyBase);
        listOfDistances.Add(Vector3.Distance(playerPosition, Fortress.transform.position), Fortress);
        listOfDistances.Add(Vector3.Distance(playerPosition, Swamp.transform.position), Swamp);
        listOfDistances.Add(Vector3.Distance(playerPosition, Farm.transform.position), Farm);

        closestPoint = listOfDistances.GetByIndex(0) as GameObject;

        return closestPoint;
    }

    void Update()
    {

    }
}