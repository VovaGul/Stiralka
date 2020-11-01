using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Stiralka
{
    public enum State
    {
        Y1,
        Y2,
        Y3,
        Y4,
        Y5,
        Y6,
        Y7,
        Y8
    }

    public enum Rezhim
    {
        Delikatnyiy,
        BistrayaStirka,
        Ezhednevniy
    }

    public class StiralkaModel : BindableBase
    {
        private State _state = State.Y1;
        public State State {
            get => _state;
            private set => SetProperty(ref _state, value);
        }

        public static System.Windows.Media.Imaging.BitmapSource KnopkaVikl = ConvertToBitmapSource(Properties.Resources.knopkaVikl);
        public static System.Windows.Media.Imaging.BitmapSource KnopkaVkl = ConvertToBitmapSource(Properties.Resources.knopkaVkl);

        private static System.Windows.Media.Imaging.BitmapSource LotokZakrit = ConvertToBitmapSource(Properties.Resources.LotokZakrit);
        private static System.Windows.Media.Imaging.BitmapSource LotokOtkrit = ConvertToBitmapSource(Properties.Resources.LotokOtkrit);

        public static System.Windows.Media.Imaging.BitmapSource SlevaOtBarabanaZakrit = ConvertToBitmapSource(Properties.Resources.ZlevaOtBarabanaZakrit);
        public static System.Windows.Media.Imaging.BitmapSource SlevaOtBarabanaOtkrit = ConvertToBitmapSource(Properties.Resources.ZkevaOtBarabanaOtkrit);

        public readonly static System.Windows.Media.Imaging.BitmapSource BarabanOtkrit = ConvertToBitmapSource(Properties.Resources.BarabanOtkrit);
        public readonly static System.Windows.Media.Imaging.BitmapSource BarabanZakrit = ConvertToBitmapSource(Properties.Resources.BarabanZakrit);

        private System.Windows.Media.Imaging.BitmapSource _lotok = LotokZakrit;
        private System.Windows.Media.Imaging.BitmapSource _knopka = KnopkaVikl;
        private int _vremyaStirki = 0;
        private System.Windows.Media.Imaging.BitmapSource _baraban = BarabanZakrit;
        private System.Windows.Media.Imaging.BitmapSource _slevaOtBarabana = SlevaOtBarabanaZakrit;

        public System.Windows.Media.Imaging.BitmapSource Lotok {
            get => _lotok;
            set => SetProperty(ref _lotok, value);
        }
        public System.Windows.Media.Imaging.BitmapSource SlevaOtBarabana
        {
            get => _slevaOtBarabana;
            set => SetProperty(ref _slevaOtBarabana, value);
        }
        public System.Windows.Media.Imaging.BitmapSource Baraban {
            get => _baraban;
            set => SetProperty(ref _baraban, value);
        }
        public System.Windows.Media.Imaging.BitmapSource Knopka {
            get => _knopka;
            set => SetProperty(ref _knopka, value);
        }

        public int VremyaStirki
        {
            get => _vremyaStirki;
            set
            {
                SetProperty(ref _vremyaStirki, value);
            }
        }

        public DelegateCommand OtkritZakritLotok { get; }
        public DelegateCommand VkluchitVikluchit { get; }
        public DelegateCommand ViborVremeni { get; }
        public DelegateCommand Delikatnyi { get; }
        public DelegateCommand BistrayaStirka { get; }
        public DelegateCommand Ezhenedelnyi { get; }
        public DelegateCommand ZakritOtkritBaraban { get; }
        public DelegateCommand Start { get; }
        public DelegateCommand StopCommand { get; }

        public bool Stiraet { get; set; }

        public StiralkaModel()
        {
            OtkritZakritLotok = new DelegateCommand(() =>
            {
                if (Stiraet)
                {
                    return;
                }
                if (Lotok == LotokZakrit)
                {
                    if (State == State.Y1)
                    {
                        State = State.Y6;
                    }
                    if (State == State.Y2)
                    {
                        State = State.Y6;
                    }
                    Lotok = LotokOtkrit;
                    return;
                }
                if (Lotok == LotokOtkrit)
                {
                    if (State == State.Y7)
                    {
                        if (Knopka == KnopkaVikl)
                        {
                            State = State.Y1;
                        }
                        if (Knopka == KnopkaVkl)
                        {
                            State = State.Y2;
                        }
                    }
                    Lotok = LotokZakrit;
                    return;
                }
            });
            VkluchitVikluchit = new DelegateCommand(() =>
            {
                if (Knopka == KnopkaVikl)
                {
                    if (State == State.Y1)
                    {
                        State = State.Y2;
                    }
                    Knopka = KnopkaVkl;
                    return;
                }
                if (Knopka == KnopkaVkl)
                {
                    if (State == State.Y2 ||
                State == State.Y3 ||
                State == State.Y4 ||
                State == State.Y5 ||
                State == State.Y6 ||
                State == State.Y8)
                    {
                        State = State.Y1;
                    }
                    Knopka = KnopkaVikl;
                    VremyaStirki = 0;
                    return;
                }
            });
            ViborVremeni = new DelegateCommand(() =>
            {
                if (Stiraet)
                {
                    return;
                }
                if (State == State.Y2)
                {
                    State = State.Y3;
                    return;
                }
                if (State == State.Y3)
                {
                    State = State.Y2;
                    return;
                }
            });
            Delikatnyi = new DelegateCommand(() =>
            {
                if (State == State.Y2)
                {
                    State = State.Y4;
                    return;
                }
                if (State == State.Y4)
                {
                    State = State.Y2;
                    return;
                }
            });
            BistrayaStirka = new DelegateCommand(() =>
            {
                if (State == State.Y2)
                {
                    State = State.Y4;
                    return;
                }
                if (State == State.Y4)
                {
                    State = State.Y2;
                    return;
                }
            });
            Ezhenedelnyi = new DelegateCommand(() =>
            {
                if (State == State.Y2)
                {
                    State = State.Y4;
                    return;
                }
                if (State == State.Y4)
                {
                    State = State.Y2;
                    return;
                }
            });
            ZakritOtkritBaraban = new DelegateCommand(() =>
            {
                if (Stiraet)
                {
                    return;
                }
                if (Baraban == BarabanZakrit)
                {
                    if (State == State.Y1 ||
                     State == State.Y2)
                    {
                        State = State.Y5;
                    }
                    if (State == State.Y8)
                    {
                        State = State.Y1;
                    }
                    SlevaOtBarabana = SlevaOtBarabanaOtkrit;
                    Baraban = BarabanOtkrit;
                    return;
                }
                if (Baraban == BarabanOtkrit)
                {
                    if (State == State.Y5)
                    {
                        if (Knopka == KnopkaVikl)
                        {
                            State = State.Y1;
                        }
                        if (Knopka == KnopkaVkl)
                        {
                            State = State.Y2;
                        }
                    }
                    SlevaOtBarabana = SlevaOtBarabanaZakrit;
                    Baraban = BarabanZakrit;
                    return;
                }
            });
            Start = new DelegateCommand(async () =>
            {
                if (Knopka == KnopkaVikl ||
                 Baraban == BarabanOtkrit ||
                 Lotok == LotokOtkrit)
                {
                    return;
                }
                Stiraet = true;
                await Task.Run(() => TimerDown());
                Stiraet = false;
            });
            StopCommand = new DelegateCommand(() =>
            {
                Stop();
            });
        }

        public void TimerDown()
        {
            while (VremyaStirki > 0)
            {
                if (State == State.Y2)
                {
                    State = State.Y7;
                }
                VremyaStirki--;
                Thread.Sleep(600);
            }
            Stop();
        }

        public async void Stop()
        {
            if (Knopka == KnopkaVikl)
            {
                return;
            }
            VremyaStirki = 0;
            await Task.Run(() => 
            {
                SystemSounds.Exclamation.Play();
                Thread.Sleep(900);
                SystemSounds.Exclamation.Play();
                Thread.Sleep(900);
                SystemSounds.Exclamation.Play();
            });
            if (State == State.Y2)
            {
                State = State.Y8;
                return;
            }
            if (State == State.Y7)
            {
                State = State.Y8;
                return;
            }
        }

        public static System.Windows.Media.Imaging.BitmapSource ConvertToBitmapSource(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;
            //BitmapImage b=new BitmapImage();
            System.Windows.Media.Imaging.BitmapSource bitSrc = null;
            var hBitmap = IntPtr.Zero;


            try
            {
                hBitmap = bitmap.GetHbitmap();
                bitSrc = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            catch (System.ComponentModel.Win32Exception)
            {
                bitSrc = null;
            }
            catch (ArgumentException)
            {
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
                hBitmap = IntPtr.Zero;
            }
            catch
            {
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
                bitSrc = null;
                hBitmap = IntPtr.Zero;
            }
            finally
            {
                //bitmap.Dispose();
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
            }
            return bitSrc;
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);
    }
}
