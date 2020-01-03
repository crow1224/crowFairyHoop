using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    //[SerializeField] private float m_timeAcceleration = 5.0f;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F5)/* ||
            Input.GetKeyDown("joystick button 6")*/)
        {
            SceneLoading(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.P)/* ||
            Input.GetKeyDown("joystick button 7")*/)
        {
            Time.timeScale = Time.timeScale <= 0.0f ? 1.0f : 0.0f;
        }

        //if (Time.timeScale > 0.0f)
        //{
        //    Time.timeScale = Input.GetKey(KeyCode.Comma) ? m_timeAcceleration : 1.0f;
        //}
#endif

#if !UNITY_EDITOR && UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }

    //----------------------------------------------------------------------------------
    private void SceneLoading(string scene_name)
    {
        Debug.Log("'" + scene_name + "' was Reloaded.");
        SceneManager.LoadScene(scene_name);
    }
}
