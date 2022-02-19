using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce.EventRouting
{
    // Game flow
    public struct TutorialStart : EventData { }
    public struct GameStart : EventData { }
    public struct GameEnd : EventData { }
    public struct TimesIsUp : EventData { }

    // Nanny
    public struct NannyStateChanged : EventData { }
    public struct NannyEntersRoom : EventData { }
    public struct NannyClosedDoor : EventData { }

    // Zilla
    public struct ZillaAproachesDoor : EventData { }
    public struct ZillaEntersRoom : EventData { }
    public struct ZillaLeftFoot : EventData { }
    public struct ZillaRightFoot : EventData { }

    // Blocks
    public struct ScoringBlocksStable : EventData { }
    public struct ScoringBlockMessy : EventData { }
    public struct FirstScoringBlockMessy : EventData { }
    public struct AllScoringBlocksMessy : EventData { }
}
