using UnityEngine.SceneManagement;
public static class SceneController 
{
    /// <summary>
    /// Restarts the scene
    /// </summary>
    public static void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}