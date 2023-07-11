using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SceanController : MonoBehaviour
{
//    [Header("SpawnPoint")]
//    [field: SerializeField] private List<Transform> spawnPointsAI = new List<Transform>();
//    private List<SpawnerAI> spawnerAI = new List<SpawnerAI>();

//    [Header("Spawner - entity")]
//    [field: SerializeField] protected int MaxCountSpawnAI = 2;
//    [field: SerializeField] protected GameObject prefabAI;
//    [field: SerializeField] private float coolDownSpawn = 10f;

//    [Header("DELETE! Points checking AISsytems")]
//    [field: SerializeField] private List<Transform> checkingPointsAI1 = new List<Transform>();
//    [field: SerializeField] private List<Transform> checkingPointsAI2 = new List<Transform>();
//    [field: SerializeField] private List<Transform> checkingPointsAI3 = new List<Transform>();
//    [field: SerializeField] private List<Transform> checkingPointsAI4 = new List<Transform>();

//    private void Awake()
//    {
//        var a = new SpawnerAI(MaxCountSpawnAI, prefabAI, spawnPointsAI[0], checkingPointsAI1);
//        a.Spawn();

//        //var b = new SpawnerAI(MaxCountSpawnAI, prefabAI, spawnPointsAI[1], checkingPointsAI2);
//        //b.Spawn();

//        //var c = new SpawnerAI(MaxCountSpawnAI, prefabAI, spawnPointsAI[2], checkingPointsAI3);
//        //c.Spawn();

//        //var d = new SpawnerAI(MaxCountSpawnAI, prefabAI, spawnPointsAI[3], checkingPointsAI4);
//        //d.Spawn();
//    }

//    public class SpawnerAI : SceanController
//    {
//        private bool spawnAI;
//        private int currentAI;
//        private Transform spawnPointAI;
//        private AbstractEntity abstractEntityAI;
//        private AISystems aiSystems;
//        private List<Transform> checkingPointAI = new List<Transform>();

//        public SpawnerAI(int MaxCountSpawnAI, GameObject prefabAI, Transform spawnPointAI, List<Transform> checkingPointsAI)
//        {
//            this.MaxCountSpawnAI = MaxCountSpawnAI;
//            this.prefabAI = prefabAI;
//            this.spawnPointAI = spawnPointAI;
//            checkingPointAI = checkingPointsAI.ToList();
//        }

//        public void Spawn()
//        {
//            Instantiate(prefabAI, spawnPointAI);
//            currentAI += 1;

//            abstractEntityAI = prefabAI.GetComponent<AbstractEntity>();
//            aiSystems = prefabAI.GetComponent<AISystems>();
//            aiSystems.PointsCheckingAI = checkingPointAI;
//            spawnAI = false;
//            StartCoroutine(RechargeSpawn());
//        }

//        private void Update() 
//        {
//            if (currentAI < MaxCountSpawnAI && spawnAI == true) 
//            {
//                Spawn();
//            }

//            if (abstractEntityAI.isLiveEntity == false && spawnAI == true) 
//            {
//                Spawn();
//            }
//        }

//        private IEnumerator RechargeSpawn()
//        {
//            yield return new WaitForSeconds(coolDownSpawn);
//            spawnAI = true;
//        }
//    }
}
