using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSetterCursor : MonoBehaviour

{
    public int SelectableTowerIndex = 0;

    public List<GameObject> PlaceableTower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        TowerSelection();
        
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out hit)){
            Transform objectHit = hit.transform;
            float lerpFactor = 40;
            transform.position = Vector3.Lerp(transform.position, objectHit.position, Time.deltaTime * lerpFactor);
            //transform.position = objectHit.position;
            if (Mouse.current.leftButton.wasPressedThisFrame){
                AttampedPlacingTower(objectHit);
            }
        }
    }

    private void AttampedPlacingTower(Transform objectHit){
        var socket = objectHit.GetComponent<TowerSocket>();
        if (socket){
            if (!socket.HeldTower){
                socket.HeldTower = Instantiate(PlaceableTower[SelectableTowerIndex], socket.transform.position, Quaternion.identity);
            }
            else{
                Debug.Log("Tower Placement failed because socket is occupied");
            }
        }
    }
    void TowerSelection(){
                 if (Keyboard.current.digit1Key.wasPressedThisFrame){
                     SelectableTowerIndex = 0;
                 }
                 if (Keyboard.current.digit2Key.wasPressedThisFrame){
                     SelectableTowerIndex = 1;
                 }
                 if (Keyboard.current.digit3Key.wasPressedThisFrame){
                     SelectableTowerIndex = 2;
                 }
                 if (Keyboard.current.digit4Key.wasPressedThisFrame){
                     SelectableTowerIndex = 3;
                 }
                 if (Keyboard.current.digit5Key.wasPressedThisFrame){
                     SelectableTowerIndex = 4;
                 }
                 if (Keyboard.current.digit6Key.wasPressedThisFrame){
                     SelectableTowerIndex = 5;
                 }
                 if (Keyboard.current.digit7Key.wasPressedThisFrame){
                     SelectableTowerIndex = 6;
                 }
                 if (Keyboard.current.digit8Key.wasPressedThisFrame){
                     SelectableTowerIndex = 7;
                 }
                 SelectableTowerIndex %= PlaceableTower.Count;
    }
}
