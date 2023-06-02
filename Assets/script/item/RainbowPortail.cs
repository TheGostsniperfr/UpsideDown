using UnityEngine;

public class RainbowPortail : MonoBehaviour
{
    private Renderer targetRenderer;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float saturation = 1f;
    [SerializeField] private float value = 1f;

    private float timeOffset;
    private MaterialPropertyBlock propertyBlock;

    private int dotColorPropertyID;

    private void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
            if (targetRenderer == null)
            {
                Debug.LogError("No material");
            }
        }

        propertyBlock = new MaterialPropertyBlock();
        dotColorPropertyID = Shader.PropertyToID("_DotColor");

        timeOffset = Random.value;
    }

    private void Update()
    {
        float time = Time.time * speed + timeOffset;
        Color color = Color.HSVToRGB(time % 1f, saturation, value);

        propertyBlock.SetColor(dotColorPropertyID, color);
        targetRenderer.SetPropertyBlock(propertyBlock);
    }
}
