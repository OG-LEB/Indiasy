using UnityEngine;

public class SpriteRotationScript : MonoBehaviour
{
    private Transform Player;

    private void Update()
    {
        if (Player == null)
        {
            Player = LevelScript.GetInstance().GetPlayerTransform();
        }
        transform.LookAt(Player, Vector3.up);
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
    }
}
