using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerSetterCursor : MonoBehaviour

{
    private static readonly int hologramShaderProperty_Blocked = Shader.PropertyToID("_Blocked");
    public int SelectableTowerIndex = 0;

    public List<GameObject> PlaceableTower;
    public List<GameObject> Holograms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        TowerSelection();
        
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out hit, 40, LayerMask.GetMask("Default"))){
            Transform objectHit = hit.transform;
            float lerpFactor = 40;
            transform.position = Vector3.Lerp(transform.position, objectHit.position, Time.deltaTime * lerpFactor);
            //transform.position = objectHit.position;
            if (Mouse.current.leftButton.wasPressedThisFrame){
                AttampedPlacingTower(objectHit);
            }
        }
        _shaderUpdate();
    }

    private void AttampedPlacingTower(Transform objectHit){
        if (!_foundsCheck()){
            return;     // Es hat an Geld gefehlt. Hier könnte ein Sound abgespielt werden
        }
        
        var socket = objectHit.GetComponent<TowerSocket>();
        if (socket){
            if (!socket.HeldTower){
                socket.HeldTower = Instantiate(PlaceableTower[SelectableTowerIndex], socket.transform.position, Quaternion.identity);
                int cost = socket.HeldTower.GetComponent<Tower>().stats.Price;
                Bank.SpendMoney.Invoke(cost);
            }
            else{
                Debug.Log("Tower Placement failed because socket is occupied");
            }
        }
    }
    void TowerSelection(){
                 if (Keyboard.current.digit1Key.wasPressedThisFrame){
                    SwapToTower(0);
                 }
                 if (Keyboard.current.digit2Key.wasPressedThisFrame){
                     SwapToTower(1);
                 }
                 if (Keyboard.current.digit3Key.wasPressedThisFrame){
                     SwapToTower(2);
                 }
                 if (Keyboard.current.digit4Key.wasPressedThisFrame){
                     SwapToTower(3);
                 }
                 if (Keyboard.current.digit5Key.wasPressedThisFrame){
                     SwapToTower(4);
                 }
                 if (Keyboard.current.digit6Key.wasPressedThisFrame){
                    SwapToTower(5);
                 }
                 if (Keyboard.current.digit7Key.wasPressedThisFrame){
                    SwapToTower(6);
                 }
                 if (Keyboard.current.digit8Key.wasPressedThisFrame){
                     SwapToTower(7);
                 }
                 SelectableTowerIndex %= PlaceableTower.Count;
    }

    private void SwapToTower(int index){
        Holograms[SelectableTowerIndex].SetActive(false);
        SelectableTowerIndex = index;
        Holograms[SelectableTowerIndex].SetActive(true);
    }

    private bool _foundsCheck(){
        int currentBalance = Bank.Instance.Balance;
        int coast = PlaceableTower[SelectableTowerIndex].GetComponent<Tower>().stats.Price;
        return currentBalance >= coast;
        /*                                          // Das is die stark verkürzte Version von:
         if (currentBalance >= coast){
            return true;
        }
        else{
            return false;
        }
        */
    }

    private void _shaderUpdate(){
        bool enoughMoney = _foundsCheck();
        bool socketIsFree = false;
        if (enoughMoney){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(ray, out hit, 40, LayerMask.GetMask("Default"))){
                Transform objectHit = hit.transform;
                var socket = objectHit.GetComponent<TowerSocket>();
                if (socket){
                    if (!socket.HeldTower){
                        socketIsFree = true;
                    }
                }
            }
        }

        bool finalBlocked = enoughMoney && socketIsFree;
        var allMeshRenderers = Holograms[SelectableTowerIndex].GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in allMeshRenderers){
            meshRenderer.material.SetFloat(hologramShaderProperty_Blocked, finalBlocked?0:1);
        }
    }
}
