using System.Runtime.InteropServices;

namespace KlearUI
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NkColor
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public static NkColor nk_rgba(int r, int g, int b, int a)
        {
            NkColor ret = new NkColor();
            ret.r = ((byte)(((r) < (255) ? (r) : (255)) < (0) ? (0) : ((r) < (255) ? (r) : (255))));
            ret.g = ((byte)(((g) < (255) ? (g) : (255)) < (0) ? (0) : ((g) < (255) ? (g) : (255))));
            ret.b = ((byte)(((b) < (255) ? (b) : (255)) < (0) ? (0) : ((b) < (255) ? (b) : (255))));
            ret.a = ((byte)(((a) < (255) ? (a) : (255)) < (0) ? (0) : ((a) < (255) ? (a) : (255))));
            return (NkColor)(ret);
        }

        public static NkColor nk_rgb_hex(sbyte* rgb)
        {
            NkColor col = new NkColor();
            sbyte* c = rgb;
            if ((*c) == ('#')) c++;
            col.r = ((byte)(Nk.nk_parse_hex(c, (int)(2))));
            col.g = ((byte)(Nk.nk_parse_hex(c + 2, (int)(2))));
            col.b = ((byte)(Nk.nk_parse_hex(c + 4, (int)(2))));
            col.a = (byte)(255);
            return (NkColor)(col);
        }

        public static NkColor nk_rgba_hex(sbyte* rgb)
        {
            NkColor col = new NkColor();
            sbyte* c = rgb;
            if ((*c) == ('#')) c++;
            col.r = ((byte)(Nk.nk_parse_hex(c, (int)(2))));
            col.g = ((byte)(Nk.nk_parse_hex(c + 2, (int)(2))));
            col.b = ((byte)(Nk.nk_parse_hex(c + 4, (int)(2))));
            col.a = ((byte)(Nk.nk_parse_hex(c + 6, (int)(2))));
            return (NkColor)(col);
        }

        public static void nk_color_hex_rgba(char* output, NkColor col)
        {
            output[0] = ((char)(((col.r & 0xF0) >> 4) <= 9 ? '0' + ((col.r & 0xF0) >> 4) : 'A' - 10 + ((col.r & 0xF0) >> 4)));
            output[1] = ((char)((col.r & 0x0F) <= 9 ? '0' + (col.r & 0x0F) : 'A' - 10 + (col.r & 0x0F)));
            output[2] = ((char)(((col.g & 0xF0) >> 4) <= 9 ? '0' + ((col.g & 0xF0) >> 4) : 'A' - 10 + ((col.g & 0xF0) >> 4)));
            output[3] = ((char)((col.g & 0x0F) <= 9 ? '0' + (col.g & 0x0F) : 'A' - 10 + (col.g & 0x0F)));
            output[4] = ((char)(((col.b & 0xF0) >> 4) <= 9 ? '0' + ((col.b & 0xF0) >> 4) : 'A' - 10 + ((col.b & 0xF0) >> 4)));
            output[5] = ((char)((col.b & 0x0F) <= 9 ? '0' + (col.b & 0x0F) : 'A' - 10 + (col.b & 0x0F)));
            output[6] = ((char)(((col.a & 0xF0) >> 4) <= 9 ? '0' + ((col.a & 0xF0) >> 4) : 'A' - 10 + ((col.a & 0xF0) >> 4)));
            output[7] = ((char)((col.a & 0x0F) <= 9 ? '0' + (col.a & 0x0F) : 'A' - 10 + (col.a & 0x0F)));
            output[8] = ('\0');
        }

        public static void nk_color_hex_rgb(char* output, NkColor col)
        {
            output[0] = ((char)(((col.r & 0xF0) >> 4) <= 9 ? '0' + ((col.r & 0xF0) >> 4) : 'A' - 10 + ((col.r & 0xF0) >> 4)));
            output[1] = ((char)((col.r & 0x0F) <= 9 ? '0' + (col.r & 0x0F) : 'A' - 10 + (col.r & 0x0F)));
            output[2] = ((char)(((col.g & 0xF0) >> 4) <= 9 ? '0' + ((col.g & 0xF0) >> 4) : 'A' - 10 + ((col.g & 0xF0) >> 4)));
            output[3] = ((char)((col.g & 0x0F) <= 9 ? '0' + (col.g & 0x0F) : 'A' - 10 + (col.g & 0x0F)));
            output[4] = ((char)(((col.b & 0xF0) >> 4) <= 9 ? '0' + ((col.b & 0xF0) >> 4) : 'A' - 10 + ((col.b & 0xF0) >> 4)));
            output[5] = ((char)((col.b & 0x0F) <= 9 ? '0' + (col.b & 0x0F) : 'A' - 10 + (col.b & 0x0F)));
            output[6] = ('\0');
        }

        public static NkColor nk_rgba_iv(int* c)
        {
            return (NkColor)(nk_rgba((int)(c[0]), (int)(c[1]), (int)(c[2]), (int)(c[3])));
        }

        public static NkColor nk_rgba_bv(byte* c)
        {
            return (NkColor)(nk_rgba((int)(c[0]), (int)(c[1]), (int)(c[2]), (int)(c[3])));
        }

        public static NkColor nk_rgb(int r, int g, int b)
        {
            NkColor ret = new NkColor();
            ret.r = ((byte)(((r) < (255) ? (r) : (255)) < (0) ? (0) : ((r) < (255) ? (r) : (255))));
            ret.g = ((byte)(((g) < (255) ? (g) : (255)) < (0) ? (0) : ((g) < (255) ? (g) : (255))));
            ret.b = ((byte)(((b) < (255) ? (b) : (255)) < (0) ? (0) : ((b) < (255) ? (b) : (255))));
            ret.a = ((byte)(255));
            return (NkColor)(ret);
        }

        public static NkColor nk_rgb_iv(int* c)
        {
            return (NkColor)(nk_rgb((int)(c[0]), (int)(c[1]), (int)(c[2])));
        }

        public static NkColor nk_rgb_bv(byte* c)
        {
            return (NkColor)(nk_rgb((int)(c[0]), (int)(c[1]), (int)(c[2])));
        }

        public static NkColor nk_rgba_u32(uint _in_)
        {
            NkColor ret = new NkColor();
            ret.r = (byte)(_in_ & 0xFF);
            ret.g = (byte)((_in_ >> 8) & 0xFF);
            ret.b = (byte)((_in_ >> 16) & 0xFF);
            ret.a = ((byte)((_in_ >> 24) & 0xFF));
            return (NkColor)(ret);
        }

        public static NkColor nk_rgba_f(float r, float g, float b, float a)
        {
            NkColor ret = new NkColor();
            ret.r = ((byte)(((0) < ((1.0f) < (r) ? (1.0f) : (r)) ? ((1.0f) < (r) ? (1.0f) : (r)) : (0)) * 255.0f));
            ret.g = ((byte)(((0) < ((1.0f) < (g) ? (1.0f) : (g)) ? ((1.0f) < (g) ? (1.0f) : (g)) : (0)) * 255.0f));
            ret.b = ((byte)(((0) < ((1.0f) < (b) ? (1.0f) : (b)) ? ((1.0f) < (b) ? (1.0f) : (b)) : (0)) * 255.0f));
            ret.a = ((byte)(((0) < ((1.0f) < (a) ? (1.0f) : (a)) ? ((1.0f) < (a) ? (1.0f) : (a)) : (0)) * 255.0f));
            return (NkColor)(ret);
        }

        public static NkColor nk_rgba_fv(float* c)
        {
            return (NkColor)(nk_rgba_f((float)(c[0]), (float)(c[1]), (float)(c[2]), (float)(c[3])));
        }

        public static NkColor nk_rgb_f(float r, float g, float b)
        {
            NkColor ret = new NkColor();
            ret.r = ((byte)(((0) < ((1.0f) < (r) ? (1.0f) : (r)) ? ((1.0f) < (r) ? (1.0f) : (r)) : (0)) * 255.0f));
            ret.g = ((byte)(((0) < ((1.0f) < (g) ? (1.0f) : (g)) ? ((1.0f) < (g) ? (1.0f) : (g)) : (0)) * 255.0f));
            ret.b = ((byte)(((0) < ((1.0f) < (b) ? (1.0f) : (b)) ? ((1.0f) < (b) ? (1.0f) : (b)) : (0)) * 255.0f));
            ret.a = (byte)(255);
            return (NkColor)(ret);
        }

        public static NkColor nk_rgb_fv(float* c)
        {
            return (NkColor)(nk_rgb_f((float)(c[0]), (float)(c[1]), (float)(c[2])));
        }

        public static NkColor nk_hsv(int h, int s, int v)
        {
            return (NkColor)(nk_hsva((int)(h), (int)(s), (int)(v), (int)(255)));
        }

        public static NkColor nk_hsv_iv(int* c)
        {
            return (NkColor)(nk_hsv((int)(c[0]), (int)(c[1]), (int)(c[2])));
        }

        public static NkColor nk_hsv_bv(byte* c)
        {
            return (NkColor)(nk_hsv((int)(c[0]), (int)(c[1]), (int)(c[2])));
        }

        public static NkColor nk_hsv_f(float h, float s, float v)
        {
            return (NkColor)(nk_hsva_f((float)(h), (float)(s), (float)(v), (float)(1.0f)));
        }

        public static NkColor nk_hsv_fv(float* c)
        {
            return (NkColor)(nk_hsv_f((float)(c[0]), (float)(c[1]), (float)(c[2])));
        }

        public static NkColor nk_hsva(int h, int s, int v, int a)
        {
            float hf = (float)(((float)(((h) < (255) ? (h) : (255)) < (0) ? (0) : ((h) < (255) ? (h) : (255)))) / 255.0f);
            float sf = (float)(((float)(((s) < (255) ? (s) : (255)) < (0) ? (0) : ((s) < (255) ? (s) : (255)))) / 255.0f);
            float vf = (float)(((float)(((v) < (255) ? (v) : (255)) < (0) ? (0) : ((v) < (255) ? (v) : (255)))) / 255.0f);
            float af = (float)(((float)(((a) < (255) ? (a) : (255)) < (0) ? (0) : ((a) < (255) ? (a) : (255)))) / 255.0f);
            return (NkColor)(nk_hsva_f((float)(hf), (float)(sf), (float)(vf), (float)(af)));
        }

        public static NkColor nk_hsva_iv(int* c)
        {
            return (NkColor)(nk_hsva((int)(c[0]), (int)(c[1]), (int)(c[2]), (int)(c[3])));
        }

        public static NkColor nk_hsva_bv(byte* c)
        {
            return (NkColor)(nk_hsva((int)(c[0]), (int)(c[1]), (int)(c[2]), (int)(c[3])));
        }

        public static NkColorF nk_hsva_colorf(float h, float s, float v, float a)
        {
            int i;
            float p;
            float q;
            float t;
            float f;
            NkColorF _out_ = new NkColorF();
            if (s <= 0.0f)
            {
                _out_.r = (float)(v);
                _out_.g = (float)(v);
                _out_.b = (float)(v);
                _out_.a = (float)(a);
                return (NkColorF)(_out_);
            }

            h = (float)(h / (60.0f / 360.0f));
            i = ((int)(h));
            f = (float)(h - (float)(i));
            p = (float)(v * (1.0f - s));
            q = (float)(v * (1.0f - (s * f)));
            t = (float)(v * (1.0f - s * (1.0f - f)));
            switch (i)
            {
                case 0:
                default:
                    _out_.r = (float)(v);
                    _out_.g = (float)(t);
                    _out_.b = (float)(p);
                    break;
                case 1:
                    _out_.r = (float)(q);
                    _out_.g = (float)(v);
                    _out_.b = (float)(p);
                    break;
                case 2:
                    _out_.r = (float)(p);
                    _out_.g = (float)(v);
                    _out_.b = (float)(t);
                    break;
                case 3:
                    _out_.r = (float)(p);
                    _out_.g = (float)(q);
                    _out_.b = (float)(v);
                    break;
                case 4:
                    _out_.r = (float)(t);
                    _out_.g = (float)(p);
                    _out_.b = (float)(v);
                    break;
                case 5:
                    _out_.r = (float)(v);
                    _out_.g = (float)(p);
                    _out_.b = (float)(q);
                    break;
            }

            _out_.a = (float)(a);
            return (NkColorF)(_out_);
        }

        public static NkColorF nk_hsva_colorfv(float* c)
        {
            return (NkColorF)(nk_hsva_colorf((float)(c[0]), (float)(c[1]), (float)(c[2]), (float)(c[3])));
        }

        public static NkColor nk_hsva_f(float h, float s, float v, float a)
        {
            NkColorF c = (NkColorF)(nk_hsva_colorf((float)(h), (float)(s), (float)(v), (float)(a)));
            return (NkColor)(nk_rgba_f((float)(c.r), (float)(c.g), (float)(c.b), (float)(c.a)));
        }

        public static NkColor nk_hsva_fv(float* c)
        {
            return (NkColor)(nk_hsva_f((float)(c[0]), (float)(c[1]), (float)(c[2]), (float)(c[3])));
        }

        public static void nk_color_f(float* r, float* g, float* b, float* a, NkColor _in_)
        {
            float s = (float)(1.0f / 255.0f);
            *r = (float)((float)(_in_.r) * s);
            *g = (float)((float)(_in_.g) * s);
            *b = (float)((float)(_in_.b) * s);
            *a = (float)((float)(_in_.a) * s);
        }

        public static void nk_color_fv(float* c, NkColor _in_)
        {
            nk_color_f(&c[0], &c[1], &c[2], &c[3], (NkColor)(_in_));
        }

        public static void nk_color_d(double* r, double* g, double* b, double* a, NkColor _in_)
        {
            double s = (double)(1.0 / 255.0);
            *r = (double)((double)(_in_.r) * s);
            *g = (double)((double)(_in_.g) * s);
            *b = (double)((double)(_in_.b) * s);
            *a = (double)((double)(_in_.a) * s);
        }

        public static void nk_color_dv(double* c, NkColor _in_)
        {
            nk_color_d(&c[0], &c[1], &c[2], &c[3], (NkColor)(_in_));
        }

        public static void nk_color_hsv_f(float* out_h, float* out_s, float* out_v, NkColor _in_)
        {
            float a;
            nk_color_hsva_f(out_h, out_s, out_v, &a, (NkColor)(_in_));
        }

        public static void nk_color_hsv_fv(float* _out_, NkColor _in_)
        {
            float a;
            nk_color_hsva_f(&_out_[0], &_out_[1], &_out_[2], &a, (NkColor)(_in_));
        }

        public static void nk_colorf_hsva_f(float* out_h, float* out_s, float* out_v, float* out_a, NkColorF _in_)
        {
            float chroma;
            float K = (float)(0.0f);
            if ((_in_.g) < (_in_.b))
            {
                float t = (float)(_in_.g);
                _in_.g = (float)(_in_.b);
                _in_.b = (float)(t);
                K = (float)(-1.0f);
            }

            if ((_in_.r) < (_in_.g))
            {
                float t = (float)(_in_.r);
                _in_.r = (float)(_in_.g);
                _in_.g = (float)(t);
                K = (float)(-2.0f / 6.0f - K);
            }

            chroma = (float)(_in_.r - (((_in_.g) < (_in_.b)) ? _in_.g : _in_.b));
            *out_h =
                (float)
                (((K + (_in_.g - _in_.b) / (6.0f * chroma + 1e-20f)) < (0))
                    ? -(K + (_in_.g - _in_.b) / (6.0f * chroma + 1e-20f))
                    : (K + (_in_.g - _in_.b) / (6.0f * chroma + 1e-20f)));
            *out_s = (float)(chroma / (_in_.r + 1e-20f));
            *out_v = (float)(_in_.r);
            *out_a = (float)(_in_.a);
        }

        public static void nk_colorf_hsva_fv(float* hsva, NkColorF _in_)
        {
            nk_colorf_hsva_f(&hsva[0], &hsva[1], &hsva[2], &hsva[3], (NkColorF)(_in_));
        }

        public static void nk_color_hsva_f(float* out_h, float* out_s, float* out_v, float* out_a, NkColor _in_)
        {
            NkColorF col = new NkColorF();
            nk_color_f(&col.r, &col.g, &col.b, &col.a, (NkColor)(_in_));
            nk_colorf_hsva_f(out_h, out_s, out_v, out_a, (NkColorF)(col));
        }

        public static void nk_color_hsva_fv(float* _out_, NkColor _in_)
        {
            nk_color_hsva_f(&_out_[0], &_out_[1], &_out_[2], &_out_[3], (NkColor)(_in_));
        }

        public static void nk_color_hsva_i(int* out_h, int* out_s, int* out_v, int* out_a, NkColor _in_)
        {
            float h;
            float s;
            float v;
            float a;
            nk_color_hsva_f(&h, &s, &v, &a, (NkColor)(_in_));
            *out_h = (int)((byte)(h * 255.0f));
            *out_s = (int)((byte)(s * 255.0f));
            *out_v = (int)((byte)(v * 255.0f));
            *out_a = (int)((byte)(a * 255.0f));
        }

        public static void nk_color_hsva_iv(int* _out_, NkColor _in_)
        {
            nk_color_hsva_i(&_out_[0], &_out_[1], &_out_[2], &_out_[3], (NkColor)(_in_));
        }

        public static void nk_color_hsva_bv(byte* _out_, NkColor _in_)
        {
            int* tmp = stackalloc int[4];
            nk_color_hsva_i(&tmp[0], &tmp[1], &tmp[2], &tmp[3], (NkColor)(_in_));
            _out_[0] = ((byte)(tmp[0]));
            _out_[1] = ((byte)(tmp[1]));
            _out_[2] = ((byte)(tmp[2]));
            _out_[3] = ((byte)(tmp[3]));
        }

        public static void nk_color_hsva_b(byte* h, byte* s, byte* v, byte* a, NkColor _in_)
        {
            int* tmp = stackalloc int[4];
            nk_color_hsva_i(&tmp[0], &tmp[1], &tmp[2], &tmp[3], (NkColor)(_in_));
            *h = ((byte)(tmp[0]));
            *s = ((byte)(tmp[1]));
            *v = ((byte)(tmp[2]));
            *a = ((byte)(tmp[3]));
        }

        public static void nk_color_hsv_i(int* out_h, int* out_s, int* out_v, NkColor _in_)
        {
            int a;
            nk_color_hsva_i(out_h, out_s, out_v, &a, (NkColor)(_in_));
        }

        public static void nk_color_hsv_b(byte* out_h, byte* out_s, byte* out_v, NkColor _in_)
        {
            int* tmp = stackalloc int[4];
            nk_color_hsva_i(&tmp[0], &tmp[1], &tmp[2], &tmp[3], (NkColor)(_in_));
            *out_h = ((byte)(tmp[0]));
            *out_s = ((byte)(tmp[1]));
            *out_v = ((byte)(tmp[2]));
        }

        public static void nk_color_hsv_iv(int* _out_, NkColor _in_)
        {
            nk_color_hsv_i(&_out_[0], &_out_[1], &_out_[2], (NkColor)(_in_));
        }

        public static void nk_color_hsv_bv(byte* _out_, NkColor _in_)
        {
            int* tmp = stackalloc int[4];
            nk_color_hsv_i(&tmp[0], &tmp[1], &tmp[2], (NkColor)(_in_));
            _out_[0] = ((byte)(tmp[0]));
            _out_[1] = ((byte)(tmp[1]));
            _out_[2] = ((byte)(tmp[2]));
        }
    }
}