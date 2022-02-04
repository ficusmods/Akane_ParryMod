extern alias Akane;
extern alias AkaneFP;

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using HarmonyLib;
using Unity;
using UnityEngine;
using IllusionPlugin;

namespace AkaneParryAnytime
{
    public class ParryPatchPlugin : IPlugin
    {
        public string Name => "ParryPatch";

        public string Version => "1.0";

        public void OnApplicationQuit()
        {
        }

        public void OnApplicationStart()
        {
            var harmony = new Harmony("com.ficus.ParryPatch");
            harmony.PatchAll();
        }


        public void OnFixedUpdate()
        {
        }

        public void OnLevelWasInitialized(int level)
        {
        }

        public void OnLevelWasLoaded(int level)
        {
            GlobalVars.player = UnityEngine.Object.FindObjectOfType<Akane::Player>();
        }

        public void OnUpdate()
        {
            
        }
    }
}
