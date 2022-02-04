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
    [HarmonyPatch(new Type[] { typeof(Vector3) })]
    public class KillEnemyOnParryPatch
    {

        static void Postfix(ref Vector3 hitPosition)
        {
            Akane::Player player = GlobalVars.player;
            List<Akane::Enemy> closeEnemies = GetCloseEnemies(player.transform.position);
            foreach(Akane::Enemy enemy in closeEnemies)
            {
                enemy.TakeRegularMelee();
            }
            if(closeEnemies.Count > 0)
            {
                player.anim.SetTrigger("Attack");
                player.anim.SetInteger("Attack Type", 0);
            }
        }
        public static List<Akane::Enemy> GetCloseEnemies(Vector3 to)
        {
            List<Akane::Enemy> ret = new List<Akane::Enemy>();
            GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            if (gameObjectsWithTag.Length > 0)
            {
                float radius = Config.ParryKillRange;
                foreach (GameObject go in gameObjectsWithTag)
                {
                    float dist = Vector3.Distance(to, go.transform.position);
                    if (dist < radius)
                    {
                        Akane::Enemy enemy = go?.GetComponent<Akane::Enemy>();
                        if (!enemy.Dead)
                        {
                            ret.Add(enemy);
                        }
                    }
                }
            }

            return ret;
        }
    }
}
