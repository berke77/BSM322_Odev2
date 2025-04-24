namespace BSM322_Odev2.Views;

public partial class VKIHesaplamaPage : ContentPage
{
    public VKIHesaplamaPage()
    {
        InitializeComponent();
        // Sayfa y�klendi�inde ilk hesaplama
        HesaplaVKI();
    }

    private void SliderKilo_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Kilo de�eri tam say� olarak g�sterme
        int kilo = (int)e.NewValue;
        labelKilo.Text = $"{kilo} kg";
        HesaplaVKI(); // De�er de�i�ti�inde VK� hesapla
    }

    private void SliderBoy_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Boy de�eri tam say� olarak g�sterme
        int boy = (int)e.NewValue;
        labelBoy.Text = $"{boy} cm";
        HesaplaVKI(); // De�er de�i�ti�inde VK� hesapla
    }

    private void HesaplaVKI()
    {
        try
        {
            double kilo = sliderKilo.Value;
            double boyCm = sliderBoy.Value;
            double boyMetre = boyCm / 100.0; // cm'den metreye �evirme

            // VK� hesaplama form�l�: kilo / (boy * boy)
            double vki = kilo / (boyMetre * boyMetre);

            // VK� de�erini ve durumu g�sterme
            labelVKI.Text = $"VK�: {vki:F2}";

            // VK� de�erine g�re durum belirleme
            string durum;
            Color renk;

            if (vki < 18.5)
            {
                durum = "Zay�f";
                renk = Color.FromRgb(135, 206, 250); // LightSkyBlue
            }
            else if (vki < 25)
            {
                durum = "Normal";
                renk = Color.FromRgb(144, 238, 144); // LightGreen
            }
            else if (vki < 30)
            {
                durum = "Fazla Kilolu";
                renk = Color.FromRgb(255, 215, 0); // Gold
            }
            else if (vki < 35)
            {
                durum = "Obez (I. S�n�f)";
                renk = Color.FromRgb(255, 165, 0); // Orange
            }
            else if (vki < 40)
            {
                durum = "Obez (II. S�n�f)";
                renk = Color.FromRgb(255, 69, 0); // OrangeRed
            }
            else
            {
                durum = "Morbid Obez";
                renk = Color.FromRgb(255, 0, 0); // Red
            }

            labelVKIDurum.Text = $"Durum: {durum}";
            frameVKIDurum.BackgroundColor = renk;
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"VK� hesaplama s�ras�nda bir hata olu�tu: {ex.Message}", "Tamam");
        }
    }
}