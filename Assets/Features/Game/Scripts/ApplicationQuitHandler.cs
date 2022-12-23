using UnityEngine;
using Zenject;

namespace Assets.Features.Game.Scripts
{
    public class ApplicationQuitHandler : MonoBehaviour
    {
        private ApplicationStartup _applicationStartup;

        [Inject]
        private void Inject(ApplicationStartup applicationStartup)
        {
            _applicationStartup = applicationStartup;
        }

        private void OnApplicationQuit()
        {
            _applicationStartup.Dispose();
        }
    }
}