public class PostRatingsModel
{
    public int postratId { get; set; }
    public int postId { get; set; }
    public int userId { get; set; }
    public int rating { get; set; }

    public UserModel User { get; set; }
    public PostModel Post { get; set; }
}
