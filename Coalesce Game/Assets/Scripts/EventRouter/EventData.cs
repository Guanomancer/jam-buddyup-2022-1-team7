using System;

namespace Coalesce.EventRouting
{
    public static class EventTypes
    {
        // Game flow
        public struct TutorialStart : IEventData { }
        public struct GameStart : IEventData { }
        public struct GameEnd : IEventData { }
        public struct TimesIsUp : IEventData { }

        // Nanny
        public struct NannyStateChanged : IEventData { public Type NewState; public override string ToString() => NewState.Name; }
        public struct NannyEntersRoom : IEventData { }
        public struct NannyClosedDoor : IEventData { }

        // Nanny Controller
        public struct NannyFoundMessyBlock : IEventData { }
        public struct NannyPickedUpMessyBlocks : IEventData { }
        public struct NannyCanReachZilla : IEventData { }
        public struct NannyPickedUpZilla : IEventData { }
        public struct NannyPutDownZilla : IEventData { }
        public struct NannyStartedMoving : IEventData { }
        public struct NannyStoppedMoving : IEventData { }

        // Zilla
        public struct ZillaAproachesDoor : IEventData { }
        public struct ZillaEntersRoom : IEventData { }
        public struct ZillaLeftFoot : IEventData { }
        public struct ZillaRightFoot : IEventData { }

        // Blocks
        public struct ScoringBlocksStable : IEventData { }
        public struct ScoringBlockMessy : IEventData { }
        public struct FirstScoringBlockMessy : IEventData { }
        public struct AllScoringBlocksMessy : IEventData { }
    }
}
