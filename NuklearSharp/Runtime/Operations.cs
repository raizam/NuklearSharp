using System.Collections.Concurrent;

namespace NuklearSharp
{
    internal static unsafe class Operations
    {
        internal static ConcurrentDictionary<long, Pointer> Pointers = new ConcurrentDictionary<long, Pointer>();

        internal static void* Malloc(long size)
        {
            var result = new PinnedArray<byte>(size);
            Pointers[(long)result.Ptr] = result;

            return result.Ptr;
        }

        internal static void Memcpy(void* a, void* b, long size)
        {
            var ap = (byte*)a;
            var bp = (byte*)b;
            for (long i = 0; i < size; ++i)
            {
                *ap++ = *bp++;
            }
        }

        internal static void MemMove(void* a, void* b, long size)
        {
            using (var temp = new PinnedArray<byte>(size))
            {
                CRuntime.Memcpy(temp.Ptr, b, size);
                CRuntime.Memcpy(a, temp.Ptr, size);
            }
        }

        internal static void Free(void* a)
        {
            Pointer pointer;
            if (!Pointers.TryRemove((long)a, out pointer))
            {
                return;
            }

            pointer.Dispose();
        }

        internal static void* Realloc(void* a, long newSize)
        {
            Pointer pointer;
            if (!Pointers.TryGetValue((long)a, out pointer))
            {
                // Allocate new
                return Malloc(newSize);
            }

            if (newSize <= pointer.Size)
            {
                // Realloc not required
                return a;
            }

            var result = Malloc(newSize);
            CRuntime.Memcpy(result, a, pointer.Size);

            Pointers.TryRemove((long)pointer.Ptr, out pointer);
            pointer.Dispose();

            return result;
        }

        internal static int Memcmp(void* a, void* b, long size)
        {
            var result = 0;
            var ap = (byte*)a;
            var bp = (byte*)b;
            for (long i = 0; i < size; ++i)
            {
                if (*ap != *bp)
                {
                    result += 1;
                }
                ap++;
                bp++;
            }

            return result;
        }
    }
}