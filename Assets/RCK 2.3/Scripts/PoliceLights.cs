//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//public enum ControlMode { simple = 1, touch = 2 }


//public class VehicleControl : MonoBehaviour
//{


//    // 차량 제어 모드 설정 및 활성화 여부
//    public ControlMode controlMode = ControlMode.simple; // 기본 제어 모드를 simple로 설정합니다.
//    public bool activeControl = false; // 차량 제어가 활성화되어 있는지 여부를 결정하는 플래그입니다.

//    // 바퀴 설정
//    //public CarWheels carWheels; // CarWheels 객체를 사용하여 바퀴 설정을 저장합니다.

//    // CarWheels 클래스 선언
//    //[System.Serializable]
//    //public class CarWheels
//    //{
//    //    public ConnectWheel wheels; // ConnectWheel 객체로 바퀴들을 설정합니다.
//    //    public WheelSetting setting; // WheelSetting 객체로 바퀴의 설정값들을 저장합니다.
//    //}

//    //// ConnectWheel 클래스 선언
//    //[System.Serializable]
//    //public class ConnectWheel
//    //{
//    //    public bool frontWheelDrive = true; // 앞바퀴 구동 여부 설정
//    //    public Transform frontRight; // 앞 오른쪽 바퀴의 Transform 객체입니다.
//    //    public Transform frontLeft; // 앞 왼쪽 바퀴의 Transform 객체입니다.

//    //    public bool backWheelDrive = true; // 뒷바퀴 구동 여부 설정
//    //    public Transform backRight; // 뒤 오른쪽 바퀴의 Transform 객체입니다.
//    //    public Transform backLeft; // 뒤 왼쪽 바퀴의 Transform 객체입니다.
//    //}

//    //// WheelSetting 클래스 선언
//    //[System.Serializable]
//    //public class WheelSetting
//    //{
//    //    public float Radius = 0.4f; // 바퀴 반경 설정
//    //    public float Weight = 1000.0f; // 바퀴 무게 설정
//    //    public float Distance = 0.2f; // 서스펜션 거리 설정
//    //}

//    // 차량 라이트 설정
//    public CarLights carLights; // CarLights 객체를 사용하여 차량의 라이트 설정을 저장합니다.

//    // CarLights 클래스 선언
//    [System.Serializable]
//    public class CarLights
//    {
//        public Light[] brakeLights; // 브레이크 라이트 배열입니다.
//        public Light[] reverseLights; // 후진등 배열입니다.
//    }

//    // 차량 소리 설정
//    public CarSounds carSounds; // CarSounds 객체를 사용하여 차량 소리를 관리합니다.

//    // CarSounds 클래스 선언
//    [System.Serializable]
//    public class CarSounds
//    {
//        public AudioSource IdleEngine, LowEngine, HighEngine; // 정지, 저속, 고속 엔진 소리를 설정합니다.
//        public AudioSource nitro; // 니트로 소리 설정
//        public AudioSource switchGear; // 기어 전환 소리 설정
//    }

//    // 차량 파티클 설정
//    public CarParticles carParticles; // CarParticles 객체를 사용하여 파티클 효과를 설정합니다.

//    // CarParticles 클래스 선언
//    [System.Serializable]
//    public class CarParticles
//    {
//        public GameObject brakeParticlePerfab; // 브레이크 파티클 프리팹입니다.
//        public ParticleSystem shiftParticle1, shiftParticle2; // 기어 전환 시 나오는 파티클 시스템입니다.
//        private GameObject[] wheelParticle = new GameObject[4]; // 각 바퀴에 대한 파티클 설정 배열입니다.
//    }

//    // 차량 엔진 설정
//    public CarSetting carSetting; // CarSetting 객체를 사용하여 차량의 엔진 및 기어 설정을 관리합니다.

//    // CarSetting 클래스 선언
//    [System.Serializable]
//    public class CarSetting
//    {
//        public bool showNormalGizmos = false; // 차량의 기즈모를 보여줄지 여부입니다.
//        public Transform carSteer; // 차량 조향에 사용되는 Transform입니다.
//        public HitGround[] hitGround; // 지면 충돌 시의 정보를 저장하는 배열입니다.

//        public List<Transform> cameraSwitchView; // 카메라 전환 시 사용할 뷰 목록입니다.

//        public float springs = 25000.0f; // 서스펜션 스프링 값
//        public float dampers = 1500.0f; // 서스펜션 댐퍼 값

