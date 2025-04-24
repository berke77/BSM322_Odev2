namespace BSM322_Odev2.Views;

public partial class KrediHesaplamaPage : ContentPage
{
    public KrediHesaplamaPage()
    {
        InitializeComponent();
        // Varsayýlan seçim
        pickerKrediTuru.SelectedIndex = 0;
    }

    private void SliderVade_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider deðeri tam sayý olarak gösterme
        int vade = (int)e.NewValue;
        labelVade.Text = $"{vade} Ay";
    }

    private void BtnHesapla_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Giriþ doðrulama
            if (pickerKrediTuru.SelectedIndex == -1)
            {
                DisplayAlert("Uyarý", "Lütfen kredi türü seçiniz.", "Tamam");
                return;
            }

            if (string.IsNullOrWhiteSpace(entryKrediTutari.Text) ||
                string.IsNullOrWhiteSpace(entryFaizOrani.Text))
            {
                DisplayAlert("Uyarý", "Lütfen tüm alanlarý doldurunuz.", "Tamam");
                return;
            }

            // Deðerleri alma ve dönüþtürme
            if (!double.TryParse(entryKrediTutari.Text, out double krediTutari) || krediTutari <= 0)
            {
                DisplayAlert("Hata", "Geçerli bir kredi tutarý giriniz.", "Tamam");
                return;
            }

            if (!double.TryParse(entryFaizOrani.Text, out double faizOrani) || faizOrani <= 0)
            {
                DisplayAlert("Hata", "Geçerli bir faiz oraný giriniz.", "Tamam");
                return;
            }

            int vade = (int)sliderVade.Value;

            // Aylýk faiz oraný hesaplama (yýllýk / 12 / 100)
            double aylikFaizOrani = faizOrani / 12 / 100;

            // Aylýk taksit hesaplama formülü: A = P * r * (1 + r)^n / ((1 + r)^n - 1)
            // A: Aylýk ödeme, P: Kredi tutarý, r: Aylýk faiz oraný, n: Toplam taksit sayýsý
            double aylikTaksit = krediTutari * aylikFaizOrani * Math.Pow(1 + aylikFaizOrani, vade) /
                               (Math.Pow(1 + aylikFaizOrani, vade) - 1);

            double toplamOdeme = aylikTaksit * vade;
            double toplamFaiz = toplamOdeme - krediTutari;

            // Sonuçlarý gösterme
            labelAylikTaksit.Text = $"Aylýk Taksit: {aylikTaksit:N2} TL";
            labelToplamOdeme.Text = $"Toplam Ödeme: {toplamOdeme:N2} TL";
            labelToplamFaiz.Text = $"Toplam Faiz: {toplamFaiz:N2} TL";
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"Hesaplama sýrasýnda bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }
}