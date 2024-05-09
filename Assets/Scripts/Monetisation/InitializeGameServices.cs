using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class InitializeGameServices : MonoBehaviour
{
    async void Start()
    {
        try
        {
            var options = new InitializationOptions()

            #if UNITY_EDITOR || DEVELOPMENT_BUILD

                .SetEnvironmentName("test");


#else

                .SetEnvironmentName("production");
                Debug.Log("production");

#endif

            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            Debug.Log("Unity game services failed to initialize :( " + exception);
        }
    }
}