//        public float carPower = 120f; // 차량 출력 값
//        public float shiftPower = 150f; // 기어 전환 시 출력 값
//        public float brakePower = 8000f; // 브레이크 출력 값

//        public Vector3 shiftCentre = new Vector3(0.0f, -0.8f, 0.0f); // 차량의 질량 중심 설정

//        public float maxSteerAngle = 25.0f; // 최대 조향 각도

//        public float shiftDownRPM = 1500.0f; // 기어를 내리는 회전수
//        public float shiftUpRPM = 2500.0f; // 기어를 올리는 회전수
//        public float idleRPM = 500.0f; // 정지 상태 회전수

//        public float stiffness = 2.0f; // 바퀴의 강성 값

//        public bool automaticGear = true; // 자동 기어 사용 여부

//        public float[] gears = { -10f, 9f, 6f, 4.5f, 3f, 2.5f }; // 기어 설정 배열

//        public float LimitBackwardSpeed = 60.0f; // 후진 속도 제한
//        public float LimitForwardSpeed = 220.0f; // 전진 속도 제한
//    }

//    // 지면 충돌 정보 클래스 선언
//    [System.Serializable]
//    public class HitGround
//    {
//        public string tag = "street"; // 충돌 대상 태그 설정
//        public bool grounded = false; // 차량이 지면에 닿았는지 여부
//        public AudioClip brakeSound; // 브레이크 시 소리 설정
//        public AudioClip groundSound; // 지면 소리 설정
//        public Color brakeColor; // 브레이크 시 파티클 색상 설정
//    }

//    // 필드 변수들 선언
//    private float steer = 0; // 현재 조향 각도
//    private float accel = 0.0f; // 가속도 값
//    [HideInInspector] public bool brake; // 브레이크 여부
//    private bool shifmotor; // 기어 전환 모터 상태

//    [HideInInspector] public float curTorque = 100f; // 현재 토크 값
//    [HideInInspector] public float powerShift = 100; // 니트로 출력 값
//    [HideInInspector] public bool shift; // 기어 전환 여부

//    private float torque = 100f; // 기본 토크 값
//    [HideInInspector] public float speed = 0.0f; // 현재 속도
//    private float lastSpeed = -10.0f; // 마지막 속도 값

//    private bool shifting = false; // 기어 전환 중 여부
//    float[] efficiencyTable = { 0.6f, 0.65f, 0.7f, 0.75f, 0.8f, 0.85f, 0.9f, 1.0f, 1.0f, 0.95f, 0.80f, 0.70f, 0.60f, 0.5f, 0.45f, 0.40f, 0.36f, 0.33f, 0.30f, 0.20f, 0.10f, 0.05f }; // 효율성 테이블
//    float efficiencyTableStep = 250.0f; // 효율성 테이블 스텝

//    private float Pitch; // 엔진 피치 값
//    private float PitchDelay; // 피치 지연 값
//    private float shiftTime = 0.0f; // 기어 전환 시간
//    private float shiftDelay = 0.0f; // 기어 전환 지연 시간

//    [HideInInspector] public int currentGear = 0; // 현재 기어
//    [HideInInspector] public bool NeutralGear = true; // 중립 기어 여부

//    [HideInInspector] public float motorRPM = 0.0f; // 모터 RPM
//    [HideInInspector] public bool Backward = false; // 후진 여부

//    // 터치 모드 관련 변수들
//    [HideInInspector] public float accelFwd = 0.0f; // 전진 가속도
//    [HideInInspector] public float accelBack = 0.0f; // 후진 가속도
//    [HideInInspector] public float steerAmount = 0.0f; // 조향 값

//    // 기타 변수들 선언
//    private float wantedRPM = 0.0f; // 원하는 RPM 값
//    private float w_rotate; // 바퀴 회전 값
//    private float slip, slip2 = 0.0f; // 바퀴 슬립 값들
//    private GameObject[] Particle = new GameObject[4]; // 파티클 배열
//    private Vector3 steerCurAngle; // 현재 조향 각도
//    private Rigidbody myRigidbody; // Rigidbody 컴포넌트
//    //private WheelComponent[] wheels; // WheelComponent 배열

