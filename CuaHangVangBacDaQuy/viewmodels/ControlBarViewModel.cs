using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CuaHangVangBacDaQuy.viewmodels
{

    public class ControlBarViewModel : BaseViewModel
    {
        #region command
        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        #endregion

        public ControlBarViewModel()
        {
            CloseWindowCommand = new RelayCommand<UserControl>((p) =>
            { return p == null ? false : true; },
            (p) =>
            {
                var pParent = GetWindowParent(p);
                if (pParent != null)
                {
                    pParent.Close();
                }
            }
            );
            MaximizeWindowCommand = new RelayCommand<UserControl>((p) =>
            {
                return p == null ? false : true;
            },
            (p) =>
            {
                var pParent = GetWindowParent(p);
                if (pParent != null)
                {
                    if (pParent.WindowState == WindowState.Maximized)
                    {
                        pParent.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        pParent.WindowState = WindowState.Maximized;
                    }

                }
            }
            );
            MinimizeWindowCommand = new RelayCommand<UserControl>((p) =>
            {
                return p == null ? false : true;
            },(p) =>
            {
                var pParent = GetWindowParent(p);
                if (pParent != null)
                {
                    pParent.WindowState = WindowState.Minimized;
                }
            }
            );
            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) =>
            {
                return p == null ? false : true;
            }, (p) =>
            {
                var pParent = GetWindowParent(p);
                if (pParent != null)
                {
                    pParent.DragMove();
                }
            }
            );
        }

        Window GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent as Window;
        }

    }
}
