﻿using System;
using System.Reflection;
using Harmony;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Common;

namespace CheatManager
{
    public static class Main
    {        
        internal static bool isExistsSMLHelperV2;        

        public static void Load()
        {
            try
            {
                Config.Config.InitConfig();
                HarmonyInstance.Create("Subnautica.CheatManager.mod").PatchAll(Assembly.GetExecutingAssembly());
                UnityHelper.EnableConsole();
                SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(OnSceneLoaded); 
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }

            isExistsSMLHelperV2 = RefHelp.IsNamespaceExists("SMLHelper.V2");            
        }
        
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main")
            {                
                CheatManager.Load();                
            }

            if (scene.name == "StartScreen")
            {
                DisplayManager.OnDisplayChanged += Screen_OnDisplayChanged;
                Logger.Load();
                Config.CmConfig.Load();
            }
        }

        public static void Screen_OnDisplayChanged()
        {
            Debug.Log($"Resolution changed!");
        }
    }  
}
