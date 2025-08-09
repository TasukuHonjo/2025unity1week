using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Honjo
{
    public class Motor : MonoBehaviour
    {
        Rigidbody rb = null;

        [SerializeField] float maxSpeed = 10;//���x���
        [SerializeField] float moveTime = 5;//���b������
        float time = 0;

        [SerializeField] bool startFg = false;

        [SerializeField] float power = 5;//�������p���[

        private bool onesFg = false;//��񂾂�

        [SerializeField]Camera topViewCamera = null;
        //private float chargeTime = 0;
        [SerializeField]private float chargeMaxTime = 30;
        [SerializeField] private float moveTimeMagnification = 1;
        [SerializeField] Image timer;
        [SerializeField] RawImage miniMap;

        private RotationY ry = null;
        private DriveHandle dh = null;

        [SerializeField]AudioClip SE_Car_Drift;
        [SerializeField]AudioClip SE_Track_Engine_Moving;
        AudioSource m_as;
        public bool driveEndFg = false;//�^�]�̏I�����Ӗ�����:�Q�[���}�l�[�W�����������Ď�����

        private float engineSmallChange = 5;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ry = GetComponent<RotationY>();
            dh = GetComponent<DriveHandle>();
            m_as = GetComponent<AudioSource>();
        }

        private void Start()
        {
            time = 0;
            timer.fillAmount = 0;
            ry.enabled = true;  //�p�x���߂�t�F�[�Y
            dh.enabled = false; //�^�]�̃t�F�[�Y
            //camManager = GameObject.Find("Managers").GetComponent<CameraManager>();
            miniMap.enabled = false;
            driveEndFg = false;
        }

        private void Update()
        {
            //���x���
            if (rb.velocity.magnitude > maxSpeed)
            {
                // ���݂̑��x������ۂ����܂܏���X�s�[�h�ɐ���
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            if (startFg) { return; }
            if (onesFg) { return; };

            if (Input.GetMouseButton(0))
            {
                //chargeTime += Time.deltaTime;
                //if(chargeTime > chargeMaxTime) { chargeTime = chargeMaxTime; }
                //float normalized = (chargeTime * moveTimeMagnification) / (chargeMaxTime * moveTimeMagnification);
                //normalized = Mathf.Clamp01(normalized); // �O�̂��� 0?1 �ɐ���
                //timer.fillAmount = normalized;
            }

            if (Input.GetMouseButtonUp(0))
            {
                DriveFg();
            }
        }

        private void FixedUpdate()
        {
            if (!startFg) return;
                time += Time.fixedDeltaTime;

            float normalized = (moveTime - time) / (chargeMaxTime * moveTimeMagnification);
            normalized = Mathf.Clamp01(normalized); // �O�̂��� 0?1 �ɐ���
            timer.fillAmount = normalized;

            if(moveTime - time <= engineSmallChange)
            {
                float _nomalized = (moveTime - time) / engineSmallChange;
                _nomalized = Mathf.Clamp01(_nomalized);
                m_as.volume = _nomalized;
            }

            if (time > moveTime) 
            {
                time = 0;
                startFg = false;
                //chargeTime = 0;
                onesFg = true;
                dh.enabled = false; //�^�]�̃t�F�[�Y
                driveEndFg = true;//�^�]�I��
                return;
            }
            // Debug�pRay��Scene�r���[�ɕ\��
            Ray ray = new Ray(transform.position,transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            rb.AddForce(transform.forward * power, ForceMode.Force);
        }

        public void DriveFg()
        {
            moveTime = chargeMaxTime * moveTimeMagnification * timer.fillAmount;
            if (!startFg) startFg = true;
            topViewCamera.depth = -2;
            //camManager.SetDriveCamera();
            miniMap.enabled = true;
            //topViewCamera.rect = new Rect(viewPortRectXY.x, viewPortRectXY.y, viewPortRectWH.x, viewPortRectWH.y);
            ry.enabled = false;  //�p�x���߂�t�F�[�Y
            dh.enabled = true; //�^�]�̃t�F�[�Y
            dh.SetTargetYRotation(transform.eulerAngles.y);
            m_as.clip = SE_Track_Engine_Moving;
            m_as.Play();
        }
    }
}

