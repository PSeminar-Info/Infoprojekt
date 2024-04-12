using UnityEngine;

namespace GroupCharacter.KY_effects.Script
{
    public class ConGUI : MonoBehaviour
    {
        public Transform mainCamera;
        public Transform cameraTrs;
        public int rotSpeed = 20;
        public GameObject[] effectObj;
        public GameObject[] effectObProj;
        private int _arrayNo;
        private int _cameraRotCon = 1;
        private readonly string[] _cameraState = { "Camera move", "Camera stop" };

        private bool _haveProFlg;
        private Vector3 _initPos;
        private GameObject _nonProFX;

        private GameObject _nowEffectObj;

        private float _num;
        private float _numBck;

        private Vector3 _tmpPos;

        private void Start()
        {
            _tmpPos = _initPos = mainCamera.localPosition;

            _haveProFlg = VisibleBt();
        }

        private void Update()
        {
            if (_cameraRotCon == 1) cameraTrs.Rotate(0, rotSpeed * Time.deltaTime, 0);

            if (_num > _numBck)
            {
                _numBck = _num;
                _tmpPos.y += 0.05f;
                _tmpPos.z -= 0.3f;
            }
            else if (_num < _numBck)
            {
                _numBck = _num;
                _tmpPos.y -= 0.05f;
                _tmpPos.z += 0.3f;
            }
            else if (_num == 0)
            {
                _tmpPos.y = _initPos.y;
                _tmpPos.z = _initPos.z;
            }

            if (_tmpPos.y < _initPos.y) _tmpPos.y = _initPos.y;
            if (_tmpPos.z > _initPos.z) _tmpPos.z = _initPos.z;

            mainCamera.localPosition = _tmpPos;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(20, 0, 30, 30), "←"))
            {
                //return
                _arrayNo--;
                if (_arrayNo < 0) _arrayNo = effectObj.Length - 1;
                effectOn();

                _haveProFlg = VisibleBt();
            }

            if (GUI.Button(new Rect(50, 0, 200, 30), effectObj[_arrayNo].name)) effectOn();

            if (GUI.Button(new Rect(250, 0, 30, 30), "→"))
            {
                //next
                _arrayNo++;
                if (_arrayNo >= effectObj.Length) _arrayNo = 0;
                effectOn();

                _haveProFlg = VisibleBt();
            }

            if (_haveProFlg)
                if (GUI.Button(new Rect(50, 30, 300, 70), "+Distorsion"))
                {
                    if (_nowEffectObj != null) Destroy(_nowEffectObj);
                    _nowEffectObj = Instantiate(_nonProFX);
                }


            if (GUI.Button(new Rect(300, 0, 200, 30), _cameraState[_cameraRotCon]))
            {
                if (_cameraRotCon == 1)
                    _cameraRotCon = 0;
                else
                    _cameraRotCon = 1;
            }

            _num = GUI.VerticalSlider(new Rect(30, 100, 20, 200), _num, 0, 20);
        }

        private bool VisibleBt()
        {
            foreach (var tmpObj in effectObProj)
                if (effectObj[_arrayNo].name == tmpObj.name)
                {
                    _nonProFX = tmpObj;
                    return true;
                }

            return false;
        }

        private void effectOn()
        {
            if (_nowEffectObj != null) Destroy(_nowEffectObj);
            _nowEffectObj = Instantiate(effectObj[_arrayNo]);
        }
    }
}