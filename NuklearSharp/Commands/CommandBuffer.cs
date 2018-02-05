using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class CommandBuffer
	{
		public void PushScissor(Rect r)
		{
			CommandScissor cmd;
			if (this== null) return;
			this.clip.x = (float)(r.x);
			this.clip.y = (float)(r.y);
			this.clip.w = (float)(r.w);
			this.clip.h = (float)(r.h);
			cmd = (CommandScissor)(Push((int)(Nuklear.NK_COMMAND_SCISSOR)));
			if (cmd== null) return;
			cmd.x = ((short)(r.x));
			cmd.y = ((short)(r.y));
			cmd.w = ((ushort)((0) < (r.w)?(r.w):(0)));
			cmd.h = ((ushort)((0) < (r.h)?(r.h):(0)));
		}

		public void StrokeLine(float x0, float y0, float x1, float y1, float line_thickness, Color c)
		{
			CommandLine cmd;
			if ((line_thickness <= 0)) return;
			cmd = (CommandLine)(Push((int)(Nuklear.NK_COMMAND_LINE)));
			if (cmd== null) return;
			cmd.line_thickness = ((ushort)(line_thickness));
			cmd.begin.x = ((short)(x0));
			cmd.begin.y = ((short)(y0));
			cmd.end.x = ((short)(x1));
			cmd.end.y = ((short)(y1));
			cmd.color = (Color)(c);
		}

		public void StrokeCurve(float ax, float ay, float ctrl0x, float ctrl0y, float ctrl1x, float ctrl1y, float bx, float by, float line_thickness, Color col)
		{
			CommandCurve cmd;
			if ((((col.a) == (0))) || (line_thickness <= 0)) return;
			cmd = (CommandCurve)(Push((int)(Nuklear.NK_COMMAND_CURVE)));
			if (cmd== null) return;
			cmd.line_thickness = ((ushort)(line_thickness));
			cmd.begin.x = ((short)(ax));
			cmd.begin.y = ((short)(ay));
			cmd.ctrl_0.x = ((short)(ctrl0x));
			cmd.ctrl_0.y = ((short)(ctrl0y));
			cmd.ctrl_1.x = ((short)(ctrl1x));
			cmd.ctrl_1.y = ((short)(ctrl1y));
			cmd.end.x = ((short)(bx));
			cmd.end.y = ((short)(by));
			cmd.color = (Color)(col);
		}

		public void StrokeRect(Rect rect, float rounding, float line_thickness, Color c)
		{
			CommandRect cmd;
			if ((((((c.a) == (0))) || ((rect.w) == (0))) || ((rect.h) == (0))) || (line_thickness <= 0)) return;
			if ((this.use_clipping) != 0) {
if (!(!(((((this.clip.x) > (rect.x + rect.w)) || ((this.clip.x + this.clip.w) < (rect.x))) || ((this.clip.y) > (rect.y + rect.h))) || ((this.clip.y + this.clip.h) < (rect.y))))) return;}

			cmd = (CommandRect)(Push((int)(Nuklear.NK_COMMAND_RECT)));
			if (cmd== null) return;
			cmd.rounding = ((ushort)(rounding));
			cmd.line_thickness = ((ushort)(line_thickness));
			cmd.x = ((short)(rect.x));
			cmd.y = ((short)(rect.y));
			cmd.w = ((ushort)((0) < (rect.w)?(rect.w):(0)));
			cmd.h = ((ushort)((0) < (rect.h)?(rect.h):(0)));
			cmd.color = (Color)(c);
		}

		public void FillRect(Rect rect, float rounding, Color c)
		{
			CommandRectFilled cmd;
			if (((((c.a) == (0))) || ((rect.w) == (0))) || ((rect.h) == (0))) return;
			if ((this.use_clipping) != 0) {
if (!(!(((((this.clip.x) > (rect.x + rect.w)) || ((this.clip.x + this.clip.w) < (rect.x))) || ((this.clip.y) > (rect.y + rect.h))) || ((this.clip.y + this.clip.h) < (rect.y))))) return;}

			cmd = (CommandRectFilled)(Push((int)(Nuklear.NK_COMMAND_RECT_FILLED)));
			if (cmd== null) return;
			cmd.rounding = ((ushort)(rounding));
			cmd.x = ((short)(rect.x));
			cmd.y = ((short)(rect.y));
			cmd.w = ((ushort)((0) < (rect.w)?(rect.w):(0)));
			cmd.h = ((ushort)((0) < (rect.h)?(rect.h):(0)));
			cmd.color = (Color)(c);
		}

		public void FillRectMultiColor(Rect rect, Color left, Color top, Color right, Color bottom)
		{
			CommandRectMultiColor cmd;
			if ((((rect.w) == (0))) || ((rect.h) == (0))) return;
			if ((this.use_clipping) != 0) {
if (!(!(((((this.clip.x) > (rect.x + rect.w)) || ((this.clip.x + this.clip.w) < (rect.x))) || ((this.clip.y) > (rect.y + rect.h))) || ((this.clip.y + this.clip.h) < (rect.y))))) return;}

			cmd = (CommandRectMultiColor)(Push((int)(Nuklear.NK_COMMAND_RECT_MULTI_COLOR)));
			if (cmd== null) return;
			cmd.x = ((short)(rect.x));
			cmd.y = ((short)(rect.y));
			cmd.w = ((ushort)((0) < (rect.w)?(rect.w):(0)));
			cmd.h = ((ushort)((0) < (rect.h)?(rect.h):(0)));
			cmd.left = (Color)(left);
			cmd.top = (Color)(top);
			cmd.right = (Color)(right);
			cmd.bottom = (Color)(bottom);
		}

		public void StrokeCircle(Rect r, float line_thickness, Color c)
		{
			CommandCircle cmd;
			if (((((r.w) == (0))) || ((r.h) == (0))) || (line_thickness <= 0)) return;
			if ((this.use_clipping) != 0) {
if (!(!(((((this.clip.x) > (r.x + r.w)) || ((this.clip.x + this.clip.w) < (r.x))) || ((this.clip.y) > (r.y + r.h))) || ((this.clip.y + this.clip.h) < (r.y))))) return;}

			cmd = (CommandCircle)(Push((int)(Nuklear.NK_COMMAND_CIRCLE)));
			if (cmd== null) return;
			cmd.line_thickness = ((ushort)(line_thickness));
			cmd.x = ((short)(r.x));
			cmd.y = ((short)(r.y));
			cmd.w = ((ushort)((r.w) < (0)?(0):(r.w)));
			cmd.h = ((ushort)((r.h) < (0)?(0):(r.h)));
			cmd.color = (Color)(c);
		}

		public void FillCircle(Rect r, Color c)
		{
			CommandCircleFilled cmd;
			if (((((c.a) == (0))) || ((r.w) == (0))) || ((r.h) == (0))) return;
			if ((this.use_clipping) != 0) {
if (!(!(((((this.clip.x) > (r.x + r.w)) || ((this.clip.x + this.clip.w) < (r.x))) || ((this.clip.y) > (r.y + r.h))) || ((this.clip.y + this.clip.h) < (r.y))))) return;}

			cmd = (CommandCircleFilled)(Push((int)(Nuklear.NK_COMMAND_CIRCLE_FILLED)));
			if (cmd== null) return;
			cmd.x = ((short)(r.x));
			cmd.y = ((short)(r.y));
			cmd.w = ((ushort)((r.w) < (0)?(0):(r.w)));
			cmd.h = ((ushort)((r.h) < (0)?(0):(r.h)));
			cmd.color = (Color)(c);
		}

		public void StrokeArc(float cx, float cy, float radius, float a_min, float a_max, float line_thickness, Color c)
		{
			CommandArc cmd;
			if ((((c.a) == (0))) || (line_thickness <= 0)) return;
			cmd = (CommandArc)(Push((int)(Nuklear.NK_COMMAND_ARC)));
			if (cmd== null) return;
			cmd.line_thickness = ((ushort)(line_thickness));
			cmd.cx = ((short)(cx));
			cmd.cy = ((short)(cy));
			cmd.r = ((ushort)(radius));
			cmd.a[0] = (float)(a_min);
			cmd.a[1] = (float)(a_max);
			cmd.color = (Color)(c);
		}

		public void FillArc(float cx, float cy, float radius, float a_min, float a_max, Color c)
		{
			CommandArcFilled cmd;
			if (((c.a) == (0))) return;
			cmd = (CommandArcFilled)(Push((int)(Nuklear.NK_COMMAND_ARC_FILLED)));
			if (cmd== null) return;
			cmd.cx = ((short)(cx));
			cmd.cy = ((short)(cy));
			cmd.r = ((ushort)(radius));
			cmd.a[0] = (float)(a_min);
			cmd.a[1] = (float)(a_max);
			cmd.color = (Color)(c);
		}

		public void StrokeTriangle(float x0, float y0, float x1, float y1, float x2, float y2, float line_thickness, Color c)
		{
			CommandTriangle cmd;
			if ((((c.a) == (0))) || (line_thickness <= 0)) return;
			if ((this.use_clipping) != 0) {
if (((!((((this.clip.x) <= (x0)) && ((x0) < (this.clip.x + this.clip.w))) && (((this.clip.y) <= (y0)) && ((y0) < (this.clip.y + this.clip.h))))) && (!((((this.clip.x) <= (x1)) && ((x1) < (this.clip.x + this.clip.w))) && (((this.clip.y) <= (y1)) && ((y1) < (this.clip.y + this.clip.h)))))) && (!((((this.clip.x) <= (x2)) && ((x2) < (this.clip.x + this.clip.w))) && (((this.clip.y) <= (y2)) && ((y2) < (this.clip.y + this.clip.h)))))) return;}

			cmd = (CommandTriangle)(Push((int)(Nuklear.NK_COMMAND_TRIANGLE)));
			if (cmd== null) return;
			cmd.line_thickness = ((ushort)(line_thickness));
			cmd.a.x = ((short)(x0));
			cmd.a.y = ((short)(y0));
			cmd.b.x = ((short)(x1));
			cmd.b.y = ((short)(y1));
			cmd.c.x = ((short)(x2));
			cmd.c.y = ((short)(y2));
			cmd.color = (Color)(c);
		}

		public void FillTriangle(float x0, float y0, float x1, float y1, float x2, float y2, Color c)
		{
			CommandTriangleFilled cmd;
			if (((c.a) == (0))) return;
			if (this== null) return;
			if ((this.use_clipping) != 0) {
if (((!((((this.clip.x) <= (x0)) && ((x0) < (this.clip.x + this.clip.w))) && (((this.clip.y) <= (y0)) && ((y0) < (this.clip.y + this.clip.h))))) && (!((((this.clip.x) <= (x1)) && ((x1) < (this.clip.x + this.clip.w))) && (((this.clip.y) <= (y1)) && ((y1) < (this.clip.y + this.clip.h)))))) && (!((((this.clip.x) <= (x2)) && ((x2) < (this.clip.x + this.clip.w))) && (((this.clip.y) <= (y2)) && ((y2) < (this.clip.y + this.clip.h)))))) return;}

			cmd = (CommandTriangleFilled)(Push((int)(Nuklear.NK_COMMAND_TRIANGLE_FILLED)));
			if (cmd== null) return;
			cmd.a.x = ((short)(x0));
			cmd.a.y = ((short)(y0));
			cmd.b.x = ((short)(x1));
			cmd.b.y = ((short)(y1));
			cmd.c.x = ((short)(x2));
			cmd.c.y = ((short)(y2));
			cmd.color = (Color)(c);
		}

		public void DrawImage(Rect r, Image img, Color col)
		{
			CommandImage cmd;
			if (this== null) return;
			if ((this.use_clipping) != 0) {
if ((((this.clip.w) == (0)) || ((this.clip.h) == (0))) || (!(!(((((this.clip.x) > (r.x + r.w)) || ((this.clip.x + this.clip.w) < (r.x))) || ((this.clip.y) > (r.y + r.h))) || ((this.clip.y + this.clip.h) < (r.y)))))) return;}

			cmd = (CommandImage)(Push((int)(Nuklear.NK_COMMAND_IMAGE)));
			if (cmd== null) return;
			cmd.x = ((short)(r.x));
			cmd.y = ((short)(r.y));
			cmd.w = ((ushort)((0) < (r.w)?(r.w):(0)));
			cmd.h = ((ushort)((0) < (r.h)?(r.h):(0)));
			cmd.img = (Image)(img);
			cmd.col = (Color)(col);
		}

		public void PushCustom(Rect r, Nuklear.NkCommandCustomCallback cb, Handle usr)
		{
			CommandCustom cmd;
			if (this== null) return;
			if ((this.use_clipping) != 0) {
if ((((this.clip.w) == (0)) || ((this.clip.h) == (0))) || (!(!(((((this.clip.x) > (r.x + r.w)) || ((this.clip.x + this.clip.w) < (r.x))) || ((this.clip.y) > (r.y + r.h))) || ((this.clip.y + this.clip.h) < (r.y)))))) return;}

			cmd = (CommandCustom)(Push((int)(Nuklear.NK_COMMAND_CUSTOM)));
			if (cmd== null) return;
			cmd.x = ((short)(r.x));
			cmd.y = ((short)(r.y));
			cmd.w = ((ushort)((0) < (r.w)?(r.w):(0)));
			cmd.h = ((ushort)((0) < (r.h)?(r.h):(0)));
			cmd.callback_data = (Handle)(usr);
			cmd.callback = cb;
		}

		public void DrawText(Rect r, char* _string_, int length, UserFont font, Color bg, Color fg)
		{
			float text_width = (float)(0);
			CommandText cmd;
			if ((((_string_== null)) || (length== 0)) || (((bg.a) == (0)) && ((fg.a) == (0)))) return;
			if ((this.use_clipping) != 0) {
if ((((this.clip.w) == (0)) || ((this.clip.h) == (0))) || (!(!(((((this.clip.x) > (r.x + r.w)) || ((this.clip.x + this.clip.w) < (r.x))) || ((this.clip.y) > (r.y + r.h))) || ((this.clip.y + this.clip.h) < (r.y)))))) return;}

			text_width = (float)(font.width((Handle)(font.userdata), (float)(font.height), _string_, (int)(length)));
			if ((text_width) > (r.w)) {
int glyphs = (int)(0);float txt_width = (float)(text_width);length = (int)(font.TextClamp(_string_, (int)(length), (float)(r.w), &glyphs, &txt_width, null, (int)(0)));}

			if (length== 0) return;
			cmd = (CommandText)(Push((int)(Nuklear.NK_COMMAND_TEXT)));
			if (cmd== null) return;
			cmd.x = ((short)(r.x));
			cmd.y = ((short)(r.y));
			cmd.w = ((ushort)(r.w));
			cmd.h = ((ushort)(r.h));
			cmd.background = (Color)(bg);
			cmd.foreground = (Color)(fg);
			cmd.font = font;
			cmd.length = (int)(length);
			cmd.height = (float)(font.height);
			cmd._string_ = new PinnedArray<char>(length); CRuntime.memcpy((void *)cmd._string_, _string_, length * sizeof(char));
			cmd._string_[length] = ('\0');
		}

		public void WidgetText(Rect b, char* _string_, int len, Text* t, uint a, UserFont f)
		{
			Rect label =  new Rect();
			float text_width;
			if ((t== null)) return;
			b.h = (float)((b.h) < (2 * t->padding.y)?(2 * t->padding.y):(b.h));
			label.x = (float)(0);
			label.w = (float)(0);
			label.y = (float)(b.y + t->padding.y);
			label.h = (float)((f.height) < (b.h - 2 * t->padding.y)?(f.height):(b.h - 2 * t->padding.y));
			text_width = (float)(f.width((Handle)(f.userdata), (float)(f.height), _string_, (int)(len)));
			text_width += (float)(2.0f * t->padding.x);
			if ((a & Nuklear.NK_TEXT_ALIGN_LEFT) != 0) {
label.x = (float)(b.x + t->padding.x);label.w = (float)((0) < (b.w - 2 * t->padding.x)?(b.w - 2 * t->padding.x):(0));}
 else if ((a & Nuklear.NK_TEXT_ALIGN_CENTERED) != 0) {
label.w = (float)((1) < (2 * t->padding.x + text_width)?(2 * t->padding.x + text_width):(1));label.x = (float)(b.x + t->padding.x + ((b.w - 2 * t->padding.x) - label.w) / 2);label.x = (float)((b.x + t->padding.x) < (label.x)?(label.x):(b.x + t->padding.x));label.w = (float)((b.x + b.w) < (label.x + label.w)?(b.x + b.w):(label.x + label.w));if ((label.w) >= (label.x)) label.w -= (float)(label.x);}
 else if ((a & Nuklear.NK_TEXT_ALIGN_RIGHT) != 0) {
label.x = (float)((b.x + t->padding.x) < ((b.x + b.w) - (2 * t->padding.x + text_width))?((b.x + b.w) - (2 * t->padding.x + text_width)):(b.x + t->padding.x));label.w = (float)(text_width + 2 * t->padding.x);}
 else return;
			if ((a & Nuklear.NK_TEXT_ALIGN_MIDDLE) != 0) {
label.y = (float)(b.y + b.h / 2.0f - f.height / 2.0f);label.h = (float)((b.h / 2.0f) < (b.h - (b.h / 2.0f + f.height / 2.0f))?(b.h - (b.h / 2.0f + f.height / 2.0f)):(b.h / 2.0f));}
 else if ((a & Nuklear.NK_TEXT_ALIGN_BOTTOM) != 0) {
label.y = (float)(b.y + b.h - f.height);label.h = (float)(f.height);}

			DrawText((Rect)(label), _string_, (int)(len), f, (Color)(t->background), (Color)(t->text));
		}

		public void WidgetTextWrap(Rect b, char* _string_, int len, Text* t, UserFont f)
		{
			float width;
			int glyphs = (int)(0);
			int fitting = (int)(0);
			int done = (int)(0);
			Rect line =  new Rect();
			Text text =  new Text();
			uint* seperator = stackalloc uint[1];
seperator[0] = (uint)(' ');

			if ((t== null)) return;
			text.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));
			text.background = (Color)(t->background);
			text.text = (Color)(t->text);
			b.w = (float)((b.w) < (2 * t->padding.x)?(2 * t->padding.x):(b.w));
			b.h = (float)((b.h) < (2 * t->padding.y)?(2 * t->padding.y):(b.h));
			b.h = (float)(b.h - 2 * t->padding.y);
			line.x = (float)(b.x + t->padding.x);
			line.y = (float)(b.y + t->padding.y);
			line.w = (float)(b.w - 2 * t->padding.x);
			line.h = (float)(2 * t->padding.y + f.height);
			fitting = (int)(f.TextClamp(_string_, (int)(len), (float)(line.w), &glyphs, &width, seperator, 1));
			while ((done) < (len)) {
if ((fitting== 0) || ((line.y + line.h) >= (b.y + b.h))) break;WidgetText((Rect)(line), &_string_[done], (int)(fitting), &text, (uint)(Nuklear.NK_TEXT_LEFT), f);done += (int)(fitting);line.y += (float)(f.height + 2 * t->padding.y);fitting = (int)(f.TextClamp(&_string_[done], (int)(len - done), (float)(line.w), &glyphs, &width, seperator, 1));}
		}

		public void DrawSymbol(int type, Rect content, Color background, Color foreground, float border_width, UserFont font)
		{
			switch (type){
case Nuklear.NK_SYMBOL_X:case Nuklear.NK_SYMBOL_UNDERSCORE:case Nuklear.NK_SYMBOL_PLUS:case Nuklear.NK_SYMBOL_MINUS:{
char X = ((type) == (Nuklear.NK_SYMBOL_X))?'x':((type) == (Nuklear.NK_SYMBOL_UNDERSCORE))?'_':((type) == (Nuklear.NK_SYMBOL_PLUS))?'+':'-';Text text =  new Text();text.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));text.background = (Color)(background);text.text = (Color)(foreground);WidgetText((Rect)(content), &X, (int)(1), &text, (uint)(Nuklear.NK_TEXT_CENTERED), font);}
break;case Nuklear.NK_SYMBOL_CIRCLE_SOLID:case Nuklear.NK_SYMBOL_CIRCLE_OUTLINE:case Nuklear.NK_SYMBOL_RECT_SOLID:case Nuklear.NK_SYMBOL_RECT_OUTLINE:{
if (((type) == (Nuklear.NK_SYMBOL_RECT_SOLID)) || ((type) == (Nuklear.NK_SYMBOL_RECT_OUTLINE))) {
FillRect((Rect)(content), (float)(0), (Color)(foreground));if ((type) == (Nuklear.NK_SYMBOL_RECT_OUTLINE)) FillRect((Rect)(content.ShrinkRectz((float)(border_width))), (float)(0), (Color)(background));}
 else {
FillCircle((Rect)(content), (Color)(foreground));if ((type) == (Nuklear.NK_SYMBOL_CIRCLE_OUTLINE)) FillCircle((Rect)(content.ShrinkRectz((float)(1))), (Color)(background));}
}
break;case Nuklear.NK_SYMBOL_TRIANGLE_UP:case Nuklear.NK_SYMBOL_TRIANGLE_DOWN:case Nuklear.NK_SYMBOL_TRIANGLE_LEFT:case Nuklear.NK_SYMBOL_TRIANGLE_RIGHT:{
int heading;Vec2* points = stackalloc Vec2[3];heading = (int)(((type) == (Nuklear.NK_SYMBOL_TRIANGLE_RIGHT))?Nuklear.NK_RIGHT:((type) == (Nuklear.NK_SYMBOL_TRIANGLE_LEFT))?Nuklear.NK_LEFT:((type) == (Nuklear.NK_SYMBOL_TRIANGLE_UP))?Nuklear.NK_UP:Nuklear.NK_DOWN);points->TriangleFromDirection((Rect)(content), (float)(0), (float)(0), (int)(heading));FillTriangle((float)(points[0].x), (float)(points[0].y), (float)(points[1].x), (float)(points[1].y), (float)(points[2].x), (float)(points[2].y), (Color)(foreground));}
break;default: case Nuklear.NK_SYMBOL_NONE:case Nuklear.NK_SYMBOL_MAX:break;}

		}

		public StyleItem DrawButton(Rect* bounds, uint state, StyleButton style)
		{
			StyleItem background;
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) background = style.hover; else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) background = style.active; else background = style.normal;
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) {
DrawImage((Rect)(*bounds), background.data.image, (Color)(Nuklear.nk_white));}
 else {
FillRect((Rect)(*bounds), (float)(style.rounding), (Color)(background.data.color));StrokeRect((Rect)(*bounds), (float)(style.rounding), (float)(style.border), (Color)(style.border_color));}

			return background;
		}

		public void DrawButtonText(Rect* bounds, Rect* content, uint state, StyleButton style, char* txt, int len, uint text_alignment, UserFont font)
		{
			Text text =  new Text();
			StyleItem background;
			background = DrawButton(bounds, (uint)(state), style);
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) text.background = (Color)(background.data.color); else text.background = (Color)(style.text_background);
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) text.text = (Color)(style.text_hover); else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) text.text = (Color)(style.text_active); else text.text = (Color)(style.text_normal);
			text.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));
			WidgetText((Rect)(*content), txt, (int)(len), &text, (uint)(text_alignment), font);
		}

		public void DrawButtonSymbol(Rect* bounds, Rect* content, uint state, StyleButton style, int type, UserFont font)
		{
			Color sym =  new Color();Color bg =  new Color();
			StyleItem background;
			background = DrawButton(bounds, (uint)(state), style);
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) bg = (Color)(background.data.color); else bg = (Color)(style.text_background);
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (Color)(style.text_hover); else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) sym = (Color)(style.text_active); else sym = (Color)(style.text_normal);
			DrawSymbol((int)(type), (Rect)(*content), (Color)(bg), (Color)(sym), (float)(1), font);
		}

		public void DrawButtonImage(Rect* bounds, Rect* content, uint state, StyleButton style, Image img)
		{
			DrawButton(bounds, (uint)(state), style);
			DrawImage((Rect)(*content), img, (Color)(Nuklear.nk_white));
		}

		public void DrawButtonTextSymbol(Rect* bounds, Rect* label, Rect* symbol, uint state, StyleButton style, char* str, int len, int type, UserFont font)
		{
			Color sym =  new Color();
			Text text =  new Text();
			StyleItem background;
			background = DrawButton(bounds, (uint)(state), style);
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) text.background = (Color)(background.data.color); else text.background = (Color)(style.text_background);
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
sym = (Color)(style.text_hover);text.text = (Color)(style.text_hover);}
 else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
