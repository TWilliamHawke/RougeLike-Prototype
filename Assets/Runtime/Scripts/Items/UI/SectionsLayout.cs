namespace Items.UI
{
    public class SectionsLayout : UILayoutWithObserver<IUISectionData<ItemSlotData>, InventorySection>
    {
        public new void CleanLayout()
        {
            base.CleanLayout();
        }

        public void CreateSection(ItemSectionTemplate template, ItemSection sectionData)
        {
            InventorySection section = CreateLayoutElement(sectionData);
            section.SetTemplate(template);
            section.StartObserving();
        }
    }
}