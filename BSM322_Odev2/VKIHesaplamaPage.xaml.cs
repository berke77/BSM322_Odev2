namespace BSM322_Odev2.Views;

public partial class VKIHesaplamaPage : ContentPage
{
    public VKIHesaplamaPage()
    {
        InitializeComponent();
        // Sayfa yüklendiðinde ilk hesaplama
        HesaplaVKI();
    }

    private void SliderKilo_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Kilo deðeri tam sayý olarak gösterme
        int kilo = (int)e.NewValue;
        labelKilo.Text = $"{kilo} kg";
        HesaplaVKI(); // Deðer deðiþtiðinde VKÝ hesapla
    }

    private void SliderBoy_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Boy deðeri tam sayý olarak gösterme
        int boy = (int)e.NewValue;
        labelBoy.Text = $"{boy} cm";
        HesaplaVKI(); // Deðer deðiþtiðinde VKÝ hesapla
    }

    private void HesaplaVKI()
    {
        try
        {
            double kilo = sliderKilo.Value;
            double boyCm = sliderBoy.Value;
            double boyMetre = boyCm / 100.0; // cm'den metreye çevirme

            // VKÝ hesaplama formülü: kilo / (boy * boy)
            double vki = kilo / (boyMetre * boyMetre);

            // VKÝ deðerini ve durumu gösterme
            labelVKI.Text = $"VKÝ: {vki:F2}";

            // VKÝ deðerine göre durum belirleme
            string durum;
            Color renk;

            if (vki < 18.5)
            {
                durum = "Zayýf";
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
                durum = "Obez (I. Sýnýf)";
                renk = Color.FromRgb(255, 165, 0); // Orange
            }
            else if (vki < 40)
            {
                durum = "Obez (II. Sýnýf)";
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
            DisplayAlert("Hata", $"VKÝ hesaplama sýrasýnda bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }
}