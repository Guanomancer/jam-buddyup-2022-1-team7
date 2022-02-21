using UnityEngine;

namespace Coalesce.Nanny
{
    public abstract class NannyStateBase
    {
        public NannyAI Nanny { get; private set; }
        public NannyController Controller { get; private set; }

        public float StateEntryTime { get; private set; }
        public float StateActiveTime => Time.time - StateEntryTime;

        public void Start(NannyAI nanny, NannyController controller)
        {
            Nanny = nanny;
            Controller = controller;
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
