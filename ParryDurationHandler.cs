extern alias Akane;
extern alias AkaneFP;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Unity;
using UnityEngine;

namespace AkaneParryAnytime
{
    public class ParryDurationHandler : MonoBehaviour
    {
        Akane::Player player;
        private void Awake()
        {
            player = UnityEngine.Object.FindObjectOfType<Akane::Player>();
            this.StartCoroutine("HandleParryTimer");
        }
        private IEnumerator HandleParryTimer()
        {
            yield return new WaitForEndOfFrame();

            float elapsed = 0.0f;
            while (elapsed < Config.ParryDuration && player.isParrying)
            {
                elapsed += UnityEngine.Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (player.isParrying)
            {
                player.StopCoroutine("ParryCoroutine");
                player.isParrying = false;
                player.anim.SetBool("Parrying", false);

                Type type = player.GetType();
                FieldInfo f_ParryEventtRef = type.GetField("ParryEventtRef", BindingFlags.Instance | BindingFlags.NonPublic);
                int num = (int)((AkaneFP::FMOD.Studio.EventInstance)f_ParryEventtRef.GetValue(player)).stop(AkaneFP::FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            GameObject.Destroy(this);
        }
    }
}
