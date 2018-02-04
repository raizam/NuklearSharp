namespace NuklearSharp
{
	unsafe partial class FontConfig
	{
		public FontConfig Clone()
		{
			return new FontConfig
			{
				next = next,
				ttf_blob = ttf_blob,
				ttf_size = ttf_size,
				ttf_data_owned_by_atlas = ttf_data_owned_by_atlas,
				merge_mode = merge_mode,
				pixel_snap = pixel_snap,
				oversample_v = oversample_v,
				oversample_h = oversample_h,
				padding = padding,
				size = size,
				coord_type = coord_type,
				spacing = spacing,
				range = range,
				font = font,
				fallback_glyph = fallback_glyph,
				n = n,
				p = p,
			};
		}
	}
}
