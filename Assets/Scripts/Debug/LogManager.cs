using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.DEBUG
{
    /// <summary>
    /// This static class will handle all matters of log made by the system during dev and/or play
    /// </summary>
    public static class LogManager
    {
        public static void InfoLog(Type type, string msg)
        {
            Debug.Log("[" + type.ToString() + "] : " + msg);
        }
        public static void WarnLog(Type type, string msg) 
        { 
            Debug.LogWarning("[" + type.ToString() + "] : " + msg);
        }
        public static void ErrorLog(Type type, string msg)
        {
            Debug.LogError("[" + type.ToString() + "] : " + msg);
        }
        public static void CriticalLog(Type type, string msg)
        {
            Debug.LogError("[" + type.ToString() + "] : == CRITICAL ERROR == :" + msg);
            Application.Quit();
        }
    }
}
