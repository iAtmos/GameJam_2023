using UnityEngine;

public class FPSController : MonoBehaviour
{
    [field: SerializeField] private int fps = 60;

    private void OnValidate()
    {
        Application.targetFrameRate = fps;
    }
}