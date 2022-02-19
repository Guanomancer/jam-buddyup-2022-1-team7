using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public struct TutorialStart : EventData { }
    public struct GameStart : EventData { }
    public struct GameEnd : EventData { }
    public struct TimesIsUp : EventData { }
    public struct NannyStateChanged : EventData { }
    public struct ZillaAproachesDoor : EventData { }
    public struct ZillaEntersRoom : EventData { }
    public struct NannyEntersRoom : EventData { }
    public struct NannyClosedDoor : EventData { }
    public struct ScoringBlocksStable : EventData { }
    public struct ScoringBlockMessy : EventData { }
    public struct FirstScoringBlockMessy : EventData { }
    public struct AllScoringBlocksMessy : EventData { }
}
