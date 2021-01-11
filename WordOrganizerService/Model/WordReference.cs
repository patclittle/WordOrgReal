using System;
namespace WordOrganizerService.Model
{
    public class WordReference
    {
        public WordReference(
            string wordName)
        {
            this.WordName = wordName;
        }

        public WordReference()
        {
        }

        public string WordName { get; set; }
    }
}
