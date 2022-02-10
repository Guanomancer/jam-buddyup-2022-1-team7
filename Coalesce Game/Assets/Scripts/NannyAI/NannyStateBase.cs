namespace Coalesce
{
    public abstract class NannyStateBase
    {
        public NannyAI Nanny { get; private set; }

        public void Start(NannyAI nanny)
        {
            Nanny = nanny;
            OnStart();
        }

        public virtual void OnStart() { }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
    }
}
