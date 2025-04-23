namespace Vehicle_Config_DotNet_.Models
{
    public class AltComponentDto
    {
        public long Id { get; set; }
        public ModelDto Model { get; set; } = null!;
        public ComponentDto Component { get; set; } = null!;
        public ComponentDto AltComponent { get; set; } = null!;
        public double PriceVariation { get; set; }
    }


}
