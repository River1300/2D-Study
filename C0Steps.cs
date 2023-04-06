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

/*
목표 : 체계적인 대화 시스템을 만들기 위해 오브젝트 구조체를 구성하고 싶다.
    1. 플레이어 앞에 있는 오브젝트가 어떤 정보를 가지고 있는지 알려줄 스크립트를 만든다.
    2. 필요 속성 : 해당 오브젝트의 ID번호, NPC인지 아닌지
    3. 상호작용이 가능한 오브젝트에 해당 스크립트를 부착하고 ID번호와 NPC인지 아닌지를 체크해 준다.
*/

/*
목표 : 오브젝트의 대화 목록을 만들고 싶다.
    1. Talk매니저를 만든다.
    2. Dictionary<> 클래스로 오브젝트 아이디와 가지고 있는 문자열 배열을 구성한다.
    3. 문자열 배열에 대사를 저장할 함수를 만들어 준다.
*/

/*
목표 : 만들어진 대화 목록을 가지고 오브젝트와 상호작용 하고 싶다.
    1. Talk매니저에서 문자열을 반환하는 public 함수를 만든다.
    2. Dictionary<>에 저장한 대사를 반환한다.
        id를 Key로 넘겨주고 배열로 나온 Value 중에서 인덱스를 전달하여 해당 대사를 반환한다.
    3. 게임 매니저에서 대화창을 띄웠었다. 게임 매니저에서 Talk매니저를 불러와서 public 함수를 호출한다.
        id는 scanObject로 받아온것을 넘겨주면 된다.
    4. 대사를 반환 받으면 text로 출력한다.
*/

/*
목표 : 오브젝트의 대화 배열이 2개 이상일 경우 모든 대화문을 출력하고 싶다.
    1. 플레이어가 스페이스바를 입력하면 게임 매니저의 Action 함수가 실행되며 대화창이 활성화 비활성화된다.
    2. 이를 모든 대화 목록이 출력된는지 확인하고 비활성화 되도록 바꿔주어야 한다.
    3. 인덱스가 대화 배열의 사이즈와 같은 값이 된다면 false로 하고 대화가 진행될 때 true로 한다.
    4. 플레이어가 오브젝트와 상호작용을 한다.
        게임 매니저에 오브젝트 정보가 전달되고 해당 정보를 매개변수로 전달하여 Talk함수를 실행한다.
        게임 매니저가 Talk함수를 통해 Talk 매니저의 GetTalk함수를 호출한다.
        Talk 매니저는 저장된 대화를 반환한다.
        게임 매니저가 대화를 반환 받아 출력하고 대화창을 활성화 한다.
        대화 인덱스를 증가시킨다.
        플레이어가 한 번 더 스페이스바를 눌러 상호작용을 한다.
        게임 매니저는 다시 한 번 Talk 매니저의 GetTalk함수를 호출한다.
        이때 인덱스의 값에 따라 대화문이나 null을 반환한다.
        대화문을 반환 받을 경우 이전과 동일하게 진행한다.
        null을 반환 받으면 대화를 종료하고 대화창을 닫는다.
        인덱스 값을 초기화 한다.
*/

/*
목표 : NPC와 상호작용할 때 대화창 위에 NPC의 초상화를 띄우고 싶다.
    1. 대화창 자식으로 초상화 이미지를 넣는다.
    2. 초상화는 4개씩 존재한다. 게임 매니저에 이미지 UI에 접근하기 위한 변수를 만들어 준다.
    3. 플레이어로 부터 받은 오브젝트 정보가 NPC일 경우 알파값을 1로 아니면 0으로 한다.
    4. Talk매니저에 초상화를 담을 Dictionary<>를 만든다.
        key = id, value = sprite
    5. 초상화를 0 ~ 3까지 번호를 부여하고 id값에 0 ~ 3을 더하여 id를 구성한다.
    6. 스프라이트를 배열로 받아서 모든 스프라이트를 적용한다.
        Dictionary에 id에 맞는 스프라이트 배열을 넣어 준다.
    7. 지정된 초상화 스프라이트를 반환할 함수를 만들어 준다.
    8. 구분자와 Split을 이용하여 초상화 인덱스를 지정한다.
    9. 게임 매니저에서 대화문을 받기 위해 Talk함수에서 Talk매니저의 GetTalk()함수를 호출하는데
        이때 게임 오브젝트가 NPC일 경우 전달받은 대화문을 구분자로 나누어 0번 인덱스만을 대화창으로 출력하고
        1번 인덱스는 다시 Talk매니저의 GetPortrait()함수를 호출하여 UI 이미지에 스프라이트를 배정한다.
*/

