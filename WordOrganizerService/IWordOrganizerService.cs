using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;
using WordOrganizerService.Model;

namespace WordOrganizerService
{
    public interface IWordOrganizerService
    {
        Task<IEnumerable<WordInformation>> GetAndSaveWordInformation(Guid instanceId, string word);

        IEnumerable<WordReference> GetAllWordsForInstance(Guid instanceId);
    }
}