using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadSound : MonoBehaviour
{
    //public AudioSource audio;
    void Awake()
    {

        DontDestroyOnLoad(transform.gameObject);
        
    }
}
