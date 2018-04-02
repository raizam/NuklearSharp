using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuklearSharp
{
    public unsafe delegate float NkTextWidthDelegate(NkHandle handle, float height, char* text, int length);

    public unsafe delegate void NkQueryFontGlyphDelegate(NkHandle handle,
        float height, NkUserFontGlyph* glyph, char codepoint, char nextCodepoint);

    public delegate void NkCommandCustomCallback(
        NkDrawList list, short x, short y, ushort w, ushort h, NkHandle callbackData);

    public delegate void NkPluginPaste(NkHandle handle, nk_text_edit textEdit);

    public unsafe delegate void NkPluginCopy(NkHandle handle, char* text, int length);

    public delegate void NkDrawNotify(NkCommandBuffer buffer, NkHandle handle);

    public delegate int NkPluginFilter(nk_text_edit textEdit, char unicode);

    public unsafe delegate float NkFloatValueGetter(void* handle, int index);

    public unsafe delegate float NkComboCallback(void* handle, int index, char** item);

    public unsafe delegate int QSortComparer(void* a, void* b);
}
