using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using XRInputDevice = UnityEngine.XR.InputDevice;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;
using System.Linq;
public class NewBehaviourScript : MonoBehaviour
{

    public string savePath = @"C:\Users\24312\Desktop";
    public string saveName = "test.txt";
    public bool enable = false;

    public Camera HMDCamera;
    public GameObject LeftController;
    public GameObject RightController;
    private List<string> contents;
    private int frameCount = 0;
    //private List<XRInputSubsystem> subsystemList = new List<XRInputSubsystem>();
    //private List<XRInputDevice> XRDeviceList = new List<XRInputDevice>();
    //private List<XRInputDevice> XRDeviceTemp = new List<XRInputDevice>();
    //private List<XRNodeState> nodeStates = new List<XRNodeState>();

    // Start is called before the first frame update
    void Start()
    {
        //// InputDevices.GetDeviceAtXRNode(node);
        //InputTracking.GetNodeStates(nodeStates);
        //SubsystemManager.GetInstances(subsystemList);
        ////int count = 0;
        ////foreach (var subsystem in subsystemList) {
        ////    count++;
        ////    Debug.LogFormat("subSystem {0}", count);
        ////    subsystem.TryGetInputDevices(XRDeviceTemp);
        ////    XRDeviceList.Concat(XRDeviceTemp);
        ////}
        ////Debug.LogFormat("at frame {0}\n", frameCount);
        //Debug.LogFormat("found {0} nodes", nodeStates.Count);
        //int count = 0;
        //foreach (XRNodeState nodeState in nodeStates) {
        //    Vector3 tempPosition = new Vector3(float.NaN, float.NaN, float.NaN);
        //    Quaternion tempRotation = new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);
        //    nodeState.TryGetPosition(out tempPosition);
        //    nodeState.TryGetRotation(out tempRotation);
        //    Debug.LogFormat("found xrnode num {2} with position: {0}\nRotation: {1}", tempPosition.ToString(), tempRotation.ToString(), count);
        //    count++;
        //}
        if (!enable) return;
        contents = new List<string>();
        if (!HMDCamera)
        {
            HMDCamera = Camera.current;
            Debug.LogFormat("no camera found");
        }
        else
        {
            Debug.LogFormat("camera found");
        }
        contents.Add("time, position, rotation");
        //var inputDevices = new List<UnityEngine.XR.InputDevice>();
        //UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        //Debug.LogFormat("found {0} input devices", inputDevices.Count);
        //foreach (var device in inputDevices) {
        //    Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;
        frameCount++;
        //SubsystemManager.GetInstances(subsystemList);
        //int found = 0;
        //foreach (var subsystem in subsystemList) {
        //    if (subsystem.TryGetInputDevices(XRDeviceTemp))
        //        found++;
        //    XRDeviceList.Concat(XRDeviceTemp);
        //}
        if (!HMDCamera)
            return;
        Vector3 currentPos = HMDCamera.transform.position;
        Quaternion currentRot = HMDCamera.transform.rotation;
        if (frameCount % 60 == 0)
        {
            Debug.LogFormat("at frame {0}\n", frameCount);
            Debug.LogFormat("camera position at {0}", HMDCamera.transform.position);
            Debug.LogFormat("content now has {0} Lines", contents.Count);
        }
        string temp = $"{Time.time}, {currentPos.ToString("F6")}, {currentRot.ToString("F6")}";
        contents.Add(temp);
    }

    private void OnDestroy()
    {
        if (!enable) return;
        Debug.LogFormat($"saving log of {contents.Count} lines");
        WriteFile(savePath, saveName, contents);
    }

    void WriteFile(string path, string name, List<string> contents)
    {
        if (File.Exists(path + '\\' + name))
        {
            File.Delete(path + "\\" + name);
        }
        using StreamWriter sw = new StreamWriter(path + '\\' + name);
        foreach (string content in contents)
        {
            sw.WriteLine(content);
        }
    }
}