//    // WheelComponent 클래스 선언
//    //private class WheelComponent
//    //{
//    //    public Transform wheel; // 바퀴 Transform
//    //    public WheelCollider collider; // 바퀴 WheelCollider
//    //    public Vector3 startPos; // 바퀴 시작 위치
//    //    public float rotation = 0.0f; // 바퀴 회전 값
//    //    public float rotation2 = 0.0f; // 바퀴 추가 회전 값
//    //    public float maxSteer; // 최대 조향 각도
//    //    public bool drive; // 구동 여부
//    //    public float pos_y = 0.0f; // y 위치
//    //}


//    //private WheelComponent SetWheelComponent(Transform wheel, float maxSteer, bool drive, float pos_y)
//    //{
//    //    WheelComponent result = new WheelComponent(); // 새로운 WheelComponent 객체를 생성합니다.
//    //    GameObject wheelCol = new GameObject(wheel.name + "WheelCollider"); // 바퀴의 이름을 사용해 새로운 게임 오브젝트를 만듭니다.

//    //    wheelCol.transform.parent = transform; // 이 게임 오브젝트의 부모를 현재 차량으로 설정합니다.
//    //    wheelCol.transform.position = wheel.position; // 게임 오브젝트의 위치를 바퀴 위치로 설정합니다.
//    //    wheelCol.transform.eulerAngles = transform.eulerAngles; // 게임 오브젝트의 회전 각도를 차량의 회전 각도와 동일하게 설정합니다.
//    //    pos_y = wheelCol.transform.localPosition.y; // `pos_y` 값을 로컬 `y` 위치로 설정합니다.

//    //    WheelCollider col = (WheelCollider)wheelCol.AddComponent(typeof(WheelCollider)); // WheelCollider 컴포넌트를 생성한 게임 오브젝트에 추가합니다.

//    //    result.wheel = wheel; // 생성된 `WheelComponent` 객체의 `wheel`을 설정합니다.
//    //    result.collider = wheelCol.GetComponent<WheelCollider>(); // 생성된 WheelCollider를 `collider`에 할당합니다.
//    //    result.drive = drive; // 구동 여부를 설정합니다.
//    //    result.pos_y = pos_y; // `pos_y` 값을 설정합니다.
//    //    result.maxSteer = maxSteer; // 최대 조향 각도를 설정합니다.
//    //    result.startPos = wheelCol.transform.localPosition; // 바퀴의 시작 위치를 저장합니다.

//    //    return result; // `WheelComponent` 객체를 반환합니다.
//    //}




//    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



//    void Awake()
//    {
//        if (carSetting.automaticGear) NeutralGear = false; // 자동 기어 설정이 켜져 있다면, 중립 기어를 비활성화합니다.

//        myRigidbody = transform.GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옵니다.
//        myRigidbody.useGravity = false;
//        //wheels = new WheelComponent[4]; // 4개의 바퀴를 위한 WheelComponent 배열을 초기화합니다.

//        // 앞 오른쪽, 왼쪽, 뒤 오른쪽, 왼쪽 바퀴를 각각 SetWheelComponent 메서드를 이용하여 초기화합니다.
//        //wheels[0] = SetWheelComponent(carWheels.wheels.frontRight, carSetting.maxSteerAngle, carWheels.wheels.frontWheelDrive, carWheels.wheels.frontRight.position.y);
//        //wheels[1] = SetWheelComponent(carWheels.wheels.frontLeft, carSetting.maxSteerAngle, carWheels.wheels.frontWheelDrive, carWheels.wheels.frontLeft.position.y);
//        //wheels[2] = SetWheelComponent(carWheels.wheels.backRight, 0, carWheels.wheels.backWheelDrive, carWheels.wheels.backRight.position.y);
//        //wheels[3] = SetWheelComponent(carWheels.wheels.backLeft, 0, carWheels.wheels.backWheelDrive, carWheels.wheels.backLeft.position.y);

//        if (carSetting.carSteer) // 조향 장치가 존재하는 경우
//            steerCurAngle = carSetting.carSteer.localEulerAngles; // 현재 조향 각도를 가져옵니다.

//        // 모든 바퀴에 대해 WheelCollider 설정을 적용합니다.
//        //foreach (WheelComponent w in wheels)
//        //{
//        //    WheelCollider col = w.collider; // 현재 바퀴의 WheelCollider를 가져옵니다.
//        //    col.suspensionDistance = carWheels.setting.Distance; // 서스펜션 거리를 설정합니다.
//        //    JointSpring js = col.suspensionSpring; // 서스펜션 스프링 값을 가져옵니다.

