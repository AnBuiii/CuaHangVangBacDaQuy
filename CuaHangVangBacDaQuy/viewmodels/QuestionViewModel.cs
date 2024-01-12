using CuaHangVangBacDaQuy.models;
using CuaHangVangBacDaQuy.viewmodels.DialogContentViewModel;
using CuaHangVangBacDaQuy.views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Newtonsoft.Json;
using ControlzEx.Standard;
using System.Globalization;

namespace CuaHangVangBacDaQuy.viewmodels
{

    public class QuestionViewModel : BaseViewModel
    {


        private const string apiKey = "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";
        private const string apiEndpoint = "https://api.openai.com/v1/completions";

        public class Message
        {
            public string Content { get; set; }
            public string TenNDCommand { get; set; }
        }
        public ObservableCollection<Message> Messages { get; set; }
        private string _userInput;
        public string UserInput
        {
            get { return _userInput; }
            set
            {
                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand SendMessageCommand { get; }
        public ICommand TenNDCommand { get; set; }
        public ICommand DeleteChatCommand { get; set; }

        private OpenDiaLog _IsOpenPayDialog;
        public OpenDiaLog IsOpenPayDialog
        {
            get => _IsOpenPayDialog;
            set { _IsOpenPayDialog = value; OnPropertyChanged(); }
        }
        public QuestionViewModel()
        {
            SendMessageCommand = new RelayCommand<QuestionView>((p) => {
                if (UserInput == "")
                {
                    return false;
                }
                return true; }, (p) => { SendMessage(); });
            Messages = new ObservableCollection<Message>();
            DeleteChatCommand = new RelayCommand<QuestionView>((p) => { return true;}, (p) => { DeleteChat(); });
        }


        private async Task<string> GetGPT3Response(string userInput)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    var request = new
                    {
                        prompt = userInput,
                        max_tokens = 500,
                        model = "gpt-3.5-turbo-instruct",
                    };

                    var jsonRequest = JsonConvert.SerializeObject(request);
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiEndpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                        string gpt3Response = jsonResponse.choices[0].text;

                        return gpt3Response;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return "Error from GPT-3 API: " + errorContent;
                    }
                }
            }
            catch (Exception ex)
            {
                return "Exception occurred: " + ex.Message;
            }
        }


        public async void SendMessage()
        {
            if (string.IsNullOrEmpty(UserInput.Trim()))
            {
                return;
            }
            if (KiemTraChuoi(UserInput)==true)
            {
                try
            {
                IsLoading = true; // Bật trạng thái "đang load"

                var userInput = UserInput.Trim();
                // Xử lý tin nhắn từ người dùng
                var newMessageFromUser = new Message
                {
                    Content = userInput.Trim(),
                    TenNDCommand = NguoiDung.Logged.TenND
                };

                Messages.Add(newMessageFromUser);
                UserInput = ""; // Xóa nội dung trong TextBox sau khi gửi
                OnPropertyChanged(nameof(UserInput));
                var newMessageFrombot = new Message
                {
                    Content = "ChatBot Đang tìm kiếm câu trả lời",
                    TenNDCommand = "ChatBot"
                };
                Messages.Add(newMessageFrombot);
                // Gửi input của người dùng tới OpenAI GPT-3
                var gpt3Response = await GetGPT3Response(userInput);
                if (gpt3Response != null)
                {
                    DeleteLastMessage();
                    // Thực hiện các bước khác khi gpt3Response không phải là null
                    var newMessageFromGPT = new Message
                    {
                        Content = gpt3Response.Trim(),
                        TenNDCommand = "ChatBot"
                    };
                    Messages.Add(newMessageFromGPT);
                    // Xóa message người dùng đã nhập

                }
                else
                {
                    // Xử lý khi gpt3Response là null
                    MessageBox.Show("GPT-3 response is null. Please handle this case accordingly.");
                }
            }
            finally
            {
                IsLoading = false; // Tắt trạng thái "đang load" dù có lỗi xảy ra hoặc không
            }
            }
            else
            {
                var newMessageFrombot = new Message
                {
                    Content = "bạn cần đặt đâu hỏi liên quan tới Đá Quý hoặc các loại đá quý liên quan",
                    TenNDCommand = "ChatBot"
                };
                Messages.Add(newMessageFrombot);
            }
            
        }

        public async void DeleteChat()
        {
            Messages.Clear();
        }
        public void DeleteLastMessage()
        {
            if (Messages.Count > 0)
            {
                Messages.RemoveAt(Messages.Count - 1);
            }
        }
        static bool KiemTraChuoi(string input)
        {
            // Chuyển chuỗi input về chữ thường và không dấu
            string normalizedInput = RemoveDiacritics(input.ToLower());

            // Chuyển mảng wordsToFind về chữ thường và không dấu
            string[] wordsToFind = { "da quy", "kim cuong", "ruby", "sapphire", "emerald", "topaz", "amethyst", "aquamarine", "opal", "turquoise", "citrine", "hong ngoc", "ngoc luc bao", "ngoc luc bao xanh la cay", "thach anh", "thach anh tim", "ngoc luc bao xanh nhat", "ngoc lam", "thach anh vang" };

            // Kiểm tra từng từ trong mảng
            foreach (string word in wordsToFind)
            {
                if (normalizedInput.Contains(word))
                {
                    return true;  // Nếu có ít nhất một từ tồn tại, trả về true và thoát khỏi vòng lặp
                }
            }

            return false;  // Nếu không có từ nào tồn tại, trả về false
        }
        static string RemoveDiacritics(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
    }

}
    

