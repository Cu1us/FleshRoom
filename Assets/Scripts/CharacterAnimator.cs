using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    float deltatime;
    public void StartTalk()
    {
        animator.SetBool("Talk", true);
    }
    public void StopTalk()
    {
        animator.SetBool("Talk", false);
    }

    public void Laugh()
    {
        animator.SetBool("Laugh", true);
    }
    private void Update()
    {
        deltatime -= Time.deltaTime;
        if (deltatime <= 0) animator.SetBool("Laugh", false);
    }
}
