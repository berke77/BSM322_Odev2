namespace BSM322_Odev2.Views;

public partial class KrediHesaplamaPage : ContentPage
{
    public KrediHesaplamaPage()
    {
        InitializeComponent();
        // Varsay�lan se�im
        pickerKrediTuru.SelectedIndex = 0;
    }

    private void SliderVade_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        // Slider de�eri tam say� olarak g�sterme
        int vade = (int)e.NewValue;
        labelVade.Text = $"{vade} Ay";
    }

    private void BtnHesapla_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Giri� do�rulama
            if (pickerKrediTuru.SelectedIndex == -1)
            {
                DisplayAlert("Uyar�", "L�tfen kredi t�r� se�iniz.", "Tamam");
                return;
            }

            if (string.IsNullOrWhiteSpace(entryKrediTutari.Text) ||
                string.IsNullOrWhiteSpace(entryFaizOrani.Text))
            {
                DisplayAlert("Uyar�", "L�tfen t�m alanlar� doldurunuz.", "Tamam");
                return;
            }

            // De�erleri alma ve d�n��t�rme
            if (!double.TryParse(entryKrediTutari.Text, out double krediTutari) || krediTutari <= 0)
            {
                DisplayAlert("Hata", "Ge�erli bir kredi tutar� giriniz.", "Tamam");
                return;
            }

            if (!double.TryParse(entryFaizOrani.Text, out double faizOrani) || faizOrani <= 0)
            {
                DisplayAlert("Hata", "Ge�erli bir faiz oran� giriniz.", "Tamam");
                return;
            }

            int vade = (int)sliderVade.Value;

            // Ayl�k faiz oran� hesaplama (y�ll�k / 12 / 100)
            double aylikFaizOrani = faizOrani / 12 / 100;

            // Ayl�k taksit hesaplama form�l�: A = P * r * (1 + r)^n / ((1 + r)^n - 1)
            // A: Ayl�k �deme, P: Kredi tutar�, r: Ayl�k faiz oran�, n: Toplam taksit say�s�
            double aylikTaksit = krediTutari * aylikFaizOrani * Math.Pow(1 + aylikFaizOrani, vade) /
                               (Math.Pow(1 + aylikFaizOrani, vade) - 1);

            double toplamOdeme = aylikTaksit * vade;
            double toplamFaiz = toplamOdeme - krediTutari;

            // Sonu�lar� g�sterme
            labelAylikTaksit.Text = $"Ayl�k Taksit: {aylikTaksit:N2} TL";
            labelToplamOdeme.Text = $"Toplam �deme: {toplamOdeme:N2} TL";
            labelToplamFaiz.Text = $"Toplam Faiz: {toplamFaiz:N2} TL";
        }
        catch (Exception ex)
        {
            DisplayAlert("Hata", $"Hesaplama s�ras�nda bir hata olu�tu: {ex.Message}", "Tamam");
        }
    }
}