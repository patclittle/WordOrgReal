using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace OEDClient
{
    public interface IOxfordDictionaryClient
    {
        Task<IEnumerable<WordInformation>> GetInformation(string word);
    }
}