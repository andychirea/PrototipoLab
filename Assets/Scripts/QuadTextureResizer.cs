using UnityEngine;

public class QuadTextureResizer : MonoBehaviour
{
    [SerializeField] private float _height = 1.7f;

    private void OnEnable() => ResizeNow();

    [ContextMenu("ResizeNow")]
    public void ResizeNow()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            var texture = renderer.sharedMaterial.mainTexture;

            if (texture == null)
            {
                Debug.Log($"Could not find texture!");
                return;
            }

            var ratio = (float)texture.width / texture.height;

            transform.localScale = new Vector3(ratio * _height, _height, 1f);
        }
        else
        {
            Debug.Log("Could not find renderer!");
        }
    }
}