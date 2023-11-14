using OnePrice.UI.Models.CommonDTOs;


namespace OnePrice.UI.Models.PostDTOs
{
    public class FullPostDTO
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string? ImgPath { get; set; }

        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public CommonCurrencyDTO Currency { get; set; }
		public CommonCategoryDTO Category { get; set; }
        public List<CommonTagDTO> Tags { get; set; }

        //public List<CommentsDTO> Comments { get; set; }

        public CommonAppUserDTO Author {get;set;}
    }
}
