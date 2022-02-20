using System;

namespace Coalesce.EventRouting
{
    public static class EventTypes
    {
        public static bool EventTypeFromString(string typeName, out Type type)
        {
            type = typeof(EventTypes).GetNestedType(typeName);
            return type != null;
        }

        public static Type[] GetAll()
        {
            return typeof(EventTypes).GetNestedTypes();
        }

        // Game flow
        public struct TutorialStart : IEventData { }
        public struct GameStart : IEventData { }
        public struct GameEnd : IEventData { }
        public struct TimesIsUp : IEventData { }

        // Nanny
        public struct NannyStateChanged : IEventData { public Type NewState; public override string ToString() => NewState.Name; }
        public struct NannyEntersRoom : IEventData { }
        public struct NannyClosedDoor : IEventData { }
        public struct NannyFoundMessyBlock : IEventData { }
        public struct NannyPickedUpMessyBlocks : IEventData { }
        public struct NannyCanReachZilla : IEventData { }
        public struct NannyPickedUpZilla : IEventData { }
        public struct NannyPutDownZilla : IEventData { }
        public struct NannyStartedMoving : IEventData { }
        public struct NannyStoppedMoving : IEventData { }
        public struct NannyGoingToRest : IEventData { };

        // Zilla
        public struct ZillaAproachesDoor : IEventData { }
        public struct ZillaEntersRoom : IEventData { }
        public struct ZillaLeftFoot : IEventData { }
        public struct ZillaRightFoot : IEventData { }

        // Blocks
        public struct ScoringBlocksStable : IEventData { }
        public struct ScoringBlockMessy : IEventData { public float DestructionRatio; }
        public struct FirstScoringBlockMessy : IEventData { }
        public struct AllScoringBlocksMessy : IEventData { }
    }
}