sym = (Color)(style.text_active);text.text = (Color)(style.text_active);}
 else {
sym = (Color)(style.text_normal);text.text = (Color)(style.text_normal);}

			text.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));
			DrawSymbol((int)(type), (Rect)(*symbol), (Color)(style.text_background), (Color)(sym), (float)(0), font);
			WidgetText((Rect)(*label), str, (int)(len), &text, (uint)(Nuklear.NK_TEXT_CENTERED), font);
		}

		public void DrawButtonTextImage(Rect* bounds, Rect* label, Rect* image, uint state, StyleButton style, char* str, int len, UserFont font, Image img)
		{
			Text text =  new Text();
			StyleItem background;
			background = DrawButton(bounds, (uint)(state), style);
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) text.background = (Color)(background.data.color); else text.background = (Color)(style.text_background);
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) text.text = (Color)(style.text_hover); else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) text.text = (Color)(style.text_active); else text.text = (Color)(style.text_normal);
			text.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));
			WidgetText((Rect)(*label), str, (int)(len), &text, (uint)(Nuklear.NK_TEXT_CENTERED), font);
			DrawImage((Rect)(*image), img, (Color)(Nuklear.nk_white));
		}

		public void DrawCheckbox(uint state, StyleToggle style, int active, Rect* label, Rect* selector, Rect* cursors, char* _string_, int len, UserFont font)
		{
			StyleItem background;
			StyleItem cursor;
			Text text =  new Text();
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;cursor = style.cursor_hover;text.text = (Color)(style.text_hover);}
 else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.hover;cursor = style.cursor_hover;text.text = (Color)(style.text_active);}
 else {
background = style.normal;cursor = style.cursor_normal;text.text = (Color)(style.text_normal);}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) {
FillRect((Rect)(*selector), (float)(0), (Color)(style.border_color));FillRect((Rect)(selector->ShrinkRectz((float)(style.border))), (float)(0), (Color)(background.data.color));}
 else DrawImage((Rect)(*selector), background.data.image, (Color)(Nuklear.nk_white));
			if ((active) != 0) {
if ((cursor.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) DrawImage((Rect)(*cursors), cursor.data.image, (Color)(Nuklear.nk_white)); else FillRect((Rect)(*cursors), (float)(0), (Color)(cursor.data.color));}

			text.padding.x = (float)(0);
			text.padding.y = (float)(0);
			text.background = (Color)(style.text_background);
			WidgetText((Rect)(*label), _string_, (int)(len), &text, (uint)(Nuklear.NK_TEXT_LEFT), font);
		}

		public void DrawOption(uint state, StyleToggle style, int active, Rect* label, Rect* selector, Rect* cursors, char* _string_, int len, UserFont font)
		{
			StyleItem background;
			StyleItem cursor;
			Text text =  new Text();
			if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;cursor = style.cursor_hover;text.text = (Color)(style.text_hover);}
 else if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.hover;cursor = style.cursor_hover;text.text = (Color)(style.text_active);}
 else {
background = style.normal;cursor = style.cursor_normal;text.text = (Color)(style.text_normal);}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) {
FillCircle((Rect)(*selector), (Color)(style.border_color));FillCircle((Rect)(selector->ShrinkRectz((float)(style.border))), (Color)(background.data.color));}
 else DrawImage((Rect)(*selector), background.data.image, (Color)(Nuklear.nk_white));
			if ((active) != 0) {
if ((cursor.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) DrawImage((Rect)(*cursors), cursor.data.image, (Color)(Nuklear.nk_white)); else FillCircle((Rect)(*cursors), (Color)(cursor.data.color));}

			text.padding.x = (float)(0);
			text.padding.y = (float)(0);
			text.background = (Color)(style.text_background);
			WidgetText((Rect)(*label), _string_, (int)(len), &text, (uint)(Nuklear.NK_TEXT_LEFT), font);
		}

		public void DrawSelectable(uint state, StyleSelectable style, int active, Rect* bounds, Rect* icon, Image img, char* _string_, int len, uint align, UserFont font)
		{
			StyleItem background;
			Text text =  new Text();
			text.padding = (Vec2)(style.padding);
			if (active== 0) {
if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.pressed;text.text = (Color)(style.text_pressed);}
 else if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;text.text = (Color)(style.text_hover);}
 else {
background = style.normal;text.text = (Color)(style.text_normal);}
}
 else {
if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.pressed_active;text.text = (Color)(style.text_pressed_active);}
 else if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover_active;text.text = (Color)(style.text_hover_active);}
 else {
background = style.normal_active;text.text = (Color)(style.text_normal_active);}
}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) {
DrawImage((Rect)(*bounds), background.data.image, (Color)(Nuklear.nk_white));text.background = (Color)(Nuklear.Rgba((int)(0), (int)(0), (int)(0), (int)(0)));}
 else {
FillRect((Rect)(*bounds), (float)(style.rounding), (Color)(background.data.color));text.background = (Color)(background.data.color);}

			if (((img) != null) && ((icon) != null)) DrawImage((Rect)(*icon), img, (Color)(Nuklear.nk_white));
			WidgetText((Rect)(*bounds), _string_, (int)(len), &text, (uint)(align), font);
		}

		public void DrawSlider(uint state, StyleSlider style, Rect* bounds, Rect* visual_cursor, float min, float value, float max)
		{
			Rect fill =  new Rect();
			Rect bar =  new Rect();
			StyleItem background;
			Color bar_color =  new Color();
			StyleItem cursor;
			if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.active;bar_color = (Color)(style.bar_active);cursor = style.cursor_active;}
 else if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;bar_color = (Color)(style.bar_hover);cursor = style.cursor_hover;}
 else {
background = style.normal;bar_color = (Color)(style.bar_normal);cursor = style.cursor_normal;}

			bar.x = (float)(bounds->x);
			bar.y = (float)((visual_cursor->y + visual_cursor->h / 2) - bounds->h / 12);
			bar.w = (float)(bounds->w);
			bar.h = (float)(bounds->h / 6);
			fill.w = (float)((visual_cursor->x + (visual_cursor->w / 2.0f)) - bar.x);
			fill.x = (float)(bar.x);
			fill.y = (float)(bar.y);
			fill.h = (float)(bar.h);
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) {
DrawImage((Rect)(*bounds), background.data.image, (Color)(Nuklear.nk_white));}
 else {
FillRect((Rect)(*bounds), (float)(style.rounding), (Color)(background.data.color));StrokeRect((Rect)(*bounds), (float)(style.rounding), (float)(style.border), (Color)(style.border_color));}

			FillRect((Rect)(bar), (float)(style.rounding), (Color)(bar_color));
			FillRect((Rect)(fill), (float)(style.rounding), (Color)(style.bar_filled));
			if ((cursor.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) DrawImage((Rect)(*visual_cursor), cursor.data.image, (Color)(Nuklear.nk_white)); else FillCircle((Rect)(*visual_cursor), (Color)(cursor.data.color));
		}

		public void DrawProgress(uint state, StyleProgress style, Rect* bounds, Rect* scursor, ulong value, ulong max)
		{
			StyleItem background;
			StyleItem cursor;
			if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.active;cursor = style.cursor_active;}
 else if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;cursor = style.cursor_hover;}
 else {
background = style.normal;cursor = style.cursor_normal;}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) {
FillRect((Rect)(*bounds), (float)(style.rounding), (Color)(background.data.color));StrokeRect((Rect)(*bounds), (float)(style.rounding), (float)(style.border), (Color)(style.border_color));}
 else DrawImage((Rect)(*bounds), background.data.image, (Color)(Nuklear.nk_white));
			if ((cursor.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) {
FillRect((Rect)(*scursor), (float)(style.rounding), (Color)(cursor.data.color));StrokeRect((Rect)(*scursor), (float)(style.rounding), (float)(style.border), (Color)(style.border_color));}
 else DrawImage((Rect)(*scursor), cursor.data.image, (Color)(Nuklear.nk_white));
		}

		public void DrawScrollbar(uint state, StyleScrollbar style, Rect* bounds, Rect* scroll)
		{
			StyleItem background;
			StyleItem cursor;
			if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.active;cursor = style.cursor_active;}
 else if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;cursor = style.cursor_hover;}
 else {
background = style.normal;cursor = style.cursor_normal;}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) {
FillRect((Rect)(*bounds), (float)(style.rounding), (Color)(background.data.color));StrokeRect((Rect)(*bounds), (float)(style.rounding), (float)(style.border), (Color)(style.border_color));}
 else {
DrawImage((Rect)(*bounds), background.data.image, (Color)(Nuklear.nk_white));}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_COLOR)) {
FillRect((Rect)(*scroll), (float)(style.rounding_cursor), (Color)(cursor.data.color));StrokeRect((Rect)(*scroll), (float)(style.rounding_cursor), (float)(style.border_cursor), (Color)(style.cursor_border_color));}
 else DrawImage((Rect)(*scroll), cursor.data.image, (Color)(Nuklear.nk_white));
		}

		public void EditDrawText(StyleEdit style, float pos_x, float pos_y, float x_offset, char* text, int byte_len, float row_height, UserFont font, Color background, Color foreground, int is_selected)
		{
			if ((((text== null) || (byte_len== 0)) || (this== null)) || (style== null)) return;
			{
int glyph_len = (int)(0);char unicode = (char)0;int text_len = (int)(0);float line_width = (float)(0);float glyph_width;char* line = text;float line_offset = (float)(0);int line_count = (int)(0);Text txt =  new Text();txt.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));txt.background = (Color)(background);txt.text = (Color)(foreground);glyph_len = (int)(Nuklear.UtfDecode(text + text_len, &unicode, (int)(byte_len - text_len)));if (glyph_len== 0) return;while (((text_len) < (byte_len)) && ((glyph_len) != 0)) {
if ((unicode) == ('\n')) {
Rect label =  new Rect();label.y = (float)(pos_y + line_offset);label.h = (float)(row_height);label.w = (float)(line_width);label.x = (float)(pos_x);if (line_count== 0) label.x += (float)(x_offset);if ((is_selected) != 0) FillRect((Rect)(label), (float)(0), (Color)(background));WidgetText((Rect)(label), line, (int)((text + text_len) - line), &txt, (uint)(Nuklear.NK_TEXT_CENTERED), font);text_len++;line_count++;line_width = (float)(0);line = text + text_len;line_offset += (float)(row_height);glyph_len = (int)(Nuklear.UtfDecode(text + text_len, &unicode, (int)(byte_len - text_len)));continue;}
if ((unicode) == ('\r')) {
text_len++;glyph_len = (int)(Nuklear.UtfDecode(text + text_len, &unicode, (int)(byte_len - text_len)));continue;}
glyph_width = (float)(font.width((Handle)(font.userdata), (float)(font.height), text + text_len, (int)(glyph_len)));line_width += (float)(glyph_width);text_len += (int)(glyph_len);glyph_len = (int)(Nuklear.UtfDecode(text + text_len, &unicode, (int)(byte_len - text_len)));continue;}if ((line_width) > (0)) {
Rect label =  new Rect();label.y = (float)(pos_y + line_offset);label.h = (float)(row_height);label.w = (float)(line_width);label.x = (float)(pos_x);if (line_count== 0) label.x += (float)(x_offset);if ((is_selected) != 0) FillRect((Rect)(label), (float)(0), (Color)(background));WidgetText((Rect)(label), line, (int)((text + text_len) - line), &txt, (uint)(Nuklear.NK_TEXT_LEFT), font);}
}

		}

		public void DrawProperty(StyleProperty style, Rect* bounds, Rect* label, uint state, char* name, int len, UserFont font)
		{
			Text text =  new Text();
			StyleItem background;
			if ((state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) {
background = style.active;text.text = (Color)(style.label_active);}
 else if ((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) {
background = style.hover;text.text = (Color)(style.label_hover);}
 else {
background = style.normal;text.text = (Color)(style.label_normal);}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE)) {
DrawImage((Rect)(*bounds), background.data.image, (Color)(Nuklear.nk_white));text.background = (Color)(Nuklear.Rgba((int)(0), (int)(0), (int)(0), (int)(0)));}
 else {
text.background = (Color)(background.data.color);FillRect((Rect)(*bounds), (float)(style.rounding), (Color)(background.data.color));StrokeRect((Rect)(*bounds), (float)(style.rounding), (float)(style.border), (Color)(background.data.color));}

			text.padding = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));
			WidgetText((Rect)(*label), name, (int)(len), &text, (uint)(Nuklear.NK_TEXT_CENTERED), font);
		}

		public void DrawColorPicker(Rect* matrix, Rect* hue_bar, Rect* alpha_bar, Colorf col)
		{
			Color black = (Color)(Nuklear.nk_black);
			Color white = (Color)(Nuklear.nk_white);
			Color black_trans =  new Color();
			float crosshair_size = (float)(7.0f);
			Color temp =  new Color();
			float* hsva = stackalloc float[4];
			float line_y;
			int i;
			Nuklear.ColorfHsvaFv(hsva, (Colorf)(col));
			for (i = (int)(0); (i) < (6); ++i) {
FillRectMultiColor((Rect)(Nuklear.Rectz((float)(hue_bar->x), (float)(hue_bar->y + (float)(i) * (hue_bar->h / 6.0f) + 0.5f), (float)(hue_bar->w), (float)((hue_bar->h / 6.0f) + 0.5f))), (Color)(Nuklear.hue_colors[i]), (Color)(Nuklear.hue_colors[i]), (Color)(Nuklear.hue_colors[i + 1]), (Color)(Nuklear.hue_colors[i + 1]));}
			line_y = ((float)((int)(hue_bar->y + hsva[0] * matrix->h + 0.5f)));
			StrokeLine((float)(hue_bar->x - 1), (float)(line_y), (float)(hue_bar->x + hue_bar->w + 2), (float)(line_y), (float)(1), (Color)(Nuklear.Rgb((int)(255), (int)(255), (int)(255))));
			if ((alpha_bar) != null) {
float alpha = (float)((0) < ((1.0f) < (col.a)?(1.0f):(col.a))?((1.0f) < (col.a)?(1.0f):(col.a)):(0));line_y = ((float)((int)(alpha_bar->y + (1.0f - alpha) * matrix->h + 0.5f)));FillRectMultiColor((Rect)(*alpha_bar), (Color)(white), (Color)(white), (Color)(black), (Color)(black));StrokeLine((float)(alpha_bar->x - 1), (float)(line_y), (float)(alpha_bar->x + alpha_bar->w + 2), (float)(line_y), (float)(1), (Color)(Nuklear.Rgb((int)(255), (int)(255), (int)(255))));}

			temp = (Color)(Nuklear.HsvF((float)(hsva[0]), (float)(1.0f), (float)(1.0f)));
			FillRectMultiColor((Rect)(*matrix), (Color)(white), (Color)(temp), (Color)(temp), (Color)(white));
			FillRectMultiColor((Rect)(*matrix), (Color)(black_trans), (Color)(black_trans), (Color)(black), (Color)(black));
			{
Vec2 p =  new Vec2();float S = (float)(hsva[1]);float V = (float)(hsva[2]);p.x = ((float)((int)(matrix->x + S * matrix->w)));p.y = ((float)((int)(matrix->y + (1.0f - V) * matrix->h)));StrokeLine((float)(p.x - crosshair_size), (float)(p.y), (float)(p.x - 2), (float)(p.y), (float)(1.0f), (Color)(white));StrokeLine((float)(p.x + crosshair_size + 1), (float)(p.y), (float)(p.x + 3), (float)(p.y), (float)(1.0f), (Color)(white));StrokeLine((float)(p.x), (float)(p.y + crosshair_size + 1), (float)(p.x), (float)(p.y + 3), (float)(1.0f), (Color)(white));StrokeLine((float)(p.x), (float)(p.y - crosshair_size), (float)(p.x), (float)(p.y - 2), (float)(1.0f), (Color)(white));}

		}

	}
}
