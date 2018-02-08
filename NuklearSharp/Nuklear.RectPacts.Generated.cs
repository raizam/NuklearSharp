using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_rp_rect
		{
			public int id;
			public ushort w;
			public ushort h;
			public ushort x;
			public ushort y;
			public int was_packed;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_rp_node
		{
			public ushort x;
			public ushort y;
			public nk_rp_node* next;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_rp__findresult
		{
			public int x;
			public int y;
			public nk_rp_node** prev_link;
		}

		public static void nk_rp_setup_allow_out_of_mem(nk_rp_context* context, int allow_out_of_mem)
		{
			if ((allow_out_of_mem) != 0) context->align = (int) (1);
			else
			{
				context->align = (int) ((context->width + context->num_nodes - 1)/context->num_nodes);
			}

		}

		public static void nk_rp_init_target(nk_rp_context* context, int width, int height, nk_rp_node* nodes, int num_nodes)
		{
			int i;
			for (i = (int) (0); (i) < (num_nodes - 1); ++i)
			{
				nodes[i].next = &nodes[i + 1];
			}
			nodes[i].next = null;
			context->init_mode = (int) (NK_RP__INIT_skyline);
			context->heuristic = (int) (NK_RP_HEURISTIC_Skyline_default);
			context->free_head = &nodes[0];
			context->active_head = &context->extra_0;
			context->width = (int) (width);
			context->height = (int) (height);
			context->num_nodes = (int) (num_nodes);
			nk_rp_setup_allow_out_of_mem(context, (int) (0));
			context->extra_0.x = (ushort) (0);
			context->extra_0.y = (ushort) (0);
			context->extra_0.next = &context->extra_1;
			context->extra_1.x = ((ushort) (width));
			context->extra_1.y = (ushort) (65535);
			context->extra_1.next = null;
		}

		public static int nk_rp__skyline_find_min_y(nk_rp_context* c, nk_rp_node* first, int x0, int width, int* pwaste)
		{
			nk_rp_node* node = first;
			int x1 = (int) (x0 + width);
			int min_y;
			int visited_width;
			int waste_area;
			min_y = (int) (0);
			waste_area = (int) (0);
			visited_width = (int) (0);
			while ((node->x) < (x1))
			{
				if ((node->y) > (min_y))
				{
					waste_area += (int) (visited_width*(node->y - min_y));
					min_y = (int) (node->y);
					if ((node->x) < (x0)) visited_width += (int) (node->next->x - x0);
					else visited_width += (int) (node->next->x - node->x);
				}
				else
				{
					int under_width = (int) (node->next->x - node->x);
					if ((under_width + visited_width) > (width)) under_width = (int) (width - visited_width);
					waste_area += (int) (under_width*(min_y - node->y));
					visited_width += (int) (under_width);
				}
				node = node->next;
			}
			*pwaste = (int) (waste_area);
			return (int) (min_y);
		}

		public static nk_rp__findresult nk_rp__skyline_find_best_pos(nk_rp_context* c, int width, int height)
		{
			int best_waste = (int) (1 << 30);
			int best_x;
			int best_y = (int) (1 << 30);
			nk_rp__findresult fr = new nk_rp__findresult();
			nk_rp_node** prev;
			nk_rp_node* node;
			nk_rp_node* tail;
			nk_rp_node** best = null;
			width = (int) (width + c->align - 1);
			width -= (int) (width%c->align);
			node = c->active_head;
			prev = &c->active_head;
			while (node->x + width <= c->width)
			{
				int y;
				int waste;
				y = (int) (nk_rp__skyline_find_min_y(c, node, (int) (node->x), (int) (width), &waste));
				if ((c->heuristic) == (NK_RP_HEURISTIC_Skyline_BL_sortHeight))
				{
					if ((y) < (best_y))
					{
						best_y = (int) (y);
						best = prev;
					}
				}
				else
				{
					if (y + height <= c->height)
					{
						if (((y) < (best_y)) || (((y) == (best_y)) && ((waste) < (best_waste))))
						{
							best_y = (int) (y);
							best_waste = (int) (waste);
							best = prev;
						}
					}
				}
				prev = &node->next;
				node = node->next;
			}
			best_x = (int) (((best) == (null)) ? 0 : (*best)->x);
			if ((c->heuristic) == (NK_RP_HEURISTIC_Skyline_BF_sortHeight))
			{
				tail = c->active_head;
				node = c->active_head;
				prev = &c->active_head;
				while ((tail->x) < (width))
				{
					tail = tail->next;
				}
				while ((tail) != null)
				{
					int xpos = (int) (tail->x - width);
					int y;
					int waste;
					while (node->next->x <= xpos)
					{
						prev = &node->next;
						node = node->next;
					}
					y = (int) (nk_rp__skyline_find_min_y(c, node, (int) (xpos), (int) (width), &waste));
					if ((y + height) < (c->height))
					{
						if (y <= best_y)
						{
							if ((((y) < (best_y)) || ((waste) < (best_waste))) || (((waste) == (best_waste)) && ((xpos) < (best_x))))
							{
								best_x = (int) (xpos);
								best_y = (int) (y);
								best_waste = (int) (waste);
								best = prev;
							}
						}
					}
					tail = tail->next;
				}
			}

			fr.prev_link = best;
			fr.x = (int) (best_x);
			fr.y = (int) (best_y);
			return (nk_rp__findresult) (fr);
		}

		public static nk_rp__findresult nk_rp__skyline_pack_rectangle(nk_rp_context* context, int width, int height)
		{
			nk_rp__findresult res = (nk_rp__findresult) (nk_rp__skyline_find_best_pos(context, (int) (width), (int) (height)));
			nk_rp_node* node;
			nk_rp_node* cur;
			if ((((res.prev_link) == (null)) || ((res.y + height) > (context->height))) || ((context->free_head) == (null)))
			{
				res.prev_link = null;
				return (nk_rp__findresult) (res);
			}

			node = context->free_head;
			node->x = ((ushort) (res.x));
			node->y = ((ushort) (res.y + height));
			context->free_head = node->next;
			cur = *res.prev_link;
			if ((cur->x) < (res.x))
			{
				nk_rp_node* next = cur->next;
				cur->next = node;
				cur = next;
			}
			else
			{
				*res.prev_link = node;
			}

			while (((cur->next) != null) && (cur->next->x <= res.x + width))
			{
				nk_rp_node* next = cur->next;
				cur->next = context->free_head;
				context->free_head = cur;
				cur = next;
			}
			node->next = cur;
			if ((cur->x) < (res.x + width)) cur->x = ((ushort) (res.x + width));
			return (nk_rp__findresult) (res);
		}

		public static void nk_rp_qsort(nk_rp_rect* array, uint len, QSortComparer cmp)
		{
			uint right;
			uint left = (uint) (0);
			uint* stack = stackalloc uint[64];
			uint pos = (uint) (0);
			uint seed = (uint) (len/2*69069 + 1);
			for (;;)
			{
				for (; (left + 1) < (len); len++)
				{
					nk_rp_rect pivot = new nk_rp_rect();
					nk_rp_rect tmp = new nk_rp_rect();
					if ((pos) == (64)) len = (uint) (stack[pos = (uint) (0)]);
					pivot = (nk_rp_rect) (array[left + seed%(len - left)]);
					seed = (uint) (seed*69069 + 1);
					stack[pos++] = (uint) (len);
					for (right = (uint) (left - 1);;)
					{
						while ((cmp(&array[++right], &pivot)) < (0))
						{
						}
						while ((cmp(&pivot, &array[--len])) < (0))
						{
						}
						if ((right) >= (len)) break;
						tmp = (nk_rp_rect) (array[right]);
						array[right] = (nk_rp_rect) (array[len]);
						array[len] = (nk_rp_rect) (tmp);
					}
				}
				if ((pos) == (0)) break;
				left = (uint) (len);
				len = (uint) (stack[--pos]);
			}
		}

		public static void nk_rp_pack_rects(nk_rp_context* context, nk_rp_rect* rects, int num_rects)
		{
			int i;
			for (i = (int) (0); (i) < (num_rects); ++i)
			{
				rects[i].was_packed = (int) (i);
			}
			nk_rp_qsort(rects, (uint) (num_rects), nk_rect_height_compare);
			for (i = (int) (0); (i) < (num_rects); ++i)
			{
				nk_rp__findresult fr =
					(nk_rp__findresult) (nk_rp__skyline_pack_rectangle(context, (int) (rects[i].w), (int) (rects[i].h)));
				if ((fr.prev_link) != null)
				{
					rects[i].x = ((ushort) (fr.x));
					rects[i].y = ((ushort) (fr.y));
				}
				else
				{
					rects[i].x = (ushort) (rects[i].y = (ushort) (0xffff));
				}
			}
			nk_rp_qsort(rects, (uint) (num_rects), nk_rect_original_order);
			for (i = (int) (0); (i) < (num_rects); ++i)
			{
				rects[i].was_packed = (int) ((((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff))) ? 0 : 1);
			}
		}
	}
}