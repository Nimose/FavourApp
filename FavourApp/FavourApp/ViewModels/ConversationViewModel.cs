using FavourApp.Models;
using System.Collections.ObjectModel;

namespace FavourApp.ViewModels
{
    public class ConversationViewModel
    {
        public ObservableCollection<Conversation> Conversations { get; private set; } = new ObservableCollection<Conversation>();

    }
}
