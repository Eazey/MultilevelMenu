using System;

namespace EazeyFramework.UI
{   
    [Flags]
    public enum MenuInteractType
    {
        /// <summary>
        /// 重复点击
        /// </summary>
        Repete = 1, //0x1

        /// <summary>
        /// 互斥
        /// </summary>
        Exclusive = 1 << 1 //0x2
    }
}

