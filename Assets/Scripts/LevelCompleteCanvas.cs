using UnityEngine;

public class LevelCompleteCanvas : MonoBehaviour
{
    public void ReplayButtonPressed()
    {
        GameManager.Instance.ResetLevel();
        Destroy(gameObject);
    }

    public void NextLevelButtonPressed()
    {
        GameManager.Instance.LoadNextLevel();
    }
}
