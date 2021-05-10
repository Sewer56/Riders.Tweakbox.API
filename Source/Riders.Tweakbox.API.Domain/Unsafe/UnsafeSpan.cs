using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SystemUnsafe = System.Runtime.CompilerServices.Unsafe;

namespace Riders.Tweakbox.API.Domain.Unsafe
{
    public static class UnsafeSpan
    {
        /// <summary>
        /// Gets a reference to the first element of a span.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetFirstElement<T>(Span<T> span) => ref MemoryMarshal.GetReference(span);

        /// <summary>
        /// Creates a slice of the span without performing bounds checks.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Slice<T>(Span<T> span, int length) => MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), length);

        /// <summary>
        /// Creates a slice of the span without performing bounds checks.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Slice<T>(Span<T> span, int start, int length) => MemoryMarshal.CreateSpan(ref SystemUnsafe.Add(ref MemoryMarshal.GetReference(span), start), length);

        /// <summary>
        /// Sets the value stored in the value parameter and advances the reference by 1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to be set and then incremented.</param>
        /// <param name="newValue">The value to assign.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRefAndIncrement<T>(ref T value, T newValue)
        {
            value = newValue;
            value = ref SystemUnsafe.Add(ref value, 1);
        }

        /// <summary>
        /// Gets the value stored in the value parameter and advances the reference by 1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to be obtained and then incremented.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetAndIncrement<T>(ref T value)
        {
            var result = value;
            value = ref SystemUnsafe.Add(ref value, 1);
            return value;
        }


    }
}
