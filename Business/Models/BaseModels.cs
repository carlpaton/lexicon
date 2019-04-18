namespace Business.Models
{
    public class BaseModel<T>
    {
        public int? Id { get; set; }
        public string Description { get; set; }

        public override int GetHashCode()
        {
            if (Id == null || Description == null)
                return 0;

            return Id.GetHashCode() ^ Description.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseModel<T>)
            {
                if (!(obj is BaseModel<T> other))
                    return false;

                return Id == other.Id
                    && Description == other.Description;
            }

            return false;
        }
    }
}