//        //    js.spring = carSetting.springs; // 서스펜션 스프링 강도를 설정합니다.
//        //    js.damper = carSetting.dampers; // 서스펜션 댐퍼 강도를 설정합니다.
//        //    col.suspensionSpring = js; // 수정된 서스펜션 스프링 값을 적용합니다.

//        //    col.radius = carWheels.setting.Radius; // 바퀴 반경을 설정합니다.
//        //    col.mass = carWheels.setting.Weight; // 바퀴 무게를 설정합니다.

//        //    // 바퀴의 전방 마찰을 설정합니다.
//        //    WheelFrictionCurve fc = col.forwardFriction;
//        //    fc.asymptoteValue = 5000.0f;
//        //    fc.extremumSlip = 2.0f;
//        //    fc.asymptoteSlip = 20.0f;
//        //    fc.stiffness = carSetting.stiffness;
//        //    col.forwardFriction = fc;

//        //    // 바퀴의 측면 마찰을 설정합니다.
//        //    fc = col.sidewaysFriction;
//        //    fc.asymptoteValue = 7500.0f;
//        //    fc.asymptoteSlip = 2.0f;
//        //    fc.stiffness = carSetting.stiffness;
//        //    col.sidewaysFriction = fc;
//        //}
//    }





//    public void ShiftUp()
//    {
//        float now = Time.timeSinceLevelLoad; // 게임 시작 후 경과된 시간을 가져옵니다.

//        if (now < shiftDelay) return; // 현재 시간이 shiftDelay보다 작다면, 기어 전환을 수행하지 않습니다.

//        if (currentGear < carSetting.gears.Length - 1) // 현재 기어가 최대 기어보다 낮다면
//        {
//            carSounds.switchGear.GetComponent<AudioSource>().Play(); // 기어 전환 소리를 재생합니다.

//            if (!carSetting.automaticGear) // 수동 기어인 경우
//            {
//                if (currentGear == 0)
//                {
//                    if (NeutralGear) { currentGear++; NeutralGear = false; } // 중립 기어에서 기어를 올립니다.
//                    else { NeutralGear = true; } // 다시 중립 기어로 전환합니다.
//                }
//                else
//                {
//                    currentGear++; // 기어를 한 단계 올립니다.
//                }
//            }
//            else
//            {
//                currentGear++; // 자동 기어인 경우 기어를 한 단계 올립니다.
//            }

//            shiftDelay = now + 1.0f; // 다음 기어 전환까지 지연 시간을 설정합니다.
//            shiftTime = 1.5f; // 기어 전환에 필요한 시간을 설정합니다.
//        }
//    }





//    public void ShiftDown()
//    {
//        float now = Time.timeSinceLevelLoad; // 게임 시작 후 경과된 시간을 가져옵니다.

//        if (now < shiftDelay) return; // 현재 시간이 shiftDelay보다 작다면, 기어 전환을 수행하지 않습니다.

//        if (currentGear > 0 || NeutralGear) // 현재 기어가 0보다 크거나 중립 기어라면
//        {
//            carSounds.switchGear.GetComponent<AudioSource>().Play(); // 기어 전환 소리를 재생합니다.

//            if (!carSetting.automaticGear) // 수동 기어인 경우
//            {
//                if (currentGear == 1)
//                {
//                    if (!NeutralGear) { currentGear--; NeutralGear = true; } // 1단에서 중립 기어로 전환합니다.
//                }
//                else if (currentGear == 0) { NeutralGear = false; } // 중립 기어에서 기어를 내립니다.
//                else { currentGear--; } // 기어를 한 단계 내립니다.
//            }
//            else
//            {
//                currentGear--; // 자동 기어인 경우 기어를 한 단계 내립니다.
//            }

//            shiftDelay = now + 0.1f; // 다음 기어 전환까지 지연 시간을 설정합니다.
//            shiftTime = 2.0f; // 기어 전환에 필요한 시간을 설정합니다.
//        }
//    }




//    void OnCollisionEnter(Collision collision)
//    {

//        if (collision.transform.root.GetComponent<VehicleControl>())
//        {

//            collision.transform.root.GetComponent<VehicleControl>().slip2 = Mathf.Clamp(collision.relativeVelocity.magnitude, 0.0f, 10.0f);

//            myRigidbody.angularVelocity = new Vector3(-myRigidbody.angularVelocity.x * 0.5f, myRigidbody.angularVelocity.y * 0.5f, -myRigidbody.angularVelocity.z * 0.5f);
//            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.5f, myRigidbody.velocity.z);


