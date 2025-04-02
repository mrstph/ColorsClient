using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorsClient.Models;

namespace ColorsClient.ViewModels
{
    public partial class ColorPaletteViewModel
    {
        public ObservableCollection<ColorViewModel> Colors { get; set; } = new();

        public ColorPaletteViewModel(ColorPalette palette)
        {
            foreach (var color in palette.Colors)
            {
                Colors.Add(new ColorViewModel(color.Type, color.Red, color.Green, color.Blue));
            }
        }
    }
}
