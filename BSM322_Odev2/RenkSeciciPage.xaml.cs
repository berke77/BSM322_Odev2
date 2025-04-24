namespace BSM322_Odev2.Views;

public partial class RenkSeciciPage : ContentPage
{
    // Rastgele sayý üreteci
    private Random random = new Random();

    public RenkSeciciPage()
    {
        InitializeComponent();
        // Sayfa yüklendiðinde ilk renk oluþtur
        UpdateColor();
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider deðerlerini güncelle
        if (sender == sliderRed)
            labelRed.Text = ((int)e.NewValue).ToString();
        else if (sender == sliderGreen)
            labelGreen.Text = ((int)e.NewValue).ToString();
        else if (sender == sliderBlue)
            labelBlue.Text = ((int)e.NewValue).ToString();

        // Renk deðiþtiðinde güncelle
        UpdateColor();
    }

    private void UpdateColor()
    {
        try
        {
            // RGB deðerlerini al
            int red = (int)sliderRed.Value;
            int green = (int)sliderGreen.Value;
            int blue = (int)sliderBlue.Value;

            // Renk kodu oluþtur
            string hexCode = $"#{red:X2}{green:X2}{blue:X2}";
            labelRenkKodu.Text = hexCode;

            // Renk nesnesini oluþtur
            Color color = Color.FromRgb(red, green, blue);

            // Frame arka planýný güncelle
            frameRenkGoster.BackgroundColor = color;

            // Renk kodunun görünürlüðünü ayarla (koyu renklerde beyaz metin)
            double luminance = (0.299 * red + 0.587 * green + 0.114 * blue) / 255;
            labelRenkKodu.TextColor = luminance > 0.5 ? Colors.Black : Colors.White;
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"Renk güncellenirken bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }

    private async void BtnKopyala_Clicked(object sender, EventArgs e)
    {
        try
        {
            string renk_kodu = labelRenkKodu.Text;
            await Clipboard.SetTextAsync(renk_kodu);
            await DisplayAlert("Kopyalandý", $"{renk_kodu}", "Tamam");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Kopyalama sýrasýnda bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }

    private void BtnRastgeleRenk_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Rastgele RGB deðerleri oluþtur
            int red = random.Next(0, 256);
            int green = random.Next(0, 256);
            int blue = random.Next(0, 256);

            // Slider deðerlerini güncelle
            sliderRed.Value = red;
            sliderGreen.Value = green;
            sliderBlue.Value = blue;

            // Etiketleri güncelle
            labelRed.Text = red.ToString();
            labelGreen.Text = green.ToString();
            labelBlue.Text = blue.ToString();

            // Rengi güncelle (Slider deðerleri deðiþtiðinde otomatik çaðrýlýr)
            UpdateColor();
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"Rastgele renk oluþturulurken bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }
}