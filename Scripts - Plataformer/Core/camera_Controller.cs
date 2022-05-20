using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_Controller : MonoBehaviour {
[SerializeField]private Transform player;
[SerializeField]private float aheadDistance;
[SerializeField] private float cameraSpeed;
    private float currentPosX;
    private float lookAhead;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}