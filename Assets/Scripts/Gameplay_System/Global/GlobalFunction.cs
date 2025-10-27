using UnityEngine;

namespace Game.GlobalFunc
{
    public class LayerUtill
    {
        public static bool CompareLayerMask(GameObject target, LayerMask dst)
        {
            if (null == target)
                return false;

            return 0 != (1 << target.layer & dst.value);
        }
    }
}