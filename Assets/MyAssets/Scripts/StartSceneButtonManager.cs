using UnityEngine;
using UnityEngine.SceneManagement;
using SFB;

public class StartSceneButtonManager : MonoBehaviour
{
    public static string pdfFileName;

    public void OnClickedStartButton()
    {
        pdfFileName = LoadPDFFile();
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickedExitButton()
    {
        QuitSystem();
    }

    private string LoadPDFFile()
    {
        ExtensionFilter[] extensions = new[] {
            new ExtensionFilter("PDF File", "pdf"),
        };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open PDF File", "", extensions, false);
        if (paths[0] == "")
        {
            QuitSystem();
        }

        return paths[0];
    }

    private void QuitSystem()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }
}
