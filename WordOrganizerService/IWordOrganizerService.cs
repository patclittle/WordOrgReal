using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace WordOrganizerService
{
    public interface IWordOrganizerService
    {
        Task<IEnumerable<WordInformation>> GetAndSaveWordInformation(Guid instanceId, string word);

        IEnumerable<string> GetAllWordsForInstance(Guid instanceId);
    }
}