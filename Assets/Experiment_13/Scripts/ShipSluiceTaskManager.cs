using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSluiceTaskManager : MonoBehaviour
{
    [SerializeField] private GameObject _shipPrefab;
    [SerializeField] private Ship _ship;
    [SerializeField] private List<CommunicatingVessels> _vesselses;
    [SerializeField] private List<Gate> _gates;

    private IShipSluiceTask _task;
    public void Start()
    {
        //StartCoroutine(DelayStart());
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLeftRightTask();
        }
        if (Input.GetMouseButtonDown(1))
        {
            CreateRightLeftTask();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }

    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(2f);
        CreateRightLeftTask();
        Play();
    }
    public void CreateLeftRightTask()
    {
        Restart();
        FluidContainer container = _vesselses[0].GetContainer();
        Vector3 position = container.GetPosition();
        position = new Vector3(position.x, container.GetWaterLevel(), position.z);// container.GetWaterLevel();

        _ship = Instantiate(_shipPrefab, position, Quaternion.Euler(new Vector3(-90,0,0))).GetComponent<Ship>();
        
        _task = new GatewayOpeningTask(_gates[0]);

        IShipSluiceTask taskGate = _task;
        for (int i = 0; i < _gates.Count; i++)
        {
            IShipSluiceTask taskFluid = new SubstanceTransfusionTask(_vesselses[i], _ship, _vesselses[i].GetHoles()[0], _vesselses[i].GetContainer());
            IShipSluiceTask taskDoor = new DoorOpenTask(_gates[i]);
            IShipSluiceTask taskShip = new MoveShipTask(_ship, _vesselses[i + 1].GetContainer());
            IShipSluiceTask taskGateClose = new GatewayCloseTask(_gates[i]);
            IShipSluiceTask taskDoorClose = new DoorCloseTask(_gates[i]);
            taskGate.SetNext(taskFluid);
            taskFluid.SetNext(taskDoor);
            taskDoor.SetNext(taskShip);
            taskShip.SetNext(taskGateClose);
            taskGateClose.SetNext(taskDoorClose);
            if (i < _gates.Count - 1)
            {
                taskGate = new GatewayOpeningTask(_gates[i + 1]);
                taskDoorClose.SetNext(taskGate);
            }
        }
    }

    public void CreateRightLeftTask()
    {
        Restart();
        FluidContainer container = _vesselses[_vesselses.Count - 1].GetContainer();
        Vector3 position = container.GetPosition();
        position = new Vector3(position.x, container.GetWaterLevel(), position.z);

        _ship = Instantiate(_shipPrefab, position, Quaternion.Euler(new Vector3(-90,-180,0))).GetComponent<Ship>();
        
        _task = new GatewayOpeningTask(_gates[_gates.Count - 1]);

        IShipSluiceTask taskGate = _task;
        for (int i = _gates.Count - 1; i >= 0; i--)
        {
            IShipSluiceTask taskFluid = new SubstanceTransfusionTask(_vesselses[i], _ship, _vesselses[i].GetHoles()[0], _vesselses[i + 1].GetContainer());
            IShipSluiceTask taskDoor = new DoorOpenTask(_gates[i]);
            IShipSluiceTask taskShip = new MoveShipTask(_ship, _vesselses[i].GetContainer());
            IShipSluiceTask taskGateClose = new GatewayCloseTask(_gates[i]);
            IShipSluiceTask taskDoorClose = new DoorCloseTask(_gates[i]);
            taskGate.SetNext(taskFluid);
            taskFluid.SetNext(taskDoor);
            taskDoor.SetNext(taskShip);
            taskShip.SetNext(taskGateClose);
            taskGateClose.SetNext(taskDoorClose);
            
            if (i > 0)
            {
                taskGate = new GatewayOpeningTask(_gates[i - 1]);
                taskDoorClose.SetNext(taskGate);
            }
        }
    }
    
    public void Play()
    {
        _task?.Start();
    }
    
    private void Restart()
    {
        _task?.Stop();
        
        if (_ship != null)
        {
            Destroy(_ship.gameObject);
        }
    }
}
