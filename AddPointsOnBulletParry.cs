extern alias Akane;
extern alias AkaneFP;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using HarmonyLib;
using Unity;
using UnityEngine;

namespace AkaneParryAnytime
{
    [HarmonyPatch(typeof(Akane::Player), "Parry")]
    [HarmonyPatch(new Type[] { typeof(Rigidbody) })]
    public class BulletParryPatch
    {

        static void Postfix(Rigidbody OtherRb)
        {
            Akane::Player player = GlobalVars.player;
            player.AddAmmo();
            player.AddAdrenalinePoint();
        }
    }
}
