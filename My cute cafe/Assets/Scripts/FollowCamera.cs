using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;          
    [SerializeField] private Vector3 offset;            
    [SerializeField] private float followSpeed;         
    [SerializeField] private float tiltAngle;           
    [SerializeField] private float movementThreshold;
    [SerializeField] private float idleTimeToLookAtPlayer;
    [SerializeField] private float maxIdleZoomIn;       
    [SerializeField] private float zoomInSpeed;

    private Rigidbody playerRigidbody;
    private float initialY;                                                  
    private float idleTimer = 0;                       
    private bool isIdle = false;                        
    private Quaternion initialRotation;
    private Vector3 initialOffset;
    private Vector3 targetOffset;

    private void Start()
    {
       

        initialY = transform.position.y;                
        playerRigidbody = player.GetComponent<Rigidbody>(); 
        initialRotation = transform.rotation;
      
    }

    private void LateUpdate()
    {
        FollowOrLookAtPlayer();
    }

    private void FollowOrLookAtPlayer()
    {
        initialOffset = offset;
        targetOffset = initialOffset;
        transform.rotation = Quaternion.Euler(tiltAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Vector3 playerVelocity = new Vector3(playerRigidbody.velocity.x, 0, playerRigidbody.velocity.z);

        if (playerVelocity.magnitude > movementThreshold)
        {
            idleTimer = 0f;
            isIdle = false;
            targetOffset = initialOffset;
            FollowPlayer();
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleTimeToLookAtPlayer)
            {
                if (!isIdle)
                {
                    isIdle = true;                  
                }
                LookAtPlayerFromFront();
            }
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, followSpeed * Time.deltaTime);
    }

    private void LookAtPlayerFromFront()
    {                
        float targetZoom = Mathf.Max(targetOffset.magnitude - (zoomInSpeed * Time.deltaTime), maxIdleZoomIn);
        targetOffset = targetOffset.normalized * targetZoom;

        Vector3 frontPosition = player.position - player.forward * targetOffset.magnitude + Vector3.up * 1.5f;
        transform.position = Vector3.Lerp(transform.position, frontPosition, followSpeed * Time.deltaTime);
        
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
