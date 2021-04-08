using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float credits;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }

        instance = this;
    }

    public void AddCredits(float amount)
    {
        credits += amount;
    }
}
