namespace Project.Scripts.BehaviorTree
{
    public abstract class BTNode
    {
        public abstract bool Execute(AIController ai);
    }
    
    
    /*
     * Root (Selector)
       │
       ├── Avoid Light? (Vampires only)
       │   ├── Retreat
       │
       ├── Can Ambush? 
       │   ├── Ambush Attack
       │
       ├── Can Detect Player?
       │   ├── Stalk Player
       │
       ├── Werewolf-Specific?
       │   ├── Howl (Summon Allies)
       │   ├── Enrage (Low Health Buff)
       │
       └── Idle (Default)
     */
}