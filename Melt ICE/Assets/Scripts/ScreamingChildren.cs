using UnityEngine;

public class ScreamingChildren : MonoBehaviour
{
    // how fast and when children will pop up
    [SerializeField] private float _childrensSpeed = 1.0f;
    [SerializeField] private float _childrensComedicTiming = 3.0f;
    private float _playerPos;

    [SerializeField] private float _popupDistance = 20.0f;
    
    void Update()
    {
        _playerPos = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if(_playerPos <= _popupDistance)
        {
            _childrensComedicTiming -= Time.deltaTime;
            if(_childrensComedicTiming > 0.0f)
            {
                transform.Translate(0, _childrensSpeed * Time.deltaTime, 0);

            }
            
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        transform.LookAt(Player.Instance.transform);

    }
}
