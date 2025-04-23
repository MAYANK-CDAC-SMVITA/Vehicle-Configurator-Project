namespace Vehicle_Config_DotNet_.Models
{
    public class VehicleDetailDto
    {
        public long Id { get; set; }
        public ModelDto Model { get; set; } = null!;
        public ComponentDto Component { get; set; } = null!;
        public string CompType { get; set; } = null!;
        public string IsConfigurable { get; set; } = null!;
    }


}
