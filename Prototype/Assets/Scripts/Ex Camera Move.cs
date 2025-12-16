using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 5f;
    
    [Header("Look Ahead Settings")]
    [SerializeField] private float lookAheadDistance = 2f;
    [SerializeField] private float stateChangeDelay = 0.3f;
    [SerializeField] private float offsetTransitionSpeed = 2f;
    
    [Header("Camera Bounds (Optional)")]
    [SerializeField] private bool useBounds;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    
    private Vector3 _currentOffset = Vector3.zero;
    private Vector3 _targetOffset = Vector3.zero;
    private float _currentStateTime;
    private bool _wasMoving;
    private Rigidbody2D _playerRb;

    private void Start()
    {
        _playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (!player) return;
        
        // 플레이어의 이동 상태 감지
        bool isMoving = Mathf.Abs(_playerRb.linearVelocity.x) > 0.1f;
        float moveDirection = Mathf.Sign(_playerRb.linearVelocity.x);
        
        // 상태 변화 감지
        bool stateChanged = (isMoving != _wasMoving);
        
        if (stateChanged)
        {
            _currentStateTime = 0f;
        }
        else
        {
            _currentStateTime += Time.deltaTime;
        }
        
        // 일정 시간 상태가 유지되면 오프셋 변경
        if (_currentStateTime >= stateChangeDelay)
        {
            // 이동 중: 이동 방향으로 오프셋 적용
            _targetOffset = isMoving ? new Vector3(moveDirection * lookAheadDistance, 0f, 0f) :
                // 정지 중: 오프셋 제거
                Vector3.zero;
        }
        
        // 오프셋을 부드럽게 전환
        _currentOffset = Vector3.Lerp(_currentOffset, _targetOffset, offsetTransitionSpeed * Time.deltaTime);
        
        // 기본 위치 계산 (플레이어 위치 + 오프셋)
        var desiredPosition = player.position + _currentOffset;
        desiredPosition.z = transform.position.z; // 카메라의 Z 위치 유지
        
        // 부드러운 이동
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        // 경계 제한 (옵션)
        if (useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);
        }
        
        transform.position = smoothedPosition;
        
        // 현재 상태 저장
        _wasMoving = isMoving;
    }
    
    // 디버그용: 카메라 범위 표시
    private void OnDrawGizmosSelected()
    {
        if (useBounds)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2, (minBounds.y + maxBounds.y) / 2, 0);
            Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
        
        if (player != null)
        {
            // 현재 타겟 위치 표시
            Gizmos.color = Color.green;
            Vector3 targetPos = player.position + _currentOffset;
            targetPos.z = 0;
            Gizmos.DrawWireSphere(targetPos, 0.5f);
            
            // Look ahead 범위 표시
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(player.position, player.position + Vector3.right * lookAheadDistance);
            Gizmos.DrawLine(player.position, player.position + Vector3.left * lookAheadDistance);
        }
    }
}