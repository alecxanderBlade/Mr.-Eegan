using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingLayerControl : MonoBehaviour
{
    [SerializeField] private PostProcessVolume cam_effect;
    private DepthOfField blur;
    private void Start()
    {
        cam_effect.profile.TryGetSettings(out blur);
        StartCoroutine(Blur_Effects());
    }
    IEnumerator Blur_Effects()
    {
        for(int j = 0; j < 2; j++)
        {
            for (float i = 1.00f; i >= 0.1f;)
            {
                blur.focusDistance.value = i;
                yield return new WaitForSeconds(0.01f);
                i -= 0.02f;
            }
            for (float i = 0.01f; i <= 1.0f;)
            {
                blur.focusDistance.value = i;
                yield return new WaitForSeconds(0.01f);
                i += 0.02f;
            }
        }   
    }
}
