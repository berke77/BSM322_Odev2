namespace BSM322_Odev2.Views;

public partial class RenkSeciciPage : ContentPage
{
    // Rastgele say� �reteci
    private Random random = new Random();

    public RenkSeciciPage()
    {
        InitializeComponent();
        // Sayfa y�klendi�inde ilk renk olu�tur
        UpdateColor();
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider de�erlerini g�ncelle
        if (sender == sliderRed)
            labelRed.Text = ((int)e.NewValue).ToString();
        else if (sender == sliderGreen)
            labelGreen.Text = ((int)e.NewValue).ToString();
        else if (sender == sliderBlue)
            labelBlue.Text = ((int)e.NewValue).ToString();

        // Renk de�i�ti�inde g�ncelle
        UpdateColor();
    }

    private void UpdateColor()
    {
        try
        {
            // RGB de�erlerini al
            int red = (int)sliderRed.Value;
            int green = (int)sliderGreen.Value;
            int blue = (int)sliderBlue.Value;

            // Renk kodu olu�tur
            string hexCode = $"#{red:X2}{green:X2}{blue:X2}";
            labelRenkKodu.Text = hexCode;

            // Renk nesnesini olu�tur
            Color color = Color.FromRgb(red, green, blue);

            // Frame arka plan�n� g�ncelle
            frameRenkGoster.BackgroundColor = color;

            // Renk kodunun g�r�n�rl���n� ayarla (koyu renklerde beyaz metin)
            double luminance = (0.299 * red + 0.587 * green + 0.114 * blue) / 255;
            labelRenkKodu.TextColor = luminance > 0.5 ? Colors.Black : Colors.White;
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"Renk g�ncellenirken bir hata olu�tu: {ex.Message}", "Tamam");
        }
    }

    private async void BtnKopyala_Clicked(object sender, EventArgs e)
    {
        try
        {
            string renk_kodu = labelRenkKodu.Text;
            await Clipboard.SetTextAsync(renk_kodu);
            await DisplayAlert("Kopyaland�", $"{renk_kodu}", "Tamam");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Kopyalama s�ras�nda bir hata olu�tu: {ex.Message}", "Tamam");
        }
    }

    private void BtnRastgeleRenk_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Rastgele RGB de�erleri olu�tur
            int red = random.Next(0, 256);
            int green = random.Next(0, 256);
            int blue = random.Next(0, 256);

            // Slider de�erlerini g�ncelle
            sliderRed.Value = red;
            sliderGreen.Value = green;
            sliderBlue.Value = blue;

            // Etiketleri g�ncelle
            labelRed.Text = red.ToString();
            labelGreen.Text = green.ToString();
            labelBlue.Text = blue.ToString();

            // Rengi g�ncelle (Slider de�erleri de�i�ti�inde otomatik �a�r�l�r)
            UpdateColor();
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"Rastgele renk olu�turulurken bir hata olu�tu: {ex.Message}", "Tamam");
        }
    }
}