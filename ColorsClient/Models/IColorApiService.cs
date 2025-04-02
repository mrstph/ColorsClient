using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorsClient.Models
{
    public interface IColorApiService
    {
        Task<List<ColorPalette>> GetPalettesAsync();
    }
}
