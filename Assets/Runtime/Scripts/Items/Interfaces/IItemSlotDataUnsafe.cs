namespace Items
{
    public interface IItemSlotDataUnsafe
	{
		void SetCount(int count);
		void IncreaseCountBy(int count);
		void DecreaseCountBy(int num);
	}
}