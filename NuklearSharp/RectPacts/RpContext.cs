using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct RpContext
	{
		public void RpSetupAllowOutOfMem(int allow_out_of_mem)
		{
			if ((allow_out_of_mem) != 0) this.align = (int)(1); else {
this.align = (int)((this.width + this.num_nodes - 1) / this.num_nodes);}

		}

		public void RpInitTarget(int width, int height, RpNode* nodes, int num_nodes)
		{
			int i;
			for (i = (int)(0); (i) < (num_nodes - 1); ++i) {nodes[i].next = &nodes[i + 1];}
			nodes[i].next = null;
			this.init_mode = (int)(Nuklear.NK_RP__INIT_skyline);
			this.heuristic = (int)(Nuklear.NK_RP_HEURISTIC_Skyline_default);
			this.free_head = &nodes[0];
			this.active_head = &this.extra_0;
			this.width = (int)(width);
			this.height = (int)(height);
			this.num_nodes = (int)(num_nodes);
			RpSetupAllowOutOfMem((int)(0));
			this.extra_0.x = (ushort)(0);
			this.extra_0.y = (ushort)(0);
			this.extra_0.next = &this.extra_1;
			this.extra_1.x = ((ushort)(width));
			this.extra_1.y = (ushort)(65535);
			this.extra_1.next = null;
		}

		public int RpSkylineFindMinY(RpNode* first, int x0, int width, int* pwaste)
		{
			RpNode* node = first;
			int x1 = (int)(x0 + width);
			int min_y;int visited_width;int waste_area;
			min_y = (int)(0);
			waste_area = (int)(0);
			visited_width = (int)(0);
			while ((node->x) < (x1)) {
if ((node->y) > (min_y)) {
waste_area += (int)(visited_width * (node->y - min_y));min_y = (int)(node->y);if ((node->x) < (x0)) visited_width += (int)(node->next->x - x0); else visited_width += (int)(node->next->x - node->x);}
 else {
int under_width = (int)(node->next->x - node->x);if ((under_width + visited_width) > (width)) under_width = (int)(width - visited_width);waste_area += (int)(under_width * (min_y - node->y));visited_width += (int)(under_width);}
node = node->next;}
			*pwaste = (int)(waste_area);
			return (int)(min_y);
		}

		public RpFindresult RpSkylineFindBestPos(int width, int height)
		{
			int best_waste = (int)(1 << 30);int best_x;int best_y = (int)(1 << 30);
			RpFindresult fr =  new RpFindresult();
			RpNode** prev;RpNode* node;RpNode* tail;RpNode** best = null;
			width = (int)(width + this.align - 1);
			width -= (int)(width % this.align);
			node = this.active_head;
			prev = &this.active_head;
			while (node->x + width <= this.width) {
int y;int waste;y = (int)(RpSkylineFindMinY(node, (int)(node->x), (int)(width), &waste));if ((this.heuristic) == (Nuklear.NK_RP_HEURISTIC_Skyline_BL_sortHeight)) {
if ((y) < (best_y)) {
best_y = (int)(y);best = prev;}
}
 else {
if (y + height <= this.height) {
if (((y) < (best_y)) || (((y) == (best_y)) && ((waste) < (best_waste)))) {
best_y = (int)(y);best_waste = (int)(waste);best = prev;}
}
}
prev = &node->next;node = node->next;}
			best_x = (int)(((best) == (null))?0:(*best)->x);
			if ((this.heuristic) == (Nuklear.NK_RP_HEURISTIC_Skyline_BF_sortHeight)) {
tail = this.active_head;node = this.active_head;prev = &this.active_head;while ((tail->x) < (width)) {tail = tail->next;}while ((tail) != null) {
int xpos = (int)(tail->x - width);int y;int waste;while (node->next->x <= xpos) {
prev = &node->next;node = node->next;}y = (int)(RpSkylineFindMinY(node, (int)(xpos), (int)(width), &waste));if ((y + height) < (this.height)) {
if (y <= best_y) {
if ((((y) < (best_y)) || ((waste) < (best_waste))) || (((waste) == (best_waste)) && ((xpos) < (best_x)))) {
best_x = (int)(xpos);best_y = (int)(y);best_waste = (int)(waste);best = prev;}
}
}
tail = tail->next;}}

			fr.prev_link = best;
			fr.x = (int)(best_x);
			fr.y = (int)(best_y);
			return (RpFindresult)(fr);
		}

		public RpFindresult RpSkylinePackRectangle(int width, int height)
		{
			RpFindresult res = (RpFindresult)(RpSkylineFindBestPos((int)(width), (int)(height)));
			RpNode* node;RpNode* cur;
			if ((((res.prev_link) == (null)) || ((res.y + height) > (this.height))) || ((this.free_head) == (null))) {
res.prev_link = null;return (RpFindresult)(res);}

			node = this.free_head;
			node->x = ((ushort)(res.x));
			node->y = ((ushort)(res.y + height));
			this.free_head = node->next;
			cur = *res.prev_link;
			if ((cur->x) < (res.x)) {
RpNode* next = cur->next;cur->next = node;cur = next;}
 else {
*res.prev_link = node;}

			while (((cur->next) != null) && (cur->next->x <= res.x + width)) {
RpNode* next = cur->next;cur->next = this.free_head;this.free_head = cur;cur = next;}
			node->next = cur;
			if ((cur->x) < (res.x + width)) cur->x = ((ushort)(res.x + width));
			return (RpFindresult)(res);
		}

		public void RpPackRects(RpRect* rects, int num_rects)
		{
			int i;
			for (i = (int)(0); (i) < (num_rects); ++i) {
rects[i].was_packed = (int)(i);}
			rects->RpQsort((uint)(num_rects), nk_rect_height_compare);
			for (i = (int)(0); (i) < (num_rects); ++i) {
RpFindresult fr = (RpFindresult)(RpSkylinePackRectangle((int)(rects[i].w), (int)(rects[i].h)));if ((fr.prev_link) != null) {
rects[i].x = ((ushort)(fr.x));rects[i].y = ((ushort)(fr.y));}
 else {
rects[i].x = (ushort)(rects[i].y = (ushort)(0xffff));}
}
			rects->RpQsort((uint)(num_rects), nk_rect_original_order);
			for (i = (int)(0); (i) < (num_rects); ++i) {rects[i].was_packed = (int)((((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff)))?0:1);}
		}

	}
}