//        }

//    }




//    void OnCollisionStay(Collision collision)
//    {

//        if (collision.transform.root.GetComponent<VehicleControl>())
//            collision.transform.root.GetComponent<VehicleControl>().slip2 = 5.0f;

//    }








//    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//    void Update()
//    {
//        if (!carSetting.automaticGear && activeControl)
//        {
//            if (Input.GetKeyDown("page up"))
//            {
//                ShiftUp(); // 사용자가 Page Up 키를 눌렀을 때 기어를 한 단계 올립니다.
//            }
//            if (Input.GetKeyDown("page down"))
//            {
//                ShiftDown(); // 사용자가 Page Down 키를 눌렀을 때 기어를 한 단계 내립니다.
//            }
//        }
//    }





//    void FixedUpdate()
//    {
//        speed = myRigidbody.velocity.magnitude * 2.7f; // 차량의 현재 속도를 계산합니다 (속도는 m/s를 km/h로 변환한 값).

//        if (speed < lastSpeed - 10 && slip < 10)
//        {
//            slip = lastSpeed / 15; // 속도가 급격히 감소했을 경우 미끄러짐 값을 계산하여 설정합니다.
//        }
//        lastSpeed = speed; // `lastSpeed`를 현재 속도로 업데이트합니다.

//        if (slip2 != 0.0f)
//        {
//            slip2 = Mathf.MoveTowards(slip2, 0.0f, 0.1f); // `slip2` 값을 점진적으로 0으로 이동시킵니다.
//        }

//        myRigidbody.centerOfMass = carSetting.shiftCentre; // 차량의 질량 중심을 설정합니다.

//        if (activeControl)
//        {
//            if (controlMode == ControlMode.simple)
//            {
//                accel = 0;
//                brake = false;
//                shift = false;

//                //if (carWheels.wheels.frontWheelDrive || carWheels.wheels.backWheelDrive)
//                //{
//                steer = Mathf.MoveTowards(steer, Input.GetAxis("Horizontal"), 0.2f); // 수평 입력을 이용해 조향 각도를 설정합니다.
//                accel = Input.GetAxis("Vertical"); // 수직 입력을 통해 가속도 설정
//                brake = Input.GetButton("Jump"); // 점프 버튼(보통 스페이스바)을 눌렀을 경우 브레이크를 설정합니다.
//                                                 //shift = Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift); // Shift 키가 눌렸는지 확인해 부스터 상태를 설정합니다.
//                                                 //}
//            }
//            else if (controlMode == ControlMode.touch)
//            {
//                if (accelFwd != 0)
//                {
//                    accel = accelFwd; // 터치 기반으로 전진 가속도를 설정합니다.
//                }
//                else
//                {
//                    accel = accelBack; // 터치 기반으로 후진 가속도를 설정합니다.
//                }
//                steer = Mathf.MoveTowards(steer, steerAmount, 0.07f); // 터치 입력에 따라 조향 각도를 변경합니다.
//            }
//        }
//        else
//        {
//            accel = 0.0f; // 차량 제어가 비활성화된 경우 가속도, 조향, 브레이크 값을 0으로 설정합니다.
//            steer = 0.0f;
//            brake = false;
//            shift = false;
//        }

//        //if (!carWheels.wheels.frontWheelDrive && !carWheels.wheels.backWheelDrive)
//        //{
//        //    accel = 0.0f; // 앞바퀴나 뒷바퀴가 구동되지 않는 경우 가속도를 0으로 설정합니다.
//        //}

//        if (carSetting.carSteer)
//        {
//            //carSetting.carSteer.localEulerAngles = new Vector3(steerCurAngle.x, steerCurAngle.y, steerCurAngle.z + (steer * -120.0f)); // 조향 장치의 각도를 설정합니다.
//        }

