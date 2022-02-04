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
    [HarmonyPatch(typeof(Akane::Player), "StartParryingMode")]
    public class QuickParryPatch
    {

        static void Prefix()
        {
            Akane::Player player = GlobalVars.player;
            Type type = player.GetType();
            FieldInfo f_canParry = type.GetField("canParry", BindingFlags.Instance | BindingFlags.NonPublic);
            f_canParry.SetValue(player, true);

            player.ParryEvent -= Player_ParryEvent;
            player.ParryEvent += Player_ParryEvent;
            player.gameObject.AddComponent<ParryDurationHandler>();
        }

        private static void Player_ParryEvent()
        {
            Akane::Player player = GlobalVars.player;
            if (player.isParrying)
            {
                player.StopCoroutine("ParryCoroutine");
                player.isParrying = false;
                player.anim.SetBool("Parrying", false);

                Type type = player.GetType();
                FieldInfo f_ParryEventtRef = type.GetField("ParryEventtRef", BindingFlags.Instance | BindingFlags.NonPublic);
                int num = (int)((AkaneFP::FMOD.Studio.EventInstance)f_ParryEventtRef.GetValue(player)).stop(AkaneFP::FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                player.Stamina += player.MaxStamina * Config.ParryStaminaGain;
                GameObject.Destroy(player.gameObject.GetComponent<ParryDurationHandler>());
            }
        }
    }
}