/*
목표 : 퀘스트 대화를 위한 기본적인 시스템을 구성하고 싶다.
    1. 퀘스트 매니저와 퀘스트 데이터라는 스크립트를 만든다.
    2. 퀘스트 매니저는 퀘스트 시스템을 관리할 관리자이고 퀘스트 데이터는 퀘스트에 필요한 속성(NPC id, 퀘스트 타이틀)을 구성하는 구조체이다.
    3. 퀘스트 데이터에서 속성을 선언하고 생성자를 만들어서 구조체를 할당할 때 자동으로 초기화 되도록 한다.
    4. 퀘스트 매니저에서 퀘스트 아이디를 key, 구조체를 value로 갖는 Dictionary를 만든다.
        퀘스트를 입력한다.
    5. 퀘스트 번호를 반환하는 함수를 만든다.
    6. 게임 매니저에서 퀘스트 번호를 추가로 연산하여 대화문을 호출한다.
    7. Talk매니저에 퀘스트 전용 대화를 만들어 준다.
    8. 퀘스트 번호는 10단위로 해당 퀘스트에서 진행 순서는 1단위로 인덱스를 나누어 대화가 끝날 때 증가시킨다.
    9. 다음 퀘스트로 넘어갈때 1단위 인덱스는 초기화, 10단위 인덱스는 증가 시킨다.
*/

/*
목표 : 퀘스트 중 다른 오브젝트와 상호작용 하는 퀘스트를 만들고 싶다.
    1. 퀘스트 전용 오브젝트를 받기위해 퀘스트 매니저에서 배열로 게임오브젝트를 만든다.
    2. 해당 오브젝트를 활성화 시킬 함수를 만든다.
    3. 대화가 끝날때 마다 오브젝트를 활성화 시킬지 체크한다.
*/

/*
목표 : 퀘스트 중 일반 대사만을 내뱉는 오브젝트는 일반 대사를 정상적으로 출력하고 싶다.
    1. Talk매니저에서 오브젝트 대사를 반환하는 함수에서 제어를 한다.
    2. 퀘스트 대화문 ID는 10단위로 만들어지고 퀘스트 대화문의 진행 인덱스는 1단위로 만들어서 오브젝트 ID와 더해져서 해당 key에 있는 대사를 반환하게 된다.
    3. 당연히 퀘스트와 연관 없는 오브젝트는 10단위와 1단위의 값이 없고 이를 이용하여 id - id % 100, id - id % 10으로 일반화 시킬 수가 있게 된다.
*/

/*
목표 : 대화창에 애니메이션을 주고 싶다.
    1. 대화창을 위한 애니매이터와 애니매이션 클립을 만든다.
    2. 대화창이 단순히 활성화 비활성화 되는 것을 아래에서 위로 움직이는 방식으로 바꾼다.
    3. 대화창이 온오프 되는 게임 매니저에서 온오프를 수정한다.
*/

/*
목표 : NPC 초상화에 애니메이션을 주고 싶다.
    1. 초상화를 위한 애니메이터와 애니메이션 클립을 만든다. Loop Check
    2. 초상화의 표정이 바뀔 때마다 위 아래로 한번 움직이는 애니매이션을 만든다.
    3. 파라미터로 트리거를 지정한다.
*/

/*
목표 : 대화창에 입력되는 문자열에 애니메이션을 주고 싶다.
    1. 타이핑 전용 스크립트를 만들어서 Text UI에 부착한다.
    2. 필요 속성 : 대사를 저장할 문자열, 타이핑 속도를 위한 변수, 텍스트를 저장할 변수, 인덱스 변수
    3. 공백에서 애니메이션이 시작된다.
    4. 한글자씩 써지는 애니매이션이 진행된다.
    5. 게임 매니저에서 대사를 받아와 그대로 출력하지 않고 타입 이펙트의 함수를 호출하여 대사를 출력한다.
*/

/*
목표 : 대화 문자가 입력될 때 사운드를 플레이 하고 싶다.
    1. UI 텍스트에 오디오 소스를 부착하고 오디오 클립을 지정한다.
    2. 타이핑이 진행될 때마다 사운드를 플레이 시킨다.
*/

/*
목표 : 타이핑이 진행되는 도중에 한 번 더 스페이스바를 누르면 모든 타이핑이 완료되게 하고 싶다.
    1. 현재 타이핑 중인지 확인할 bool 변수가 필요하다.
    2. 타이핑이 진행 중일때는 true, 타이핑이 종료되면 false
    3. 게임 매니저에서 대화중 스페이스바를 또 누르면 다음 대화로 넘어간다.
        우리는 타이핑을 완료시키는게 목표이기 떄문에 true일 경우에는 다음 대화 인덱스로 증가시키지 못하게 제어해야 한다.
*/

/*
목표 : 메뉴 화면 구축하기
    1. ESC버튼 눌렀을 때 나오는 메뉴 화면을 씬에 구성한다.
    2. 계속하기, 저장하기, 종료하기
    3. 퀘스트 타이틀 화면
    4. ESC 버튼을 누르면 메뉴 화면을 활성화 한다.
*/

/*
목표 : 계속하기 버튼
    1. OnClick
    2. 메뉴 게임 오브젝트 전달
    3. 유니티에서 제공하는 SetActive 함수 등록
*/

/*
목표 : 퀘스트 타이들
    1. 게임 매니저에서 타이틀 텍스트를 받아온다.
    2. 퀘스트 매니저의 타이틀 함수를 호출한다.
*/

/*
목표 : 게임 종료
    1. Application로 종료할 종료 함수를 만든다.
*/

/*
목표 : 저장하기
    1. 게임 매니저에 세이브와 로드 함수를 만든다.
    2. 현재 진행중인 퀘스트, 플레이어 위치를 저장한다.
    3. PlayerPrefs로 저장한다. 저장 이름에 값을 넣는다.
    4. PlayerPrefs로 불러와서 각 변수에 다시 배정한다.
*/