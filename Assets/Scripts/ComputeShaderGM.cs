using System;
using DefaultNamespace;
using UnityEngine;

public class ComputeShaderGM : MonoBehaviour
{
    [SerializeField] private ComputeShader computeShader;
    [SerializeField] private Collider2D planeCollider;
    [SerializeField] private SpriteRenderer planeRenderer;
    [SerializeField] private GameObject plane;
    [SerializeField] private Camera cam;
    
    public int resolution;
    private bool hasUvPrev
    
    private RenderTexture renderTexture;
    private void Awake()
    {
        float planeWidth = plane.transform.localScale.z;
        float planeHeight = plane.transform.localScale.x;
        renderTexture = ComputeShaderHelpers.CreateRenderTexture(Mathf.CeilToInt(resolution * (planeWidth / planeHeight)), resolution);
        planeRenderer.material.mainTexture = renderTexture;
        
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.SetTexture(1, "Result", renderTexture);
        computeShader.SetInts("Resolution", renderTexture.width, renderTexture.height);
        ComputeShaderHelpers.Dispatch(computeShader, renderTexture.width, renderTexture.height, 1, kernalIndex:0);
    }

    // Update is called once per frame
    void Update()
    {
        bool mouseIsDown = Input.GetMouseButton(0);
        if (!mouseIsDown) hasUvPrev = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        planeCollider.ray
    }
}
