public class ByteCount
{
	public byte Byte;
	public int Count;
}

public class HuffmanTreeNode
{
	public HuffmanTreeNode Left, Right, Parent;
	public ByteCount ByteCount;

	public HuffmanTreeNode()
	{
	}

	public HuffmanTreeNode(HuffmanTreeNode left, HuffmanTreeNode right)
	{
		Left = left;
		Right = right;
		Left.Parent = Right.Parent = this;
		if (ByteCount == null)
			ByteCount = new ByteCount();
		this.ByteCount.Count = Left.ByteCount.Count + Right.ByteCount.Count;
	}
}