namespace Vehicle_Config_DotNet_.Models
{
    public class ModelDto
    {
        public long Id { get; set; }
        public SegmentDto Segment { get; set; } = null!;
        public ManufacturerDto Manufacturer { get; set; } = null!;
        public string ModName { get; set; } = null!;
        public int Price { get; set; }
        public string ImagePath { get; set; } = null!;
        public int MinQty { get; set; }
        public int? SafetyRating { get; set; }
    }

}
