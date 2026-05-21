namespace BroGarage.API.Data.Seeds
{
    public class BaseSeed
    {
        private static readonly DateTime FromDate = new(1970, 1, 1);

        private static readonly DateTime DefaultDate = new(2022, 10, 19, 20, 14, 0);

        protected static readonly DateTime CreatedDate = DefaultDate.Date;
        protected static readonly TimeSpan CreatedTime = DefaultDate.TimeOfDay;

        protected static readonly int CreatedUserId = 1;

        protected static readonly long CreatedTimeStamp = Convert.ToInt64(DefaultDate.Subtract(FromDate).TotalSeconds);
    }
}
