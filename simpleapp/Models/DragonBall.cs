namespace simpleapp.Models{

    public class DragonBalls{
        public int ?Id { get; set; }
        public string ?Fighter { get; set; }
        public string ?Description { get; set; }
        
        public string ?ImageURL { get; set; }
        public string ?Affiliation { get; set; }
    }

    public class DragonBallApiResponse{
        public int ?id { get; set; }
        public string ?name { get; set; }
        public string ?description { get; set; }
        public string ?image { get; set; }
        public string ?affiliation { get; set; }
    }
}