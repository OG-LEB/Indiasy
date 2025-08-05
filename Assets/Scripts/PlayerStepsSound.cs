using UnityEngine;

public class PlayerStepsSound : MonoBehaviour
{
    private AudioSource audio;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((Mathf.Abs(rigidbody.velocity.x) > 0.25f || Mathf.Abs(rigidbody.velocity.z) > 0.25f) && Mathf.Abs(rigidbody.velocity.y) == 0)
        {
            if (!audio.isPlaying)
            {
                //Debug.Log("Start steps sound");
                audio.Play();
            }
        }
        else
        {
            if (audio.isPlaying)
            {
                //Debug.Log("Stop steps sound");
                audio.Stop();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            audio.pitch = 1.25f;
        }
        else
        {
            audio.pitch = 1f;
        }
    }

}
