namespace NuklearSharp
{
	unsafe partial struct RpContext
	{
		public int width;
		public int height;
		public int align;
		public int init_mode;
		public int heuristic;
		public int num_nodes;
		public RpNode* active_head;
		public RpNode* free_head;
		public RpNode extra_0, extra_1;
	}
}
