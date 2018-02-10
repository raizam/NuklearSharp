using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		public unsafe partial class nk_memory_status
		{
			public void* memory;
			public uint type;
			public ulong size;
			public ulong allocated;
			public ulong needed;
			public ulong calls;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_buffer_marker
		{
			public int active;
			public ulong offset;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_memory
		{
			public void* ptr;
			public ulong size;
		}

		public static void nk_buffer_init(nk_buffer b, ulong initial_size)
		{
			if (((b == null)) || (initial_size == 0)) return;

			b.type = (int) (NK_BUFFER_DYNAMIC);
			b.memory.ptr = CRuntime.malloc((ulong) (initial_size));
			b.memory.size = (ulong) (initial_size);
			b.size = (ulong) (initial_size);
			b.grow_factor = (float) (2.0f);

		}

		public static void nk_buffer_init_fixed(nk_buffer b, void* m, ulong size)
		{
			if (((b == null) || (m == null)) || (size == 0)) return;

			b.type = (int) (NK_BUFFER_FIXED);
			b.memory.ptr = m;
			b.memory.size = (ulong) (size);
			b.size = (ulong) (size);
		}

		public static void* nk_buffer_realloc(nk_buffer b, ulong capacity, ref ulong size)
		{
			void* temp;
			ulong buffer_size;
			if ((((b == null)))) return null;
			buffer_size = (ulong) (b.memory.size);
			temp = CRuntime.malloc((ulong) (capacity));
			if (temp == null) return null;
			size = (ulong) (capacity);
			if (temp != b.memory.ptr)
			{
				nk_memcopy(temp, b.memory.ptr, (ulong) (buffer_size));
				CRuntime.free(b.memory.ptr);
			}

			if ((b.size) == (buffer_size))
			{
				b.size = (ulong) (capacity);
				return temp;
			}
			else
			{
				void* dst;
				void* src;
				ulong back_size;
				back_size = (ulong) (buffer_size - b.size);
				dst = ((void*) ((byte*) (temp) + (capacity - back_size)));
				src = ((void*) ((byte*) (temp) + (b.size)));
				nk_memcopy(dst, src, (ulong) (back_size));
				b.size = (ulong) (capacity - back_size);
			}

			return temp;
		}

		public static void* nk_buffer_alloc(nk_buffer b, int type, ulong size, ulong align)
		{
			int full;
			ulong alignment;
			void* unaligned;
			void* memory;
			if ((b == null) || (size == 0)) return null;
			b.needed += (ulong) (size);
			if ((type) == (NK_BUFFER_FRONT)) unaligned = ((void*) ((byte*) (b.memory.ptr) + (b.allocated)));
			else unaligned = ((void*) ((byte*) (b.memory.ptr) + (b.size - size)));
			memory = nk_buffer_align(unaligned, (ulong) (align), &alignment, (int) (type));
			if ((type) == (NK_BUFFER_FRONT)) full = (int) ((b.allocated + size + alignment) > (b.size) ? 1 : 0);
			else
				full = (int) ((b.size - ((b.size) < (size + alignment) ? (b.size) : (size + alignment))) <= b.allocated ? 1 : 0);
			if ((full) != 0)
			{
				ulong capacity;
				if (b.type != NK_BUFFER_DYNAMIC) return null;
				if (((b.type != NK_BUFFER_DYNAMIC))) return null;
				capacity = ((ulong) ((float) (b.memory.size)*b.grow_factor));
				capacity =
					(ulong)
						((capacity) < (nk_round_up_pow2((uint) (b.allocated + size)))
							? (nk_round_up_pow2((uint) (b.allocated + size)))
							: (capacity));
				b.memory.ptr = nk_buffer_realloc(b, (ulong) (capacity), ref b.memory.size);
				if (b.memory.ptr == null) return null;
				if ((type) == (NK_BUFFER_FRONT)) unaligned = ((void*) ((byte*) (b.memory.ptr) + (b.allocated)));
				else unaligned = ((void*) ((byte*) (b.memory.ptr) + (b.size - size)));
				memory = nk_buffer_align(unaligned, (ulong) (align), &alignment, (int) (type));
			}

			if ((type) == (NK_BUFFER_FRONT)) b.allocated += (ulong) (size + alignment);
			else b.size -= (ulong) (size + alignment);
			b.needed += (ulong) (alignment);
			b.calls++;
			return memory;
		}

		public static void nk_buffer_push(nk_buffer b, int type, void* memory, ulong size, ulong align)
		{
			void* mem = nk_buffer_alloc(b, (int) (type), (ulong) (size), (ulong) (align));
			if (mem == null) return;
			nk_memcopy(mem, memory, (ulong) (size));
		}

		public static void nk_buffer_mark(nk_buffer buffer, int type)
		{
			if (buffer == null) return;
			buffer.marker[type].active = (int) (nk_true);
			if ((type) == (NK_BUFFER_BACK)) buffer.marker[type].offset = (ulong) (buffer.size);
			else buffer.marker[type].offset = (ulong) (buffer.allocated);
		}

		public static void nk_buffer_reset(nk_buffer buffer, int type)
		{
			if (buffer == null) return;
			if ((type) == (NK_BUFFER_BACK))
			{
				buffer.needed -= (ulong) (buffer.memory.size - buffer.marker[type].offset);
				if ((buffer.marker[type].active) != 0) buffer.size = (ulong) (buffer.marker[type].offset);
				else buffer.size = (ulong) (buffer.memory.size);
				buffer.marker[type].active = (int) (nk_false);
			}
			else
			{
				buffer.needed -= (ulong) (buffer.allocated - buffer.marker[type].offset);
				if ((buffer.marker[type].active) != 0) buffer.allocated = (ulong) (buffer.marker[type].offset);
				else buffer.allocated = (ulong) (0);
				buffer.marker[type].active = (int) (nk_false);
			}

		}

		public static void nk_buffer_clear(nk_buffer b)
		{
			if (b == null) return;
			b.allocated = (ulong) (0);
			b.size = (ulong) (b.memory.size);
			b.calls = (ulong) (0);
			b.needed = (ulong) (0);
		}

		public static void nk_buffer_free(nk_buffer b)
		{
			if ((b == null) || (b.memory.ptr == null)) return;
			if ((b.type) == (NK_BUFFER_FIXED)) return;

			CRuntime.free(b.memory.ptr);
		}

		public static void nk_buffer_info(nk_memory_status s, nk_buffer b)
		{
			if ((s == null) || (b == null)) return;
			s.allocated = (ulong) (b.allocated);
			s.size = (ulong) (b.memory.size);
			s.needed = (ulong) (b.needed);
			s.memory = b.memory.ptr;
			s.calls = (ulong) (b.calls);
		}

		public static void* nk_buffer_memory(nk_buffer buffer)
		{
			if (buffer == null) return null;
			return buffer.memory.ptr;
		}

		public static void* nk_buffer_memory_const(nk_buffer buffer)
		{
			if (buffer == null) return null;
			return buffer.memory.ptr;
		}

		public static ulong nk_buffer_total(nk_buffer buffer)
		{
			if (buffer == null) return (ulong) (0);
			return (ulong) (buffer.memory.size);
		}
	}
}