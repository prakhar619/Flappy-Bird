using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private float RestartDelay = 1f;
    [SerializeField] private bool EndGame_bool;
    public GameObject RetryObj;
    public void EndGame()
    {
        RetryObj.SetActive(true);
        Invoke("Invokin", RestartDelay);
    }

    public void Invokin()
    {
        EndGame_bool = true;
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        RetryObj.SetActive(false);
    }

    void Start()
    {
        RetryObj = GameObject.Find("Retry");
        RetryObj.SetActive(false);
    }

    void Awake()
    {
        EndGame_bool = false;
    }

    void Update()
    {
        if(EndGame_bool == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // Invoke("Restart",RestartDelay);
                Restart();
            }
        }
    }
}
