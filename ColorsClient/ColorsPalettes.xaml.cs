using ColorsClient.ViewModels;

namespace ColorsClient
{
    public partial class ColorsPalettes : ContentPage
    {
        private readonly ColorPalettesViewModel _viewModel;

        public ColorsPalettes(ColorPalettesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel.Palettes.Count == 0)
                _viewModel.LoadPalettesCommand.Execute(null);
        }
    }
}
