namespace TaskrowSharp
{
    public class IndexData
    {
        public IndexDataPhoto Photo { get; set; }

        public IndexData(IndexDataPhoto indexDataPhoto)
        {
            this.Photo = indexDataPhoto;
        }

        internal IndexData(ApiModels.IndexDataApi indexDataApi)
        {
            if (indexDataApi.Photos != null)
                this.Photo = new IndexDataPhoto(indexDataApi.Photos);
        }
    }
}
