using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_API.Model;

namespace UWP_API.Services
{
    interface ISongService
    {
        Task<Song> Create(Song song);
        Task<List<Song>> GetList();
        Task<Song> GetDetail(string name);
        Task<Song> Update(Song user);
        Task<bool> Delete(string name);
    }
}
