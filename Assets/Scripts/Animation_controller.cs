using UnityEngine;

public class Animation_controller : MonoBehaviour
{
    const string PLAYER_CLIMB = "Climb";
    const string PLAYER_RUN = "Run";
    Animator Animator;
    void Start()
    {
        Animator = transform.GetChild(0).GetComponentInChildren<Animator>();
    }
    public void run(bool value = true)
    {
        Animator.SetBool(PLAYER_RUN, value);
    }
    public void climb(bool value = true)
    {
        Animator.SetBool(PLAYER_CLIMB, value);
    }
}
