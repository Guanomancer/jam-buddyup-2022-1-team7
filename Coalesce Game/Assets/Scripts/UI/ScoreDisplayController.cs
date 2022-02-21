using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Coalesce.UI
{
    public class ScoreDisplayController : MonoBehaviour
    {
        private void Start()
            => GetComponent<Text>().text = $"{(float)System.Math.Round(Mathf.Clamp(FindObjectOfType<ScoreKeeper>().DestructionRatio * 100f, 0f, 100f), 2)}%";
    }
}
