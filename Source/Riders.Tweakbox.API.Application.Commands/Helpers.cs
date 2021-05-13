using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Riders.Tweakbox.API.Application.Commands
{
    internal static class Helpers
    {
        /// <summary>
        /// Checks if two lists have equal members in equal order.
        /// It's SequenceEqual but faster.
        /// </summary>
        public static bool ListsEqual<T>(this List<T> first, List<T> second) where T : IEquatable<T>
        {
            if (first.Count != second.Count) 
                return false;

            ref var firstElement  = ref GetFirstElement(CollectionsMarshal.AsSpan(first));
            ref var secondElement = ref GetFirstElement(CollectionsMarshal.AsSpan(second));

            for (int i = 0; i < first.Count; i++)
            {
                if (!firstElement.Equals(secondElement))
                    return false;

                firstElement  = ref Unsafe.Add(ref firstElement, 1);
                secondElement = ref Unsafe.Add(ref secondElement, 1);
            }

            return true;
        }

        /// <summary>
        /// Gets a reference to the first element of a span.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ref T GetFirstElement<T>(Span<T> span) => ref MemoryMarshal.GetReference(span);
    }
}
