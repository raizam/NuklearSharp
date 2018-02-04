// Generated by Sichem at 2/7/2018 6:51:03 PM

using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConvertConfig
	{
		public void* DrawVertex(void* dst, Vec2 pos, Vec2 uv, Colorf color)
		{
			void* result = (void*) ((sbyte*) (dst) + this.vertex_size);
			fixed (DrawVertexLayoutElement* elem_iter2 = this.vertex_layout)
			{
				DrawVertexLayoutElement* elem_iter = elem_iter2;
				while (elem_iter->IsEndOfLayout() == 0)
				{
					void* address = (void*) ((sbyte*) (dst) + elem_iter->offset);
					switch (elem_iter->attribute)
					{
						case Nuklear.NK_VERTEX_ATTRIBUTE_COUNT:
						default:
							;
							break;
						case Nuklear.NK_VERTEX_POSITION:
							Nuklear.DrawVertexElement(address, &pos.x, (int) (2), (int) (elem_iter->format));
							break;
						case Nuklear.NK_VERTEX_TEXCOORD:
							Nuklear.DrawVertexElement(address, &uv.x, (int) (2), (int) (elem_iter->format));
							break;
						case Nuklear.NK_VERTEX_COLOR:
							Nuklear.DrawVertexColor(address, &color.r, (int) (elem_iter->format));
							break;
					}
					elem_iter++;
				}
			}
			return result;
		}
	}
}