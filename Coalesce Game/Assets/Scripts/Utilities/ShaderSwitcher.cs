using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coalesce
{
    public class ShaderSwitcher : MonoBehaviour
    {
        [SerializeField]
        private Material[] _materials;
        [SerializeField]
        private Shader _oldShader;
        [SerializeField]
        private Shader _newShader;
        [SerializeField]
        private ShaderSwitcherTextureRemap[] _textureRemaps;

        [ContextMenu("Grab shader from material")]
        public void GrabShaderFromMaterial()
            => _oldShader = _materials.Length > 0 ? _materials[0].shader : null;

        [ContextMenu("Grab material texture names")]
        public void GrabMaterialTextureNames()
        {
            var names = _materials[0].GetTexturePropertyNames();
            _textureRemaps = new ShaderSwitcherTextureRemap[names.Length];
            for (int i = 0; i < names.Length; i++)
                _textureRemaps[i] = new ShaderSwitcherTextureRemap { FromMapName = names[i] };
        }

        [ContextMenu("Apply new shader")]
        public void Apply()
        {
            if (_materials.Length == 0)
                return;

            var textures = new Texture[_materials.Length, _textureRemaps.Length];
            for(int r = 0; r < _textureRemaps.Length; r++)
            {
                var remap = _textureRemaps[r];
                if(remap.FromMapName != "" && remap.ToMapName != "")
                {
                    for (int i = 0; i < _materials.Length; i++)
                    {
                        textures[i, r] = _materials[i].GetTexture(remap.FromMapName);
                        if (textures[i, r] == null)
                            Debug.LogWarning($"No texture set for old material property {remap.FromMapName}.", this);
                    }
                }
            }

            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].shader = _newShader;
                for(int r = 0; r < _textureRemaps.Length; r++)
                    _materials[i].SetTexture(_textureRemaps[r].ToMapName, textures[i, r]);
            }
        }
    }

    [System.Serializable]
    public class ShaderSwitcherTextureRemap
    {
        [SerializeField]
        public string FromMapName;
        [SerializeField]
        public string ToMapName;
    }
}
