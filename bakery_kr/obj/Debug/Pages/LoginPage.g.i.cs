﻿#pragma checksum "..\..\..\Pages\LoginPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D1989BEE4F78AD24189765D7858C5100CD2C7A44A4E07BA94FF1B9BE0FE961FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using bakery_kr.Pages;


namespace bakery_kr.Pages {
    
    
    /// <summary>
    /// LoginPage
    /// </summary>
    public partial class LoginPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBoxLPage;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBoxLogin;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VisibTBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ImgShowHide;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnLogin;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnReg;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Pages\LoginPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame LogPage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/bakery_kr;component/pages/loginpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\LoginPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TBoxLPage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.TBoxLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.VisibTBoxPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PBoxPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 26 "..\..\..\Pages\LoginPage.xaml"
            this.PBoxPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.PBoxPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ImgShowHide = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\..\Pages\LoginPage.xaml"
            this.ImgShowHide.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ImgShowHide_MouseLeave);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\Pages\LoginPage.xaml"
            this.ImgShowHide.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ImgShowHide_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\Pages\LoginPage.xaml"
            this.ImgShowHide.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ImgShowHide_PreviewMouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\Pages\LoginPage.xaml"
            this.BtnLogin.Click += new System.Windows.RoutedEventHandler(this.BtnLogin_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnReg = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Pages\LoginPage.xaml"
            this.BtnReg.Click += new System.Windows.RoutedEventHandler(this.BtnReg_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.LogPage = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

