using System;
using UnityEngine;

public sealed class InputController : MonoBehaviour
{
    public Action<CircleComponent> OnTapAction;
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                var touchPosition =
                    Camera.main!.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0f));
                var hit = Physics2D.GetRayIntersection(touchPosition);

                if (hit.collider)
                {
                    if (hit.collider.gameObject.CompareTag("Circle"))
                    {
                        OnTapAction.Invoke(hit.collider.gameObject.GetComponent<CircleComponent>());
                    }
                }
            }
        }
    }
}