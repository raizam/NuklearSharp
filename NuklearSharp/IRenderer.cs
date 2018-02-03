namespace NuklearSharp
{
	public interface IRenderer
	{
		/// <summary>
		/// Creates a texture and returns its unique id
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		int CreateTexture(int width, int height, byte[] data);

		/// <summary>
		/// Called at the beginning of the draw
		/// </summary>
		void BeginDraw();

		/// <summary>
		/// Fills vertex and index buffers with data
		/// </summary>
		/// <param name="vertices"></param>
		/// <param name="indices"></param>
		/// <param name="vertex_count"></param>
		/// <param name="vertex_stride"></param>
		void SetBuffers(byte[] vertices, short[] indices, int vertex_count, int vertex_stride);

		/// <summary>
		/// Draw
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="textureId"></param>
		/// <param name="startIndex"></param>
		/// <param name="primitiveCount"></param>
		void Draw(int x, int y, int w, int h, int textureId, int startIndex, int primitiveCount);


		/// <summary>
		/// Called at the end of the draw
		/// </summary>
		void EndDraw();
	}
}
