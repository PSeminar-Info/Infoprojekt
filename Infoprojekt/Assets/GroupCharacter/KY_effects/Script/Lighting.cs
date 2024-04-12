using System.Collections;
using UnityEngine;

namespace GroupCharacter.KY_effects.Script
{
    public class Lighting : MonoBehaviour
    {
        public float lighting = 1;
        public Light lightPower;
        public bool flashFlg;
        public float flashTimer = 0.3f;
        public float revOnTime;
        public float keepOnTime;
        public float keepTime;

        public bool flashingFlg;
        public float minLight;
        public float maxLight = 1;
        public float flashingOff;
        public float flashingOffPower;
        public float flashingOffIntensity = 1;

        private bool _lightKeepFlg;
        private bool _lightOffFlg;

        private void Start()
        {
            lightPower = GetComponent<Light>();

            Flash();
            SetRev();
            keepOn();
            SetFlashingOff();
        }

        private void Update()
        {
            if (flashingFlg)
            {
                if (_lightOffFlg)
                {
                    lightPower.intensity -= lighting * Time.deltaTime;
                    if (lightPower.intensity <= minLight) _lightOffFlg = false;
                }
                else
                {
                    lightPower.intensity += lighting * Time.deltaTime;
                    if (lightPower.intensity > maxLight) _lightOffFlg = true;
                }
            }
            else if (lightPower.intensity > 0 && lightPower.enabled && !_lightKeepFlg)
            {
                lightPower.intensity -= lighting * Time.deltaTime;
            }

            if (_lightKeepFlg && keepTime > 0)
            {
                keepTime -= Time.deltaTime;
                if (keepTime <= 0) _lightKeepFlg = false;
            }
        }


        private IEnumerator Flash()
        {
            if (flashFlg)
            {
                lightPower.enabled = false;
                yield return new WaitForSeconds(flashTimer);
                lightPower.enabled = true;
            }
        }

        private IEnumerator SetRev()
        {
            if (revOnTime > 0)
            {
                yield return new WaitForSeconds(revOnTime);
                lighting *= -1;
            }
        }

        private IEnumerator keepOn()
        {
            if (keepOnTime > 0)
            {
                yield return new WaitForSeconds(keepOnTime);
                _lightKeepFlg = true;
            }
        }

        private IEnumerator SetFlashingOff()
        {
            if (flashingOff > 0)
            {
                yield return new WaitForSeconds(flashingOff);
                flashingFlg = false;
                if (flashingOffPower > 0)
                {
                    lightPower.intensity = flashingOffIntensity;
                    lighting = flashingOffPower;
                }
            }
        }
    }
}