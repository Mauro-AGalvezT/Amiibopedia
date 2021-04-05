using Amiibopedia.Helpers;
using Amiibopedia.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Amiibopedia.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Amiibo> _amiibos;

        public ObservableCollection<Character> Characters { get; set; }
        public ObservableCollection<Amiibo> Amiibos
        {
            get => _amiibos;
            set
            {
                _amiibos = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }

        public MainPageViewModel()
        {
            SearchCommand =
                new Command(async (param) =>
                {
                    var character = param as Character;
                    if (character != null)
                    {
                        string url = $"https://www.amiiboapi.com/api/amiibo/?character={character.name}";
                        var service = new HttpsHelper<Amiibos>();
                        var amiibos = await service.GetRestServiceDataAsync(url);
                        Amiibos = new ObservableCollection<Amiibo>(amiibos.amiibo);
                    }
                });
        }

        public async Task LoadCharacters()
        {
            var url = "https://amiiboapi.com/api/character/";
            var service = new HttpsHelper<Characters>();
            var characters = await service.GetRestServiceDataAsync(url);
            Characters = new ObservableCollection<Character>(characters.amiibo);

        }

    }
}
