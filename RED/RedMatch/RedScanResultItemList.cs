using System.Collections;
using System.Collections.Generic;

namespace RED.Match
{
	public class RedScanResultItemList : IEnumerable<RedScanResultItem>
	{
		public RedScanResultItemList()
		{
			Items = new List<RedScanResultItem>();
		}

		// Default Indexer for this object
		public RedScanResultItem this[int index]
		{
			get { return Items[index]; }
			set { Items[index] = value; }
		}

		public IEnumerator<RedScanResultItem> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public List<RedScanResultItem> Items { get; private set; }

		public int Count { get { return Items.Count; } }

		public void AddItem(RedScanResultItem v)
		{
			if (v != null)
			{
				Items.Add(v);
			}
		}

		//public RedScanResultItem AddItem(DirectoryInfo di, DirectorySearchStatusTypes status, string errorMsg)
		//{
		//    RedScanResultItem item = null;
		//    if (di != null)
		//    {
		//        item = new RedScanResultItem(di, status, errorMsg);
		//        Items.Add(item);
		//    }
		//    return item;
		//}

		public void Clear()
		{
			// Calling Clear() will not actually reduce the memory (used by the List<T> itself),
			// as it doesn't shrink the list's capacity, only eliminates the values contained within it.
			// Creating a new List<T> will cause a new list to be allocated, which will
			// in turn cause more allocations with growth. This, however, does not
			// mean that it will be slower - in many cases, reallocating will be faster
			// as you're less likely to promote the large arrays into higher
			// garbage collection generations, which in turn can keep the GC process much faster.
			//Items.Clear();
			Items = new List<RedScanResultItem>();
		}
	}
}