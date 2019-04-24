namespace Business.Models
{
    public class EntryPlatformBusinessModel
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public int PlatformId { get; set; }
        public string Description { get; set; }
        public PlatformBusinessModel PlatformModel { get; set; }

        public EntryPlatformBusinessModel()
        {
            Description = "";
            PlatformModel = new PlatformBusinessModel();
        }

        public override int GetHashCode()
        {
            if (Description == null)
                return 0;

            return Id.GetHashCode() 
                ^ EntryId.GetHashCode()
                ^ PlatformId.GetHashCode()
                ^ Description.GetHashCode()
                ^ PlatformModel.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is EntryPlatformBusinessModel)
            {
                if (!(obj is EntryPlatformBusinessModel other))
                    return false;

                return Id == other.Id
                    && EntryId == other.EntryId
                    && PlatformId == other.PlatformId
                    && Description == other.Description
                    && PlatformModel == other.PlatformModel;
            }

            return false;
        }
    }
}
