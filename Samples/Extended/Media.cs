using System;
using NuklearSharp;

namespace Extended
{
	public class Media
	{
		public Nuklear.nk_font font_14;
		public Nuklear.nk_font font_18;
		public Nuklear.nk_font font_20;
		public Nuklear.nk_font font_22;

		public Nuklear.nk_image uncheckd;
		public Nuklear.nk_image checkd;
		public Nuklear.nk_image rocket;
		public Nuklear.nk_image cloud;
		public Nuklear.nk_image pen;
		public Nuklear.nk_image play;
		public Nuklear.nk_image pause;
		public Nuklear.nk_image stop;
		public Nuklear.nk_image prev;
		public Nuklear.nk_image next;
		public Nuklear.nk_image tools;
		public Nuklear.nk_image dir;
		public Nuklear.nk_image copy;
		public Nuklear.nk_image convert;
		public Nuklear.nk_image del;
		public Nuklear.nk_image edit;
		public Nuklear.nk_image[] images = new Nuklear.nk_image[9];
		public Nuklear.nk_image[] menu = new Nuklear.nk_image[6];		
		
		public Media ()
		{
		}
	}
}

