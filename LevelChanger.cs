using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public GameObject PortalPrefab;
    public GameObject PlayerPrefab;
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject temp = Instantiate(PortalPrefab, new Vector3(0f, 0, 0), Quaternion.identity);
            Destroy(temp, 2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }
}
