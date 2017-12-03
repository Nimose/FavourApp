using FavourApp.Models;
using FavourApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FavourApp.ViewModels
{
    public class ConversationViewModel
    {
        public ObservableCollection<Conversation> Conversations { get; private set; } = new ObservableCollection<Conversation>();

        public async void GetConversations(string userId)
        {
            var favorService = new FavorService();
            List<Conversation> conversations = await favorService.GetConversationsAsync(userId);
            foreach (var conversation in conversations)
            {
                Conversations.Add(conversation);
            }
        }


        public void SendMessage(Models.Message message)
        {
            var favorService = new FavorService();
            favorService.SendMessageAsync(message);
        }
    }
}

