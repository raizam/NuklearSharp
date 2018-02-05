using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Window
	{
		public void PushTable(Table tbl)
		{
			if (this.tables== null) {
this.tables = tbl;tbl.next = null;tbl.prev = null;tbl.size = (uint)(0);this.table_count = (uint)(1);return;}

			this.tables.prev = tbl;
			tbl.next = this.tables;
			tbl.prev = null;
			tbl.size = (uint)(0);
			this.tables = tbl;
			this.table_count++;
		}

		public void RemoveTable(Table tbl)
		{
			if ((this.tables) == (tbl)) this.tables = tbl.next;
			if ((tbl.next) != null) tbl.next.prev = tbl.prev;
			if ((tbl.prev) != null) tbl.prev.next = tbl.next;
			tbl.next = null;
			tbl.prev = null;
		}

		public uint* FindValue(uint name)
		{
			Table iter = this.tables;
			while ((iter) != null) {
uint i = (uint)(0);uint size = (uint)(iter.size);for (i = (uint)(0); (i) < (size); ++i) {
if ((iter.keys[i]) == (name)) {
iter.seq = (uint)(this.seq);return (uint *)iter.values + i;}
}size = (uint)(51);iter = iter.next;}
			return null;
		}

	}
}
