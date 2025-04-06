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

        // �������� ������ �� Preferences
        var menuItems = new[]
        {
                "����", "�����", "����", "����� ������", "����� ���������",
                "����� ���������", "��������� �����", "������", "��������", "��������"
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
        TotalLabel.Text = $"�����: {total}?";
    }

    private decimal GetPriceForItem(string itemName)
    {
        // ���� ������ ��������������� ������� ��������
        return itemName switch
        {
            "����" => 320,
            "�����" => 780,
            "����" => 450,
            "����� ������" => 290,
            "����� ���������" => 520,
            "����� ���������" => 480,
            "��������� �����" => 350,
            "������" => 420,
            "��������" => 380,
            "��������" => 280,
            _ => 0
        };
    }

    private void OnRemoveItemClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        string itemName = button.CommandParameter.ToString();

        Preferences.Set(itemName, 0);
        LoadCartItems(); // ��������� ������
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        if (!(CartItemsList.ItemsSource as IEnumerable<CartItem>).Any())
        {
            await DisplayAlert("������� �����", "�������� ������ � �������", "OK");
            return;
        }

        // ������� �������
        foreach (var itemName in new[]
        {
                "����", "�����", "����", "����� ������", "����� ���������",
                "����� ���������", "��������� �����", "������", "��������", "��������"
            })
        {
            Preferences.Remove(itemName);
        }

        await DisplayAlert("�������", "����� ��������!", "OK");
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
