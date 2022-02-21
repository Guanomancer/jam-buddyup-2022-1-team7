using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public interface IZilla { }
    public interface IBlock { bool IsMessy(); Transform transform { get; } GameObject gameObject { get; } }
}
