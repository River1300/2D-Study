/*
만들 게임 : 쯔꾸르 형식의 2D 플랫폼 게임
    기능
        플레이어 이동
        NPC 대화
        오브젝트 상호작용
        퀘스트
        저장, 불러오기
*/

/*
목표 : 게임에 사용될 맵을 만들고 싶다.
    1. 맵을 만들기 위해 스프라이트를 그린다.
    2. 스프라이트는 Rule Tile을 이용하여 그리도록 한다.
    3. Grid에 TileMap을 분류하여 플레이어가 걸어다닐 맵과 충돌할 맵을 따로 그린다.
    4. Player가 맵 밖으로 나가지 못하도록 경계선을 그려준다.
        Mask Interaction + Rigidbody2D + Tilemap Collider2D + Composite Collider2D
*/

/*
목표 : 플레이어가 키보드 입력에 따라 움직이게 하고 싶다.
    1. 필요 속성 : Rigidbody2D, 속도, 수평값, 수직값
    2. 매 프레임마다 입력을 감지하여 변수에 수직, 수평 값을 저장한다.
    3. 저장한 변수를 이용하여 velocity에 배정한다.
    4. 속도를 곱해준다.
    5. 쯔꾸르 게임은 평면에 붙어서 이동한다. Player의 중력값을 0으로 설정한다.
*/

/*
목표 : 플레이어가 대각선 방향으로 움직이지 못하게 하고 싶다.
    1. 필요 속성 : 수평 이동인지 체크할 불값(isHorizonMove), 수직/수평 버튼 입력을 체크할 불값(hDown,vDown,hUp,vUp)
    2. (isHorizonMove) 값에 따라서 수평 이동만 하거나 수직 이동만 하게 한다.
    3. 버튼이 눌렸을 때와 버튼이 때어졌을 때 (isHorizonMove)에 값을 준다.
        수평/수직 으로 버튼이 눌렸는지, 때어젔는지 확인하는 bool 변수를 만들고
        그 bool 변수를 통해서 플레이어가 수평으로 이동하는지 아닌지를 정한다.
        버튼이 때어졌을 때는 수평값이 0인지 아닌지로 (isHorizonMove)값을 준다.
*/

/*
목표 : 플레이어가 이동할 때 애니메이션을 출력하고 싶다.
    1. 필요 속성 : Animator
    2. 먼저 스프라이트를 방향 별 이동 + 방향 별 멈춤으로 애니메이션 클립을 만든다.
    3. 플레이어는 어떤 상태에서든 움직일 수 있고 움직임의 입력이 없어지면 그 방향으로 멈춘다.
    4. 수평과 수직으로 사용자 입력을 받으므로 int형 수직, 수평 파라미터를 만든다.
        0보다 크거나 작거나 혹은 같은지로 애니메이션 클립을 이동한다.
        파라미터는 전달될 때마다 애니메이터가 갱신한다. 
            한쪽 방향으로 움직일 때 같은 값을 계속해서 파라미터로 전달하지 않도록 제어문을 만들어 준다.
            또 불 파라미터를 추가로 만들어서 이동 중인지를 체크한다.
    5. 플레이어가 움직이는 입력키를 받을 때 파라미터를 전달한다.
*/

/*
목표 : 플레이어의 앞에있는 오브젝트와 상호작용하기 위해 오브젝트 정보를 받아오고 싶다.
    1. 필요 속성 : Vector3, RaycastHit2D, LayerMask, GameObject
    2. 게임 씬에서 시각적으로 Ray를 표시해 준다.
        여기서 Ray의 발사 방향이 필요하다.
        방향 벡터를 만들어 주고 버튼 입력과 속도를 측정해서 벡터 값을 지정해 준다.
        버튼을 동시에 눌렀다가 하나만 때었을 때 Ray가 이전 방향으로 발사된다.
            이를 방지하기 위해 버튼의 입력을 제어문에서 제거하고 애니메이션의 파라미터( bool )을 받아와서 애니메이션이 바뀔 때 방향에 대한 속도를 체크해 준다.
    3. RaycastHit2D 구조체로 Ray에 충돌한 오브젝트의 정보를 받아온다.
        Ray는 Physics2D.Raycast()로 발사한다.
    4. rayHit에 있는 collider가 null이 아니라면 오브젝트 정보를 받아왔다는 뜻
    5. 받은 오브젝트 정보를 GameObject로 저장한다.
    6. 사용자가 오브젝트 앞에서 스페이스바를 입력하면 해당 오브젝트와 상호작용 한다.
*/

/*
목표 : 대화창 UI를 구성하고 싶다.
    1. 필요 속성 : Canvas, UI Image, UI Text, 대화창 스프라이트
    2. 대화창 스프라이트의 위치와 크기를 지정한다.
    3. 스프라이트 이미지가 깨지므로 스프라이트 에디터로 들어가 Border을 수정해 준다.
    4. Image Type을 Slice로 변경해 준다.
    5. Text UI 추가
*/

/*
목표 : Debug Log로 출력하던 오브젝트 이름을 대화창을 통해 출력하고 싶다.
    1. 데이터 전달을 위해 게임 매니저를 만든다.
    2. 필요 속성 : 플레이어가 스캔한 오브젝트 정보, Text UI
    3. 상호 작용 함수를 public 으로 만든다.
        매개 변수로 플레이어가 스캔한 오브젝트 정보를 받는다.
        받은 정보를 게임 오브젝트로 저장하고 Text 로 전달한다.
    4. 플레이어 스크립트에서 게임 매니저의 함수를 호출할 수 있도록 게임 매니저 변수를 만든다.
    5. 게임 매니저 변수를 통해 public 함수를 호출한다.
*/

/*
목표 : 오브젝트와 상호작용을 할 때만 대화창이 나타나게 하고 싶다.
    1. 필요 속성 : 대화창 게임 오브젝트, 대화창 On/Off 상태 표시 bool 변수
    2. 게임 매니저의 상호작용 함수가 실행될 때 대화창을 활성화되고, bool 변수가 true가 된다.
    3. 이미 bool이 true일 경우 false로 바꾸고 대화창을 비활성화 한다.
        그냥 bool을 활성화 비활성화에 배정한다.
*/

/*
목표 : 플레이어가 오브젝트와 상호작용 하고 있는 중 이라면 움직일 수 없게 하고 싶다.
    1. 플레이어 스크립트에서 게임 매니저의 bool변수를 받아서 사용한다.
    2. bool 변수가 true라는 것은 상호작용 중 이라는 것이다.
    3. 플레이어 스크립트에서 이동 값 변수, 버튼 입력 변수를 0, false로 둔다.
*/

/*
목표 : 대화창에 커서 애니메이션을 넣고 싶다.
    1. 대화창 자식으로 커서 이미지를 넣는다.
    2. 애니메이터 컴포넌트를 부착한다.
    3. 애니메이터와 애니메이션 클립을 만든다.
*/