namespace Ahmetzanov06._04;

public partial class CartPage : ContentPage
{
	public CartPage()
	{
		InitializeComponent();
        LoadCartItems();
    }

    private void LoadCartItems()
    {
        var cartItems = new List<CartItem>();
        decimal total = 0;

        // Получаем товары из Preferences
        var menuItems = new[]
        {
                "Борщ", "Стейк", "Плов", "Салат Цезарь", "Пицца Маргарита",
                "Паста Карбонара", "Греческий салат", "Бургер", "Пельмени", "Тирамису"
            };

        foreach (var itemName in menuItems)
        {
            int quantity = Preferences.Get(itemName, 0);
            if (quantity > 0)
            {
                decimal price = GetPriceForItem(itemName);
                cartItems.Add(new CartItem
                {
                    Name = itemName,
                    Quantity = quantity,
                    Price = price,
                    Total = price * quantity
                });
                total += price * quantity;
            }
        }

        CartItemsList.ItemsSource = cartItems;
        TotalLabel.Text = $"Итого: {total}?";
    }

    private decimal GetPriceForItem(string itemName)
    {
        // Цены должны соответствовать главной странице
        return itemName switch
        {
            "Борщ" => 320,
            "Стейк" => 780,
            "Плов" => 450,
            "Салат Цезарь" => 290,
            "Пицца Маргарита" => 520,
            "Паста Карбонара" => 480,
            "Греческий салат" => 350,
            "Бургер" => 420,
            "Пельмени" => 380,
            "Тирамису" => 280,
            _ => 0
        };
    }

    private void OnRemoveItemClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string itemName = button.CommandParameter.ToString();

        Preferences.Set(itemName, 0);
        LoadCartItems(); // Обновляем список
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        if (!(CartItemsList.ItemsSource as IEnumerable<CartItem>).Any())
        {
            await DisplayAlert("Корзина пуста", "Добавьте товары в корзину", "OK");
            return;
        }

        // Очищаем корзину
        foreach (var itemName in new[]
        {
                "Борщ", "Стейк", "Плов", "Салат Цезарь", "Пицца Маргарита",
                "Паста Карбонара", "Греческий салат", "Бургер", "Пельмени", "Тирамису"
            })
        {
            Preferences.Remove(itemName);
        }

        await DisplayAlert("Спасибо", "Заказ оформлен!", "OK");
        await Navigation.PopAsync();
    }
}

public class CartItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}
