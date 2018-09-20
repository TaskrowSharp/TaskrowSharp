using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp
{
    public class IndexDataPhoto
    {
        public string SmallRelativeURL { get; set; }
        public string LargeRelativeURL { get; set; }
        public string SmallDefaultURL { get; set; }
        public string LargeDefaultURL { get; set; }
        public string ContactPhotoRelativeURL { get; set; }

        public IndexDataPhoto(string smallRelativeURL, string largeRelativeURL, string smallDefaultURL, string largeDefaultURL, string contactPhotoRelativeURL)
        {
            this.SmallRelativeURL = smallRelativeURL;
            this.LargeRelativeURL = largeRelativeURL;
            this.SmallDefaultURL = smallDefaultURL;
            this.LargeDefaultURL = largeDefaultURL;
            this.ContactPhotoRelativeURL = contactPhotoRelativeURL;
        }

        internal IndexDataPhoto(ApiModels.IndexDataPhotoApi indexDataPhotoApi)
        {
            this.SmallRelativeURL = indexDataPhotoApi.SmallRelativeURL;
            this.LargeRelativeURL = indexDataPhotoApi.LargeRelativeURL;
            this.SmallDefaultURL = indexDataPhotoApi.SmallDefaultURL;
            this.LargeDefaultURL = indexDataPhotoApi.LargeDefaultURL;
            this.ContactPhotoRelativeURL = indexDataPhotoApi.ContactPhotoRelativeURL;
        }
    }
}
