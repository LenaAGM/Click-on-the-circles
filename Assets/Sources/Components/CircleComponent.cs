using System.Threading.Tasks;
using UnityEngine;

public class CircleComponent : MonoBehaviour
{
    [SerializeField] private Renderer Renderer;
    [SerializeField] private Animator Animator;

    [HideInInspector] public int Score;
    
    public void Init(Material material, int score)
    {
        Renderer.material = material;
        Score = score;
    }

    public async void Destroy()
    {
        GetComponent<Collider2D>().enabled = false;
        Animator.SetBool("IsClose", true);
        await Task.Delay(500);
        Destroy(gameObject);
    }
}