//        if (carSetting.automaticGear && (currentGear == 1) && (accel < 0.0f))
//        {
//            if (speed < 5.0f)
//            {
//                ShiftDown(); // 차량이 자동 기어 모드이고 현재 기어가 1단이며 속도가 충분히 낮고 후진 중이라면 기어를 내립니다.
//            }
//        }
//        else if (carSetting.automaticGear && (currentGear == 0) && (accel > 0.0f))
//        {
//            if (speed < 5.0f)
//            {
//                ShiftUp(); // 차량이 자동 기어 모드이고 현재 기어가 중립이며 속도가 충분히 낮고 전진 중이라면 기어를 올립니다.
//            }
//        }
//        else if (carSetting.automaticGear && (motorRPM > carSetting.shiftUpRPM) && (accel > 0.0f) && speed > 10.0f && !brake)
//        {
//            ShiftUp(); // RPM이 높고 가속 중이며 속도가 10km/h 이상이고 브레이크를 밟지 않았다면 기어를 올립니다.
//        }
//        else if (carSetting.automaticGear && (motorRPM < carSetting.shiftDownRPM) && (currentGear > 1))
//        {
//            ShiftDown(); // RPM이 낮고 현재 기어가 1단보다 크다면 기어를 내립니다.
//        }

//        if (speed < 1.0f)
//        {
//            Backward = true; // 속도가 1km/h 미만일 때 후진 상태로 전환합니다.
//        }

//        if (currentGear == 0 && Backward == true)
//        {
//            if (speed < carSetting.gears[0] * -10)
//            {
//                accel = -accel; // 후진 기어일 때 후진 한계 속도를 초과하면 가속도를 반대로 설정합니다.
//            }
//        }
//        else
//        {
//            Backward = false; // 후진 상태가 아니면 `Backward`를 `false`로 설정합니다.
//        }

//        // 브레이크 라이트 설정
//        //foreach (Light brakeLight in carLights.brakeLights)
//        //{
//        //    if (brake || accel < 0 || speed < 1.0f)
//        //    {
//        //        brakeLight.intensity = Mathf.MoveTowards(brakeLight.intensity, 8, 0.5f); // 브레이크 또는 속도가 낮은 경우 브레이크 라이트를 켭니다.
//        //    }
//        //    else
//        //    {
//        //        brakeLight.intensity = Mathf.MoveTowards(brakeLight.intensity, 0, 0.5f); // 브레이크 상태가 아니라면 라이트를 끕니다.
//        //    }

//        //    brakeLight.enabled = brakeLight.intensity == 0 ? false : true; // 라이트의 밝기에 따라 켜짐/꺼짐을 설정합니다.
//        //}

//        // 후진 라이트 설정
//        //foreach (Light WLight in carLights.reverseLights)
//        //{
//        //    if (speed > 2.0f && currentGear == 0)
//        //    {
//        //        WLight.intensity = Mathf.MoveTowards(WLight.intensity, 8, 0.5f); // 속도가 2km/h 이상이고 중립 기어일 때 후진등을 켭니다.
//        //    }
//        //    else
//        //    {
//        //        WLight.intensity = Mathf.MoveTowards(WLight.intensity, 0, 0.5f); // 그렇지 않은 경우 후진등을 끕니다.
//        //    }
//        //    WLight.enabled = WLight.intensity == 0 ? false : true; // 라이트의 밝기에 따라 켜짐/꺼짐을 설정합니다.
//        //}

//        // 기타 계산 및 엔진 소리 관련 설정은 아래와 같은 방식으로 `FixedUpdate` 내에서 지속적으로 업데이트됩니다.

//        // 메서드의 나머지 부분에서는 바퀴 회전, 슬립 값, 니트로 부스트 등을 처리하며, 이는 전체 차량의 움직임 및 효과적인 제어를 가능하게 합니다.

//        // 바퀴 회전 각도, 마찰력, 니트로 부스트의 활성화 등 다양한 차량의 물리적 속성을 계속해서 관리하고 업데이트합니다.

//        // `FixedUpdate` 메서드는 Unity에서 물리 계산을 위해 일정한 시간 간격으로 호출되므로, 차량의 물리적 움직임과 관련된 부분은 이 메서드에서 처리하는 것이 중요합니다.
//    }





//    /////////////// Show Normal Gizmos ////////////////////////////

//    void OnDrawGizmos()
//    {

//        if (!carSetting.showNormalGizmos || Application.isPlaying) return;

//        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

//        Gizmos.matrix = rotationMatrix;
//        Gizmos.color = new Color(1, 0, 0, 0.5f);

//        Gizmos.DrawCube(Vector3.up / 1.5f, new Vector3(2.5f, 2.0f, 6));
//        Gizmos.DrawSphere(carSetting.shiftCentre / transform.lossyScale.x, 0.2f);

//    }




//}

//이 코드 cube(리지드바디 있음 gravity 미사용) 에 붙이면 작동할까?