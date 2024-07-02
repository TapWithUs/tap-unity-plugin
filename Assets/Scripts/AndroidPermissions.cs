using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class AndroidPermissions : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

#if UNITY_ANDROID
    if (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
    {
      Permission.RequestUserPermission("android.permission.BLUETOOTH_CONNECT");
    }


    if (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADMIN"))
    {
      Permission.RequestUserPermission("android.permission.BLUETOOTH_ADMIN");
    }


#endif
  }

  // Update is called once per frame
  void Update()
  {

  }
}

