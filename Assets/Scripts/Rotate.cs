using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private List<GameObject> killCells;
    [SerializeField] float _speed;

    void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (_player.GetStat(TypeStats.KILL_CELL_COUNT) > 0 && !killCells.All((cell) => cell.activeInHierarchy))
        {

            for (int i = 0; i < _player.GetStat(TypeStats.KILL_CELL_COUNT); i++)
            {
                killCells[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, _speed * Time.time);
    }
}
