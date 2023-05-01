using UnityEngine;
using AppsFlyerSDK;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private UIController UIController;
    [SerializeField] private Text scoreText;
    [SerializeField] private float forceImpulseToPlayer;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private Button startButton;

    [SerializeField] private AudioSource swapSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource dieSound;
    [SerializeField] private AudioSource addOneScore;

    private bool _isDied = true;
    private bool _canTakeScore = false;

    private int count = 0;

    private Rigidbody2D _rb;
    private Animator _anim;

    private Vector2 _vector = new Vector2(0f, 0.5f);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isDied)
                MoveToUpper();
        }
    }

    private void Awake()
    {
        _isDied = false;
        startButton.gameObject.SetActive(true);
        deathScreen.SetActive(false);
        startScreen.SetActive(true);
        AppsFlyer.getConversionData(name);
        print("onInstallConversionData: " + name.ToString());
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        count = 0;
        scoreText.text = "Очки: " + count.ToString();
    }

    public void StartGame()
    {
        levelGenerator.StartLevel();
        startScreen.SetActive(false);
        _anim.enabled = true;
        _canTakeScore = true;
        _rb.WakeUp();
        _rb.constraints = RigidbodyConstraints2D.None;
        UIController.StartGameButtonFalse();
        deathScreen.SetActive(false);
    }

    public void MoveToUpper()
    {
        _rb.AddForce(_vector, ForceMode2D.Impulse);
        swapSound.Play();
    }

    public void LoseGame()
    {
        _isDied = true;
        hitSound.Play();
        _rb.mass = 0;
        dieSound.Play();
        levelGenerator.StopLevel();
        deathScreen.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GameObject1 collided with " + col.name);
        if (col.gameObject.tag == "OneScore")
        {
            if (_canTakeScore)
            {
                addOneScore.Play();
                count++;
                scoreText.text = "Очки: " + count.ToString();
            }
        }

        if (col.gameObject.tag == "LoseBird")
        {
            if (!_isDied)
                LoseGame();
        }
    }
}
