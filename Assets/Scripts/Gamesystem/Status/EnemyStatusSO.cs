using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatusSO : ScriptableObject
{
    public List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();
    [System.Serializable] 
    public class EnemyStatus
    {
        [SerializeField] int HP;
        [SerializeField] int Attack1;

        public int hp => HP;
        public int attack1 => Attack1;
    }
    
    
}
