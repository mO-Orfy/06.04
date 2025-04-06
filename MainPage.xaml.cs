namespace Ahmetzanov06._04
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }
        private void OnAddItemClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            string itemName = button.CommandParameter?.ToString() ?? "Неизвестный товар";
            Preferences.Set(itemName, Preferences.Get(itemName, 0) + 1);
        }
        private async void OnGoToCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage());
        }
    }

}
