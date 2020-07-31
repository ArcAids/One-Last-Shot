using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class TweenSpriteFlash : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float duration;
    [SerializeField] int frequency;

    SpriteRenderer renderer;
    Color baseColor;

    private void Awake()
    {
        TryGetComponent(out renderer);
        baseColor = renderer.color ;
    }

    public void Flash()
    {
        Debug.Log("Flash!");
        Flash(duration, frequency);
    }

    public void Flash(float duration, int frequency)
    {
        renderer.DOColor(color,duration).SetEase(Ease.Flash).SetLoops(frequency,LoopType.Restart).OnComplete(ResetColor);

    }

    void ResetColor()
    {
        renderer.color = baseColor; 
    }
}
