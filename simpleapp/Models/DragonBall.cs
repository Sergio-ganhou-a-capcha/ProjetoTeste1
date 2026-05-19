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
        public Fighter ?fighter { get; set; }
        public Description ?description { get; set; }
        public Image ?image { get; set; }
        public Affiliation ?affiliation { get; set; }
    }

    public class Fighter{
        public string ?name { get; set; }
    }

    public class Description{
        public string ?description { get; set; }
    }
    public class Affiliation{
        public string ?affiliation { get; set; }
    }

    public class Image{
        public string ?png { get; set; }
    }
}