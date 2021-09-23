using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera Camera3D;
    public int ImageSize;
    public Sprite Result;

    private RenderTexture mRenderTexture;

    public void SetTarget(Transform target)
    {
        mRenderTexture = new RenderTexture(ImageSize, ImageSize, 32);
        Camera3D.targetTexture = mRenderTexture;
        transform.SetParent(target, false);
        Camera3D.Render();
        transform.SetParent(null, false);
    }

    private void OnPostRender()
    {
        if (mRenderTexture != null)
        {
            RenderTexture.active = mRenderTexture;
            var virtualPhoto = new Texture2D(ImageSize, ImageSize, TextureFormat.RGBA32, false);
            virtualPhoto.ReadPixels(new Rect(0, 0, ImageSize, ImageSize), 0, 0);
            virtualPhoto.Apply();

            RenderTexture.active = null;
            Camera3D.targetTexture = null;

            Result = Sprite.Create(virtualPhoto, new Rect(Vector2.zero, new Vector2(ImageSize, ImageSize)), Vector2.zero);

            mRenderTexture = null;
        }
    }
}
