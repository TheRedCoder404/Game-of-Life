using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace DefaultNamespace
{
    public static class ComputeShaderHelpers
    {
        // Compute Shader Helper functions from Sebastian Lague!!

        public const FilterMode defaultFilterMode = FilterMode.Bilinear;
        public const GraphicsFormat defaultGraphicsFormat = GraphicsFormat.R32G32B32A32_SFloat;
        
        public static void Dispatch(this ComputeShader computeShader, Vector3Int numIterations, int kernalIndex = 0)
        {
            Dispatch(computeShader, numIterations.x, numIterations.y, numIterations.z, kernalIndex);
        }
        
        public static void Dispatch(this ComputeShader computeShader, int x, int y = 1, int z = 1, int kernalIndex = 0)
        {
            Vector3Int threadGroupSize = GetThreadGroupSizes(computeShader, kernalIndex);
            int numGroupX = Mathf.CeilToInt(x / (float)threadGroupSize.x);
            int numGroupY = Mathf.CeilToInt(y / (float)threadGroupSize.y);
            int numGroupZ = Mathf.CeilToInt(z / (float)threadGroupSize.z);
            computeShader.Dispatch(kernalIndex, numGroupX, numGroupY, numGroupZ);
        }

        private static Vector3Int GetThreadGroupSizes(ComputeShader computeShader, int kernalIndex = 0)
        {
            uint x, y, z;
            computeShader.GetKernelThreadGroupSizes(kernalIndex, out x, out y, out z);
            return new Vector3Int((int)x, (int)y, (int)z);
        }

		public static RenderTexture CreateRenderTexture(int width, int height, FilterMode filterMode, GraphicsFormat format, string name = "Unnamed", DepthTextureMode depthMode = DepthTextureMode.None, bool useMipMaps = false)
		{
			RenderTexture texture = new RenderTexture(width, height, (int)depthMode);
			texture.graphicsFormat = format;
			texture.enableRandomWrite = true;
			texture.autoGenerateMips = false;
			texture.useMipMap = useMipMaps;
			texture.Create();

			texture.name = name;
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.filterMode = filterMode;
			return texture;
		}

		public static RenderTexture CreateRenderTexture(int width, int height)
		{
			return CreateRenderTexture(width, height, defaultFilterMode, defaultGraphicsFormat);
		}
    }
}