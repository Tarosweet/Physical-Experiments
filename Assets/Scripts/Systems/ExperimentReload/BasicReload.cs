using UnityEngine.SceneManagement;

namespace Systems.ExperimentReload
{
    public class BasicReload : ExperimentReloader
    {
        public override void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
