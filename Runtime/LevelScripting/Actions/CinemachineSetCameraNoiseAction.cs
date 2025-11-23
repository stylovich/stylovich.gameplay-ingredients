using UnityEngine;
using Unity.Cinemachine;
using TriInspector;

namespace GameplayIngredients.Actions
{
    [Callable("Cinemachine", "Misc/ic-cinemachine.png")]
    [AddComponentMenu(ComponentMenu.cinemachinePath + "Cinemachine Set Camera Noise Action")]

    public class CinemachineSetCameraNoiseAction : ActionBase
    {
        [SerializeField]
        bool useLiveCamera;
        [SerializeField, HideIf("useLiveCamera")]
        CinemachineCamera targetCamera;

        [SerializeField]
        NoiseSettings settings;

        public override void Execute(GameObject instigator = null)
        {
             CinemachineCamera cam = useLiveCamera ?
                Manager.Get<VirtualCameraManager>().GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineCamera
                : targetCamera;

            if(cam == null)
            {
                Debug.Log("CinemachineSetCameraNoiseAction : Cannot find a suitable CinemachineVirtualCamera to set Noise to");
                return;
            }

            var noise = cam.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;

            if(noise == null && settings != null)
                noise = cam.gameObject.AddComponent<CinemachineBasicMultiChannelPerlin>();

            noise.NoiseProfile = settings;
        }

        public override string GetDefaultName() => $"CM Set Noise ({settings.name}) for {(useLiveCamera? "Live Camera" : targetCamera?.gameObject.name)}";

    }

}

