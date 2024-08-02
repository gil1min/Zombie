using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    public float moveDuration = 1f;
    public int SunPoint = 50;

    private void Start()
    {
        
    }

    public void LinearTo(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, moveDuration);
    }

    public void JumpTo(Vector3 targetPosition)
    {
        targetPosition.z = -1;
        Vector3 centerPosition = (transform.position + targetPosition) / 2f;
        float distance = Vector3.Distance(transform.position, targetPosition);
        centerPosition.y += distance / 2f;
        transform.DOPath(new Vector3[] { transform.position, centerPosition, targetPosition },
            moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    public void OnMouseDown()
    {
        transform.DOMove(SunManager.Instance.GetSunPointTextPosition(), moveDuration).OnComplete(
            () =>
            {
                Destroy(this.gameObject);
                SunManager.Instance.AddSun(SunPoint);
            });
    }
}
