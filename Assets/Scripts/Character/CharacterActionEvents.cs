using UnityEngine;

namespace CharacterSystem {

    public class OnCharacterUpdate { }
    
    public class OnCharacterKilled { }
    
    public class OnCharacterCollisionEnter {
        public OnCharacterCollisionEnter(GameObject other) {
            Other = other;
        }
        public GameObject Other;
    }

}