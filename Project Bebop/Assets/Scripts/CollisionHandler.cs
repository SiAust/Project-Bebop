using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 3f;
    [SerializeField] float levalLoadDelay = 3f;
    AudioSource audioSource;
    // Audio clips
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip collisionClip;

    [SerializeField] ParticleSystem winParticles;
    [SerializeField] ParticleSystem collisionParticles;
    Rigidbody rocketRigidbody;
    bool isTransitioning = false;
    int currentLevel;
    public static bool collisionDisabled;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        rocketRigidbody = GetComponent<Rigidbody>();
        collisionDisabled = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collisionDisabled=" + collisionDisabled);
        if (isTransitioning || collisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Respawn, why are you landing here?");
                break;
            case "Finish":
                Debug.Log("Finished, well done!");
                WinSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel, economy is not great.");
                break;
            case "Enemy":
                Debug.Log("Arghhhhh!!");
                StartCrashSequence();
                break;
            case "Obstacle":
                Debug.Log("Obstacles are annoying!");
                CollisionSequence();
                break;
            default:
                CollisionSequence();
                break;
        }

    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionClip);
        collisionParticles.Play();
        // Delay restart by 1 second
        Invoke("ReloadLevel", crashDelay);
    }

    void CollisionSequence()
    {
        audioSource.PlayOneShot(collisionClip);
    }

    void WinSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        rocketRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        audioSource.Stop();
        audioSource.PlayOneShot(winClip);
        winParticles.Play();
        Invoke("LoadNextLevel", 3);

    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    void LoadNextLevel()
    {
        int nextLevel;
        if (currentLevel + 1 == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        else
        {
            nextLevel = currentLevel + 1;
        }
        SceneManager.LoadScene(nextLevel);
    }

}
