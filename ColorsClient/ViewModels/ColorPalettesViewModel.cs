using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorsClient.Models;

namespace ColorsClient.ViewModels
{
    public partial class ColorPalettesViewModel
    {
        private readonly IColorApiService _colorApiService;
        public ObservableCollection<ColorPaletteViewModel> Palettes { get; } = new();
        public ICommand LoadPalettesCommand { get; }

        public ColorPalettesViewModel(IColorApiService colorApiService)
        {
            _colorApiService = colorApiService;
            LoadPalettesCommand = new Command(async () => await LoadPalettes());
        }

        private async Task LoadPalettes()
        {
            if (Palettes.Any()) return;

            var palettes = await _colorApiService.GetPalettesAsync();
            foreach (var palette in palettes)
            {
                Palettes.Add(new ColorPaletteViewModel(palette));
            }
        }
    }
}
