using UnityEngine;

namespace Coalesce.DeadCode
{
    public abstract class NannyStateBase
    {
        public NannyAI Nanny { get; private set; }

        protected float StateEntryTime { get; private set; }

        public void Start(NannyAI nanny)
        {
            Nanny = nanny;
            OnStart();
        }

        public void Enter()
        {
            StateEntryTime = Time.time;
            OnEnter();
        }

        public virtual void OnStart() { }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
    }
}
