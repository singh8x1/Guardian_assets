using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_placer : MonoBehaviour
{
    [SerializeField] private LayerMask PlacementColliderMask;
    [SerializeField] private Camera PlayerCamera;
    private GameObject CurrentPlacingEnemy;
    // Start is called before the first frame update


    //  void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (CurrentPlacingEnemy != null)
        {
            Ray camray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hitinfo;
            if (Physics.Raycast(camray, out RaycastHit hitInfo, 100f))
            {
                CurrentPlacingEnemy.transform.position = hitInfo.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // if (hitInfo.collider.gameObject.CompareTag("CantPlace"))
                //{
                CurrentPlacingEnemy = null;
                //}

            }
        }
    }



    public void SetTowerToPlace(GameObject SlimePBR)
    {
        CurrentPlacingEnemy = Instantiate(SlimePBR, Vector3.zero, Quaternion.identity);
    }
}
