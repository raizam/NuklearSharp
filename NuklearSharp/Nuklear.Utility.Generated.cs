using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		public unsafe partial class nk_baked_font
		{
			public float height;
			public float ascent;
			public float descent;
			public uint glyph_offset;
			public uint glyph_count;
			public uint* ranges;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_key
		{
			public int down;
			public uint clicked;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct conv
		{
			public uint i;
			public float f;
		}

		public static float nk_sqrt(float x)
		{
			return (float) (x*nk_inv_sqrt((float) (x)));
		}

		public static float nk_sin(float x)
		{
			float a0 = (float) (+1.91059300966915117e-31f);
			float a1 = (float) (+1.00086760103908896f);
			float a2 = (float) (-1.21276126894734565e-2f);
			float a3 = (float) (-1.38078780785773762e-1f);
			float a4 = (float) (-2.67353392911981221e-2f);
			float a5 = (float) (+2.08026600266304389e-2f);
			float a6 = (float) (-3.03996055049204407e-3f);
			float a7 = (float) (+1.38235642404333740e-4f);
			return (float) (a0 + x*(a1 + x*(a2 + x*(a3 + x*(a4 + x*(a5 + x*(a6 + x*a7)))))));
		}

		public static float nk_cos(float x)
		{
			float a0 = (float) (+1.00238601909309722f);
			float a1 = (float) (-3.81919947353040024e-2f);
			float a2 = (float) (-3.94382342128062756e-1f);
			float a3 = (float) (-1.18134036025221444e-1f);
			float a4 = (float) (+1.07123798512170878e-1f);
			float a5 = (float) (-1.86637164165180873e-2f);
			float a6 = (float) (+9.90140908664079833e-4f);
			float a7 = (float) (-5.23022132118824778e-14f);
			return (float) (a0 + x*(a1 + x*(a2 + x*(a3 + x*(a4 + x*(a5 + x*(a6 + x*a7)))))));
		}

		public static uint nk_round_up_pow2(uint v)
		{
			v--;
			v |= (uint) (v >> 1);
			v |= (uint) (v >> 2);
			v |= (uint) (v >> 4);
			v |= (uint) (v >> 8);
			v |= (uint) (v >> 16);
			v++;
			return (uint) (v);
		}

		public static nk_rect nk_get_null_rect()
		{
			return (nk_rect) (nk_null_rect);
		}

		public static nk_rect nk_rect_(float x, float y, float w, float h)
		{
			nk_rect r = new nk_rect();
			r.x = (float) (x);
			r.y = (float) (y);
			r.w = (float) (w);
			r.h = (float) (h);
			return (nk_rect) (r);
		}

		public static nk_rect nk_recti_(int x, int y, int w, int h)
		{
			nk_rect r = new nk_rect();
			r.x = ((float) (x));
			r.y = ((float) (y));
			r.w = ((float) (w));
			r.h = ((float) (h));
			return (nk_rect) (r);
		}

		public static nk_rect nk_rectv(float* r)
		{
			return (nk_rect) (nk_rect_((float) (r[0]), (float) (r[1]), (float) (r[2]), (float) (r[3])));
		}

		public static nk_rect nk_rectiv(int* r)
		{
			return (nk_rect) (nk_recti_((int) (r[0]), (int) (r[1]), (int) (r[2]), (int) (r[3])));
		}

		public static nk_vec2 nk_vec2_(float x, float y)
		{
			nk_vec2 ret = new nk_vec2();
			ret.x = (float) (x);
			ret.y = (float) (y);
			return (nk_vec2) (ret);
		}

		public static nk_vec2 nk_vec2i_(int x, int y)
		{
			nk_vec2 ret = new nk_vec2();
			ret.x = ((float) (x));
			ret.y = ((float) (y));
			return (nk_vec2) (ret);
		}

		public static nk_vec2 nk_vec2v(float* v)
		{
			return (nk_vec2) (nk_vec2_((float) (v[0]), (float) (v[1])));
		}

		public static nk_vec2 nk_vec2iv(int* v)
		{
			return (nk_vec2) (nk_vec2i_((int) (v[0]), (int) (v[1])));
		}

		public static int nk_is_lower(int c)
		{
			return (int) ((((c) >= ('a')) && (c <= 'z')) || (((c) >= (0xE0)) && (c <= 0xFF)) ? 1 : 0);
		}

		public static int nk_is_upper(int c)
		{
			return (int) ((((c) >= ('A')) && (c <= 'Z')) || (((c) >= (0xC0)) && (c <= 0xDF)) ? 1 : 0);
		}

		public static int nk_to_upper(int c)
		{
			return (int) ((((c) >= ('a')) && (c <= 'z')) ? (c - ('a' - 'A')) : c);
		}

		public static int nk_to_lower(int c)
		{
			return (int) ((((c) >= ('A')) && (c <= 'Z')) ? (c - ('a' + 'A')) : c);
		}

		public static void* nk_memcopy(void* dst0, void* src0, ulong length)
		{
			ulong t;
			sbyte* dst = (sbyte*) (dst0);
			sbyte* src = (sbyte*) (src0);
			if (((length) == (0)) || ((dst) == (src))) goto done;
			if ((dst) < (src))
			{
				t = ((ulong) (src));
				if (((t | (ulong) (dst)) & (sizeof (int) - 1)) != 0)
				{
					if ((((t ^ (ulong) (dst)) & (sizeof (int) - 1)) != 0) || ((length) < (sizeof (int)))) t = (ulong) (length);
					else t = (ulong) (sizeof (int) - (t & (sizeof (int) - 1)));
					length -= (ulong) (t);
					do
					{
						*dst++ = (sbyte) (*src++);
					} while ((--t) != 0);
				}
				t = (ulong) (length/sizeof (int));
				if ((t) != 0)
					do
					{
						*(int*) ((void*) (dst)) = (int) (*(int*) ((void*) (src)));
						src += sizeof (int);
						dst += sizeof (int);
					} while ((--t) != 0);
				t = (ulong) (length & (sizeof (int) - 1));
				if ((t) != 0)
					do
					{
						*dst++ = (sbyte) (*src++);
					} while ((--t) != 0);
			}
			else
			{
				src += length;
				dst += length;
				t = ((ulong) (src));
				if (((t | (ulong) (dst)) & (sizeof (int) - 1)) != 0)
				{
					if ((((t ^ (ulong) (dst)) & (sizeof (int) - 1)) != 0) || (length <= sizeof (int))) t = (ulong) (length);
					else t &= (ulong) (sizeof (int) - 1);
					length -= (ulong) (t);
					do
					{
						*--dst = (sbyte) (*--src);
					} while ((--t) != 0);
				}
				t = (ulong) (length/sizeof (int));
				if ((t) != 0)
					do
					{
						src -= sizeof (int);
						dst -= sizeof (int);
						*(int*) ((void*) (dst)) = (int) (*(int*) ((void*) (src)));
					} while ((--t) != 0);
				t = (ulong) (length & (sizeof (int) - 1));
				if ((t) != 0)
					do
					{
						*--dst = (sbyte) (*--src);
					} while ((--t) != 0);
			}

			done:
			;
			return (dst0);
		}

		public static void nk_memset(void* ptr, int c0, ulong size)
		{
			byte* dst = (byte*) (ptr);
			uint c = (uint) (0);
			ulong t = (ulong) (0);
			if ((c = (uint) ((byte) (c0))) != 0)
			{
				c = (uint) ((c << 8) | c);
				if (sizeof (uint) > 2) c = (uint) ((c << 16) | c);
			}

			dst = (byte*) (ptr);
			if ((size) < (3*sizeof (uint)))
			{
				while ((size--) != 0)
				{
					*dst++ = ((byte) (c0));
				}
				return;
			}

			if ((t = (ulong) (((ulong) ((long) (dst))) & (sizeof (uint) - 1))) != 0)
			{
				t = (ulong) (sizeof (uint) - t);
				size -= (ulong) (t);
				do
				{
					*dst++ = ((byte) (c0));
				} while (--t != 0);
			}

			t = (ulong) (size/sizeof (uint));
			do
			{
				*(uint*) ((void*) (dst)) = (uint) (c);
				dst += sizeof (uint);
			} while (--t != 0);
			t = (ulong) (size & (sizeof (uint) - 1));
			if (t != 0)
			{
				do
				{
					*dst++ = ((byte) (c0));
				} while (--t != 0);
			}

		}

		public static void nk_zero(void* ptr, ulong size)
		{
			nk_memset(ptr, (int) (0), (ulong) (size));
		}

		public static int nk_strlen(char* str)
		{
			int siz = (int) (0);
			while (((str) != null) && (*str++ != '\0'))
			{
				siz++;
			}
			return (int) (siz);
		}

		public static int nk_strtoi(char* str, char** endptr)
		{
			int neg = (int) (1);
			char* p = str;
			int value = (int) (0);
			if (str == null) return (int) (0);
			while ((*p) == (' '))
			{
				p++;
			}
			if ((*p) == ('-'))
			{
				neg = (int) (-1);
				p++;
			}

			while ((((*p) != 0) && ((*p) >= ('0'))) && (*p <= '9'))
			{
				value = (int) (value*10 + (*p - '0'));
				p++;
			}
			if ((endptr) != null) *endptr = p;
			return (int) (neg*value);
		}

		public static double nk_strtod(char* str, char** endptr)
		{
			double m;
			double neg = (double) (1.0);
			char* p = str;
			double value = (double) (0);
			double number = (double) (0);
			if (str == null) return (double) (0);
			while ((*p) == (' '))
			{
				p++;
			}
			if ((*p) == ('-'))
			{
				neg = (double) (-1.0);
				p++;
			}

			while ((((*p) != 0) && (*p != '.')) && (*p != 'e'))
			{
				value = (double) (value*10.0 + (double) (*p - '0'));
				p++;
			}
			if ((*p) == ('.'))
			{
				p++;
				for (m = (double) (0.1); ((*p) != 0) && (*p != 'e'); p++)
				{
					value = (double) (value + (double) (*p - '0')*m);
					m *= (double) (0.1);
				}
			}

			if ((*p) == ('e'))
			{
				int i;
				int pow;
				int div;
				p++;
				if ((*p) == ('-'))
				{
					div = (int) (nk_true);
					p++;
				}
				else if ((*p) == ('+'))
				{
					div = (int) (nk_false);
					p++;
				}
				else div = (int) (nk_false);
				for (pow = (int) (0); *p != 0; p++)
				{
					pow = (int) (pow*10 + (*p - '0'));
				}
				for (m = (double) (1.0) , i = (int) (0); (i) < (pow); i++)
				{
					m *= (double) (10.0);
				}
				if ((div) != 0) value /= (double) (m);
				else value *= (double) (m);
			}

			number = (double) (value*neg);
			if ((endptr) != null) *endptr = p;
			return (double) (number);
		}

		public static float nk_strtof(char* str, char** endptr)
		{
			float float_value;
			double double_value;
			double_value = (double) (nk_strtod(str, endptr));
			float_value = ((float) (double_value));
			return (float) (float_value);
		}

		public static int nk_stricmpn(char* s1, char* s2, int n)
		{
			int c1;
			int c2;
			int d;
			do
			{
				c1 = (int) (*s1++);
				c2 = (int) (*s2++);
				if (n-- == 0) return (int) (0);
				d = (int) (c1 - c2);
				while ((d) != 0)
				{
					if ((c1 <= 'Z') && ((c1) >= ('A')))
					{
						d += (int) ('a' - 'A');
						if (d == 0) break;
					}
					if ((c2 <= 'Z') && ((c2) >= ('A')))
					{
						d -= (int) ('a' - 'A');
						if (d == 0) break;
					}
					return (int) ((((d) >= (0) ? 1 : 0) << 1) - 1);
				}
			} while ((c1) != 0);
			return (int) (0);
		}

		public static int nk_str_match_here(sbyte* regexp, char* text)
		{
			if ((regexp[0]) == ('\0')) return (int) (1);
			if ((regexp[1]) == ('*')) return (int) (nk_str_match_star((int) (regexp[0]), regexp + 2, text));
			if (((regexp[0]) == ('$')) && ((regexp[1]) == ('\0'))) return (int) ((*text) == ('\0') ? 1 : 0);
			if ((*text != '\0') && (((regexp[0]) == ('.')) || ((regexp[0]) == (*text))))
				return (int) (nk_str_match_here(regexp + 1, text + 1));
			return (int) (0);
		}

		public static int nk_str_match_star(int c, sbyte* regexp, char* text)
		{
			do
			{
				if ((nk_str_match_here(regexp, text)) != 0) return (int) (1);
			} while ((*text != '\0') && (((*text++) == (c)) || ((c) == ('.'))));
			return (int) (0);
		}

		public static int nk_string_float_limit(char* _string_, int prec)
		{
			int dot = (int) (0);
			char* c = _string_;
			while ((*c) != 0)
			{
				if ((*c) == ('.'))
				{
					dot = (int) (1);
					c++;
					continue;
				}
				if ((dot) == (prec + 1))
				{
					*c = (char) 0;
					break;
				}
				if ((dot) > (0)) dot++;
				c++;
			}
			return (int) (c - _string_);
		}

		public static double nk_pow(double x, int n)
		{
			double r = (double) (1);
			int plus = (int) ((n) >= (0) ? 1 : 0);
			n = (int) ((plus) != 0 ? n : -n);
			while ((n) > (0))
			{
				if ((n & 1) == (1)) r *= (double) (x);
				n /= (int) (2);
				x *= (double) (x);
			}
			return (double) ((plus) != 0 ? r : 1.0/r);
		}

		public static int nk_ifloord(double x)
		{
			x = ((double) ((int) (x) - (((x) < (0.0)) ? 1 : 0)));
			return (int) (x);
		}

		public static int nk_ifloorf(float x)
		{
			x = ((float) ((int) (x) - (((x) < (0.0f)) ? 1 : 0)));
			return (int) (x);
		}

		public static int nk_iceilf(float x)
		{
			if ((x) >= (0))
			{
				int i = (int) (x);
				return (int) (((x) > (i)) ? i + 1 : i);
			}
			else
			{
				int t = (int) (x);
				float r = (float) (x - (float) (t));
				return (int) (((r) > (0.0f)) ? t + 1 : t);
			}

		}

		public static int nk_log10(double n)
		{
			int neg;
			int ret;
			int exp = (int) (0);
			neg = (int) (((n) < (0)) ? 1 : 0);
			ret = (int) ((neg) != 0 ? (int) (-n) : (int) (n));
			while ((ret/10) > (0))
			{
				ret /= (int) (10);
				exp++;
			}
			if ((neg) != 0) exp = (int) (-exp);
			return (int) (exp);
		}

		public static void nk_strrev_ascii(char* s)
		{
			int len = (int) (nk_strlen(s));
			int end = (int) (len/2);
			int i = (int) (0);
			char t;
			for (; (i) < (end); ++i)
			{
				t = (s[i]);
				s[i] = (s[len - 1 - i]);
				s[len - 1 - i] = t;
			}
		}

		public static char* nk_itoa(char* s, int n)
		{
			int i = (int) (0);
			if ((n) == (0))
			{
				s[i++] = ('0');
				s[i] = (char) (0);
				return s;
			}

			if ((n) < (0))
			{
				s[i++] = ('-');
				n = (int) (-n);
			}

			while ((n) > (0))
			{
				s[i++] = (char) (('0' + (char) (n%10)));
				n /= (int) (10);
			}
			s[i] = (char) (0);
			if ((s[0]) == ('-')) ++s;
			nk_strrev_ascii(s);
			return s;
		}

		public static char* nk_dtoa(char* s, double n)
		{
			int useExp = (int) (0);
			int digit = (int) (0);
			int m = (int) (0);
			int m1 = (int) (0);
			char* c = s;
			int neg = (int) (0);
			if (s == null) return null;
			if ((n) == (0.0))
			{
				s[0] = ('0');
				s[1] = ('\0');
				return s;
			}

			neg = (int) ((n) < (0) ? 1 : 0);
			if ((neg) != 0) n = (double) (-n);
			m = (int) (nk_log10((double) (n)));
			useExp = (int) ((((m) >= (14)) || (((neg) != 0) && ((m) >= (9)))) || (m <= -9) ? 1 : 0);
			if ((neg) != 0) *(c++) = ('-');
			if ((useExp) != 0)
			{
				if ((m) < (0)) m -= (int) (1);
				n = (double) (n/nk_pow((double) (10.0), (int) (m)));
				m1 = (int) (m);
				m = (int) (0);
			}

			if ((m) < (1.0))
			{
				m = (int) (0);
			}

			while (((n) > (0.00000000000001)) || ((m) >= (0)))
			{
				double weight = (double) (nk_pow((double) (10.0), (int) (m)));
				if ((weight) > (0))
				{
					double t = (double) (n/weight);
					digit = (int) (nk_ifloord((double) (t)));
					n -= (double) ((double) (digit)*weight);
					*(c++) = (char) (('0' + (char) (digit)));
				}
				if (((m) == (0)) && ((n) > (0))) *(c++) = ('.');
				m--;
			}
			if ((useExp) != 0)
			{
				int i;
				int j;
				*(c++) = ('e');
				if ((m1) > (0))
				{
					*(c++) = ('+');
				}
				else
				{
					*(c++) = ('-');
					m1 = (int) (-m1);
				}
				m = (int) (0);
				while ((m1) > (0))
				{
					*(c++) = (char) (('0' + (char) (m1%10)));
					m1 /= (int) (10);
					m++;
				}
				c -= m;
				for (i = (int) (0) , j = (int) (m - 1); (i) < (j); i++ , j--)
				{
					c[i] ^= (c[j]);
					c[j] ^= (c[i]);
					c[i] ^= (c[j]);
				}
				c += m;
			}

			*(c) = ('\0');
			return s;
		}

		public static uint nk_murmur_hash(void* key, int len, uint seed)
		{
			nk_murmur_hash_union conv = new nk_murmur_hash_union(null);
			byte* data = (byte*) (key);
			int nblocks = (int) (len/4);
			uint h1 = (uint) (seed);
			uint c1 = (uint) (0xcc9e2d51);
			uint c2 = (uint) (0x1b873593);
			byte* tail;
			uint* blocks;
			uint k1;
			int i;
			if (key == null) return (uint) (0);
			conv.b = (data + nblocks*4);
			blocks = conv.i;
			for (i = (int) (-nblocks); i != 0; ++i)
			{
				k1 = (uint) (blocks[i]);
				k1 *= (uint) (c1);
				k1 = (uint) ((k1) << (15) | ((k1) >> (32 - 15)));
				k1 *= (uint) (c2);
				h1 ^= (uint) (k1);
				h1 = (uint) ((h1) << (13) | ((h1) >> (32 - 13)));
				h1 = (uint) (h1*5 + 0xe6546b64);
			}
			tail = (data + nblocks*4);
			k1 = (uint) (0);
			int l = (int) (len & 3);
			switch (l)
			{
				case 1:
				case 2:
				case 3:
					if ((l) == (2))
					{
						k1 ^= ((uint) (tail[1] << 8));
					}
					else if ((l) == (3))
					{
						k1 ^= ((uint) (tail[2] << 16));
					}
					k1 ^= (uint) (tail[0]);
					k1 *= (uint) (c1);
					k1 = (uint) ((k1) << (15) | ((k1) >> (32 - 15)));
					k1 *= (uint) (c2);
					h1 ^= (uint) (k1);
					break;
				default:
					break;
			}

			h1 ^= ((uint) (len));
			h1 ^= (uint) (h1 >> 16);
			h1 *= (uint) (0x85ebca6b);
			h1 ^= (uint) (h1 >> 13);
			h1 *= (uint) (0xc2b2ae35);
			h1 ^= (uint) (h1 >> 16);
			return (uint) (h1);
		}

		public static int nk_parse_hex(sbyte* p, int length)
		{
			int i = (int) (0);
			int len = (int) (0);
			while ((len) < (length))
			{
				i <<= 4;
				if (((p[len]) >= ('a')) && (p[len] <= 'f')) i += (int) ((p[len] - 'a') + 10);
				else if (((p[len]) >= ('A')) && (p[len] <= 'F')) i += (int) ((p[len] - 'A') + 10);
				else i += (int) (p[len] - '0');
				len++;
			}
			return (int) (i);
		}

		public static nk_color nk_rgba(int r, int g, int b, int a)
		{
			nk_color ret = new nk_color();
			ret.r = ((byte) (((r) < (255) ? (r) : (255)) < (0) ? (0) : ((r) < (255) ? (r) : (255))));
			ret.g = ((byte) (((g) < (255) ? (g) : (255)) < (0) ? (0) : ((g) < (255) ? (g) : (255))));
			ret.b = ((byte) (((b) < (255) ? (b) : (255)) < (0) ? (0) : ((b) < (255) ? (b) : (255))));
			ret.a = ((byte) (((a) < (255) ? (a) : (255)) < (0) ? (0) : ((a) < (255) ? (a) : (255))));
			return (nk_color) (ret);
		}

		public static nk_color nk_rgb_hex(sbyte* rgb)
		{
			nk_color col = new nk_color();
			sbyte* c = rgb;
			if ((*c) == ('#')) c++;
			col.r = ((byte) (nk_parse_hex(c, (int) (2))));
			col.g = ((byte) (nk_parse_hex(c + 2, (int) (2))));
			col.b = ((byte) (nk_parse_hex(c + 4, (int) (2))));
			col.a = (byte) (255);
			return (nk_color) (col);
		}

		public static nk_color nk_rgba_hex(sbyte* rgb)
		{
			nk_color col = new nk_color();
			sbyte* c = rgb;
			if ((*c) == ('#')) c++;
			col.r = ((byte) (nk_parse_hex(c, (int) (2))));
			col.g = ((byte) (nk_parse_hex(c + 2, (int) (2))));
			col.b = ((byte) (nk_parse_hex(c + 4, (int) (2))));
			col.a = ((byte) (nk_parse_hex(c + 6, (int) (2))));
			return (nk_color) (col);
		}

		public static void nk_color_hex_rgba(char* output, nk_color col)
		{
			output[0] = ((char) (((col.r & 0xF0) >> 4) <= 9 ? '0' + ((col.r & 0xF0) >> 4) : 'A' - 10 + ((col.r & 0xF0) >> 4)));
			output[1] = ((char) ((col.r & 0x0F) <= 9 ? '0' + (col.r & 0x0F) : 'A' - 10 + (col.r & 0x0F)));
			output[2] = ((char) (((col.g & 0xF0) >> 4) <= 9 ? '0' + ((col.g & 0xF0) >> 4) : 'A' - 10 + ((col.g & 0xF0) >> 4)));
			output[3] = ((char) ((col.g & 0x0F) <= 9 ? '0' + (col.g & 0x0F) : 'A' - 10 + (col.g & 0x0F)));
			output[4] = ((char) (((col.b & 0xF0) >> 4) <= 9 ? '0' + ((col.b & 0xF0) >> 4) : 'A' - 10 + ((col.b & 0xF0) >> 4)));
			output[5] = ((char) ((col.b & 0x0F) <= 9 ? '0' + (col.b & 0x0F) : 'A' - 10 + (col.b & 0x0F)));
			output[6] = ((char) (((col.a & 0xF0) >> 4) <= 9 ? '0' + ((col.a & 0xF0) >> 4) : 'A' - 10 + ((col.a & 0xF0) >> 4)));
			output[7] = ((char) ((col.a & 0x0F) <= 9 ? '0' + (col.a & 0x0F) : 'A' - 10 + (col.a & 0x0F)));
			output[8] = ('\0');
		}

		public static void nk_color_hex_rgb(char* output, nk_color col)
		{
			output[0] = ((char) (((col.r & 0xF0) >> 4) <= 9 ? '0' + ((col.r & 0xF0) >> 4) : 'A' - 10 + ((col.r & 0xF0) >> 4)));
			output[1] = ((char) ((col.r & 0x0F) <= 9 ? '0' + (col.r & 0x0F) : 'A' - 10 + (col.r & 0x0F)));
			output[2] = ((char) (((col.g & 0xF0) >> 4) <= 9 ? '0' + ((col.g & 0xF0) >> 4) : 'A' - 10 + ((col.g & 0xF0) >> 4)));
			output[3] = ((char) ((col.g & 0x0F) <= 9 ? '0' + (col.g & 0x0F) : 'A' - 10 + (col.g & 0x0F)));
			output[4] = ((char) (((col.b & 0xF0) >> 4) <= 9 ? '0' + ((col.b & 0xF0) >> 4) : 'A' - 10 + ((col.b & 0xF0) >> 4)));
			output[5] = ((char) ((col.b & 0x0F) <= 9 ? '0' + (col.b & 0x0F) : 'A' - 10 + (col.b & 0x0F)));
			output[6] = ('\0');
		}

		public static nk_color nk_rgba_iv(int* c)
		{
			return (nk_color) (nk_rgba((int) (c[0]), (int) (c[1]), (int) (c[2]), (int) (c[3])));
		}

		public static nk_color nk_rgba_bv(byte* c)
		{
			return (nk_color) (nk_rgba((int) (c[0]), (int) (c[1]), (int) (c[2]), (int) (c[3])));
		}

		public static nk_color nk_rgb(int r, int g, int b)
		{
			nk_color ret = new nk_color();
			ret.r = ((byte) (((r) < (255) ? (r) : (255)) < (0) ? (0) : ((r) < (255) ? (r) : (255))));
			ret.g = ((byte) (((g) < (255) ? (g) : (255)) < (0) ? (0) : ((g) < (255) ? (g) : (255))));
			ret.b = ((byte) (((b) < (255) ? (b) : (255)) < (0) ? (0) : ((b) < (255) ? (b) : (255))));
			ret.a = ((byte) (255));
			return (nk_color) (ret);
		}

		public static nk_color nk_rgb_iv(int* c)
		{
			return (nk_color) (nk_rgb((int) (c[0]), (int) (c[1]), (int) (c[2])));
		}

		public static nk_color nk_rgb_bv(byte* c)
		{
			return (nk_color) (nk_rgb((int) (c[0]), (int) (c[1]), (int) (c[2])));
		}

		public static nk_color nk_rgba_u32(uint _in_)
		{
			nk_color ret = new nk_color();
			ret.r = (byte) (_in_ & 0xFF);
			ret.g = (byte) ((_in_ >> 8) & 0xFF);
			ret.b = (byte) ((_in_ >> 16) & 0xFF);
			ret.a = ((byte) ((_in_ >> 24) & 0xFF));
			return (nk_color) (ret);
		}

		public static nk_color nk_rgba_f(float r, float g, float b, float a)
		{
			nk_color ret = new nk_color();
			ret.r = ((byte) (((0) < ((1.0f) < (r) ? (1.0f) : (r)) ? ((1.0f) < (r) ? (1.0f) : (r)) : (0))*255.0f));
			ret.g = ((byte) (((0) < ((1.0f) < (g) ? (1.0f) : (g)) ? ((1.0f) < (g) ? (1.0f) : (g)) : (0))*255.0f));
			ret.b = ((byte) (((0) < ((1.0f) < (b) ? (1.0f) : (b)) ? ((1.0f) < (b) ? (1.0f) : (b)) : (0))*255.0f));
			ret.a = ((byte) (((0) < ((1.0f) < (a) ? (1.0f) : (a)) ? ((1.0f) < (a) ? (1.0f) : (a)) : (0))*255.0f));
			return (nk_color) (ret);
		}

		public static nk_color nk_rgba_fv(float* c)
		{
			return (nk_color) (nk_rgba_f((float) (c[0]), (float) (c[1]), (float) (c[2]), (float) (c[3])));
		}

		public static nk_color nk_rgb_f(float r, float g, float b)
		{
			nk_color ret = new nk_color();
			ret.r = ((byte) (((0) < ((1.0f) < (r) ? (1.0f) : (r)) ? ((1.0f) < (r) ? (1.0f) : (r)) : (0))*255.0f));
			ret.g = ((byte) (((0) < ((1.0f) < (g) ? (1.0f) : (g)) ? ((1.0f) < (g) ? (1.0f) : (g)) : (0))*255.0f));
			ret.b = ((byte) (((0) < ((1.0f) < (b) ? (1.0f) : (b)) ? ((1.0f) < (b) ? (1.0f) : (b)) : (0))*255.0f));
			ret.a = (byte) (255);
			return (nk_color) (ret);
		}

		public static nk_color nk_rgb_fv(float* c)
		{
			return (nk_color) (nk_rgb_f((float) (c[0]), (float) (c[1]), (float) (c[2])));
		}

		public static nk_color nk_hsv(int h, int s, int v)
		{
			return (nk_color) (nk_hsva((int) (h), (int) (s), (int) (v), (int) (255)));
		}

		public static nk_color nk_hsv_iv(int* c)
		{
			return (nk_color) (nk_hsv((int) (c[0]), (int) (c[1]), (int) (c[2])));
		}

		public static nk_color nk_hsv_bv(byte* c)
		{
			return (nk_color) (nk_hsv((int) (c[0]), (int) (c[1]), (int) (c[2])));
		}

		public static nk_color nk_hsv_f(float h, float s, float v)
		{
			return (nk_color) (nk_hsva_f((float) (h), (float) (s), (float) (v), (float) (1.0f)));
		}

		public static nk_color nk_hsv_fv(float* c)
		{
			return (nk_color) (nk_hsv_f((float) (c[0]), (float) (c[1]), (float) (c[2])));
		}

		public static nk_color nk_hsva(int h, int s, int v, int a)
		{
			float hf = (float) (((float) (((h) < (255) ? (h) : (255)) < (0) ? (0) : ((h) < (255) ? (h) : (255))))/255.0f);
			float sf = (float) (((float) (((s) < (255) ? (s) : (255)) < (0) ? (0) : ((s) < (255) ? (s) : (255))))/255.0f);
			float vf = (float) (((float) (((v) < (255) ? (v) : (255)) < (0) ? (0) : ((v) < (255) ? (v) : (255))))/255.0f);
			float af = (float) (((float) (((a) < (255) ? (a) : (255)) < (0) ? (0) : ((a) < (255) ? (a) : (255))))/255.0f);
			return (nk_color) (nk_hsva_f((float) (hf), (float) (sf), (float) (vf), (float) (af)));
		}

		public static nk_color nk_hsva_iv(int* c)
		{
			return (nk_color) (nk_hsva((int) (c[0]), (int) (c[1]), (int) (c[2]), (int) (c[3])));
		}

		public static nk_color nk_hsva_bv(byte* c)
		{
			return (nk_color) (nk_hsva((int) (c[0]), (int) (c[1]), (int) (c[2]), (int) (c[3])));
		}

		public static nk_colorf nk_hsva_colorf(float h, float s, float v, float a)
		{
			int i;
			float p;
			float q;
			float t;
			float f;
			nk_colorf _out_ = new nk_colorf();
			if (s <= 0.0f)
			{
				_out_.r = (float) (v);
				_out_.g = (float) (v);
				_out_.b = (float) (v);
				_out_.a = (float) (a);
				return (nk_colorf) (_out_);
			}

			h = (float) (h/(60.0f/360.0f));
			i = ((int) (h));
			f = (float) (h - (float) (i));
			p = (float) (v*(1.0f - s));
			q = (float) (v*(1.0f - (s*f)));
			t = (float) (v*(1.0f - s*(1.0f - f)));
			switch (i)
			{
				case 0:
				default:
					_out_.r = (float) (v);
					_out_.g = (float) (t);
					_out_.b = (float) (p);
					break;
				case 1:
					_out_.r = (float) (q);
					_out_.g = (float) (v);
					_out_.b = (float) (p);
					break;
				case 2:
					_out_.r = (float) (p);
					_out_.g = (float) (v);
					_out_.b = (float) (t);
					break;
				case 3:
					_out_.r = (float) (p);
					_out_.g = (float) (q);
					_out_.b = (float) (v);
					break;
				case 4:
					_out_.r = (float) (t);
					_out_.g = (float) (p);
					_out_.b = (float) (v);
					break;
				case 5:
					_out_.r = (float) (v);
					_out_.g = (float) (p);
					_out_.b = (float) (q);
					break;
			}

			_out_.a = (float) (a);
			return (nk_colorf) (_out_);
		}

		public static nk_colorf nk_hsva_colorfv(float* c)
		{
			return (nk_colorf) (nk_hsva_colorf((float) (c[0]), (float) (c[1]), (float) (c[2]), (float) (c[3])));
		}

		public static nk_color nk_hsva_f(float h, float s, float v, float a)
		{
			nk_colorf c = (nk_colorf) (nk_hsva_colorf((float) (h), (float) (s), (float) (v), (float) (a)));
			return (nk_color) (nk_rgba_f((float) (c.r), (float) (c.g), (float) (c.b), (float) (c.a)));
		}

		public static nk_color nk_hsva_fv(float* c)
		{
			return (nk_color) (nk_hsva_f((float) (c[0]), (float) (c[1]), (float) (c[2]), (float) (c[3])));
		}

		public static void nk_color_f(float* r, float* g, float* b, float* a, nk_color _in_)
		{
			float s = (float) (1.0f/255.0f);
			*r = (float) ((float) (_in_.r)*s);
			*g = (float) ((float) (_in_.g)*s);
			*b = (float) ((float) (_in_.b)*s);
			*a = (float) ((float) (_in_.a)*s);
		}

		public static void nk_color_fv(float* c, nk_color _in_)
		{
			nk_color_f(&c[0], &c[1], &c[2], &c[3], (nk_color) (_in_));
		}

		public static void nk_color_d(double* r, double* g, double* b, double* a, nk_color _in_)
		{
			double s = (double) (1.0/255.0);
			*r = (double) ((double) (_in_.r)*s);
			*g = (double) ((double) (_in_.g)*s);
			*b = (double) ((double) (_in_.b)*s);
			*a = (double) ((double) (_in_.a)*s);
		}

		public static void nk_color_dv(double* c, nk_color _in_)
		{
			nk_color_d(&c[0], &c[1], &c[2], &c[3], (nk_color) (_in_));
		}

		public static void nk_color_hsv_f(float* out_h, float* out_s, float* out_v, nk_color _in_)
		{
			float a;
			nk_color_hsva_f(out_h, out_s, out_v, &a, (nk_color) (_in_));
		}

		public static void nk_color_hsv_fv(float* _out_, nk_color _in_)
		{
			float a;
			nk_color_hsva_f(&_out_[0], &_out_[1], &_out_[2], &a, (nk_color) (_in_));
		}

		public static void nk_colorf_hsva_f(float* out_h, float* out_s, float* out_v, float* out_a, nk_colorf _in_)
		{
			float chroma;
			float K = (float) (0.0f);
			if ((_in_.g) < (_in_.b))
			{
				float t = (float) (_in_.g);
				_in_.g = (float) (_in_.b);
				_in_.b = (float) (t);
				K = (float) (-1.0f);
			}

			if ((_in_.r) < (_in_.g))
			{
				float t = (float) (_in_.r);
				_in_.r = (float) (_in_.g);
				_in_.g = (float) (t);
				K = (float) (-2.0f/6.0f - K);
			}

			chroma = (float) (_in_.r - (((_in_.g) < (_in_.b)) ? _in_.g : _in_.b));
			*out_h =
				(float)
					(((K + (_in_.g - _in_.b)/(6.0f*chroma + 1e-20f)) < (0))
						? -(K + (_in_.g - _in_.b)/(6.0f*chroma + 1e-20f))
						: (K + (_in_.g - _in_.b)/(6.0f*chroma + 1e-20f)));
			*out_s = (float) (chroma/(_in_.r + 1e-20f));
			*out_v = (float) (_in_.r);
			*out_a = (float) (_in_.a);
		}

		public static void nk_colorf_hsva_fv(float* hsva, nk_colorf _in_)
		{
			nk_colorf_hsva_f(&hsva[0], &hsva[1], &hsva[2], &hsva[3], (nk_colorf) (_in_));
		}

		public static void nk_color_hsva_f(float* out_h, float* out_s, float* out_v, float* out_a, nk_color _in_)
		{
			nk_colorf col = new nk_colorf();
			nk_color_f(&col.r, &col.g, &col.b, &col.a, (nk_color) (_in_));
			nk_colorf_hsva_f(out_h, out_s, out_v, out_a, (nk_colorf) (col));
		}

		public static void nk_color_hsva_fv(float* _out_, nk_color _in_)
		{
			nk_color_hsva_f(&_out_[0], &_out_[1], &_out_[2], &_out_[3], (nk_color) (_in_));
		}

		public static void nk_color_hsva_i(int* out_h, int* out_s, int* out_v, int* out_a, nk_color _in_)
		{
			float h;
			float s;
			float v;
			float a;
			nk_color_hsva_f(&h, &s, &v, &a, (nk_color) (_in_));
			*out_h = (int) ((byte) (h*255.0f));
			*out_s = (int) ((byte) (s*255.0f));
			*out_v = (int) ((byte) (v*255.0f));
			*out_a = (int) ((byte) (a*255.0f));
		}

		public static void nk_color_hsva_iv(int* _out_, nk_color _in_)
		{
			nk_color_hsva_i(&_out_[0], &_out_[1], &_out_[2], &_out_[3], (nk_color) (_in_));
		}

		public static void nk_color_hsva_bv(byte* _out_, nk_color _in_)
		{
			int* tmp = stackalloc int[4];
			nk_color_hsva_i(&tmp[0], &tmp[1], &tmp[2], &tmp[3], (nk_color) (_in_));
			_out_[0] = ((byte) (tmp[0]));
			_out_[1] = ((byte) (tmp[1]));
			_out_[2] = ((byte) (tmp[2]));
			_out_[3] = ((byte) (tmp[3]));
		}

		public static void nk_color_hsva_b(byte* h, byte* s, byte* v, byte* a, nk_color _in_)
		{
			int* tmp = stackalloc int[4];
			nk_color_hsva_i(&tmp[0], &tmp[1], &tmp[2], &tmp[3], (nk_color) (_in_));
			*h = ((byte) (tmp[0]));
			*s = ((byte) (tmp[1]));
			*v = ((byte) (tmp[2]));
			*a = ((byte) (tmp[3]));
		}

		public static void nk_color_hsv_i(int* out_h, int* out_s, int* out_v, nk_color _in_)
		{
			int a;
			nk_color_hsva_i(out_h, out_s, out_v, &a, (nk_color) (_in_));
		}

		public static void nk_color_hsv_b(byte* out_h, byte* out_s, byte* out_v, nk_color _in_)
		{
			int* tmp = stackalloc int[4];
			nk_color_hsva_i(&tmp[0], &tmp[1], &tmp[2], &tmp[3], (nk_color) (_in_));
			*out_h = ((byte) (tmp[0]));
			*out_s = ((byte) (tmp[1]));
			*out_v = ((byte) (tmp[2]));
		}

		public static void nk_color_hsv_iv(int* _out_, nk_color _in_)
		{
			nk_color_hsv_i(&_out_[0], &_out_[1], &_out_[2], (nk_color) (_in_));
		}

		public static void nk_color_hsv_bv(byte* _out_, nk_color _in_)
		{
			int* tmp = stackalloc int[4];
			nk_color_hsv_i(&tmp[0], &tmp[1], &tmp[2], (nk_color) (_in_));
			_out_[0] = ((byte) (tmp[0]));
			_out_[1] = ((byte) (tmp[1]));
			_out_[2] = ((byte) (tmp[2]));
		}

		public static nk_handle nk_handle_ptr(void* ptr)
		{
			nk_handle handle = new nk_handle();
			handle.ptr = ptr;
			return (nk_handle) (handle);
		}

		public static nk_handle nk_handle_id(int id)
		{
			nk_handle handle = new nk_handle();
			nk_zero(&handle, (ulong) (sizeof (nk_handle)));
			handle.id = (int) (id);
			return (nk_handle) (handle);
		}

		public static nk_image nk_subimage_ptr(void* ptr, ushort w, ushort h, nk_rect r)
		{
			nk_image s = new nk_image();

			s.handle.ptr = ptr;
			s.w = (ushort) (w);
			s.h = (ushort) (h);
			s.region[0] = ((ushort) (r.x));
			s.region[1] = ((ushort) (r.y));
			s.region[2] = ((ushort) (r.w));
			s.region[3] = ((ushort) (r.h));
			return (nk_image) (s);
		}

		public static nk_image nk_subimage_id(int id, ushort w, ushort h, nk_rect r)
		{
			nk_image s = new nk_image();

			s.handle.id = (int) (id);
			s.w = (ushort) (w);
			s.h = (ushort) (h);
			s.region[0] = ((ushort) (r.x));
			s.region[1] = ((ushort) (r.y));
			s.region[2] = ((ushort) (r.w));
			s.region[3] = ((ushort) (r.h));
			return (nk_image) (s);
		}

		public static nk_image nk_image_ptr(void* ptr)
		{
			nk_image s = new nk_image();

			s.handle.ptr = ptr;
			s.w = (ushort) (0);
			s.h = (ushort) (0);
			s.region[0] = (ushort) (0);
			s.region[1] = (ushort) (0);
			s.region[2] = (ushort) (0);
			s.region[3] = (ushort) (0);
			return (nk_image) (s);
		}

		public static nk_image nk_image_id(int id)
		{
			nk_image s = new nk_image();

			s.handle.id = (int) (id);
			s.w = (ushort) (0);
			s.h = (ushort) (0);
			s.region[0] = (ushort) (0);
			s.region[1] = (ushort) (0);
			s.region[2] = (ushort) (0);
			s.region[3] = (ushort) (0);
			return (nk_image) (s);
		}

		public static int nk_text_clamp(nk_user_font font, char* text, int text_len, float space, int* glyphs,
			float* text_width, uint* sep_list, int sep_count)
		{
			int i = (int) (0);
			int glyph_len = (int) (0);
			float last_width = (float) (0);
			char unicode = (char) 0;
			float width = (float) (0);
			int len = (int) (0);
			int g = (int) (0);
			float s;
			int sep_len = (int) (0);
			int sep_g = (int) (0);
			float sep_width = (float) (0);
			sep_count = (int) ((sep_count) < (0) ? (0) : (sep_count));
			glyph_len = (int) (nk_utf_decode(text, &unicode, (int) (text_len)));
			while ((((glyph_len) != 0) && ((width) < (space))) && ((len) < (text_len)))
			{
				len += (int) (glyph_len);
				s = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), text, (int) (len)));
				for (i = (int) (0); (i) < (sep_count); ++i)
				{
					if (unicode != sep_list[i]) continue;
					sep_width = (float) (last_width = (float) (width));
					sep_g = (int) (g + 1);
					sep_len = (int) (len);
					break;
				}
				if ((i) == (sep_count))
				{
					last_width = (float) (sep_width = (float) (width));
					sep_g = (int) (g + 1);
				}
				width = (float) (s);
				glyph_len = (int) (nk_utf_decode(text + len, &unicode, (int) (text_len - len)));
				g++;
			}
			if ((len) >= (text_len))
			{
				*glyphs = (int) (g);
				*text_width = (float) (last_width);
				return (int) (len);
			}
			else
			{
				*glyphs = (int) (sep_g);
				*text_width = (float) (sep_width);
				return (int) ((sep_len == 0) ? len : sep_len);
			}

		}

		public static nk_vec2 nk_text_calculate_text_bounds(nk_user_font font, char* begin, int byte_len, float row_height,
			char** remaining, nk_vec2* out_offset, int* glyphs, int op)
		{
			float line_height = (float) (row_height);
			nk_vec2 text_size = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			float line_width = (float) (0.0f);
			float glyph_width;
			int glyph_len = (int) (0);
			char unicode = (char) 0;
			int text_len = (int) (0);
			if (((begin == null) || (byte_len <= 0)) || (font == null))
				return (nk_vec2) (nk_vec2_((float) (0), (float) (row_height)));
			glyph_len = (int) (nk_utf_decode(begin, &unicode, (int) (byte_len)));
			if (glyph_len == 0) return (nk_vec2) (text_size);
			glyph_width = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), begin, (int) (glyph_len)));
			*glyphs = (int) (0);
			while (((text_len) < (byte_len)) && ((glyph_len) != 0))
			{
				if ((unicode) == ('\n'))
				{
					text_size.x = (float) ((text_size.x) < (line_width) ? (line_width) : (text_size.x));
					text_size.y += (float) (line_height);
					line_width = (float) (0);
					*glyphs += (int) (1);
					if ((op) == (NK_STOP_ON_NEW_LINE)) break;
					text_len++;
					glyph_len = (int) (nk_utf_decode(begin + text_len, &unicode, (int) (byte_len - text_len)));
					continue;
				}
				if ((unicode) == ('\r'))
				{
					text_len++;
					*glyphs += (int) (1);
					glyph_len = (int) (nk_utf_decode(begin + text_len, &unicode, (int) (byte_len - text_len)));
					continue;
				}
				*glyphs = (int) (*glyphs + 1);
				text_len += (int) (glyph_len);
				line_width += (float) (glyph_width);
				glyph_len = (int) (nk_utf_decode(begin + text_len, &unicode, (int) (byte_len - text_len)));
				glyph_width =
					(float) (font.width((nk_handle) (font.userdata), (float) (font.height), begin + text_len, (int) (glyph_len)));
				continue;
			}
			if ((text_size.x) < (line_width)) text_size.x = (float) (line_width);
			if ((out_offset) != null)
				*out_offset = (nk_vec2) (nk_vec2_((float) (line_width), (float) (text_size.y + line_height)));
			if (((line_width) > (0)) || ((text_size.y) == (0.0f))) text_size.y += (float) (line_height);
			if ((remaining) != null) *remaining = begin + text_len;
			return (nk_vec2) (text_size);
		}

		public static void* nk_buffer_align(void* unaligned, ulong align, ulong* alignment, int type)
		{
			void* memory = null;
			switch (type)
			{
				default:
				case NK_BUFFER_MAX:
				case NK_BUFFER_FRONT:
					if ((align) != 0)
					{
						memory = ((void*) ((long) (((ulong) ((long) ((byte*) (unaligned) + (align - 1)))) & ~(align - 1))));
						*alignment = ((ulong) ((byte*) (memory) - (byte*) (unaligned)));
					}
					else
					{
						memory = unaligned;
						*alignment = (ulong) (0);
					}
					break;
				case NK_BUFFER_BACK:
					if ((align) != 0)
					{
						memory = ((void*) ((long) (((ulong) ((long) ((byte*) (unaligned)))) & ~(align - 1))));
						*alignment = ((ulong) ((byte*) (unaligned) - (byte*) (memory)));
					}
					else
					{
						memory = unaligned;
						*alignment = (ulong) (0);
					}
					break;
			}

			return memory;
		}

		public static void nk_draw_vertex_color(void* attr, float* vals, int format)
		{
			float* val = stackalloc float[4];
			if (((format) < (NK_FORMAT_COLOR_BEGIN)) || ((format) > (NK_FORMAT_COLOR_END))) return;
			val[0] = (float) ((0) < ((1.0f) < (vals[0]) ? (1.0f) : (vals[0])) ? ((1.0f) < (vals[0]) ? (1.0f) : (vals[0])) : (0));
			val[1] = (float) ((0) < ((1.0f) < (vals[1]) ? (1.0f) : (vals[1])) ? ((1.0f) < (vals[1]) ? (1.0f) : (vals[1])) : (0));
			val[2] = (float) ((0) < ((1.0f) < (vals[2]) ? (1.0f) : (vals[2])) ? ((1.0f) < (vals[2]) ? (1.0f) : (vals[2])) : (0));
			val[3] = (float) ((0) < ((1.0f) < (vals[3]) ? (1.0f) : (vals[3])) ? ((1.0f) < (vals[3]) ? (1.0f) : (vals[3])) : (0));
			switch (format)
			{
				default:
					;
					break;
				case NK_FORMAT_R8G8B8A8:
				case NK_FORMAT_R8G8B8:
				{
					nk_color col = (nk_color) (nk_rgba_fv(val));
					nk_memcopy(attr, &col.r, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_B8G8R8A8:
				{
					nk_color col = (nk_color) (nk_rgba_fv(val));
					nk_color bgra = (nk_color) (nk_rgba((int) (col.b), (int) (col.g), (int) (col.r), (int) (col.a)));
					nk_memcopy(attr, &bgra, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_R16G15B16:
				{
					ushort* col = stackalloc ushort[3];
					col[0] = ((ushort) (val[0]*(float) (65535)));
					col[1] = ((ushort) (val[1]*(float) (65535)));
					col[2] = ((ushort) (val[2]*(float) (65535)));
					nk_memcopy(attr, col, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_R16G15B16A16:
				{
					ushort* col = stackalloc ushort[4];
					col[0] = ((ushort) (val[0]*(float) (65535)));
					col[1] = ((ushort) (val[1]*(float) (65535)));
					col[2] = ((ushort) (val[2]*(float) (65535)));
					col[3] = ((ushort) (val[3]*(float) (65535)));
					nk_memcopy(attr, col, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_R32G32B32:
				{
					uint* col = stackalloc uint[3];
					col[0] = ((uint) (val[0]*(float) (4294967295u)));
					col[1] = ((uint) (val[1]*(float) (4294967295u)));
					col[2] = ((uint) (val[2]*(float) (4294967295u)));
					nk_memcopy(attr, col, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_R32G32B32A32:
				{
					uint* col = stackalloc uint[4];
					col[0] = ((uint) (val[0]*(float) (4294967295u)));
					col[1] = ((uint) (val[1]*(float) (4294967295u)));
					col[2] = ((uint) (val[2]*(float) (4294967295u)));
					col[3] = ((uint) (val[3]*(float) (4294967295u)));
					nk_memcopy(attr, col, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_R32G32B32A32_FLOAT:
					nk_memcopy(attr, val, (ulong) (sizeof (float)*4));
					break;
				case NK_FORMAT_R32G32B32A32_DOUBLE:
				{
					double* col = stackalloc double[4];
					col[0] = ((double) (val[0]));
					col[1] = ((double) (val[1]));
					col[2] = ((double) (val[2]));
					col[3] = ((double) (val[3]));
					nk_memcopy(attr, col, (ulong) (sizeof (nk_color)));
				}
					break;
				case NK_FORMAT_RGB32:
				case NK_FORMAT_RGBA32:
				{
					nk_color col = (nk_color) (nk_rgba_fv(val));
					uint color = (uint) (nk_color_u32((nk_color) (col)));
					nk_memcopy(attr, &color, (ulong) (sizeof (uint)));
				}
					break;
			}

		}

		public static void nk_draw_vertex_element(void* dst, float* values, int value_count, int format)
		{
			int value_index;
			void* attribute = dst;
			if (((format) >= (NK_FORMAT_COLOR_BEGIN)) && (format <= NK_FORMAT_COLOR_END)) return;
			for (value_index = (int) (0); (value_index) < (value_count); ++value_index)
			{
				switch (format)
				{
					default:
						;
						break;
					case NK_FORMAT_SCHAR:
					{
						sbyte value =
							(sbyte)
								(((values[value_index]) < ((float) (127)) ? (values[value_index]) : ((float) (127))) < ((float) (-127))
									? ((float) (-127))
									: ((values[value_index]) < ((float) (127)) ? (values[value_index]) : ((float) (127))));
						nk_memcopy(attribute, &value, (ulong) (sizeof (byte)));
						attribute = (void*) (((sbyte*) (attribute) + sizeof (byte)));
					}
						break;
					case NK_FORMAT_SSHORT:
					{
						short value =
							(short)
								(((values[value_index]) < ((float) (32767)) ? (values[value_index]) : ((float) (32767))) < ((float) (-32767))
									? ((float) (-32767))
									: ((values[value_index]) < ((float) (32767)) ? (values[value_index]) : ((float) (32767))));
						nk_memcopy(attribute, &value, (ulong) (sizeof (short)));
						attribute = (void*) ((sbyte*) (attribute) + sizeof (short));
					}
						break;
					case NK_FORMAT_SINT:
					{
						int value =
							(int)
								(((values[value_index]) < ((float) (2147483647)) ? (values[value_index]) : ((float) (2147483647))) <
								 ((float) (-2147483647))
									? ((float) (-2147483647))
									: ((values[value_index]) < ((float) (2147483647)) ? (values[value_index]) : ((float) (2147483647))));
						nk_memcopy(attribute, &value, (ulong) (sizeof (int)));
						attribute = (void*) ((sbyte*) (attribute) + sizeof (int));
					}
						break;
					case NK_FORMAT_UCHAR:
					{
						byte value =
							(byte)
								(((values[value_index]) < ((float) (256)) ? (values[value_index]) : ((float) (256))) < ((float) (0))
									? ((float) (0))
									: ((values[value_index]) < ((float) (256)) ? (values[value_index]) : ((float) (256))));
						nk_memcopy(attribute, &value, (ulong) (sizeof (byte)));
						attribute = (void*) (((sbyte*) (attribute) + sizeof (byte)));
					}
						break;
					case NK_FORMAT_USHORT:
					{
						ushort value =
							(ushort)
								(((values[value_index]) < ((float) (65535)) ? (values[value_index]) : ((float) (65535))) < ((float) (0))
									? ((float) (0))
									: ((values[value_index]) < ((float) (65535)) ? (values[value_index]) : ((float) (65535))));
						nk_memcopy(attribute, &value, (ulong) (sizeof (short)));
						attribute = (void*) ((sbyte*) (attribute) + sizeof (short));
					}
						break;
					case NK_FORMAT_UINT:
					{
						uint value =
							(uint)
								(((values[value_index]) < ((float) (4294967295u)) ? (values[value_index]) : ((float) (4294967295u))) <
								 ((float) (0))
									? ((float) (0))
									: ((values[value_index]) < ((float) (4294967295u)) ? (values[value_index]) : ((float) (4294967295u))));
						nk_memcopy(attribute, &value, (ulong) (sizeof (uint)));
						attribute = (void*) ((sbyte*) (attribute) + sizeof (uint));
					}
						break;
					case NK_FORMAT_FLOAT:
						nk_memcopy(attribute, &values[value_index], (ulong) (sizeof (float)));
						attribute = (void*) (((sbyte*) (attribute) + sizeof (float)));
						break;
					case NK_FORMAT_DOUBLE:
					{
						double value = (double) (values[value_index]);
						nk_memcopy(attribute, &value, (ulong) (sizeof (double)));
						attribute = (void*) (((sbyte*) (attribute) + sizeof (double)));
					}
						break;
				}
			}
		}

		public static void* nk_draw_vertex(void* dst, nk_convert_config config, nk_vec2 pos, nk_vec2 uv, nk_colorf color)
		{
			void* result = (void*) ((sbyte*) (dst) + config.vertex_size);
			fixed (nk_draw_vertex_layout_element* elem_iter2 = config.vertex_layout)
			{
				nk_draw_vertex_layout_element* elem_iter = elem_iter2;
				while (nk_draw_vertex_layout_element_is_end_of_layout(elem_iter) == 0)
				{
					void* address = (void*) ((sbyte*) (dst) + elem_iter->offset);
					switch (elem_iter->attribute)
					{
						case NK_VERTEX_ATTRIBUTE_COUNT:
						default:
							;
							break;
						case NK_VERTEX_POSITION:
							nk_draw_vertex_element(address, &pos.x, (int) (2), (int) (elem_iter->format));
							break;
						case NK_VERTEX_TEXCOORD:
							nk_draw_vertex_element(address, &uv.x, (int) (2), (int) (elem_iter->format));
							break;
						case NK_VERTEX_COLOR:
							nk_draw_vertex_color(address, &color.r, (int) (elem_iter->format));
							break;
					}
					elem_iter++;
				}
			}
			return result;
		}

		public static int nk_rect_height_compare(void* a, void* b)
		{
			nk_rp_rect* p = (nk_rp_rect*) (a);
			nk_rp_rect* q = (nk_rp_rect*) (b);
			if ((p->h) > (q->h)) return (int) (-1);
			if ((p->h) < (q->h)) return (int) (1);
			return (int) (((p->w) > (q->w)) ? -1 : ((p->w) < (q->w)) ? 1 : 0);
		}

		public static int nk_rect_original_order(void* a, void* b)
		{
			nk_rp_rect* p = (nk_rp_rect*) (a);
			nk_rp_rect* q = (nk_rp_rect*) (b);
			return (int) (((p->was_packed) < (q->was_packed)) ? -1 : ((p->was_packed) > (q->was_packed)) ? 1 : 0);
		}

		public static ushort nk_ttUSHORT(byte* p)
		{
			return (ushort) (p[0]*256 + p[1]);
		}

		public static short nk_ttSHORT(byte* p)
		{
			return (short) (p[0]*256 + p[1]);
		}

		public static uint nk_ttULONG(byte* p)
		{
			return (uint) ((p[0] << 24) + (p[1] << 16) + (p[2] << 8) + p[3]);
		}

		public static uint nk_tt__find_table(byte* data, uint fontstart, string tag)
		{
			int num_tables = (int) (nk_ttUSHORT(data + fontstart + 4));
			uint tabledir = (uint) (fontstart + 12);
			int i;
			for (i = (int) (0); (i) < (num_tables); ++i)
			{
				uint loc = (uint) (tabledir + (uint) (16*i));
				if (((((((data + loc + 0)[0]) == (tag[0])) && (((data + loc + 0)[1]) == (tag[1]))) &&
				      (((data + loc + 0)[2]) == (tag[2]))) && (((data + loc + 0)[3]) == (tag[3]))))
					return (uint) (nk_ttULONG(data + loc + 8));
			}
			return (uint) (0);
		}

		public static void nk_tt__handle_clipped_edge(float* scanline, int x, nk_tt__active_edge* e, float x0, float y0,
			float x1, float y1)
		{
			if ((y0) == (y1)) return;
			if ((y0) > (e->ey)) return;
			if ((y1) < (e->sy)) return;
			if ((y0) < (e->sy))
			{
				x0 += (float) ((x1 - x0)*(e->sy - y0)/(y1 - y0));
				y0 = (float) (e->sy);
			}

			if ((y1) > (e->ey))
			{
				x1 += (float) ((x1 - x0)*(e->ey - y1)/(y1 - y0));
				y1 = (float) (e->ey);
			}

			if ((x0 <= x) && (x1 <= x)) scanline[x] += (float) (e->direction*(y1 - y0));
			else if (((x0) >= (x + 1)) && ((x1) >= (x + 1)))
			{
			}
			else
			{
				scanline[x] += (float) (e->direction*(y1 - y0)*(1.0f - ((x0 - (float) (x)) + (x1 - (float) (x)))/2.0f));
			}

		}

		public static void nk_tt__fill_active_edges_new(float* scanline, float* scanline_fill, int len, nk_tt__active_edge* e,
			float y_top)
		{
			float y_bottom = (float) (y_top + 1);
			while ((e) != null)
			{
				if ((e->fdx) == (0))
				{
					float x0 = (float) (e->fx);
					if ((x0) < (len))
					{
						if ((x0) >= (0))
						{
							nk_tt__handle_clipped_edge(scanline, (int) (x0), e, (float) (x0), (float) (y_top), (float) (x0),
								(float) (y_bottom));
							nk_tt__handle_clipped_edge(scanline_fill - 1, (int) ((int) (x0) + 1), e, (float) (x0), (float) (y_top),
								(float) (x0), (float) (y_bottom));
						}
						else
						{
							nk_tt__handle_clipped_edge(scanline_fill - 1, (int) (0), e, (float) (x0), (float) (y_top), (float) (x0),
								(float) (y_bottom));
						}
					}
				}
				else
				{
					float x0 = (float) (e->fx);
					float dx = (float) (e->fdx);
					float xb = (float) (x0 + dx);
					float x_top;
					float x_bottom;
					float y0;
					float y1;
					float dy = (float) (e->fdy);
					if ((e->sy) > (y_top))
					{
						x_top = (float) (x0 + dx*(e->sy - y_top));
						y0 = (float) (e->sy);
					}
					else
					{
						x_top = (float) (x0);
						y0 = (float) (y_top);
					}
					if ((e->ey) < (y_bottom))
					{
						x_bottom = (float) (x0 + dx*(e->ey - y_top));
						y1 = (float) (e->ey);
					}
					else
					{
						x_bottom = (float) (xb);
						y1 = (float) (y_bottom);
					}
					if (((((x_top) >= (0)) && ((x_bottom) >= (0))) && ((x_top) < (len))) && ((x_bottom) < (len)))
					{
						if (((int) (x_top)) == ((int) (x_bottom)))
						{
							float height;
							int x = (int) (x_top);
							height = (float) (y1 - y0);
							scanline[x] += (float) (e->direction*(1.0f - ((x_top - (float) (x)) + (x_bottom - (float) (x)))/2.0f)*height);
							scanline_fill[x] += (float) (e->direction*height);
						}
						else
						{
							int x;
							int x1;
							int x2;
							float y_crossing;
							float step;
							float sign;
							float area;
							if ((x_top) > (x_bottom))
							{
								float t;
								y0 = (float) (y_bottom - (y0 - y_top));
								y1 = (float) (y_bottom - (y1 - y_top));
								t = (float) (y0);
								y0 = (float) (y1);
								y1 = (float) (t);
								t = (float) (x_bottom);
								x_bottom = (float) (x_top);
								x_top = (float) (t);
								dx = (float) (-dx);
								dy = (float) (-dy);
								t = (float) (x0);
								x0 = (float) (xb);
								xb = (float) (t);
							}
							x1 = ((int) (x_top));
							x2 = ((int) (x_bottom));
							y_crossing = (float) (((float) (x1) + 1 - x0)*dy + y_top);
							sign = (float) (e->direction);
							area = (float) (sign*(y_crossing - y0));
							scanline[x1] += (float) (area*(1.0f - ((x_top - (float) (x1)) + (float) (x1 + 1 - x1))/2.0f));
							step = (float) (sign*dy);
							for (x = (int) (x1 + 1); (x) < (x2); ++x)
							{
								scanline[x] += (float) (area + step/2);
								area += (float) (step);
							}
							y_crossing += (float) (dy*(float) (x2 - (x1 + 1)));
							scanline[x2] +=
								(float) (area + sign*(1.0f - ((float) (x2 - x2) + (x_bottom - (float) (x2)))/2.0f)*(y1 - y_crossing));
							scanline_fill[x2] += (float) (sign*(y1 - y0));
						}
					}
					else
					{
						int x;
						for (x = (int) (0); (x) < (len); ++x)
						{
							float ya = (float) (y_top);
							float x1 = (float) (x);
							float x2 = (float) (x + 1);
							float x3 = (float) (xb);
							float y3 = (float) (y_bottom);
							float yb;
							float y2;
							yb = (float) (((float) (x) - x0)/dx + y_top);
							y2 = (float) (((float) (x) + 1 - x0)/dx + y_top);
							if (((x0) < (x1)) && ((x3) > (x2)))
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x1), (float) (yb));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x1), (float) (yb), (float) (x2), (float) (y2));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x2), (float) (y2), (float) (x3), (float) (y3));
							}
							else if (((x3) < (x1)) && ((x0) > (x2)))
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x2), (float) (y2));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x2), (float) (y2), (float) (x1), (float) (yb));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x1), (float) (yb), (float) (x3), (float) (y3));
							}
							else if (((x0) < (x1)) && ((x3) > (x1)))
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x1), (float) (yb));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x1), (float) (yb), (float) (x3), (float) (y3));
							}
							else if (((x3) < (x1)) && ((x0) > (x1)))
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x1), (float) (yb));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x1), (float) (yb), (float) (x3), (float) (y3));
							}
							else if (((x0) < (x2)) && ((x3) > (x2)))
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x2), (float) (y2));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x2), (float) (y2), (float) (x3), (float) (y3));
							}
							else if (((x3) < (x2)) && ((x0) > (x2)))
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x2), (float) (y2));
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x2), (float) (y2), (float) (x3), (float) (y3));
							}
							else
							{
								nk_tt__handle_clipped_edge(scanline, (int) (x), e, (float) (x0), (float) (ya), (float) (x3), (float) (y3));
							}
						}
					}
				}
				e = e->next;
			}
		}

		public static void nk_tt__h_prefilter(byte* pixels, int w, int h, int stride_in_bytes, int kernel_width)
		{
			byte* buffer = stackalloc byte[8];
			int safe_w = (int) (w - kernel_width);
			int j;
			for (j = (int) (0); (j) < (h); ++j)
			{
				int i;
				uint total;
				nk_memset(buffer, (int) (0), (ulong) (kernel_width));
				total = (uint) (0);
				switch (kernel_width)
				{
					case 2:
						for (i = (int) (0); i <= safe_w; ++i)
						{
							total += ((uint) (pixels[i] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i]);
							pixels[i] = ((byte) (total/2));
						}
						break;
					case 3:
						for (i = (int) (0); i <= safe_w; ++i)
						{
							total += ((uint) (pixels[i] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i]);
							pixels[i] = ((byte) (total/3));
						}
						break;
					case 4:
						for (i = (int) (0); i <= safe_w; ++i)
						{
							total += (uint) ((uint) (pixels[i]) - buffer[i & (8 - 1)]);
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i]);
							pixels[i] = ((byte) (total/4));
						}
						break;
					case 5:
						for (i = (int) (0); i <= safe_w; ++i)
						{
							total += ((uint) (pixels[i] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i]);
							pixels[i] = ((byte) (total/5));
						}
						break;
					default:
						for (i = (int) (0); i <= safe_w; ++i)
						{
							total += ((uint) (pixels[i] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i]);
							pixels[i] = ((byte) (total/(uint) (kernel_width)));
						}
						break;
				}
				for (; (i) < (w); ++i)
				{
					total -= ((uint) (buffer[i & (8 - 1)]));
					pixels[i] = ((byte) (total/(uint) (kernel_width)));
				}
				pixels += stride_in_bytes;
			}
		}

		public static void nk_tt__v_prefilter(byte* pixels, int w, int h, int stride_in_bytes, int kernel_width)
		{
			byte* buffer = stackalloc byte[8];
			int safe_h = (int) (h - kernel_width);
			int j;
			for (j = (int) (0); (j) < (w); ++j)
			{
				int i;
				uint total;
				nk_memset(buffer, (int) (0), (ulong) (kernel_width));
				total = (uint) (0);
				switch (kernel_width)
				{
					case 2:
						for (i = (int) (0); i <= safe_h; ++i)
						{
							total += ((uint) (pixels[i*stride_in_bytes] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i*stride_in_bytes]);
							pixels[i*stride_in_bytes] = ((byte) (total/2));
						}
						break;
					case 3:
						for (i = (int) (0); i <= safe_h; ++i)
						{
							total += ((uint) (pixels[i*stride_in_bytes] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i*stride_in_bytes]);
							pixels[i*stride_in_bytes] = ((byte) (total/3));
						}
						break;
					case 4:
						for (i = (int) (0); i <= safe_h; ++i)
						{
							total += ((uint) (pixels[i*stride_in_bytes] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i*stride_in_bytes]);
							pixels[i*stride_in_bytes] = ((byte) (total/4));
						}
						break;
					case 5:
						for (i = (int) (0); i <= safe_h; ++i)
						{
							total += ((uint) (pixels[i*stride_in_bytes] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i*stride_in_bytes]);
							pixels[i*stride_in_bytes] = ((byte) (total/5));
						}
						break;
					default:
						for (i = (int) (0); i <= safe_h; ++i)
						{
							total += ((uint) (pixels[i*stride_in_bytes] - buffer[i & (8 - 1)]));
							buffer[(i + kernel_width) & (8 - 1)] = (byte) (pixels[i*stride_in_bytes]);
							pixels[i*stride_in_bytes] = ((byte) (total/(uint) (kernel_width)));
						}
						break;
				}
				for (; (i) < (h); ++i)
				{
					total -= ((uint) (buffer[i & (8 - 1)]));
					pixels[i*stride_in_bytes] = ((byte) (total/(uint) (kernel_width)));
				}
				pixels += 1;
			}
		}

		public static float nk_tt__oversample_shift(int oversample)
		{
			if (oversample == 0) return (float) (0.0f);
			return (float) ((float) (-(oversample - 1))/(2.0f*(float) (oversample)));
		}

		public static int nk_range_count(uint* range)
		{
			uint* iter = range;
			if (range == null) return (int) (0);
			while (*(iter++) != 0)
			{
			}
			return (int) (((iter) == (range)) ? 0 : (int) ((iter - range)/2));
		}

		public static int nk_range_glyph_count(uint* range, int count)
		{
			int i = (int) (0);
			int total_glyphs = (int) (0);
			for (i = (int) (0); (i) < (count); ++i)
			{
				int diff;
				uint f = (uint) (range[(i*2) + 0]);
				uint t = (uint) (range[(i*2) + 1]);
				diff = ((int) ((t - f) + 1));
				total_glyphs += (int) (diff);
			}
			return (int) (total_glyphs);
		}

		public static void nk_font_baker_memory(ulong* temp, ref int glyph_count, nk_font_config config_list, int count)
		{
			int range_count = (int) (0);
			int total_range_count = (int) (0);
			nk_font_config iter;
			nk_font_config i;
			if (config_list == null)
			{
				*temp = (ulong) (0);
				glyph_count = (int) (0);
				return;
			}

			glyph_count = (int) (0);
			for (iter = config_list; iter != null; iter = iter.next)
			{
				i = iter;
				do
				{
					if (i.range == null) iter.range = nk_font_default_glyph_ranges();
					range_count = (int) (nk_range_count(i.range));
					total_range_count += (int) (range_count);
					glyph_count += (int) (nk_range_glyph_count(i.range, (int) (range_count)));
				} while ((i = i.n) != iter);
			}
			*temp = (ulong) ((ulong) (glyph_count)*(ulong) sizeof (nk_rp_rect));
			*temp += (ulong) ((ulong) (total_range_count)*(ulong) sizeof (nk_tt_pack_range));
			*temp += (ulong) ((ulong) (glyph_count)*(ulong) sizeof (nk_tt_packedchar));
			*temp += (ulong) ((ulong) (count)*(ulong) sizeof (nk_font_bake_data));
			*temp += (ulong) (sizeof (nk_font_baker));
			*temp += (ulong) (nk_rect_align + nk_range_align + nk_char_align);
			*temp += (ulong) (nk_build_align + nk_baker_align);
		}

		public static nk_font_baker* nk_font_baker_(void* memory, int glyph_count, int count)
		{
			nk_font_baker* baker;
			if (memory == null) return null;
			baker =
				(nk_font_baker*)
					((void*) ((long) (((ulong) ((long) ((byte*) (memory) + (nk_baker_align - 1)))) & ~(nk_baker_align - 1))));
			baker->build =
				(nk_font_bake_data*)
					((void*) ((long) (((ulong) ((long) ((byte*) (baker + 1) + (nk_build_align - 1)))) & ~(nk_build_align - 1))));
			baker->packed_chars =
				(nk_tt_packedchar*)
					((void*)
						((long) (((ulong) ((long) ((byte*) (baker->build + count) + (nk_char_align - 1)))) & ~(nk_char_align - 1))));
			baker->rects =
				(nk_rp_rect*)
					((void*)
						((long)
							(((ulong) ((long) ((byte*) (baker->packed_chars + glyph_count) + (nk_rect_align - 1)))) & ~(nk_rect_align - 1))));
			baker->ranges =
				(nk_tt_pack_range*)
					((void*)
						((long) (((ulong) ((long) ((byte*) (baker->rects + glyph_count) + (nk_range_align - 1)))) & ~(nk_range_align - 1))));

			return baker;
		}

		public static void nk_font_bake_custom_data(void* img_memory, int img_width, int img_height, nk_recti img_dst,
			byte* texture_data_mask, int tex_width, int tex_height, char white, char black)
		{
			byte* pixels;
			int y = (int) (0);
			int x = (int) (0);
			int n = (int) (0);
			if ((((img_memory == null) || (img_width == 0)) || (img_height == 0)) || (texture_data_mask == null)) return;
			pixels = (byte*) (img_memory);
			for (y = (int) (0) , n = (int) (0); (y) < (tex_height); ++y)
			{
				for (x = (int) (0); (x) < (tex_width); ++x , ++n)
				{
					int off0 = (int) ((img_dst.x + x) + (img_dst.y + y)*img_width);
					int off1 = (int) (off0 + 1 + tex_width);
					pixels[off0] = (byte) (((texture_data_mask[n]) == (white)) ? 0xFF : 0x00);
					pixels[off1] = (byte) (((texture_data_mask[n]) == (black)) ? 0xFF : 0x00);
				}
			}
		}

		public static void nk_font_bake_convert(void* out_memory, int img_width, int img_height, void* in_memory)
		{
			int n = (int) (0);
			uint* dst;
			byte* src;
			if ((((out_memory == null) || (in_memory == null)) || (img_height == 0)) || (img_width == 0)) return;
			dst = (uint*) (out_memory);
			src = (byte*) (in_memory);
			for (n = (int) (img_width*img_height); (n) > (0); n--)
			{
				*dst++ = (uint) (((uint) (*src++) << 24) | 0x00FFFFFF);
			}
		}

		public static uint nk_decompress_length(byte* input)
		{
			return (uint) ((input[8] << 24) + (input[9] << 16) + (input[10] << 8) + input[11]);
		}

		public static void nk__match(byte* data, uint length)
		{
			if ((nk__dout + length) > (nk__barrier))
			{
				nk__dout += length;
				return;
			}

			if ((data) < (nk__barrier4))
			{
				nk__dout = nk__barrier + 1;
				return;
			}

			while ((length--) != 0)
			{
				*nk__dout++ = (byte) (*data++);
			}
		}

		public static void nk__lit(byte* data, uint length)
		{
			if ((nk__dout + length) > (nk__barrier))
			{
				nk__dout += length;
				return;
			}

			if ((data) < (nk__barrier2))
			{
				nk__dout = nk__barrier + 1;
				return;
			}

			nk_memcopy(nk__dout, data, (ulong) (length));
			nk__dout += length;
		}

		public static byte* nk_decompress_token(byte* i)
		{
			if ((*i) >= (0x20))
			{
				if ((*i) >= (0x80))
				{
					nk__match(nk__dout - i[1] - 1, (uint) ((uint) (i[0]) - 0x80 + 1));
					i += 2;
				}
				else if ((*i) >= (0x40))
				{
					nk__match(nk__dout - (((i[0] << 8) + i[(0) + 1]) - 0x4000 + 1), (uint) ((uint) (i[2]) + 1));
					i += 3;
				}
				else
				{
					nk__lit(i + 1, (uint) ((uint) (i[0]) - 0x20 + 1));
					i += 1 + (i[0] - 0x20 + 1);
				}
			}
			else
			{
				if ((*i) >= (0x18))
				{
					nk__match(nk__dout - (uint) (((i[0] << 16) + ((i[(0) + 1] << 8) + i[((0) + 1) + 1])) - 0x180000 + 1),
						(uint) ((uint) (i[3]) + 1));
					i += 4;
				}
				else if ((*i) >= (0x10))
				{
					nk__match(nk__dout - (uint) (((i[0] << 16) + ((i[(0) + 1] << 8) + i[((0) + 1) + 1])) - 0x100000 + 1),
						(uint) ((uint) ((i[3] << 8) + i[(3) + 1]) + 1));
					i += 5;
				}
				else if ((*i) >= (0x08))
				{
					nk__lit(i + 2, (uint) ((uint) ((i[0] << 8) + i[(0) + 1]) - 0x0800 + 1));
					i += 2 + (((i[0] << 8) + i[(0) + 1]) - 0x0800 + 1);
				}
				else if ((*i) == (0x07))
				{
					nk__lit(i + 3, (uint) ((uint) ((i[1] << 8) + i[(1) + 1]) + 1));
					i += 3 + (((i[1] << 8) + i[(1) + 1]) + 1);
				}
				else if ((*i) == (0x06))
				{
					nk__match(nk__dout - (uint) (((i[1] << 16) + ((i[(1) + 1] << 8) + i[((1) + 1) + 1])) + 1), (uint) (i[4] + 1u));
					i += 5;
				}
				else if ((*i) == (0x04))
				{
					nk__match(nk__dout - (uint) (((i[1] << 16) + ((i[(1) + 1] << 8) + i[((1) + 1) + 1])) + 1),
						(uint) ((uint) ((i[4] << 8) + i[(4) + 1]) + 1u));
					i += 6;
				}
			}

			return i;
		}

		public static uint nk_adler32(uint adler32, byte* buffer, uint buflen)
		{
			int ADLER_MOD = (int) (65521);
			int s1 = (int) (adler32 & 0xffff);
			int s2 = (int) (adler32 >> 16);
			int blocklen;
			int i;
			blocklen = (int) (buflen%5552);
			while ((buflen) != 0)
			{
				for (i = (int) (0); (i + 7) < (blocklen); i += (int) (8))
				{
					s1 += (int) (buffer[0]);
					s2 += (int) (s1);
					s1 += (int) (buffer[1]);
					s2 += (int) (s1);
					s1 += (int) (buffer[2]);
					s2 += (int) (s1);
					s1 += (int) (buffer[3]);
					s2 += (int) (s1);
					s1 += (int) (buffer[4]);
					s2 += (int) (s1);
					s1 += (int) (buffer[5]);
					s2 += (int) (s1);
					s1 += (int) (buffer[6]);
					s2 += (int) (s1);
					s1 += (int) (buffer[7]);
					s2 += (int) (s1);
					buffer += 8;
				}
				for (; (i) < (blocklen); ++i)
				{
					s1 += (int) (*buffer++);
					s2 += (int) (s1);
				}
				s1 %= (int) (ADLER_MOD);
				s2 %= (int) (ADLER_MOD);
				buflen -= ((uint) (blocklen));
				blocklen = (int) (5552);
			}
			return (uint) ((uint) (s2 << 16) + (uint) (s1));
		}

		public static uint nk_decompress(byte* output, byte* i, uint length)
		{
			uint olen;
			if (((i[0] << 24) + ((i[(0) + 1] << 16) + ((i[((0) + 1) + 1] << 8) + i[(((0) + 1) + 1) + 1]))) != 0x57bC0000)
				return (uint) (0);
			if (((i[4] << 24) + ((i[(4) + 1] << 16) + ((i[((4) + 1) + 1] << 8) + i[(((4) + 1) + 1) + 1]))) != 0)
				return (uint) (0);
			olen = (uint) (nk_decompress_length(i));
			nk__barrier2 = i;
			nk__barrier3 = i + length;
			nk__barrier = output + olen;
			nk__barrier4 = output;
			i += 16;
			nk__dout = output;
			for (;;)
			{
				byte* old_i = i;
				i = nk_decompress_token(i);
				if ((i) == (old_i))
				{
					if (((*i) == (0x05)) && ((i[1]) == (0xfa)))
					{
						if (nk__dout != output + olen) return (uint) (0);
						if (nk_adler32((uint) (1), output, (uint) (olen)) !=
						    (uint) ((i[2] << 24) + ((i[(2) + 1] << 16) + ((i[((2) + 1) + 1] << 8) + i[(((2) + 1) + 1) + 1]))))
							return (uint) (0);
						return (uint) (olen);
					}
					else
					{
						return (uint) (0);
					}
				}
				if ((nk__dout) > (output + olen)) return (uint) (0);
			}
		}

		public static uint nk_decode_85_byte(sbyte c)
		{
			return (uint) (((c) >= ('\\')) ? c - 36 : c - 35);
		}

		public static void nk_decode_85(byte* dst, byte* src)
		{
			while ((*src) != 0)
			{
				uint tmp =
					(uint)
						(nk_decode_85_byte((sbyte) (src[0])) +
						 85*
						 (nk_decode_85_byte((sbyte) (src[1])) +
						  85*
						  (nk_decode_85_byte((sbyte) (src[2])) +
						   85*(nk_decode_85_byte((sbyte) (src[3])) + 85*nk_decode_85_byte((sbyte) (src[4]))))));
				dst[0] = ((byte) ((tmp >> 0) & 0xFF));
				dst[1] = ((byte) ((tmp >> 8) & 0xFF));
				dst[2] = ((byte) ((tmp >> 16) & 0xFF));
				dst[3] = ((byte) ((tmp >> 24) & 0xFF));
				src += 5;
				dst += 4;
			}
		}

		public static nk_font_config nk_font_config_(float pixel_height)
		{
			nk_font_config cfg = new nk_font_config();

			cfg.ttf_blob = null;
			cfg.ttf_size = (ulong) (0);
			cfg.ttf_data_owned_by_atlas = (byte) (0);
			cfg.size = (float) (pixel_height);
			cfg.oversample_h = (byte) (3);
			cfg.oversample_v = (byte) (1);
			cfg.pixel_snap = (byte) (0);
			cfg.coord_type = (int) (NK_COORD_UV);
			cfg.spacing = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			cfg.range = nk_font_default_glyph_ranges();
			cfg.merge_mode = (byte) (0);
			cfg.fallback_glyph = '?';
			cfg.font = null;
			cfg.n = null;
			return (nk_font_config) (cfg);
		}

		public static int nk_button_behavior(ref uint state, nk_rect r, nk_input i, int behavior)
		{
			int ret = (int) (0);
			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if (i == null) return (int) (0);
			if ((nk_input_is_mouse_hovering_rect(i, (nk_rect) (r))) != 0)
			{
				state = (uint) (NK_WIDGET_STATE_HOVERED);
				if ((nk_input_is_mouse_down(i, (int) (NK_BUTTON_LEFT))) != 0) state = (uint) (NK_WIDGET_STATE_ACTIVE);
				if ((nk_input_has_mouse_click_in_rect(i, (int) (NK_BUTTON_LEFT), (nk_rect) (r))) != 0)
				{
					ret =
						(int)
							((behavior != NK_BUTTON_DEFAULT)
								? nk_input_is_mouse_down(i, (int) (NK_BUTTON_LEFT))
								: nk_input_is_mouse_pressed(i, (int) (NK_BUTTON_LEFT)));
				}
			}

			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(i, (nk_rect) (r)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(i, (nk_rect) (r))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
			return (int) (ret);
		}

		public static int nk_do_button(ref uint state, nk_command_buffer _out_, nk_rect r, nk_style_button style,
			nk_input _in_, int behavior, nk_rect* content)
		{
			nk_rect bounds = new nk_rect();
			if ((_out_ == null) || (style == null)) return (int) (nk_false);
			content->x = (float) (r.x + style.padding.x + style.border + style.rounding);
			content->y = (float) (r.y + style.padding.y + style.border + style.rounding);
			content->w = (float) (r.w - (2*style.padding.x + style.border + style.rounding*2));
			content->h = (float) (r.h - (2*style.padding.y + style.border + style.rounding*2));
			bounds.x = (float) (r.x - style.touch_padding.x);
			bounds.y = (float) (r.y - style.touch_padding.y);
			bounds.w = (float) (r.w + 2*style.touch_padding.x);
			bounds.h = (float) (r.h + 2*style.touch_padding.y);
			return (int) (nk_button_behavior(ref state, (nk_rect) (bounds), _in_, (int) (behavior)));
		}

		public static int nk_do_button_text(ref uint state, nk_command_buffer _out_, nk_rect bounds, char* _string_, int len,
			uint align, int behavior, nk_style_button style, nk_input _in_, nk_user_font font)
		{
			nk_rect content = new nk_rect();
			int ret = (int) (nk_false);
			if ((((_out_ == null) || (style == null)) || (font == null)) || (_string_ == null)) return (int) (nk_false);
			ret = (int) (nk_do_button(ref state, _out_, (nk_rect) (bounds), style, _in_, (int) (behavior), &content));
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_button_text(_out_, &bounds, &content, (uint) (state), style, _string_, (int) (len), (uint) (align), font);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (int) (ret);
		}

		public static int nk_do_button_symbol(ref uint state, nk_command_buffer _out_, nk_rect bounds, int symbol,
			int behavior, nk_style_button style, nk_input _in_, nk_user_font font)
		{
			int ret;
			nk_rect content = new nk_rect();
			if ((((_out_ == null) || (style == null)) || (font == null))) return (int) (nk_false);
			ret = (int) (nk_do_button(ref state, _out_, (nk_rect) (bounds), style, _in_, (int) (behavior), &content));
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_button_symbol(_out_, &bounds, &content, (uint) (state), style, (int) (symbol), font);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (int) (ret);
		}

		public static int nk_do_button_image(ref uint state, nk_command_buffer _out_, nk_rect bounds, nk_image img, int b,
			nk_style_button style, nk_input _in_)
		{
			int ret;
			nk_rect content = new nk_rect();
			if (((_out_ == null) || (style == null))) return (int) (nk_false);
			ret = (int) (nk_do_button(ref state, _out_, (nk_rect) (bounds), style, _in_, (int) (b), &content));
			content.x += (float) (style.image_padding.x);
			content.y += (float) (style.image_padding.y);
			content.w -= (float) (2*style.image_padding.x);
			content.h -= (float) (2*style.image_padding.y);
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_button_image(_out_, &bounds, &content, (uint) (state), style, img);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (int) (ret);
		}

		public static int nk_do_button_text_symbol(ref uint state, nk_command_buffer _out_, nk_rect bounds, int symbol,
			char* str, int len, uint align, int behavior, nk_style_button style, nk_user_font font, nk_input _in_)
		{
			int ret;
			nk_rect tri = new nk_rect();
			nk_rect content = new nk_rect();
			if (((_out_ == null) || (style == null)) || (font == null)) return (int) (nk_false);
			ret = (int) (nk_do_button(ref state, _out_, (nk_rect) (bounds), style, _in_, (int) (behavior), &content));
			tri.y = (float) (content.y + (content.h/2) - font.height/2);
			tri.w = (float) (font.height);
			tri.h = (float) (font.height);
			if ((align & NK_TEXT_ALIGN_LEFT) != 0)
			{
				tri.x = (float) ((content.x + content.w) - (2*style.padding.x + tri.w));
				tri.x = (float) ((tri.x) < (0) ? (0) : (tri.x));
			}
			else tri.x = (float) (content.x + 2*style.padding.x);

			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_button_text_symbol(_out_, &bounds, &content, &tri, (uint) (state), style, str, (int) (len), (int) (symbol),
				font);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (int) (ret);
		}

		public static int nk_do_button_text_image(ref uint state, nk_command_buffer _out_, nk_rect bounds, nk_image img,
			char* str, int len, uint align, int behavior, nk_style_button style, nk_user_font font, nk_input _in_)
		{
			int ret;
			nk_rect icon = new nk_rect();
			nk_rect content = new nk_rect();
			if ((((_out_ == null) || (font == null)) || (style == null)) || (str == null)) return (int) (nk_false);
			ret = (int) (nk_do_button(ref state, _out_, (nk_rect) (bounds), style, _in_, (int) (behavior), &content));
			icon.y = (float) (bounds.y + style.padding.y);
			icon.w = (float) (icon.h = (float) (bounds.h - 2*style.padding.y));
			if ((align & NK_TEXT_ALIGN_LEFT) != 0)
			{
				icon.x = (float) ((bounds.x + bounds.w) - (2*style.padding.x + icon.w));
				icon.x = (float) ((icon.x) < (0) ? (0) : (icon.x));
			}
			else icon.x = (float) (bounds.x + 2*style.padding.x);
			icon.x += (float) (style.image_padding.x);
			icon.y += (float) (style.image_padding.y);
			icon.w -= (float) (2*style.image_padding.x);
			icon.h -= (float) (2*style.image_padding.y);
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_button_text_image(_out_, &bounds, &content, &icon, (uint) (state), style, str, (int) (len), font, img);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (int) (ret);
		}

		public static int nk_do_toggle(ref uint state, nk_command_buffer _out_, nk_rect r, int* active, char* str, int len,
			int type, nk_style_toggle style, nk_input _in_, nk_user_font font)
		{
			int was_active;
			nk_rect bounds = new nk_rect();
			nk_rect select = new nk_rect();
			nk_rect cursor = new nk_rect();
			nk_rect label = new nk_rect();
			if ((((_out_ == null) || (style == null)) || (font == null)) || (active == null)) return (int) (0);
			r.w = (float) ((r.w) < (font.height + 2*style.padding.x) ? (font.height + 2*style.padding.x) : (r.w));
			r.h = (float) ((r.h) < (font.height + 2*style.padding.y) ? (font.height + 2*style.padding.y) : (r.h));
			bounds.x = (float) (r.x - style.touch_padding.x);
			bounds.y = (float) (r.y - style.touch_padding.y);
			bounds.w = (float) (r.w + 2*style.touch_padding.x);
			bounds.h = (float) (r.h + 2*style.touch_padding.y);
			select.w = (float) (font.height);
			select.h = (float) (select.w);
			select.y = (float) (r.y + r.h/2.0f - select.h/2.0f);
			select.x = (float) (r.x);
			cursor.x = (float) (select.x + style.padding.x + style.border);
			cursor.y = (float) (select.y + style.padding.y + style.border);
			cursor.w = (float) (select.w - (2*style.padding.x + 2*style.border));
			cursor.h = (float) (select.h - (2*style.padding.y + 2*style.border));
			label.x = (float) (select.x + select.w + style.spacing);
			label.y = (float) (select.y);
			label.w = (float) (((r.x + r.w) < (label.x) ? (label.x) : (r.x + r.w)) - label.x);
			label.h = (float) (select.w);
			was_active = (int) (*active);
			*active = (int) (nk_toggle_behavior(_in_, (nk_rect) (bounds), ref state, (int) (*active)));
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			if ((type) == (NK_TOGGLE_CHECK))
			{
				nk_draw_checkbox(_out_, (uint) (state), style, (int) (*active), &label, &select, &cursor, str, (int) (len), font);
			}
			else
			{
				nk_draw_option(_out_, (uint) (state), style, (int) (*active), &label, &select, &cursor, str, (int) (len), font);
			}

			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return was_active != *active ? 1 : 0;
		}

		public static int nk_do_selectable(ref uint state, nk_command_buffer _out_, nk_rect bounds, char* str, int len,
			uint align, ref int value, nk_style_selectable style, nk_input _in_, nk_user_font font)
		{
			int old_value;
			nk_rect touch = new nk_rect();
			if (((((((_out_ == null)) || (str == null)) || (len == 0))) || (style == null)) ||
			    (font == null)) return (int) (0);
			old_value = (int) (value);
			touch.x = (float) (bounds.x - style.touch_padding.x);
			touch.y = (float) (bounds.y - style.touch_padding.y);
			touch.w = (float) (bounds.w + style.touch_padding.x*2);
			touch.h = (float) (bounds.h + style.touch_padding.y*2);
			if ((nk_button_behavior(ref state, (nk_rect) (touch), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				value = value != 0 ? 0 : 1;
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_selectable(_out_, (uint) (state), style, (int) (value), &bounds, null, null, str, (int) (len), (uint) (align),
				font);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return old_value != value ? 1 : 0;
		}

		public static int nk_do_selectable_image(ref uint state, nk_command_buffer _out_, nk_rect bounds, char* str, int len,
			uint align, ref int value, nk_image img, nk_style_selectable style, nk_input _in_, nk_user_font font)
		{
			int old_value;
			nk_rect touch = new nk_rect();
			nk_rect icon = new nk_rect();
			if (((((((_out_ == null)) || (str == null)) || (len == 0))) || (style == null)) ||
			    (font == null)) return (int) (0);
			old_value = (int) (value);
			touch.x = (float) (bounds.x - style.touch_padding.x);
			touch.y = (float) (bounds.y - style.touch_padding.y);
			touch.w = (float) (bounds.w + style.touch_padding.x*2);
			touch.h = (float) (bounds.h + style.touch_padding.y*2);
			if ((nk_button_behavior(ref state, (nk_rect) (touch), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				value = value != 0 ? 0 : 1;
			icon.y = (float) (bounds.y + style.padding.y);
			icon.w = (float) (icon.h = (float) (bounds.h - 2*style.padding.y));
			if ((align & NK_TEXT_ALIGN_LEFT) != 0)
			{
				icon.x = (float) ((bounds.x + bounds.w) - (2*style.padding.x + icon.w));
				icon.x = (float) ((icon.x) < (0) ? (0) : (icon.x));
			}
			else icon.x = (float) (bounds.x + 2*style.padding.x);
			icon.x += (float) (style.image_padding.x);
			icon.y += (float) (style.image_padding.y);
			icon.w -= (float) (2*style.image_padding.x);
			icon.h -= (float) (2*style.image_padding.y);
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_selectable(_out_, (uint) (state), style, (int) (value), &bounds, &icon, img, str, (int) (len), (uint) (align),
				font);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return old_value != value ? 1 : 0;
		}

		public static float nk_slider_behavior(ref uint state, nk_rect* logical_cursor, nk_rect* visual_cursor, nk_input _in_,
			nk_rect bounds, float slider_min, float slider_max, float slider_value, float slider_step, float slider_steps)
		{
			int left_mouse_down;
			int left_mouse_click_in_cursor;
			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			left_mouse_down =
				(int) (((_in_) != null) && ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down) != 0) ? 1 : 0);
			left_mouse_click_in_cursor =
				(int)
					(((_in_) != null) &&
					 ((nk_input_has_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (*visual_cursor), (int) (nk_true))) !=
					  0)
						? 1
						: 0);
			if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
			{
				float ratio = (float) (0);
				float d = (float) (_in_.mouse.pos.x - (visual_cursor->x + visual_cursor->w*0.5f));
				float pxstep = (float) (bounds.w/slider_steps);
				state = (uint) (NK_WIDGET_STATE_ACTIVE);
				if ((((d) < (0)) ? -(d) : (d)) >= (pxstep))
				{
					float steps = (float) ((int) ((((d) < (0)) ? -(d) : (d))/pxstep));
					slider_value += (float) (((d) > (0)) ? (slider_step*steps) : -(slider_step*steps));
					slider_value =
						(float)
							(((slider_value) < (slider_max) ? (slider_value) : (slider_max)) < (slider_min)
								? (slider_min)
								: ((slider_value) < (slider_max) ? (slider_value) : (slider_max)));
					ratio = (float) ((slider_value - slider_min)/slider_step);
					logical_cursor->x = (float) (bounds.x + (logical_cursor->w*ratio));
					((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.x = (float) (logical_cursor->x);
				}
			}

			if ((nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (bounds))) != 0) state = (uint) (NK_WIDGET_STATE_HOVERED);
			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (bounds)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (bounds))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
			return (float) (slider_value);
		}

		public static float nk_do_slider(ref uint state, nk_command_buffer _out_, nk_rect bounds, float min, float val,
			float max, float step, nk_style_slider style, nk_input _in_, nk_user_font font)
		{
			float slider_range;
			float slider_min;
			float slider_max;
			float slider_value;
			float slider_steps;
			float cursor_offset;
			nk_rect visual_cursor = new nk_rect();
			nk_rect logical_cursor = new nk_rect();
			if ((_out_ == null) || (style == null)) return (float) (0);
			bounds.x = (float) (bounds.x + style.padding.x);
			bounds.y = (float) (bounds.y + style.padding.y);
			bounds.h = (float) ((bounds.h) < (2*style.padding.y) ? (2*style.padding.y) : (bounds.h));
			bounds.w =
				(float)
					((bounds.w) < (2*style.padding.x + style.cursor_size.x) ? (2*style.padding.x + style.cursor_size.x) : (bounds.w));
			bounds.w -= (float) (2*style.padding.x);
			bounds.h -= (float) (2*style.padding.y);
			if ((style.show_buttons) != 0)
			{
				uint ws = 0;
				nk_rect button = new nk_rect();
				button.y = (float) (bounds.y);
				button.w = (float) (bounds.h);
				button.h = (float) (bounds.h);
				button.x = (float) (bounds.x);
				if (
					(nk_do_button_symbol(ref ws, _out_, (nk_rect) (button), (int) (style.dec_symbol), (int) (NK_BUTTON_DEFAULT),
						style.dec_button, _in_, font)) != 0) val -= (float) (step);
				button.x = (float) ((bounds.x + bounds.w) - button.w);
				if (
					(nk_do_button_symbol(ref ws, _out_, (nk_rect) (button), (int) (style.inc_symbol), (int) (NK_BUTTON_DEFAULT),
						style.inc_button, _in_, font)) != 0) val += (float) (step);
				bounds.x = (float) (bounds.x + button.w + style.spacing.x);
				bounds.w = (float) (bounds.w - (2*button.w + 2*style.spacing.x));
			}

			bounds.x += (float) (style.cursor_size.x*0.5f);
			bounds.w -= (float) (style.cursor_size.x);
			slider_max = (float) ((min) < (max) ? (max) : (min));
			slider_min = (float) ((min) < (max) ? (min) : (max));
			slider_value =
				(float)
					(((val) < (slider_max) ? (val) : (slider_max)) < (slider_min)
						? (slider_min)
						: ((val) < (slider_max) ? (val) : (slider_max)));
			slider_range = (float) (slider_max - slider_min);
			slider_steps = (float) (slider_range/step);
			cursor_offset = (float) ((slider_value - slider_min)/step);
			logical_cursor.h = (float) (bounds.h);
			logical_cursor.w = (float) (bounds.w/slider_steps);
			logical_cursor.x = (float) (bounds.x + (logical_cursor.w*cursor_offset));
			logical_cursor.y = (float) (bounds.y);
			visual_cursor.h = (float) (style.cursor_size.y);
			visual_cursor.w = (float) (style.cursor_size.x);
			visual_cursor.y = (float) ((bounds.y + bounds.h*0.5f) - visual_cursor.h*0.5f);
			visual_cursor.x = (float) (logical_cursor.x - visual_cursor.w*0.5f);
			slider_value =
				(float)
					(nk_slider_behavior(ref state, &logical_cursor, &visual_cursor, _in_, (nk_rect) (bounds), (float) (slider_min),
						(float) (slider_max), (float) (slider_value), (float) (step), (float) (slider_steps)));
			visual_cursor.x = (float) (logical_cursor.x - visual_cursor.w*0.5f);
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_slider(_out_, (uint) (state), style, &bounds, &visual_cursor, (float) (slider_min), (float) (slider_value),
				(float) (slider_max));
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (float) (slider_value);
		}

		public static ulong nk_progress_behavior(ref uint state, nk_input _in_, nk_rect r, nk_rect cursor, ulong max,
			ulong value, int modifiable)
		{
			int left_mouse_down = (int) (0);
			int left_mouse_click_in_cursor = (int) (0);
			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if ((_in_ == null) || (modifiable == 0)) return (ulong) (value);
			left_mouse_down =
				(int) (((_in_) != null) && ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down) != 0) ? 1 : 0);
			left_mouse_click_in_cursor =
				(int)
					(((_in_) != null) &&
					 ((nk_input_has_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (cursor), (int) (nk_true))) != 0)
						? 1
						: 0);
			if ((nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (r))) != 0) state = (uint) (NK_WIDGET_STATE_HOVERED);
			if ((((_in_) != null) && ((left_mouse_down) != 0)) && ((left_mouse_click_in_cursor) != 0))
			{
				if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
				{
					float ratio = (float) (((0) < (_in_.mouse.pos.x - cursor.x) ? (_in_.mouse.pos.x - cursor.x) : (0))/cursor.w);
					value =
						((ulong)
							((((float) (max)*ratio) < ((float) (max)) ? ((float) (max)*ratio) : ((float) (max))) < (0)
								? (0)
								: (((float) (max)*ratio) < ((float) (max)) ? ((float) (max)*ratio) : ((float) (max)))));
					((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.x = (float) (cursor.x + cursor.w/2.0f);
					state |= (uint) (NK_WIDGET_STATE_ACTIVE);
				}
			}

			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (r)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (r))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
			return (ulong) (value);
		}

		public static ulong nk_do_progress(ref uint state, nk_command_buffer _out_, nk_rect bounds, ulong value, ulong max,
			int modifiable, nk_style_progress style, nk_input _in_)
		{
			float prog_scale;
			ulong prog_value;
			nk_rect cursor = new nk_rect();
			if ((_out_ == null) || (style == null)) return (ulong) (0);
			cursor.w =
				(float) ((bounds.w) < (2*style.padding.x + 2*style.border) ? (2*style.padding.x + 2*style.border) : (bounds.w));
			cursor.h =
				(float) ((bounds.h) < (2*style.padding.y + 2*style.border) ? (2*style.padding.y + 2*style.border) : (bounds.h));
			cursor =
				(nk_rect)
					(nk_pad_rect((nk_rect) (bounds),
						(nk_vec2) (nk_vec2_((float) (style.padding.x + style.border), (float) (style.padding.y + style.border)))));
			prog_scale = (float) ((float) (value)/(float) (max));
			prog_value = (ulong) ((value) < (max) ? (value) : (max));
			prog_value =
				(ulong)
					(nk_progress_behavior(ref state, _in_, (nk_rect) (bounds), (nk_rect) (cursor), (ulong) (max), (ulong) (prog_value),
						(int) (modifiable)));
			cursor.w = (float) (cursor.w*prog_scale);
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_progress(_out_, (uint) (state), style, &bounds, &cursor, (ulong) (value), (ulong) (max));
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (ulong) (prog_value);
		}

		public static float nk_scrollbar_behavior(ref uint state, nk_input _in_, int has_scrolling, nk_rect* scroll,
			ref nk_rect cursor, nk_rect* empty0, nk_rect* empty1, float scroll_offset, float target, float scroll_step, int o)
		{
			uint ws = (uint) (0);
			int left_mouse_down;
			int left_mouse_click_in_cursor;
			float scroll_delta;
			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if (_in_ == null) return (float) (scroll_offset);
			left_mouse_down = (int) (((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down);
			left_mouse_click_in_cursor =
				(int) (nk_input_has_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (cursor), (int) (nk_true)));
			if ((nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (*scroll))) != 0) state = (uint) (NK_WIDGET_STATE_HOVERED);
			scroll_delta = (float) (((o) == (NK_VERTICAL)) ? _in_.mouse.scroll_delta.y : _in_.mouse.scroll_delta.x);
			if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
			{
				float pixel;
				float delta;
				state = (uint) (NK_WIDGET_STATE_ACTIVE);
				if ((o) == (NK_VERTICAL))
				{
					float cursor_y;
					pixel = (float) (_in_.mouse.delta.y);
					delta = (float) ((pixel/scroll->h)*target);
					scroll_offset =
						(float)
							(((scroll_offset + delta) < (target - scroll->h) ? (scroll_offset + delta) : (target - scroll->h)) < (0)
								? (0)
								: ((scroll_offset + delta) < (target - scroll->h) ? (scroll_offset + delta) : (target - scroll->h)));
					cursor_y = (float) (scroll->y + ((scroll_offset/target)*scroll->h));
					((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.y = (float) (cursor_y + cursor.h/2.0f);
				}
				else
				{
					float cursor_x;
					pixel = (float) (_in_.mouse.delta.x);
					delta = (float) ((pixel/scroll->w)*target);
					scroll_offset =
						(float)
							(((scroll_offset + delta) < (target - scroll->w) ? (scroll_offset + delta) : (target - scroll->w)) < (0)
								? (0)
								: ((scroll_offset + delta) < (target - scroll->w) ? (scroll_offset + delta) : (target - scroll->w)));
					cursor_x = (float) (scroll->x + ((scroll_offset/target)*scroll->w));
					((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.x = (float) (cursor_x + cursor.w/2.0f);
				}
			}
			else if (((((nk_input_is_key_pressed(_in_, (int) (NK_KEY_SCROLL_UP))) != 0) && ((o) == (NK_VERTICAL))) &&
			          ((has_scrolling) != 0)) ||
			         ((nk_button_behavior(ref ws, (nk_rect) (*empty0), _in_, (int) (NK_BUTTON_DEFAULT))) != 0))
			{
				if ((o) == (NK_VERTICAL))
					scroll_offset = (float) ((0) < (scroll_offset - scroll->h) ? (scroll_offset - scroll->h) : (0));
				else scroll_offset = (float) ((0) < (scroll_offset - scroll->w) ? (scroll_offset - scroll->w) : (0));
			}
			else if (((((nk_input_is_key_pressed(_in_, (int) (NK_KEY_SCROLL_DOWN))) != 0) && ((o) == (NK_VERTICAL))) &&
			          ((has_scrolling) != 0)) ||
			         ((nk_button_behavior(ref ws, (nk_rect) (*empty1), _in_, (int) (NK_BUTTON_DEFAULT))) != 0))
			{
				if ((o) == (NK_VERTICAL))
					scroll_offset =
						(float)
							((scroll_offset + scroll->h) < (target - scroll->h) ? (scroll_offset + scroll->h) : (target - scroll->h));
				else
					scroll_offset =
						(float)
							((scroll_offset + scroll->w) < (target - scroll->w) ? (scroll_offset + scroll->w) : (target - scroll->w));
			}
			else if ((has_scrolling) != 0)
			{
				if ((((scroll_delta) < (0)) || ((scroll_delta) > (0))))
				{
					scroll_offset = (float) (scroll_offset + scroll_step*(-scroll_delta));
					if ((o) == (NK_VERTICAL))
						scroll_offset =
							(float)
								(((scroll_offset) < (target - scroll->h) ? (scroll_offset) : (target - scroll->h)) < (0)
									? (0)
									: ((scroll_offset) < (target - scroll->h) ? (scroll_offset) : (target - scroll->h)));
					else
						scroll_offset =
							(float)
								(((scroll_offset) < (target - scroll->w) ? (scroll_offset) : (target - scroll->w)) < (0)
									? (0)
									: ((scroll_offset) < (target - scroll->w) ? (scroll_offset) : (target - scroll->w)));
				}
				else if ((nk_input_is_key_pressed(_in_, (int) (NK_KEY_SCROLL_START))) != 0)
				{
					if ((o) == (NK_VERTICAL)) scroll_offset = (float) (0);
				}
				else if ((nk_input_is_key_pressed(_in_, (int) (NK_KEY_SCROLL_END))) != 0)
				{
					if ((o) == (NK_VERTICAL)) scroll_offset = (float) (target - scroll->h);
				}
			}

			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (*scroll)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (*scroll))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
			return (float) (scroll_offset);
		}

		public static float nk_do_scrollbarv(ref uint state, nk_command_buffer _out_, nk_rect scroll, int has_scrolling,
			float offset, float target, float step, float button_pixel_inc, nk_style_scrollbar style, nk_input _in_,
			nk_user_font font)
		{
			nk_rect empty_north = new nk_rect();
			nk_rect empty_south = new nk_rect();
			nk_rect cursor = new nk_rect();
			float scroll_step;
			float scroll_offset;
			float scroll_off;
			float scroll_ratio;
			if ((_out_ == null) || (style == null)) return (float) (0);
			scroll.w = (float) ((scroll.w) < (1) ? (1) : (scroll.w));
			scroll.h = (float) ((scroll.h) < (0) ? (0) : (scroll.h));
			if (target <= scroll.h) return (float) (0);
			if ((style.show_buttons) != 0)
			{
				uint ws = 0;
				float scroll_h;
				nk_rect button = new nk_rect();
				button.x = (float) (scroll.x);
				button.w = (float) (scroll.w);
				button.h = (float) (scroll.w);
				scroll_h = (float) ((scroll.h - 2*button.h) < (0) ? (0) : (scroll.h - 2*button.h));
				scroll_step = (float) ((step) < (button_pixel_inc) ? (step) : (button_pixel_inc));
				button.y = (float) (scroll.y);
				if (
					(nk_do_button_symbol(ref ws, _out_, (nk_rect) (button), (int) (style.dec_symbol), (int) (NK_BUTTON_REPEATER),
						style.dec_button, _in_, font)) != 0) offset = (float) (offset - scroll_step);
				button.y = (float) (scroll.y + scroll.h - button.h);
				if (
					(nk_do_button_symbol(ref ws, _out_, (nk_rect) (button), (int) (style.inc_symbol), (int) (NK_BUTTON_REPEATER),
						style.inc_button, _in_, font)) != 0) offset = (float) (offset + scroll_step);
				scroll.y = (float) (scroll.y + button.h);
				scroll.h = (float) (scroll_h);
			}

			scroll_step = (float) ((step) < (scroll.h) ? (step) : (scroll.h));
			scroll_offset =
				(float)
					(((offset) < (target - scroll.h) ? (offset) : (target - scroll.h)) < (0)
						? (0)
						: ((offset) < (target - scroll.h) ? (offset) : (target - scroll.h)));
			scroll_ratio = (float) (scroll.h/target);
			scroll_off = (float) (scroll_offset/target);
			cursor.h =
				(float)
					(((scroll_ratio*scroll.h) - (2*style.border + 2*style.padding.y)) < (0)
						? (0)
						: ((scroll_ratio*scroll.h) - (2*style.border + 2*style.padding.y)));
			cursor.y = (float) (scroll.y + (scroll_off*scroll.h) + style.border + style.padding.y);
			cursor.w = (float) (scroll.w - (2*style.border + 2*style.padding.x));
			cursor.x = (float) (scroll.x + style.border + style.padding.x);
			empty_north.x = (float) (scroll.x);
			empty_north.y = (float) (scroll.y);
			empty_north.w = (float) (scroll.w);
			empty_north.h = (float) ((cursor.y - scroll.y) < (0) ? (0) : (cursor.y - scroll.y));
			empty_south.x = (float) (scroll.x);
			empty_south.y = (float) (cursor.y + cursor.h);
			empty_south.w = (float) (scroll.w);
			empty_south.h =
				(float)
					(((scroll.y + scroll.h) - (cursor.y + cursor.h)) < (0) ? (0) : ((scroll.y + scroll.h) - (cursor.y + cursor.h)));
			scroll_offset =
				(float)
					(nk_scrollbar_behavior(ref state, _in_, (int) (has_scrolling), &scroll, ref cursor, &empty_north, &empty_south,
						(float) (scroll_offset), (float) (target), (float) (scroll_step), (int) (NK_VERTICAL)));
			scroll_off = (float) (scroll_offset/target);
			cursor.y = (float) (scroll.y + (scroll_off*scroll.h) + style.border_cursor + style.padding.y);
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_scrollbar(_out_, (uint) (state), style, &scroll, &cursor);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (float) (scroll_offset);
		}

		public static float nk_do_scrollbarh(ref uint state, nk_command_buffer _out_, nk_rect scroll, int has_scrolling,
			float offset, float target, float step, float button_pixel_inc, nk_style_scrollbar style, nk_input _in_,
			nk_user_font font)
		{
			nk_rect cursor = new nk_rect();
			nk_rect empty_west = new nk_rect();
			nk_rect empty_east = new nk_rect();
			float scroll_step;
			float scroll_offset;
			float scroll_off;
			float scroll_ratio;
			if ((_out_ == null) || (style == null)) return (float) (0);
			scroll.h = (float) ((scroll.h) < (1) ? (1) : (scroll.h));
			scroll.w = (float) ((scroll.w) < (2*scroll.h) ? (2*scroll.h) : (scroll.w));
			if (target <= scroll.w) return (float) (0);
			if ((style.show_buttons) != 0)
			{
				uint ws = 0;
				float scroll_w;
				nk_rect button = new nk_rect();
				button.y = (float) (scroll.y);
				button.w = (float) (scroll.h);
				button.h = (float) (scroll.h);
				scroll_w = (float) (scroll.w - 2*button.w);
				scroll_step = (float) ((step) < (button_pixel_inc) ? (step) : (button_pixel_inc));
				button.x = (float) (scroll.x);
				if (
					(nk_do_button_symbol(ref ws, _out_, (nk_rect) (button), (int) (style.dec_symbol), (int) (NK_BUTTON_REPEATER),
						style.dec_button, _in_, font)) != 0) offset = (float) (offset - scroll_step);
				button.x = (float) (scroll.x + scroll.w - button.w);
				if (
					(nk_do_button_symbol(ref ws, _out_, (nk_rect) (button), (int) (style.inc_symbol), (int) (NK_BUTTON_REPEATER),
						style.inc_button, _in_, font)) != 0) offset = (float) (offset + scroll_step);
				scroll.x = (float) (scroll.x + button.w);
				scroll.w = (float) (scroll_w);
			}

			scroll_step = (float) ((step) < (scroll.w) ? (step) : (scroll.w));
			scroll_offset =
				(float)
					(((offset) < (target - scroll.w) ? (offset) : (target - scroll.w)) < (0)
						? (0)
						: ((offset) < (target - scroll.w) ? (offset) : (target - scroll.w)));
			scroll_ratio = (float) (scroll.w/target);
			scroll_off = (float) (scroll_offset/target);
			cursor.w = (float) ((scroll_ratio*scroll.w) - (2*style.border + 2*style.padding.x));
			cursor.x = (float) (scroll.x + (scroll_off*scroll.w) + style.border + style.padding.x);
			cursor.h = (float) (scroll.h - (2*style.border + 2*style.padding.y));
			cursor.y = (float) (scroll.y + style.border + style.padding.y);
			empty_west.x = (float) (scroll.x);
			empty_west.y = (float) (scroll.y);
			empty_west.w = (float) (cursor.x - scroll.x);
			empty_west.h = (float) (scroll.h);
			empty_east.x = (float) (cursor.x + cursor.w);
			empty_east.y = (float) (scroll.y);
			empty_east.w = (float) ((scroll.x + scroll.w) - (cursor.x + cursor.w));
			empty_east.h = (float) (scroll.h);
			scroll_offset =
				(float)
					(nk_scrollbar_behavior(ref state, _in_, (int) (has_scrolling), &scroll, ref cursor, &empty_west, &empty_east,
						(float) (scroll_offset), (float) (target), (float) (scroll_step), (int) (NK_HORIZONTAL)));
			scroll_off = (float) (scroll_offset/target);
			cursor.x = (float) (scroll.x + (scroll_off*scroll.w));
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_scrollbar(_out_, (uint) (state), style, &scroll, &cursor);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			return (float) (scroll_offset);
		}

		public static uint nk_do_edit(ref uint state, nk_command_buffer _out_, nk_rect bounds, uint flags,
			NkPluginFilter filter, nk_text_edit edit, nk_style_edit style, nk_input _in_, nk_user_font font)
		{
			nk_rect area = new nk_rect();
			uint ret = (uint) (0);
			float row_height;
			sbyte prev_state = (sbyte) (0);
			sbyte is_hovered = (sbyte) (0);
			sbyte select_all = (sbyte) (0);
			sbyte cursor_follow = (sbyte) (0);
			nk_rect old_clip = new nk_rect();
			nk_rect clip = new nk_rect();
			if (((_out_ == null)) || (style == null)) return (uint) (ret);
			area.x = (float) (bounds.x + style.padding.x + style.border);
			area.y = (float) (bounds.y + style.padding.y + style.border);
			area.w = (float) (bounds.w - (2.0f*style.padding.x + 2*style.border));
			area.h = (float) (bounds.h - (2.0f*style.padding.y + 2*style.border));
			if ((flags & NK_EDIT_MULTILINE) != 0)
				area.w = (float) ((0) < (area.w - style.scrollbar_size.x) ? (area.w - style.scrollbar_size.x) : (0));
			row_height = (float) ((flags & NK_EDIT_MULTILINE) != 0 ? font.height + style.row_padding : area.h);
			old_clip = (nk_rect) (_out_.clip);
			nk_unify(ref clip, ref old_clip, (float) (area.x), (float) (area.y), (float) (area.x + area.w),
				(float) (area.y + area.h));
			prev_state = ((sbyte) (edit.active));
			is_hovered = ((sbyte) (nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (bounds))));
			if ((((_in_) != null) && ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked) != 0)) &&
			    ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down) != 0))
			{
				edit.active =
					(byte)
						((((bounds.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (bounds.x + bounds.w))) &&
						 (((bounds.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (bounds.y + bounds.h)))
							? 1
							: 0);
			}

			if ((prev_state == 0) && ((edit.active) != 0))
			{
				int type = (int) ((flags & NK_EDIT_MULTILINE) != 0 ? NK_TEXT_EDIT_MULTI_LINE : NK_TEXT_EDIT_SINGLE_LINE);
				nk_textedit_clear_state(edit, (int) (type), filter);
				if ((flags & NK_EDIT_AUTO_SELECT) != 0) select_all = (sbyte) (nk_true);
				if ((flags & NK_EDIT_GOTO_END_ON_ACTIVATE) != 0)
				{
					edit.cursor = (int) (edit._string_.len);
					_in_ = null;
				}
			}
			else if (edit.active == 0) edit.mode = (byte) (NK_TEXT_EDIT_MODE_VIEW);
			if ((flags & NK_EDIT_READ_ONLY) != 0) edit.mode = (byte) (NK_TEXT_EDIT_MODE_VIEW);
			else if ((flags & NK_EDIT_ALWAYS_INSERT_MODE) != 0) edit.mode = (byte) (NK_TEXT_EDIT_MODE_INSERT);
			ret = (uint) ((edit.active) != 0 ? NK_EDIT_ACTIVE : NK_EDIT_INACTIVE);
			if (prev_state != edit.active) ret |= (uint) ((edit.active) != 0 ? NK_EDIT_ACTIVATED : NK_EDIT_DEACTIVATED);
			if (((edit.active) != 0) && ((_in_) != null))
			{
				int shift_mod = (int) (_in_.keyboard.keys[NK_KEY_SHIFT].down);
				float mouse_x = (float) ((_in_.mouse.pos.x - area.x) + edit.scrollbar.x);
				float mouse_y = (float) ((_in_.mouse.pos.y - area.y) + edit.scrollbar.y);
				is_hovered = ((sbyte) (nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (area))));
				if ((select_all) != 0)
				{
					nk_textedit_select_all(edit);
				}
				else if ((((is_hovered) != 0) && ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down) != 0)) &&
				         ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked) != 0))
				{
					nk_textedit_click(edit, (float) (mouse_x), (float) (mouse_y), font, (float) (row_height));
				}
				else if ((((is_hovered) != 0) && ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down) != 0)) &&
				         ((_in_.mouse.delta.x != 0.0f) || (_in_.mouse.delta.y != 0.0f)))
				{
					nk_textedit_drag(edit, (float) (mouse_x), (float) (mouse_y), font, (float) (row_height));
					cursor_follow = (sbyte) (nk_true);
				}
				else if ((((is_hovered) != 0) && ((_in_.mouse.buttons[NK_BUTTON_RIGHT].clicked) != 0)) &&
				         ((_in_.mouse.buttons[NK_BUTTON_RIGHT].down) != 0))
				{
					nk_textedit_key(edit, (int) (NK_KEY_TEXT_WORD_LEFT), (int) (nk_false), font, (float) (row_height));
					nk_textedit_key(edit, (int) (NK_KEY_TEXT_WORD_RIGHT), (int) (nk_true), font, (float) (row_height));
					cursor_follow = (sbyte) (nk_true);
				}
				{
					int i;
					int old_mode = (int) (edit.mode);
					for (i = (int) (0); (i) < (NK_KEY_MAX); ++i)
					{
						if (((i) == (NK_KEY_ENTER)) || ((i) == (NK_KEY_TAB))) continue;
						if ((nk_input_is_key_pressed(_in_, (int) (i))) != 0)
						{
							nk_textedit_key(edit, (int) (i), (int) (shift_mod), font, (float) (row_height));
							cursor_follow = (sbyte) (nk_true);
						}
					}
					if (old_mode != edit.mode)
					{
						_in_.keyboard.text_len = (int) (0);
					}
				}
				edit.filter = filter;
				if ((_in_.keyboard.text_len) != 0)
				{
					nk_textedit_text(edit, _in_.keyboard.text, (int) (_in_.keyboard.text_len));
					cursor_follow = (sbyte) (nk_true);
					_in_.keyboard.text_len = (int) (0);
				}
				if ((nk_input_is_key_pressed(_in_, (int) (NK_KEY_ENTER))) != 0)
				{
					cursor_follow = (sbyte) (nk_true);
					if (((flags & NK_EDIT_CTRL_ENTER_NEWLINE) != 0) && ((shift_mod) != 0)) nk_textedit_text(edit, "\n", (int) (1));
					else if ((flags & NK_EDIT_SIG_ENTER) != 0) ret |= (uint) (NK_EDIT_COMMITED);
					else nk_textedit_text(edit, "\n", (int) (1));
				}
				{
					int copy = (int) (nk_input_is_key_pressed(_in_, (int) (NK_KEY_COPY)));
					int cut = (int) (nk_input_is_key_pressed(_in_, (int) (NK_KEY_CUT)));
					if ((((copy) != 0) || ((cut) != 0)) && ((flags & NK_EDIT_CLIPBOARD) != 0))
					{
						char* text;
						int b = (int) (edit.select_start);
						int e = (int) (edit.select_end);
						int begin = (int) ((b) < (e) ? (b) : (e));
						int end = (int) ((b) < (e) ? (e) : (b));
						fixed (char* str2 = edit._string_.str)
						{
							text = str2 + begin;
							if ((edit.clip.copy) != null) edit.clip.copy((nk_handle) (edit.clip.userdata), text, (int) (end - begin));
							if (((cut) != 0) && ((flags & NK_EDIT_READ_ONLY) == 0))
							{
								nk_textedit_cut(edit);
								cursor_follow = (sbyte) (nk_true);
							}
						}
					}
				}
				{
					int paste = (int) (nk_input_is_key_pressed(_in_, (int) (NK_KEY_PASTE)));
					if ((((paste) != 0) && ((flags & NK_EDIT_CLIPBOARD) != 0)) && ((edit.clip.paste) != null))
					{
						edit.clip.paste((nk_handle) (edit.clip.userdata), edit);
						cursor_follow = (sbyte) (nk_true);
					}
				}
				{
					int tab = (int) (nk_input_is_key_pressed(_in_, (int) (NK_KEY_TAB)));
					if (((tab) != 0) && ((flags & NK_EDIT_ALLOW_TAB) != 0))
					{
						nk_textedit_text(edit, "    ", (int) (4));
						cursor_follow = (sbyte) (nk_true);
					}
				}
			}

			if ((edit.active) != 0) state = (uint) (NK_WIDGET_STATE_ACTIVE);
			else if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if ((is_hovered) != 0) state |= (uint) (NK_WIDGET_STATE_HOVERED);
			{
				fixed (char* text = edit._string_.str)
				{
					int len = (int) (edit._string_.len);
					{
						nk_style_item background;
						if ((state & NK_WIDGET_STATE_ACTIVED) != 0) background = style.active;
						else if ((state & NK_WIDGET_STATE_HOVER) != 0) background = style.hover;
						else background = style.normal;
						if ((background.type) == (NK_STYLE_ITEM_COLOR))
						{
							nk_stroke_rect(_out_, (nk_rect) (bounds), (float) (style.rounding), (float) (style.border),
								(nk_color) (style.border_color));
							nk_fill_rect(_out_, (nk_rect) (bounds), (float) (style.rounding), (nk_color) (background.data.color));
						}
						else nk_draw_image(_out_, (nk_rect) (bounds), background.data.image, (nk_color) (nk_white));
					}
					area.w = (float) ((0) < (area.w - style.cursor_size) ? (area.w - style.cursor_size) : (0));
					if ((edit.active) != 0)
					{
						int total_lines = (int) (1);
						nk_vec2 text_size = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
						char* cursor_ptr = null;
						char* select_begin_ptr = null;
						char* select_end_ptr = null;
						nk_vec2 cursor_pos = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
						nk_vec2 selection_offset_start = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
						nk_vec2 selection_offset_end = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
						int selection_begin = (int) ((edit.select_start) < (edit.select_end) ? (edit.select_start) : (edit.select_end));
						int selection_end = (int) ((edit.select_start) < (edit.select_end) ? (edit.select_end) : (edit.select_start));
						float line_width = (float) (0.0f);
						if (((text) != null) && ((len) != 0))
						{
							float glyph_width;
							int glyph_len = (int) (0);
							char unicode = (char) 0;
							int text_len = (int) (0);
							int glyphs = (int) (0);
							int row_begin = (int) (0);
							glyph_len = (int) (nk_utf_decode(text, &unicode, (int) (len)));
							glyph_width = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), text, (int) (glyph_len)));
							line_width = (float) (0);
							while (((text_len) < (len)) && ((glyph_len) != 0))
							{
								if ((cursor_ptr == null) && ((glyphs) == (edit.cursor)))
								{
									int glyph_offset;
									nk_vec2 out_offset = new nk_vec2();
									nk_vec2 row_size = new nk_vec2();
									char* remaining;
									cursor_pos.y = (float) ((float) (total_lines - 1)*row_height);
									row_size =
										(nk_vec2)
											(nk_text_calculate_text_bounds(font, text + row_begin, (int) (text_len - row_begin), (float) (row_height),
												&remaining, &out_offset, &glyph_offset, (int) (NK_STOP_ON_NEW_LINE)));
									cursor_pos.x = (float) (row_size.x);
									cursor_ptr = text + text_len;
								}
								if (((select_begin_ptr == null) && (edit.select_start != edit.select_end)) && ((glyphs) == (selection_begin)))
								{
									int glyph_offset;
									nk_vec2 out_offset = new nk_vec2();
									nk_vec2 row_size = new nk_vec2();
									char* remaining;
									selection_offset_start.y = (float) ((float) ((total_lines - 1) < (0) ? (0) : (total_lines - 1))*row_height);
									row_size =
										(nk_vec2)
											(nk_text_calculate_text_bounds(font, text + row_begin, (int) (text_len - row_begin), (float) (row_height),
												&remaining, &out_offset, &glyph_offset, (int) (NK_STOP_ON_NEW_LINE)));
									selection_offset_start.x = (float) (row_size.x);
									select_begin_ptr = text + text_len;
								}
								if (((select_end_ptr == null) && (edit.select_start != edit.select_end)) && ((glyphs) == (selection_end)))
								{
									int glyph_offset;
									nk_vec2 out_offset = new nk_vec2();
									nk_vec2 row_size = new nk_vec2();
									char* remaining;
									selection_offset_end.y = (float) ((float) (total_lines - 1)*row_height);
									row_size =
										(nk_vec2)
											(nk_text_calculate_text_bounds(font, text + row_begin, (int) (text_len - row_begin), (float) (row_height),
												&remaining, &out_offset, &glyph_offset, (int) (NK_STOP_ON_NEW_LINE)));
									selection_offset_end.x = (float) (row_size.x);
									select_end_ptr = text + text_len;
								}
								if ((unicode) == ('\n'))
								{
									text_size.x = (float) ((text_size.x) < (line_width) ? (line_width) : (text_size.x));
									total_lines++;
									line_width = (float) (0);
									text_len++;
									glyphs++;
									row_begin = (int) (text_len);
									glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (len - text_len)));
									glyph_width =
										(float) (font.width((nk_handle) (font.userdata), (float) (font.height), text + text_len, (int) (glyph_len)));
									continue;
								}
								glyphs++;
								text_len += (int) (glyph_len);
								line_width += (float) (glyph_width);
								glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (len - text_len)));
								glyph_width =
									(float) (font.width((nk_handle) (font.userdata), (float) (font.height), text + text_len, (int) (glyph_len)));
								continue;
							}
							text_size.y = (float) ((float) (total_lines)*row_height);
							if ((cursor_ptr == null) && ((edit.cursor) == (edit._string_.len)))
							{
								cursor_pos.x = (float) (line_width);
								cursor_pos.y = (float) (text_size.y - row_height);
							}
						}
						{
							if ((cursor_follow) != 0)
							{
								if ((flags & NK_EDIT_NO_HORIZONTAL_SCROLL) == 0)
								{
									float scroll_increment = (float) (area.w*0.25f);
									if ((cursor_pos.x) < (edit.scrollbar.x))
										edit.scrollbar.x =
											((float) ((int) ((0.0f) < (cursor_pos.x - scroll_increment) ? (cursor_pos.x - scroll_increment) : (0.0f))));
									if ((cursor_pos.x) >= (edit.scrollbar.x + area.w))
										edit.scrollbar.x = ((float) ((int) ((0.0f) < (cursor_pos.x) ? (cursor_pos.x) : (0.0f))));
								}
								else edit.scrollbar.x = (float) (0);
								if ((flags & NK_EDIT_MULTILINE) != 0)
								{
									if ((cursor_pos.y) < (edit.scrollbar.y))
										edit.scrollbar.y = (float) ((0.0f) < (cursor_pos.y - row_height) ? (cursor_pos.y - row_height) : (0.0f));
									if ((cursor_pos.y) >= (edit.scrollbar.y + area.h)) edit.scrollbar.y = (float) (edit.scrollbar.y + row_height);
								}
								else edit.scrollbar.y = (float) (0);
							}
							if ((flags & NK_EDIT_MULTILINE) != 0)
							{
								uint ws = 0;
								nk_rect scroll = new nk_rect();
								float scroll_target;
								float scroll_offset;
								float scroll_step;
								float scroll_inc;
								scroll = (nk_rect) (area);
								scroll.x = (float) ((bounds.x + bounds.w - style.border) - style.scrollbar_size.x);
								scroll.w = (float) (style.scrollbar_size.x);
								scroll_offset = (float) (edit.scrollbar.y);
								scroll_step = (float) (scroll.h*0.10f);
								scroll_inc = (float) (scroll.h*0.01f);
								scroll_target = (float) (text_size.y);
								edit.scrollbar.y =
									(float)
										(nk_do_scrollbarv(ref ws, _out_, (nk_rect) (scroll), (int) (0), (float) (scroll_offset),
											(float) (scroll_target), (float) (scroll_step), (float) (scroll_inc), style.scrollbar, _in_, font));
							}
						}
						{
							nk_color background_color = new nk_color();
							nk_color text_color = new nk_color();
							nk_color sel_background_color = new nk_color();
							nk_color sel_text_color = new nk_color();
							nk_color cursor_color = new nk_color();
							nk_color cursor_text_color = new nk_color();
							nk_style_item background;
							nk_push_scissor(_out_, (nk_rect) (clip));
							if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
							{
								background = style.active;
								text_color = (nk_color) (style.text_active);
								sel_text_color = (nk_color) (style.selected_text_hover);
								sel_background_color = (nk_color) (style.selected_hover);
								cursor_color = (nk_color) (style.cursor_hover);
								cursor_text_color = (nk_color) (style.cursor_text_hover);
							}
							else if ((state & NK_WIDGET_STATE_HOVER) != 0)
							{
								background = style.hover;
								text_color = (nk_color) (style.text_hover);
								sel_text_color = (nk_color) (style.selected_text_hover);
								sel_background_color = (nk_color) (style.selected_hover);
								cursor_text_color = (nk_color) (style.cursor_text_hover);
								cursor_color = (nk_color) (style.cursor_hover);
							}
							else
							{
								background = style.normal;
								text_color = (nk_color) (style.text_normal);
								sel_text_color = (nk_color) (style.selected_text_normal);
								sel_background_color = (nk_color) (style.selected_normal);
								cursor_color = (nk_color) (style.cursor_normal);
								cursor_text_color = (nk_color) (style.cursor_text_normal);
							}
							if ((background.type) == (NK_STYLE_ITEM_IMAGE))
								background_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
							else background_color = (nk_color) (background.data.color);
							if ((edit.select_start) == (edit.select_end))
							{
								fixed (char* begin = edit._string_.str)
								{
									int l = (int) (edit._string_.len);
									nk_edit_draw_text(_out_, style, (float) (area.x - edit.scrollbar.x), (float) (area.y - edit.scrollbar.y),
										(float) (0), begin, (int) (l), (float) (row_height), font, (nk_color) (background_color),
										(nk_color) (text_color), (int) (nk_false));
								}
							}
							else
							{
								if ((edit.select_start != edit.select_end) && ((selection_begin) > (0)))
								{
									fixed (char* begin = edit._string_.str)
									{
										nk_edit_draw_text(_out_, style, (float) (area.x - edit.scrollbar.x), (float) (area.y - edit.scrollbar.y),
											(float) (0), begin, (int) (select_begin_ptr - begin), (float) (row_height), font,
											(nk_color) (background_color),
											(nk_color) (text_color), (int) (nk_false));
									}
								}
								if (edit.select_start != edit.select_end)
								{
									if (select_end_ptr == null)
									{
										char* begin = text;
										select_end_ptr = begin + edit._string_.len;
									}
									nk_edit_draw_text(_out_, style, (float) (area.x - edit.scrollbar.x),
										(float) (area.y + selection_offset_start.y - edit.scrollbar.y), (float) (selection_offset_start.x),
										select_begin_ptr, (int) (select_end_ptr - select_begin_ptr), (float) (row_height), font,
										(nk_color) (sel_background_color), (nk_color) (sel_text_color), (int) (nk_true));
								}
								if (((edit.select_start != edit.select_end) && ((selection_end) < (edit._string_.len))))
								{
									char* begin = select_end_ptr;
									char* end = text + edit._string_.len;
									nk_edit_draw_text(_out_, style, (float) (area.x - edit.scrollbar.x),
										(float) (area.y + selection_offset_end.y - edit.scrollbar.y), (float) (selection_offset_end.x), begin,
										(int) (end - begin), (float) (row_height), font, (nk_color) (background_color), (nk_color) (text_color),
										(int) (nk_true));
								}
							}
							if ((edit.select_start) == (edit.select_end))
							{
								if (((edit.cursor) >= (edit._string_.len)) || (((cursor_ptr) != null) && ((*cursor_ptr) == ('\n'))))
								{
									nk_rect cursor = new nk_rect();
									cursor.w = (float) (style.cursor_size);
									cursor.h = (float) (font.height);
									cursor.x = (float) (area.x + cursor_pos.x - edit.scrollbar.x);
									cursor.y = (float) (area.y + cursor_pos.y + row_height/2.0f - cursor.h/2.0f);
									cursor.y -= (float) (edit.scrollbar.y);
									nk_fill_rect(_out_, (nk_rect) (cursor), (float) (0), (nk_color) (cursor_color));
								}
								else
								{
									int glyph_len;
									nk_rect label = new nk_rect();
									nk_text txt = new nk_text();
									char unicode;
									glyph_len = (int) (nk_utf_decode(cursor_ptr, &unicode, (int) (4)));
									label.x = (float) (area.x + cursor_pos.x - edit.scrollbar.x);
									label.y = (float) (area.y + cursor_pos.y - edit.scrollbar.y);
									label.w =
										(float) (font.width((nk_handle) (font.userdata), (float) (font.height), cursor_ptr, (int) (glyph_len)));
									label.h = (float) (row_height);
									txt.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
									txt.background = (nk_color) (cursor_color);
									txt.text = (nk_color) (cursor_text_color);
									nk_fill_rect(_out_, (nk_rect) (label), (float) (0), (nk_color) (cursor_color));
									nk_widget_text(_out_, (nk_rect) (label), cursor_ptr, (int) (glyph_len), &txt, (uint) (NK_TEXT_LEFT), font);
								}
							}
						}
					}
					else
					{
						int l = (int) (edit._string_.len);
						fixed (char* begin = edit._string_.str)
						{
							nk_style_item background;
							nk_color background_color = new nk_color();
							nk_color text_color = new nk_color();
							nk_push_scissor(_out_, (nk_rect) (clip));
							if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
							{
								background = style.active;
								text_color = (nk_color) (style.text_active);
							}
							else if ((state & NK_WIDGET_STATE_HOVER) != 0)
							{
								background = style.hover;
								text_color = (nk_color) (style.text_hover);
							}
							else
							{
								background = style.normal;
								text_color = (nk_color) (style.text_normal);
							}
							if ((background.type) == (NK_STYLE_ITEM_IMAGE))
								background_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
							else background_color = (nk_color) (background.data.color);
							nk_edit_draw_text(_out_, style, (float) (area.x - edit.scrollbar.x), (float) (area.y - edit.scrollbar.y),
								(float) (0), begin, (int) (l), (float) (row_height), font, (nk_color) (background_color),
								(nk_color) (text_color),
								(int) (nk_false));
						}
					}
					nk_push_scissor(_out_, (nk_rect) (old_clip));
				}
			}

			return (uint) (ret);
		}

		public static void nk_drag_behavior(ref uint state, nk_input _in_, nk_rect drag, nk_property_variant* variant,
			float inc_per_pixel)
		{
			int left_mouse_down =
				(int) (((_in_) != null) && ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down) != 0) ? 1 : 0);
			int left_mouse_click_in_cursor =
				(int)
					(((_in_) != null) &&
					 ((nk_input_has_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (drag), (int) (nk_true))) != 0)
						? 1
						: 0);
			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if ((nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (drag))) != 0) state = (uint) (NK_WIDGET_STATE_HOVERED);
			if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
			{
				float delta;
				float pixels;
				pixels = (float) (_in_.mouse.delta.x);
				delta = (float) (pixels*inc_per_pixel);
				switch (variant->kind)
				{
					default:
						break;
					case NK_PROPERTY_INT:
						variant->value.i = (int) (variant->value.i + (int) (delta));
						variant->value.i =
							(int)
								(((variant->value.i) < (variant->max_value.i) ? (variant->value.i) : (variant->max_value.i)) <
								 (variant->min_value.i)
									? (variant->min_value.i)
									: ((variant->value.i) < (variant->max_value.i) ? (variant->value.i) : (variant->max_value.i)));
						break;
					case NK_PROPERTY_FLOAT:
						variant->value.f = (float) (variant->value.f + delta);
						variant->value.f =
							(float)
								(((variant->value.f) < (variant->max_value.f) ? (variant->value.f) : (variant->max_value.f)) <
								 (variant->min_value.f)
									? (variant->min_value.f)
									: ((variant->value.f) < (variant->max_value.f) ? (variant->value.f) : (variant->max_value.f)));
						break;
					case NK_PROPERTY_DOUBLE:
						variant->value.d = (double) (variant->value.d + (double) (delta));
						variant->value.d =
							(double)
								(((variant->value.d) < (variant->max_value.d) ? (variant->value.d) : (variant->max_value.d)) <
								 (variant->min_value.d)
									? (variant->min_value.d)
									: ((variant->value.d) < (variant->max_value.d) ? (variant->value.d) : (variant->max_value.d)));
						break;
				}
				state = (uint) (NK_WIDGET_STATE_ACTIVE);
			}

			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (drag)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (drag))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
		}

		public static void nk_property_behavior(ref uint ws, nk_input _in_, nk_rect property, nk_rect label, nk_rect edit,
			nk_rect empty, ref int state, nk_property_variant* variant, float inc_per_pixel)
		{
			if (((_in_) != null) && ((state) == (NK_PROPERTY_DEFAULT)))
			{
				if ((nk_button_behavior(ref ws, (nk_rect) (edit), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
					state = (int) (NK_PROPERTY_EDIT);
				else if ((nk_input_is_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (label), (int) (nk_true))) != 0)
					state = (int) (NK_PROPERTY_DRAG);
				else if ((nk_input_is_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (empty), (int) (nk_true))) != 0)
					state = (int) (NK_PROPERTY_DRAG);
			}

			if ((state) == (NK_PROPERTY_DRAG))
			{
				nk_drag_behavior(ref ws, _in_, (nk_rect) (property), variant, (float) (inc_per_pixel));
				if ((ws & NK_WIDGET_STATE_ACTIVED) == 0) state = (int) (NK_PROPERTY_DEFAULT);
			}

		}

		public static void nk_do_property(ref uint ws, nk_command_buffer _out_, nk_rect property, char* name,
			nk_property_variant* variant, float inc_per_pixel, char* buffer, ref int len, ref int state, ref int cursor,
			ref int select_begin, ref int select_end, nk_style_property style, int filter, nk_input _in_, nk_user_font font,
			nk_text_edit text_edit, int behavior)
		{
			NkPluginFilter[] filters = new NkPluginFilter[2];
			filters[0] = nk_filter_decimal;
			filters[1] = nk_filter_float;

			int active;
			int old;
			int num_len = 0;
			int name_len;
			char* _string_ = stackalloc char[64];
			float size;
			char* dst = null;
			bool length_is_len = false;
			int length;
			nk_rect left = new nk_rect();
			nk_rect right = new nk_rect();
			nk_rect label = new nk_rect();
			nk_rect edit = new nk_rect();
			nk_rect empty = new nk_rect();
			left.h = (float) (font.height/2);
			left.w = (float) (left.h);
			left.x = (float) (property.x + style.border + style.padding.x);
			left.y = (float) (property.y + style.border + property.h/2.0f - left.h/2);
			name_len = (int) (nk_strlen(name));
			size = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), name, (int) (name_len)));
			label.x = (float) (left.x + left.w + style.padding.x);
			label.w = (float) (size + 2*style.padding.x);
			label.y = (float) (property.y + style.border + style.padding.y);
			label.h = (float) (property.h - (2*style.border + 2*style.padding.y));
			right.y = (float) (left.y);
			right.w = (float) (left.w);
			right.h = (float) (left.h);
			right.x = (float) (property.x + property.w - (right.w + style.padding.x));
			if ((state) == (NK_PROPERTY_EDIT))
			{
				size = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), buffer, (int) (len)));
				size += (float) (style.edit.cursor_size);
				length = len;
				length_is_len = true;
				dst = buffer;
			}
			else
			{
				switch (variant->kind)
				{
					default:
						break;
					case NK_PROPERTY_INT:
						nk_itoa(_string_, (int) (variant->value.i));
						num_len = (int) (nk_strlen(_string_));
						break;
					case NK_PROPERTY_FLOAT:
						nk_dtoa(_string_, (double) (variant->value.f));
						num_len = (int) (nk_string_float_limit(_string_, (int) (2)));
						break;
					case NK_PROPERTY_DOUBLE:
						nk_dtoa(_string_, (double) (variant->value.d));
						num_len = (int) (nk_string_float_limit(_string_, (int) (2)));
						break;
				}
				size = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), _string_, (int) (num_len)));
				dst = _string_;
				length = num_len;
			}

			edit.w = (float) (size + 2*style.padding.x);
			edit.w = (float) ((edit.w) < (right.x - (label.x + label.w)) ? (edit.w) : (right.x - (label.x + label.w)));
			edit.x = (float) (right.x - (edit.w + style.padding.x));
			edit.y = (float) (property.y + style.border);
			edit.h = (float) (property.h - (2*style.border));
			empty.w = (float) (edit.x - (label.x + label.w));
			empty.x = (float) (label.x + label.w);
			empty.y = (float) (property.y);
			empty.h = (float) (property.h);
			old = (int) ((state) == (NK_PROPERTY_EDIT) ? 1 : 0);
			nk_property_behavior(ref ws, _in_, (nk_rect) (property), (nk_rect) (label), (nk_rect) (edit), (nk_rect) (empty),
				ref state, variant, (float) (inc_per_pixel));
			if ((style.draw_begin) != null) style.draw_begin(_out_, (nk_handle) (style.userdata));
			nk_draw_property(_out_, style, &property, &label, (uint) (ws), name, (int) (name_len), font);
			if ((style.draw_end) != null) style.draw_end(_out_, (nk_handle) (style.userdata));
			if (
				(nk_do_button_symbol(ref ws, _out_, (nk_rect) (left), (int) (style.sym_left), (int) (behavior), style.dec_button,
					_in_, font)) != 0)
			{
				switch (variant->kind)
				{
					default:
						break;
					case NK_PROPERTY_INT:
						variant->value.i =
							(int)
								(((variant->value.i - variant->step.i) < (variant->max_value.i)
									? (variant->value.i - variant->step.i)
									: (variant->max_value.i)) < (variant->min_value.i)
									? (variant->min_value.i)
									: ((variant->value.i - variant->step.i) < (variant->max_value.i)
										? (variant->value.i - variant->step.i)
										: (variant->max_value.i)));
						break;
					case NK_PROPERTY_FLOAT:
						variant->value.f =
							(float)
								(((variant->value.f - variant->step.f) < (variant->max_value.f)
									? (variant->value.f - variant->step.f)
									: (variant->max_value.f)) < (variant->min_value.f)
									? (variant->min_value.f)
									: ((variant->value.f - variant->step.f) < (variant->max_value.f)
										? (variant->value.f - variant->step.f)
										: (variant->max_value.f)));
						break;
					case NK_PROPERTY_DOUBLE:
						variant->value.d =
							(double)
								(((variant->value.d - variant->step.d) < (variant->max_value.d)
									? (variant->value.d - variant->step.d)
									: (variant->max_value.d)) < (variant->min_value.d)
									? (variant->min_value.d)
									: ((variant->value.d - variant->step.d) < (variant->max_value.d)
										? (variant->value.d - variant->step.d)
										: (variant->max_value.d)));
						break;
				}
			}

			if (
				(nk_do_button_symbol(ref ws, _out_, (nk_rect) (right), (int) (style.sym_right), (int) (behavior), style.inc_button,
					_in_, font)) != 0)
			{
				switch (variant->kind)
				{
					default:
						break;
					case NK_PROPERTY_INT:
						variant->value.i =
							(int)
								(((variant->value.i + variant->step.i) < (variant->max_value.i)
									? (variant->value.i + variant->step.i)
									: (variant->max_value.i)) < (variant->min_value.i)
									? (variant->min_value.i)
									: ((variant->value.i + variant->step.i) < (variant->max_value.i)
										? (variant->value.i + variant->step.i)
										: (variant->max_value.i)));
						break;
					case NK_PROPERTY_FLOAT:
						variant->value.f =
							(float)
								(((variant->value.f + variant->step.f) < (variant->max_value.f)
									? (variant->value.f + variant->step.f)
									: (variant->max_value.f)) < (variant->min_value.f)
									? (variant->min_value.f)
									: ((variant->value.f + variant->step.f) < (variant->max_value.f)
										? (variant->value.f + variant->step.f)
										: (variant->max_value.f)));
						break;
					case NK_PROPERTY_DOUBLE:
						variant->value.d =
							(double)
								(((variant->value.d + variant->step.d) < (variant->max_value.d)
									? (variant->value.d + variant->step.d)
									: (variant->max_value.d)) < (variant->min_value.d)
									? (variant->min_value.d)
									: ((variant->value.d + variant->step.d) < (variant->max_value.d)
										? (variant->value.d + variant->step.d)
										: (variant->max_value.d)));
						break;
				}
			}

			if ((old != NK_PROPERTY_EDIT) && ((state) == (NK_PROPERTY_EDIT)))
			{
				nk_memcopy(buffer, dst, (ulong) (length));
				cursor = (int) (nk_utf_len(buffer, (int) (length)));
				len = (int) (length);
				length = len;
				dst = buffer;
				active = (int) (0);
			}
			else active = (int) ((state) == (NK_PROPERTY_EDIT) ? 1 : 0);
			nk_textedit_clear_state(text_edit, (int) (NK_TEXT_EDIT_SINGLE_LINE), filters[filter]);
			text_edit.active = ((byte) (active));
			text_edit._string_.str = text_edit._string_.str.Substring(0, length);
			text_edit.cursor =
				(int) (((cursor) < (length) ? (cursor) : (length)) < (0) ? (0) : ((cursor) < (length) ? (cursor) : (length)));
			text_edit.select_start =
				(int)
					(((select_begin) < (length) ? (select_begin) : (length)) < (0)
						? (0)
						: ((select_begin) < (length) ? (select_begin) : (length)));
			text_edit.select_end =
				(int)
					(((select_end) < (length) ? (select_end) : (length)) < (0)
						? (0)
						: ((select_end) < (length) ? (select_end) : (length)));
			text_edit.mode = (byte) (NK_TEXT_EDIT_MODE_INSERT);
			nk_do_edit(ref ws, _out_, (nk_rect) (edit), (uint) (NK_EDIT_FIELD | NK_EDIT_AUTO_SELECT), filters[filter], text_edit,
				style.edit, ((state) == (NK_PROPERTY_EDIT)) ? _in_ : null, font);
			if (length_is_len)
			{
				len = (int) (text_edit._string_.len);
			}
			cursor = (int) (text_edit.cursor);
			select_begin = (int) (text_edit.select_start);
			select_end = (int) (text_edit.select_end);
			if (((text_edit.active) != 0) && ((nk_input_is_key_pressed(_in_, (int) (NK_KEY_ENTER))) != 0))
				text_edit.active = (byte) (nk_false);
			if (((active) != 0) && (text_edit.active == 0))
			{
				state = (int) (NK_PROPERTY_DEFAULT);
				buffer[len] = ('\0');
				switch (variant->kind)
				{
					default:
						break;
					case NK_PROPERTY_INT:
						variant->value.i = (int) (nk_strtoi(buffer, null));
						variant->value.i =
							(int)
								(((variant->value.i) < (variant->max_value.i) ? (variant->value.i) : (variant->max_value.i)) <
								 (variant->min_value.i)
									? (variant->min_value.i)
									: ((variant->value.i) < (variant->max_value.i) ? (variant->value.i) : (variant->max_value.i)));
						break;
					case NK_PROPERTY_FLOAT:
						nk_string_float_limit(buffer, (int) (2));
						variant->value.f = (float) (nk_strtof(buffer, null));
						variant->value.f =
							(float)
								(((variant->value.f) < (variant->max_value.f) ? (variant->value.f) : (variant->max_value.f)) <
								 (variant->min_value.f)
									? (variant->min_value.f)
									: ((variant->value.f) < (variant->max_value.f) ? (variant->value.f) : (variant->max_value.f)));
						break;
					case NK_PROPERTY_DOUBLE:
						nk_string_float_limit(buffer, (int) (2));
						variant->value.d = (double) (nk_strtod(buffer, null));
						variant->value.d =
							(double)
								(((variant->value.d) < (variant->max_value.d) ? (variant->value.d) : (variant->max_value.d)) <
								 (variant->min_value.d)
									? (variant->min_value.d)
									: ((variant->value.d) < (variant->max_value.d) ? (variant->value.d) : (variant->max_value.d)));
						break;
				}
			}

		}

		public static int nk_color_picker_behavior(ref uint state, nk_rect* bounds, nk_rect* matrix, nk_rect* hue_bar,
			nk_rect* alpha_bar, nk_colorf* color, nk_input _in_)
		{
			float* hsva = stackalloc float[4];
			int value_changed = (int) (0);
			int hsv_changed = (int) (0);
			nk_colorf_hsva_fv(hsva, (nk_colorf) (*color));
			if ((nk_button_behavior(ref state, (nk_rect) (*matrix), _in_, (int) (NK_BUTTON_REPEATER))) != 0)
			{
				hsva[1] =
					(float)
						((0) <
						 ((1.0f) < ((_in_.mouse.pos.x - matrix->x)/(matrix->w - 1))
							 ? (1.0f)
							 : ((_in_.mouse.pos.x - matrix->x)/(matrix->w - 1)))
							? ((1.0f) < ((_in_.mouse.pos.x - matrix->x)/(matrix->w - 1))
								? (1.0f)
								: ((_in_.mouse.pos.x - matrix->x)/(matrix->w - 1)))
							: (0));
				hsva[2] =
					(float)
						(1.0f -
						 ((0) <
						  ((1.0f) < ((_in_.mouse.pos.y - matrix->y)/(matrix->h - 1))
							  ? (1.0f)
							  : ((_in_.mouse.pos.y - matrix->y)/(matrix->h - 1)))
							 ? ((1.0f) < ((_in_.mouse.pos.y - matrix->y)/(matrix->h - 1))
								 ? (1.0f)
								 : ((_in_.mouse.pos.y - matrix->y)/(matrix->h - 1)))
							 : (0)));
				value_changed = (int) (hsv_changed = (int) (1));
			}

			if ((nk_button_behavior(ref state, (nk_rect) (*hue_bar), _in_, (int) (NK_BUTTON_REPEATER))) != 0)
			{
				hsva[0] =
					(float)
						((0) <
						 ((1.0f) < ((_in_.mouse.pos.y - hue_bar->y)/(hue_bar->h - 1))
							 ? (1.0f)
							 : ((_in_.mouse.pos.y - hue_bar->y)/(hue_bar->h - 1)))
							? ((1.0f) < ((_in_.mouse.pos.y - hue_bar->y)/(hue_bar->h - 1))
								? (1.0f)
								: ((_in_.mouse.pos.y - hue_bar->y)/(hue_bar->h - 1)))
							: (0));
				value_changed = (int) (hsv_changed = (int) (1));
			}

			if ((alpha_bar) != null)
			{
				if ((nk_button_behavior(ref state, (nk_rect) (*alpha_bar), _in_, (int) (NK_BUTTON_REPEATER))) != 0)
				{
					hsva[3] =
						(float)
							(1.0f -
							 ((0) <
							  ((1.0f) < ((_in_.mouse.pos.y - alpha_bar->y)/(alpha_bar->h - 1))
								  ? (1.0f)
								  : ((_in_.mouse.pos.y - alpha_bar->y)/(alpha_bar->h - 1)))
								 ? ((1.0f) < ((_in_.mouse.pos.y - alpha_bar->y)/(alpha_bar->h - 1))
									 ? (1.0f)
									 : ((_in_.mouse.pos.y - alpha_bar->y)/(alpha_bar->h - 1)))
								 : (0)));
					value_changed = (int) (1);
				}
			}

			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if ((hsv_changed) != 0)
			{
				*color = (nk_colorf) (nk_hsva_colorfv(hsva));
				state = (uint) (NK_WIDGET_STATE_ACTIVE);
			}

			if ((value_changed) != 0)
			{
				color->a = (float) (hsva[3]);
				state = (uint) (NK_WIDGET_STATE_ACTIVE);
			}

			if ((nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (*bounds))) != 0) state = (uint) (NK_WIDGET_STATE_HOVERED);
			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (*bounds)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (*bounds))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
			return (int) (value_changed);
		}

		public static int nk_do_color_picker(ref uint state, nk_command_buffer _out_, nk_colorf* col, int fmt, nk_rect bounds,
			nk_vec2 padding, nk_input _in_, nk_user_font font)
		{
			int ret = (int) (0);
			nk_rect matrix = new nk_rect();
			nk_rect hue_bar = new nk_rect();
			nk_rect alpha_bar = new nk_rect();
			float bar_w;
			if ((((_out_ == null) || (col == null))) || (font == null)) return (int) (ret);
			bar_w = (float) (font.height);
			bounds.x += (float) (padding.x);
			bounds.y += (float) (padding.x);
			bounds.w -= (float) (2*padding.x);
			bounds.h -= (float) (2*padding.y);
			matrix.x = (float) (bounds.x);
			matrix.y = (float) (bounds.y);
			matrix.h = (float) (bounds.h);
			matrix.w = (float) (bounds.w - (3*padding.x + 2*bar_w));
			hue_bar.w = (float) (bar_w);
			hue_bar.y = (float) (bounds.y);
			hue_bar.h = (float) (matrix.h);
			hue_bar.x = (float) (matrix.x + matrix.w + padding.x);
			alpha_bar.x = (float) (hue_bar.x + hue_bar.w + padding.x);
			alpha_bar.y = (float) (bounds.y);
			alpha_bar.w = (float) (bar_w);
			alpha_bar.h = (float) (matrix.h);
			ret =
				(int)
					(nk_color_picker_behavior(ref state, &bounds, &matrix, &hue_bar, ((fmt) == (NK_RGBA)) ? &alpha_bar : null, col,
						_in_));
			nk_draw_color_picker(_out_, &matrix, &hue_bar, ((fmt) == (NK_RGBA)) ? &alpha_bar : null, (nk_colorf) (*col));
			return (int) (ret);
		}

		public static nk_style_item nk_style_item_hide()
		{
			nk_style_item i = new nk_style_item();
			i.type = (int) (NK_STYLE_ITEM_COLOR);
			i.data.color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			return (nk_style_item) (i);
		}

		public static int nk_panel_has_header(uint flags, char* title)
		{
			int active = (int) (0);
			active = (int) (flags & (NK_WINDOW_CLOSABLE | NK_WINDOW_MINIMIZABLE));
			active = (int) (((active) != 0) || ((flags & NK_WINDOW_TITLE) != 0) ? 1 : 0);
			active = (int) ((((active) != 0) && ((flags & NK_WINDOW_HIDDEN) == 0)) && ((title) != null) ? 1 : 0);
			return (int) (active);
		}

		public static int nk_panel_is_sub(int type)
		{
			return (int) ((type & NK_PANEL_SET_SUB) != 0 ? 1 : 0);
		}

		public static int nk_panel_is_nonblock(int type)
		{
			return (int) ((type & NK_PANEL_SET_NONBLOCK) != 0 ? 1 : 0);
		}

		public static nk_property_variant nk_property_variant_int(int value, int min_value, int max_value, int step)
		{
			nk_property_variant result = new nk_property_variant();
			result.kind = (int) (NK_PROPERTY_INT);
			result.value.i = (int) (value);
			result.min_value.i = (int) (min_value);
			result.max_value.i = (int) (max_value);
			result.step.i = (int) (step);
			return (nk_property_variant) (result);
		}

		public static nk_property_variant nk_property_variant_float(float value, float min_value, float max_value, float step)
		{
			nk_property_variant result = new nk_property_variant();
			result.kind = (int) (NK_PROPERTY_FLOAT);
			result.value.f = (float) (value);
			result.min_value.f = (float) (min_value);
			result.max_value.f = (float) (max_value);
			result.step.f = (float) (step);
			return (nk_property_variant) (result);
		}

		public static nk_property_variant nk_property_variant_double(double value, double min_value, double max_value,
			double step)
		{
			nk_property_variant result = new nk_property_variant();
			result.kind = (int) (NK_PROPERTY_DOUBLE);
			result.value.d = (double) (value);
			result.min_value.d = (double) (min_value);
			result.max_value.d = (double) (max_value);
			result.step.d = (double) (step);
			return (nk_property_variant) (result);
		}
	}
}