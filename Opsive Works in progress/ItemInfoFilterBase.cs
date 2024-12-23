namespace Opsive.UltimateInventorySystem.UI.Grid
{
    using Opsive.Shared.Utility;
    using Opsive.UltimateInventorySystem.Core.DataStructures;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class ItemInfoFilterBase : ItemInfoFilterSorterBase
    {
        /// <summary>
        /// Filter the item info.
        /// </summary>
        /// <param name="itemInfo">The item info.</param>
        /// <returns>True if the list can contain the item info.</returns>
        public abstract bool Filter(ItemInfo itemInfo);

        /// <summary>
        /// Filter the list.
        /// </summary>
        /// <param name="input">The input list.</param>
        /// <param name="outputPooledArray">The reference to an output array.</param>
        /// <returns>The filtered/sorted list.</returns>
        public override ListSlice<ItemInfo> Filter(ListSlice<ItemInfo> input, ref ItemInfo[] outputPooledArray)
        {
            if (outputPooledArray == null || outputPooledArray.Length < input.Count)
            {
                outputPooledArray = new ItemInfo[input.Count];
            }

            var count = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (!Filter(input[i])) { continue; }

                outputPooledArray[count] = input[i];
                count++;
            }

            return new ListSlice<ItemInfo>(outputPooledArray, 0, count);

        }

        /// <summary>
        /// Filter the item info.
        /// </summary>
        /// <param name="itemInfo">The item info.</param>
        /// <returns>True if the list can contain the item info.</returns>
        public override bool CanContain(ItemInfo input)
        {
            return Filter(input);
        }
    }
}
