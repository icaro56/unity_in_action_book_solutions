using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // força a exposição da variável no editor, mas outros scripts continuam não enxergando a variável privada
    [SerializeField]
    private GameObject enemyPrefab;

    private GameObject _enemy;

    private float _enemySpeed = 3.0f;
    private float _enemyStartSpeed = 3.0f;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        _enemySpeed = _enemyStartSpeed * value;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
            _enemy.GetComponent<WanderingAI>().speed = _enemySpeed;
        }
    }

}
