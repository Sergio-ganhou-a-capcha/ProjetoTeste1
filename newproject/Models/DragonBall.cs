namespace newproject.Models{
    public class DragonBall{
        public string ?UniqueId { get; set; }
        public string ?OfficialName { get; set; }
        public string ?imageUrl { get; set; }
        public string ?affiliation  { get; set; }
    }

    public class DragonBallApiResponse{
        public int ?id { get; set; }
        public string ?name { get; set; }
        public string ?image { get; set; }
        public string ?affiliation  { get; set; }
    }
}