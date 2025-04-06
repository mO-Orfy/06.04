using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ahmetzanov06._04.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<MenuItem> MenuItems { get; } = new();

        private int _cartItemsCount;
        public int CartItemsCount
        {
            get => _cartItemsCount;
            set
            {
                if (_cartItemsCount != value)
                {
                    _cartItemsCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddToCartCommand { get; }

        public MenuViewModel()
        {
            // Инициализация меню
            InitializeMenuItems();

            AddToCartCommand = new Command<MenuItem>(AddToCart);
        }

        private void InitializeMenuItems()
        {
            MenuItems.Clear();

            MenuItems.Add(new MenuItem { Name = "Борщ", Price = 320, Description = "Традиционный суп" });
            MenuItems.Add(new MenuItem { Name = "Стейк", Price = 780, Description = "Говядина мраморная" });
            MenuItems.Add(new MenuItem { Name = "Плов", Price = 450, Description = "С бараниной" });
            MenuItems.Add(new MenuItem { Name = "Салат Цезарь", Price = 290, Description = "С курицей" });
            MenuItems.Add(new MenuItem { Name = "Пицца Маргарита", Price = 520, Description = "Классическая" });
            MenuItems.Add(new MenuItem { Name = "Паста Карбонара", Price = 480, Description = "Со сливочным соусом" });
            MenuItems.Add(new MenuItem { Name = "Греческий салат", Price = 350, Description = "С фетой" });
            MenuItems.Add(new MenuItem { Name = "Бургер", Price = 420, Description = "Говяжий" });
            MenuItems.Add(new MenuItem { Name = "Пельмени", Price = 380, Description = "Домашние" });
            MenuItems.Add(new MenuItem { Name = "Тирамису", Price = 280, Description = "Десерт" });
        }

        private void AddToCart(MenuItem item)
        {
            CartItemsCount++;
            Preferences.Set(item.Name, Preferences.Get(item.Name, 0) + 1);
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
    

